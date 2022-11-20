using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using CW.Common;

namespace Lean.Texture
{
	/// <summary>This allows you to modify the brightness of all pixels in the current texture.</summary>
	[System.Serializable]
	public class LeanFilterBrightness : LeanFilter
	{
		public override string Title
		{
			get
			{
				return "Modify Brightness";
			}
		}

		/// <summary>The brightness of all pixels will be multiplied by this value.</summary>
		public float Multiplier { set { multiplier = value; } get { return multiplier; } } [SerializeField] private float multiplier = 1.0f;

		[BurstCompile]
		struct FilterJob : IJobParallelFor
		{
			public NativeArray<float4> INOUT;

			[ReadOnly] public float Multiplier;

			public void Execute(int index)
			{
				var pixel = INOUT[index];
				var hsv   = default(float3);

				Color.RGBToHSV(new Color(pixel.x, pixel.y, pixel.z), out hsv.x, out hsv.y, out hsv.z);

				hsv.z *= Multiplier;

				var color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);

				pixel.x = color.r;
				pixel.y = color.g;
				pixel.z = color.b;

				INOUT[index] = pixel;
			}
		}

		public override void Schedule(LeanPendingTexture data)
		{
			var filter = new FilterJob();

			filter.INOUT      = data.Pixels;
			filter.Multiplier = multiplier;

			data.Handle = filter.Schedule(data.Pixels.Length, 32, data.Handle);
		}

#if UNITY_EDITOR
		protected override void DrawInspector()
		{
			CwEditor.Draw("multiplier", "The brightness of all pixels will be multiplied by this value.");
		}
#endif
	}
}