using UnityEngine;
using Unity.Collections;
using Unity.Mathematics;

namespace Lean.Texture
{
	/// <summary>This class contains useful methods used for sampling texture data.</summary>
	[System.Serializable]
	public static class LeanSample
	{
		private static Texture2D blitBuffer;

		private static Material blitMaterial;

#if UNITY_EDITOR
		private static Texture2D directBuffer;
#endif

		private static int _NewTexture = Shader.PropertyToID("_NewTexture");
		private static int _NewSize    = Shader.PropertyToID("_NewSize");

		public static NativeArray<float4> GetPixels(UnityEngine.Texture source, Allocator allocator, bool linear)
		{
			var finalSize = default(int2);

			return GetPixels(source, allocator, linear, ref finalSize);
		}

		public static NativeArray<float4> GetPixels(UnityEngine.Texture source, Allocator allocator, bool linear, ref int2 finalSize)
		{
			if (source != null)
			{
#if UNITY_EDITOR
				var path = UnityEditor.AssetDatabase.GetAssetPath(source);

				if (string.IsNullOrEmpty(path) == false)
				{
					if (path.EndsWith(".png", System.StringComparison.InvariantCultureIgnoreCase) == true || path.EndsWith(".jpg", System.StringComparison.InvariantCultureIgnoreCase) == true)
					{
						var data = System.IO.File.ReadAllBytes(path);

						if (data.Length > 0)
						{
							if (directBuffer == null)
							{
								directBuffer = new Texture2D(1, 1, TextureFormat.RGBA32, false);
							}

							directBuffer.LoadImage(data);

							source = directBuffer;
						}
					}
				}
#endif
				var oldActive     = RenderTexture.active;
				var renderTexture = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);

				if (blitBuffer == null)
				{
					blitBuffer = new Texture2D(source.width, source.height, TextureFormat.RGBAFloat, false);
				}
				else if (blitBuffer.width != source.width || blitBuffer.height != source.height)
				{
					blitBuffer.Resize(source.width, source.height);
				}

				if (blitMaterial == null)
				{
					blitMaterial = new Material(Shader.Find("Hidden/Lean/Texture/Blit"));
				}

				blitMaterial.SetTexture(_NewTexture, source);
				blitMaterial.SetVector(_NewSize, new Vector2(source.width, source.height));

				if (linear == false && QualitySettings.activeColorSpace == ColorSpace.Linear)
				{
					Graphics.Blit(source, renderTexture, blitMaterial, 1);
				}
				else
				{
					Graphics.Blit(source, renderTexture, blitMaterial, 0);
				}

				RenderTexture.active = renderTexture;

				blitBuffer.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);

				RenderTexture.active = oldActive;

				RenderTexture.ReleaseTemporary(renderTexture);

				finalSize.x = source.width;
				finalSize.y = source.height;

				return new NativeArray<float4>(blitBuffer.GetRawTextureData<float4>(), allocator);
			}

