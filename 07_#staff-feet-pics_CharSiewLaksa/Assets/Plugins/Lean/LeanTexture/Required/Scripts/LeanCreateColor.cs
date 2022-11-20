using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using CW.Common;

namespace Lean.Texture
{
	/// <summary>This lets you create a texture where all pixels have the same color value.</summary>
	[System.Serializable]
	public class LeanCreateColor : LeanCreate
	{
		public override string Title
		{
			get
			{
				return "Color";
			}
		}

		/// <summary>The size of the created texture.</summary>
		public int2 Size { set { size = value; } get { return size; } } [SerializeField] private int2 size = new int2(1024, 1024);

		/// <summary>The color of the created texture.</summary>
		public Color Color { set { color = value; } get { return color; } } [SerializeField] private Color color = Color.white;

		[BurstCompile]
		struct CreateJob : IJobParallelFor
		{
			[WriteOnly] public NativeArray<float4> OUT;

			[ReadOnly] public Color Color;

			public void Execute(int index)
			{
				var pixel = default(float4);

				pixel = (Vector4)Color;

				OUT[index] = pixel;
			}
		}

		public override LeanPendingTexture TrySchedule(TextureWrapMode wrapU, TextureWrapMode wrapV, bool linear)
		{
			if (size.x > 0 && size.y > 0)
			{
				var data   = LeanPendingTexture.Create(size, wrapU, wrapV, linear);
				var create = new CreateJob();

				create.OUT   = data.Pixels;
				create.Color = color;

				data.Handle = create.Schedule(data.Pixels.Length, 32);

				return data;
			}

			return null;
		}

#if UNITY_EDITOR
		protected override void DrawInspector()
		{
			CwEditor.Draw("size", "The size of the created texture.");
			CwEditor.Draw("color", "The color of the created texture.");
		}
#endif
	}
}