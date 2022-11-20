using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using CW.Common;

namespace Lean.Texture
{
	/// <summary>This lets you create a texture from up to 4 other textures. A single value is extracted from each of the textures using the channel(s) you specify, and this is stored in each of the final texture's channels.</summary>
	[System.Serializable]
	public class LeanCreateTextureCombined : LeanCreate
	{
		public override string Title
		{
			get
			{
				return "Channel Packed Texture";
			}
		}

		public enum ChannelType
		{
			Red,
			Green,
			Blue,
			Alpha,
			GrayscaleRGB
		}

		/// <summary>The texture that will be stored in the <b>Red</b> channel of the created texture.</summary>
		public UnityEngine.Texture RedTexture { set { redTexture = value; } get { return redTexture; } } [SerializeField] private UnityEngine.Texture redTexture;

		/// <summary>The channel from the <b>RedTexture</b> that will be stored in the created texture's <b>Red</b> channel.</summary>
		public ChannelType RedChannel { set { redChannel = (int)value; } get { return (ChannelType)redChannel; } } [SerializeField] [LeanEnum(typeof(ChannelType))] private int redChannel = (int)ChannelType.Red;

		/// <summary>The texture that will be stored in the <b>Green</b> channel of the created texture.</summary>
		public UnityEngine.Texture GreenTexture { set { greenTexture = value; } get { return greenTexture; } } [SerializeField] private UnityEngine.Texture greenTexture;

		/// <summary>The channel from the <b>GreenTexture</b> that will be stored in the created texture's <b>Green</b> channel.</summary>
		public ChannelType GreenChannel { set { greenChannel = (int)value; } get { return (ChannelType)greenChannel; } } [SerializeField] [LeanEnum(typeof(ChannelType))] private int greenChannel = (int)ChannelType.Green;

		/// <summary>The texture that will be stored in the <b>Blue</b> channel of the created texture.</summary>
		public UnityEngine.Texture BlueTexture { set { blueTexture = value; } get { return blueTexture; } } [SerializeField] private UnityEngine.Texture blueTexture;

		/// <summary>The channel from the <b>BlueTexture</b> that will be stored in the created texture's <b>Blue</b> channel.</summary>
		public ChannelType BlueChannel { set { blueChannel = (int)value; } get { return (ChannelType)blueChannel; } } [SerializeField] [LeanEnum(typeof(ChannelType))] private int blueChannel = (int)ChannelType.Blue;

		/// <summary>The texture that will be stored in the <b>Alpha</b> channel of the created texture.</summary>
		public UnityEngine.Texture AlphaTexture { set { alphaTexture = value; } get { return alphaTexture; } } [SerializeField] private UnityEngine.Texture alphaTexture;

		/// <summary>The channel from the <b>alphaTexture</b> that will be stored in the created texture's <b>alpha</b> channel.</summary>
		public ChannelType AlphaChannel { set { alphaChannel = (int)value; } get { return (ChannelType)alphaChannel; } } [SerializeField] [LeanEnum(typeof(ChannelType))] private int alphaChannel = (int)ChannelType.Alpha;
		
		/// <summary>By default the created texture will be given the largest width and largest height of the input textures, but this setting allows you to override this.
		/// -1 = Do not override size.</summary>
		public int2 OverrideSize { set { overrideSize = value; } get { return overrideSize; } } [SerializeField] private int2 overrideSize = new int2(-1, -1);

		[BurstCompile]
		struct CreateJob : IJobParallelFor
		{
			[WriteOnly] public NativeArray<float4> DstData;
			[ ReadOnly] public int2                DstSize;

			[ReadOnly] public NativeArray<float4> DataR;
			[ReadOnly] public NativeArray<float4> DataG;
			[ReadOnly] public NativeArray<float4> DataB;
			[ReadOnly] public NativeArray<float4> DataA;

			[ReadOnly] public int2 SizeR;
			[ReadOnly] public int2 SizeG;
			[ReadOnly] public int2 SizeB;
			[ReadOnly] public int2 SizeA;

			[ReadOnly] public float4 WeightR;
			[ReadOnly] public float4 WeightG;
			[ReadOnly] public float4 WeightB;
			[ReadOnly] public float4 WeightA;