			return default(NativeArray<float4>);
		}

		public static float4 Tex2D_Cubic_WrapXY(NativeArray<float4> pixels, int2 size, double2 uv)
		{
			uv = uv * (size - 1);

			var fracX = (float)((uv.x % 1.0 + 1.0) % 1.0);
			var fracY = (float)((uv.y % 1.0 + 1.0) % 1.0);
			var x     = (long)math.floor(uv.x % size.x);
			var y     = (long)math.floor(uv.y % size.y);

			var aa = Tex2D_Point_WrapXY(pixels, size, x - 1, y - 1); var ba = Tex2D_Point_WrapXY(pixels, size, x, y - 1); var ca = Tex2D_Point_WrapXY(pixels, size, x + 1, y - 1); var da = Tex2D_Point_WrapXY(pixels, size, x + 2, y - 1);
			var ab = Tex2D_Point_WrapXY(pixels, size, x - 1, y    ); var bb = Tex2D_Point_WrapXY(pixels, size, x, y    ); var cb = Tex2D_Point_WrapXY(pixels, size, x + 1, y    ); var db = Tex2D_Point_WrapXY(pixels, size, x + 2, y    );
			var ac = Tex2D_Point_WrapXY(pixels, size, x - 1, y + 1); var bc = Tex2D_Point_WrapXY(pixels, size, x, y + 1); var cc = Tex2D_Point_WrapXY(pixels, size, x + 1, y + 1); var dc = Tex2D_Point_WrapXY(pixels, size, x + 2, y + 1);
			var ad = Tex2D_Point_WrapXY(pixels, size, x - 1, y + 2); var bd = Tex2D_Point_WrapXY(pixels, size, x, y + 2); var cd = Tex2D_Point_WrapXY(pixels, size, x + 1, y + 2); var dd = Tex2D_Point_WrapXY(pixels, size, x + 2, y + 2);

			var a = Hermite(aa, ba, ca, da, fracX);
			var b = Hermite(ab, bb, cb, db, fracX);
			var c = Hermite(ac, bc, cc, dc, fracX);
			var d = Hermite(ad, bd, cd, dd, fracX);

			return Hermite(a, b, c, d, fracY);
		}

		public static float4 Tex2D_Linear(NativeArray<float4> pixels, int2 size, double2 uv)
		{
			uv = math.clamp(uv, 0.0, 1.0) * (size - 1);

			var fracX = (float)(uv.x % 1.0);
			var fracY = (float)(uv.y % 1.0);
			var x     = (long)math.floor(uv.x);
			var y     = (long)math.floor(uv.y);

			var aa = Tex2D_Point(pixels, size, x, y    ); var ba = Tex2D_Point(pixels, size, x + 1, y    );
			var ab = Tex2D_Point(pixels, size, x, y + 1); var bb = Tex2D_Point(pixels, size, x + 1, y + 1);

			var a = math.lerp(aa, ba, fracX);
			var b = math.lerp(ab, bb, fracX);

			return math.lerp(a, b, fracY);
		}

		public static float4 Tex2D_Linear_WrapXY(NativeArray<float4> pixels, int2 size, double2 uv)
		{
			uv = uv * (size - 1);

			var fracX = (float)((uv.x % 1.0 + 1.0) % 1.0);
			var fracY = (float)((uv.y % 1.0 + 1.0) % 1.0);
			var x     = (long)math.round(uv.x % size.x);
			var y     = (long)math.round(uv.y % size.y);

			var aa = Tex2D_Point_WrapXY(pixels, size, x, y    ); var ba = Tex2D_Point_WrapXY(pixels, size, x + 1, y    );
			var ab = Tex2D_Point_WrapXY(pixels, size, x, y + 1); var bb = Tex2D_Point_WrapXY(pixels, size, x + 1, y + 1);

			var a = math.lerp(aa, ba, fracX);
			var b = math.lerp(ab, bb, fracX);

			return math.lerp(a, b, fracY);
		}

		public static float4 Tex2D_Point_WrapXY(NativeArray<float4> pixels, int2 size, double2 uv)
		{
			var x = (long)math.round(uv.x * (size.x - 1));
			var y = (long)math.round(uv.y * (size.y - 1));

			return Tex2D_Point_WrapXY(pixels, size, x, y);
		}

		public static float4 Tex2D_Point_WrapXY(NativeArray<float4> pixels, int2 size, long x, long y)
		{
			x = (x % size.x + size.x) % size.x;
			y = (y % size.y + size.y) % size.y;

			return pixels[(int)(x + y * size.x)];
		}

		public static float4 Tex2D_Point(NativeArray<float4> pixels, int2 size, double2 uv)
		{
			var x = (long)math.round(uv.x * (size.x - 1));
			var y = (long)math.round(uv.y * (size.y - 1));

			return Tex2D_Point(pixels, size, x, y);
		}

		public static float4 Tex2D_Point(NativeArray<float4> pixels, int2 size, long x, long y)
		{
			x = math.clamp(x, 0, size.x - 1);
			y = math.clamp(y, 0, size.y - 1);

			return pixels[(int)(x + y * size.x)];
		}

		public static float4 Hermite(float4 a, float4 b, float4 c, float4 d, float t)
		{
			var tt   = t * t;
			var tt3  = tt * 3.0f;
			var ttt  = t * tt;
			var ttt2 = ttt * 2.0f;
			var m0   = (c - a) * 0.5f;
			var m1   = (d - b) * 0.5f;
			var a0   =  ttt2 - tt3 + 1.0f;
			var a1   =  ttt  - tt * 2.0f + t;
			var a2   =  ttt  - tt;
			var a3   = -ttt2 + tt3;

			return a0 * b + a1 * m0 + a2 * m1 + a3 * c;
		}
	}
}