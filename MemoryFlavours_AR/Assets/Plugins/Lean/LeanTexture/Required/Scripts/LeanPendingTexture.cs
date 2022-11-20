using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;

namespace Lean.Texture
{
	/// <summary>This class stores information about a texture that is currently being generated.
	/// NOTE: After getting an instance of this class from the <b>LeanTexture.TrySchedule</b> method, you must manually call its <b>Complete</b> method within 3 frames.</summary>
	public class LeanPendingTexture
	{
		public NativeArray<float4> Pixels;

		public TextureWrapMode WrapU;

		public TextureWrapMode WrapV;

		public int2 Size;

		public bool Linear;

		public JobHandle Handle;

		public Texture2D OutputTexture;

		public List<System.IDisposable> tempDatas = new List<System.IDisposable>();

		private NativeArray<float4> swapBuffer;

		[BurstCompile]
		struct DownsampleJob : IJobParallelFor
		{
			[WriteOnly] public NativeArray<float4> Dst;
			[ ReadOnly] public int2                DstSize;
			[ ReadOnly] public NativeArray<float4> Src;
			[ ReadOnly] public int2                SrcSize;

			public void Execute(int index)
			{
				var x  = index % DstSize.x;
				var y  = index / DstSize.x;
				var uv = new float2(x, y) / (DstSize - 1);

				Dst[index] = LeanSample.Tex2D_Point(Src, SrcSize, uv);
			}
		}

		[BurstCompile]
		struct ConvertToGammaJob : IJobParallelFor
		{
			public NativeArray<float4> Pixels;

			public void Execute(int index)
			{
				var color = (Color)(Vector4)Pixels[index];

				Pixels[index] = (Vector4)color.gamma;
			}
		}

		public void DoubleBuffer(ref NativeArray<float4> inData, ref NativeArray<float4> outData)
		{
			var size = default(int2); DoubleBuffer(ref inData, ref outData, ref size);
		}

		public void DoubleBuffer(ref NativeArray<float4> inData, ref NativeArray<float4> outData, ref int2 inoutSize)
		{
			if (swapBuffer.IsCreated == false)
			{
				swapBuffer = new NativeArray<float4>(Pixels.Length, Allocator.TempJob); Register(swapBuffer);
			}

			inoutSize = Size;

			inData = Pixels;
			outData = swapBuffer;

			Pixels     = outData;
			swapBuffer = inData;
		}

		public void DoubleBufferResize(ref NativeArray<float4> inData, ref NativeArray<float4> outData, ref int2 inSize, ref int2 outSize, int2 newSize)
		{
			if (newSize.x <= 0) newSize.x = Size.x;
			if (newSize.y <= 0) newSize.y = Size.y;

			inSize  = Size;
			outSize = newSize;

			if (Size.x == outSize.x && Size.y == outSize.y)
			{
				DoubleBuffer(ref inData, ref outData);
			}
			else
			{
				swapBuffer = new NativeArray<float4>(outSize.x * outSize.y, Allocator.TempJob); Register(swapBuffer);

				inData  = Pixels;
				outData = swapBuffer;

				Pixels     = outData;
				swapBuffer = inData;
			}

			Size = newSize;
		}

		public void Downsample(NativeArray<float4> srcData, int2 srcSize, ref NativeArray<float4> dstData, ref int2 dstSize)
		{
			var downsample = new DownsampleJob();

			dstSize = srcSize / 2;
			dstData = new NativeArray<float4>(dstSize.x * dstSize.y, Allocator.TempJob); Register(dstData);
			
			downsample.Src     = srcData;
			downsample.SrcSize = srcSize;
			downsample.Dst     = dstData;
			downsample.DstSize = dstSize;

			Handle = downsample.Schedule(dstData.Length, 32, Handle);
		}

		public void ConvertToGamma()
		{
			var convert = new ConvertToGammaJob();
			
			convert.Pixels = Pixels;

			Handle = convert.Schedule(Pixels.Length, 32, Handle);
		}

		public void Register(System.IDisposable tempData)
		{
			tempDatas.Add(tempData);
		}

		public static LeanPendingTexture Create(int2 size, TextureWrapMode wrapU, TextureWrapMode wrapV, bool linear)
		{
			var data = pool.Count > 0 ? pool.Pop() : new LeanPendingTexture();

			data.Pixels = new NativeArray<float4>(size.x * size.y, Allocator.TempJob); data.Register(data.Pixels);
			data.Size   = size;
			data.WrapU  = wrapU;
			data.WrapV  = wrapV;
			data.Linear = linear;

			return data;
		}

		/// <summary>This method will convert this pending texture into the final <b>Texture2D</b>, and then clean up & pool itself.
		/// NOTE: After scheduling a texture to be generated, you must manually call this method within 3 frames.</summary>
		public Texture2D Complete()
		{
			Handle.Complete();

			OutputTexture.Apply();

			foreach (var tempData in tempDatas)
			{
				tempData.Dispose();
			}

			tempDatas.Clear();

			Pixels     = default(NativeArray<float4>);
			swapBuffer = default(NativeArray<float4>);

			pool.Push(this);

			return OutputTexture;
		}

		private static Stack<LeanPendingTexture> pool = new Stack<LeanPendingTexture>();
	}
}