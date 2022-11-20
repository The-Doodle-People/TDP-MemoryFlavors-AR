using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using CW.Common;

namespace Lean.Texture
{
	/// <summary>This lets you create a texture based on a color gradient.</summary>
	[System.Serializable]
	public class LeanCreateGradient : LeanCreate
	{
		public override string Title
		{
			get
			{
				return "Gradient";
			}
		}

		public enum CoordinateType
		{
			ZeroOne,
			Pixel
		}

		/// <summary>The size of the created texture.</summary>
		public int2 Size { set { size = value; } get { return size; } } [SerializeField] private int2 size = new int2(1024, 1024);

		/// <summary>The first color of the gradient.</summary>
		public Color ColorA { set { colorA = value; } get { return colorA; } } [SerializeField] private Color colorA = Color.white;

		/// <summary>The second color of the gradient.</summary>
		public Color ColorB { set { colorB = value; } get { return colorB; } } [SerializeField] private Color colorB = Color.gray;

		/// <summary>The coordinate space of the points, where 0 is the bottom left.</summary>
		public CoordinateType Coordinate { set { coordinate = (int)value; } get { return (CoordinateType)coordinate; } } [SerializeField] [LeanEnum(typeof(CoordinateType))] private int coordinate;

		/// <summary>The first point of the gradient.</summary>
		public float2 PointA { set { pointA = value; } get { return pointA; } } [SerializeField] private float2 pointA;

		/// <summary>The second point of the gradient.</summary>
		public float2 PointB { set { pointB = value; } get { return pointB; } } [SerializeField] private float2 pointB = new float2(1.0f, 1.0f);

		[BurstCompile]
		struct CreateJob : IJobParallelFor
		{
			[WriteOnly] public NativeArray<float4> OUT;

			[ReadOnly] public int2   Size;
			[ReadOnly] public float4 ColorA;
			[ReadOnly] public float4 ColorB;
			[ReadOnly] public float2 PointA;
			[ReadOnly] public float2 PointB;

			public void Execute(int index)
			{
				var x   = index % Size.x;
				var y   = index / Size.x;
				var uv  = new float2(x, y) / (Size - 1);
				var ab  = PointB - PointA;
				var abd = math.max(math.lengthsq(ab), 0.0001f);
				var d01 = math.saturate(Vector2.Dot(uv - PointA, ab) / abd);

				OUT[index] = math.lerp(ColorA, ColorB, d01);
			}
		}

		public override LeanPendingTexture TrySchedule(TextureWrapMode wrapU, TextureWrapMode wrapV, bool linear)
		{
			if (size.x > 0 && size.y > 0)
			{
				var data   = LeanPendingTexture.Create(size, wrapU, wrapV, linear);
				var create = new CreateJob();

				create.OUT    = data.Pixels;
				create.Size   = data.Size;
				create.ColorA = (Vector4)colorA;
				create.ColorB = (Vector4)colorB;
				create.PointA = pointA;
				create.PointB = pointB;

				if (Coordinate == CoordinateType.Pixel)
				{
					create.PointA /= size;
					create.PointB /= size;
				}

				data.Handle = create.Schedule(data.Pixels.Length, 32);

				return data;
			}

			return null;
		}

#if UNITY_EDITOR
		protected override void DrawInspector()
		{
			CwEditor.Draw("size", "The size of the created texture.");
			CwEditor.Draw("colorA", "The first color of the gradient.");
			CwEditor.Draw("colorB", "The second color of the gradient.");

			CwEditor.Separator();

			CwEditor.Draw("coordinate", "The coordinate space of the points, where 0 is the bottom left.");
			CwEditor.Draw("pointA", "The first point of the gradient.");
			CwEditor.Draw("pointB", "The second point of the gradient.");
		}
#endif
	}
}