			public void Execute(int index)
			{
				var x     = index % DstSize.x;
				var y     = index / DstSize.x;
				var uv    = new float2(x, y) / (DstSize - 1);
				var pixel = default(float4);

				if (DataR.Length > 0)
				{
					pixel.x = math.dot(LeanSample.Tex2D_Linear_WrapXY(DataR, SizeR, uv), WeightR);
				}

				if (DataG.Length > 0)
				{
					pixel.y = math.dot(LeanSample.Tex2D_Linear_WrapXY(DataG, SizeG, uv), WeightG);
				}

				if (DataB.Length > 0)
				{
					pixel.z = math.dot(LeanSample.Tex2D_Linear_WrapXY(DataB, SizeB, uv), WeightB);
				}

				if (DataA.Length > 0)
				{
					pixel.w = math.dot(LeanSample.Tex2D_Linear_WrapXY(DataA, SizeA, uv), WeightA);
				}

				DstData[index] = pixel;
			}
		}

		public override LeanPendingTexture TrySchedule(TextureWrapMode wrapU, TextureWrapMode wrapV, bool linear)
		{
			if (redTexture != null || greenTexture != null || blueTexture != null || alphaTexture != null)
			{
				var create = new CreateJob();
				var data   = LeanPendingTexture.Create(CalculateSize(), wrapU, wrapV, linear);

				Write(data,   redTexture,   RedChannel, ref create.DataR, ref create.SizeR, ref create.WeightR);
				Write(data, greenTexture, GreenChannel, ref create.DataG, ref create.SizeG, ref create.WeightG);
				Write(data,  blueTexture,  BlueChannel, ref create.DataB, ref create.SizeB, ref create.WeightB);
				Write(data, alphaTexture, AlphaChannel, ref create.DataA, ref create.SizeA, ref create.WeightA);

				create.DstData  = data.Pixels;
				create.DstSize = data.Size;

				data.Handle = create.Schedule(data.Pixels.Length, 32);

				return data;
			}

			return null;
		}

		private int2 CalculateSize()
		{
			var size  = default(int2);

			if (  redTexture != null) size = math.max(size, new int2(  redTexture.width,   redTexture.height));
			if (greenTexture != null) size = math.max(size, new int2(greenTexture.width, greenTexture.height));
			if ( blueTexture != null) size = math.max(size, new int2( blueTexture.width,  blueTexture.height));
			if (alphaTexture != null) size = math.max(size, new int2(alphaTexture.width, alphaTexture.height));

			if (overrideSize.x > 0) size.x = overrideSize.x;
			if (overrideSize.y > 0) size.y = overrideSize.y;

			return size;
		}

		private void Write(LeanPendingTexture data, UnityEngine.Texture texture, ChannelType channel, ref NativeArray<float4> source, ref int2 size, ref float4 weight)
		{
			if (texture != null)
			{
				source = LeanSample.GetPixels(texture, Allocator.TempJob, data.Linear);
				size   = new int2(texture.width, texture.height);

				switch (channel)
				{
					case ChannelType.Red:   weight.x = 1.0f; break;
					case ChannelType.Green: weight.y = 1.0f; break;
					case ChannelType.Blue:  weight.z = 1.0f; break;
					case ChannelType.Alpha: weight.w = 1.0f; break;
					case ChannelType.GrayscaleRGB: weight.x = 0.2627f; weight.y = 0.6780f; weight.z = 0.0593f; break;
				}
			}
			else
			{
				source = new NativeArray<float4>(0, Allocator.TempJob);
			}

			data.Register(source);
		}

#if UNITY_EDITOR
		protected override void DrawInspector()
		{
			CwEditor.Draw("overrideSize", "By default the created texture will be given the largest width and largest height of the input textures, but this setting allows you to override this.\n\n-1 = Do not override size.");

			CwEditor.Separator();

			CwEditor.BeginError(ObjectExists("redTexture") == false && ObjectExists("redTexture") == false && ObjectExists("redTexture") == false && ObjectExists("redTexture") == false);
				DrawChannel("Red =", "redTexture", "redChannel");
				DrawChannel("Green =", "greenTexture", "greenChannel");
				DrawChannel("Blue =", "blueTexture", "blueChannel");
				DrawChannel("Alpha =", "alphaTexture", "alphaChannel");
			CwEditor.EndError();
		}

		private void DrawChannel(string title, string pathA, string pathB)
		{
			UnityEditor.EditorGUILayout.BeginHorizontal();
				UnityEditor.EditorGUILayout.PropertyField(CwEditor.GetProperty(pathA), new GUIContent(title));
				UnityEditor.EditorGUILayout.PropertyField(CwEditor.GetProperty(pathB), GUIContent.none, GUILayout.Width(80));
			UnityEditor.EditorGUILayout.EndHorizontal();
		}
#endif
	}
}