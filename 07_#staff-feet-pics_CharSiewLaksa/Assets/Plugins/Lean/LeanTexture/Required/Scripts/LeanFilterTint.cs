using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using CW.Common;

namespace Lean.Texture
{
	/// <summary>This allows you to multiply all pixels in the current texture by the specified color.</summary>
	[System.Serializable]
	public class LeanFilterTint : LeanFilter
	{
		public override string Title
		{
			get
			{
				return "Tint Color";
			}
		}

		/// <summary>All pixels in the texture will be multiplied by this color.</summary>
		public Color Color { set { color = value; } get { return color; } } [SerializeField] private Color color = Color.white;

		[BurstCompile]
		struct FilterJob : IJobParallelFor
		{
			public NativeArray<float4> INOUT;

			[ReadOnly] public float4 Color;

			public void Execute(int index)
			{
				var pixel = INOUT[index];

				pixel *= Color;

				INOUT[index] = pixel;
			}
		}

		public override void Schedule(LeanPendingTexture data)
		{
			var filter = new FilterJob();

			filter.INOUT = data.Pixels;
			filter.Color  = (Vector4)color;

			data.Handle = filter.Schedule(data.Pixels.Length, 32, data.Handle);
		}

#if UNITY_EDITOR
		protected override void DrawInspector()
		{
			CwEditor.Draw("color", "All pixels in the texture will be multiplied by this color.");
		}
#endif
	}
}