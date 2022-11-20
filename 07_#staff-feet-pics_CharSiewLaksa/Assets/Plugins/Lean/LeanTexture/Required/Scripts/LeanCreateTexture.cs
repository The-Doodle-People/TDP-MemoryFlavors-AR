using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using CW.Common;

namespace Lean.Texture
{
	/// <summary>This lets you load a texture so you can then modify it with various filters.</summary>
	[System.Serializable]
	public class LeanCreateTexture : LeanCreate
	{
		public override string Title
		{
			get
			{
				return "Texture";
			}
		}

		/// <summary>The texture that will be used to create the texture.</summary>
		public UnityEngine.Texture Texture { set { texture = value; } get { return texture; } } [SerializeField] private UnityEngine.Texture texture;

		/// <summary>The size of the created texture.
		/// -1 = Use existing texture size.</summary>
		public int2 OverrideSize { set { overrideSize = value; } get { return overrideSize; } } [SerializeField] private int2 overrideSize = new int2(-1, -1);

		[BurstCompile]
		struct CreateJob : IJobParallelFor
		{
			[WriteOnly] public NativeArray<float4> DstData;
			[ ReadOnly] public int2                DstSize;

			[ReadOnly] public NativeArray<float4> SrcData;
			[ReadOnly] public int2                SrcSize;

			public void Execute(int index)
			{
				var x  = index % DstSize.x;
				var y  = index / DstSize.x;
				var uv = new float2(x, y) / (DstSize - 1);

				DstData[index] = LeanSample.Tex2D_Linear(SrcData, SrcSize, uv);
			}
		}

		public override LeanPendingTexture TrySchedule(TextureWrapMode wrapU, TextureWrapMode wrapV, bool linear)
		{
			if (texture != null)
			{
				var srcSize = default(int2);
				var srcData = LeanSample.GetPixels(texture, Allocator.TempJob, linear, ref srcSize);
				var dstSize = srcSize;

				if (overrideSize.x > 0) dstSize.x = overrideSize.x;
				if (overrideSize.y > 0) dstSize.y = overrideSize.y;

				var data   = LeanPendingTexture.Create(dstSize, wrapU, wrapV, linear);
				var create = new CreateJob();

				create.DstData = data.Pixels;
				create.DstSize = data.Size;
				create.SrcData = srcData; data.Register(create.SrcData);
				create.SrcSize = srcSize;

				data.Handle = create.Schedule(data.Pixels.Length, 32);

				return data;
			}

			return null;
		}

#if UNITY_EDITOR
		protected override void DrawInspector()
		{
			CwEditor.BeginError(ObjectExists("texture") == false);
				CwEditor.Draw("texture", "The texture that will be used to create the texture.");
			CwEditor.EndError();
			CwEditor.Draw("overrideSize", "The size of the created texture.\n\n-1 = Use existing texture size.");
		}
#endif
	}
}