using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using CW.Common;

namespace Lean.Texture
{
	/// <summary>This will convert the texture's RGB values into a single grayscale value, and write it to the specified channels.</summary>
	[System.Serializable]
	public class LeanFilterGrayscale : LeanFilter
	{
		public override string Title
		{
			get
			{
				return "Convert To Grayscale";
			}
		}

		public enum OutputType
		{
			RGB,
			Red,
			Green,
			Blue,
			Alpha
		}

		/// <summary>The strength of the grayscale effect.
		/// 0 = No change.
		/// 1 = Fully grayscale.</summary>
		public float Strength { set { strength = value; } get { return strength; } } [SerializeField] [Range(0.0f, 1.0f)] private float strength = 1.0f;

		/// <summary>This allows you to specify the channels the grayscale value will be written to.</summary>
		public OutputType OutputTo { set { outputTo = (int)value; } get { return (OutputType)outputTo; } } [SerializeField] [LeanEnum(typeof(OutputType))] private int outputTo;

		[BurstCompile]
		struct FilterJob : IJobParallelFor
		{
			public NativeArray<float4> INOUT;

			[ReadOnly] public float  Strength;
			[ReadOnly] public float4 Weights;

			public void Execute(int index)
			{
				var pixel = INOUT[index];
				var gray  = pixel.x * 0.2627f + pixel.y * 0.6780f + pixel.z * 0.0593f;

				pixel = math.lerp(pixel, gray, Weights * Strength);

				INOUT[index] = pixel;
			}
		}

		public override void Schedule(LeanPendingTexture data)
		{
			var filter = new FilterJob();

			filter.INOUT    = data.Pixels;
			filter.Strength = strength;

			switch (OutputTo)
			{
				case OutputType.RGB:   filter.Weights.xyz = 1.0f; break;
				case OutputType.Red:   filter.Weights.x   = 1.0f; break;
				case OutputType.Green: filter.Weights.y   = 1.0f; break;
				case OutputType.Blue:  filter.Weights.z   = 1.0f; break;
				case OutputType.Alpha: filter.Weights.w   = 1.0f; break;
			}

			data.Handle = filter.Schedule(data.Pixels.Length, 32, data.Handle);
		}

#if UNITY_EDITOR
		protected override void DrawInspector()
		{
			CwEditor.Draw("strength", "The strength of the grayscale effect.\n\n0 = No change.\n\n1 = Fully grayscale.");
			CwEditor.Draw("outputTo", "This allows you to specify the channels the grayscale value will be written to.");
		}
#endif
	}
}