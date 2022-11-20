using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using CW.Common;

namespace Lean.Texture
{
	/// <summary>This allows you to modify the hue of all pixels in the current texture.</summary>
	[System.Serializable]
	public class LeanFilterHue : LeanFilter
	{
		public override string Title
		{
			get
			{
				return "Rotate Hue";
			}
		}

		/// <summary>The hue of all pixels will be offset by this value.</summary>
		public float Offset { set { offset = value; } get { return offset; } } [SerializeField] private float offset;

		[BurstCompile]
		struct FilterJob : IJobParallelFor
		{
			public NativeArray<float4> INOUT;

			[ReadOnly] public float Offset;

			public void Execute(int index)
			{
				var pixel = INOUT[index];
				var hsv   = default(float3);

				Color.RGBToHSV(new Color(pixel.x, pixel.y, pixel.z), out hsv.x, out hsv.y, out hsv.z);

				hsv.x = ((hsv.x + Offset) % 1.0f + 1.0f) % 1.0f;

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

			filter.INOUT = data.Pixels;
			filter.Offset = offset;

			data.Handle = filter.Schedule(data.Pixels.Length, 32, data.Handle);
		}

#if UNITY_EDITOR
		protected override void DrawInspector()
		{
			CwEditor.Draw("offset", "The hue of all pixels will be offset by this value.");
		}
#endif
	}
}