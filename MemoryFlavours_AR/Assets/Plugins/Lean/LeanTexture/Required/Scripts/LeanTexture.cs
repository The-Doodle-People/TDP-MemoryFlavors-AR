using UnityEngine;
using System.Collections.Generic;
using Lean.Common;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;

namespace Lean.Texture
{
	/// <summary>This class can generate a texture based on the specified <b>Create</b> technique, which is then modified by the specified <b>Filters</b>.
	/// The texture can be immediately generated using the <b>Generate</b> method, or it can be generated asynchronously with the <b>TrySchedule</b> method.</summary>
	[System.Serializable]
	public class LeanTexture
	{
		public enum PresetType
		{
			ColorOnly                   = 10,
			ColorPlusAlpha              = 11,
			ColorPlusTransparency       = 12,
			LinearColorOnly             = 20,
			LinearColorPlusAlpha        = 21,
			LinearColorPlusTransparency = 22,
			NormalMap                   = 30,
			AlphaOnly                   = 40,
			RedOnly                     = 41,
		}

		public enum PreviewType
		{
			None,
			R,
			G,
			B,
			A,
			RGB
		}

		private struct Color24
		{
			public byte r;
			public byte g;
			public byte b;
		}

		public const string HelpUrlPrefix = LeanCommon.HelpUrlPrefix + "LeanTexture#";

		public const string ComponentPathPrefix = "Lean/Texture/Lean ";

		/// <summary>This allows you to specify what kind of texture is being generated. This changes the output texture format, preview behavior, and warnings in the filter inspectors.
		/// NOTE: The <b>Color</b> presets are for gamma/sRGB textures, and the <b>LinearColor</b> presets are for linear color space textures.
		/// NOTE: The <b>PlusAlpha</b> and <b>PlusTransparency</b> presets change how the preview is rendered. If your alpha channel stores non-transparency data (e.g. smoothness, height) then you should use the <b>PlusAlpha</b> presets.</summary>
		public PresetType Preset { set { preset = value; } get { return preset; } } [SerializeField] private PresetType preset = PresetType.ColorPlusAlpha;

		/// <summary>When generating the texture, some settings like the <b>WrapMode</b> will be copied from the texture you specify here.</summary>
		public Texture2D SettingsFrom { set { settingsFrom = value; } get { return settingsFrom; } } [SerializeField] private Texture2D settingsFrom;

		/// <summary>If <b>SettingsFrom</b> is set to None/null, then this allows you to specify the wrap mode of the generated texture.</summary>
		public TextureWrapMode WrapModeU { set { wrapModeU = value; } get { return wrapModeU; } } [SerializeField] private TextureWrapMode wrapModeU;

		/// <summary>If <b>SettingsFrom</b> is set to None/null, then this allows you to specify the wrap mode of the generated texture.</summary>
		public TextureWrapMode WrapModeV { set { wrapModeV = value; } get { return wrapModeV; } } [SerializeField] private TextureWrapMode wrapModeV;

		/// <summary>If <b>SettingsFrom</b> is set to None/null, then this allows you to specify the mip map creation of the generated texture.</summary>
		public bool MipmapEnabled { set { mipmapEnabled = value; } get { return mipmapEnabled; } } [SerializeField] private bool mipmapEnabled;

		/// <summary>The texture will begin with this creation method.</summary>
		public LeanCreate Create { set { create = value; } get { return create; } } [SerializeReference] private LeanCreate create;

		/// <summary>The texture will then be modified by these filters.</summary>
		public List<LeanFilter> Filters { get { if (filters == null) filters = new List<LeanFilter>(); return filters; } } [SerializeReference] private List<LeanFilter> filters;

		/// <summary>Does this texture have horizontal or vertical wrapping?</summary>
		public bool HasWrapping
		{
			get
			{
				if (settingsFrom != null)
				{
					return settingsFrom.wrapModeU != TextureWrapMode.Clamp || settingsFrom.wrapModeV != TextureWrapMode.Clamp;
				}

				return wrapModeU != TextureWrapMode.Clamp || wrapModeV != TextureWrapMode.Clamp;
			}
		}

		public static bool PresetIsLinear(PresetType preset)
		{
			return
				preset == PresetType.LinearColorOnly ||
				preset == PresetType.LinearColorPlusAlpha ||
				preset == PresetType.LinearColorPlusTransparency ||
				preset == PresetType.NormalMap ||
				preset == PresetType.AlphaOnly ||
				preset == PresetType.RedOnly;
		}

		public static bool PresetHasColor(PresetType preset)
		{
			return
				preset == PresetType.ColorOnly ||
				preset == PresetType.ColorPlusAlpha ||
				preset == PresetType.ColorPlusTransparency ||
				preset == PresetType.LinearColorPlusAlpha ||
				preset == PresetType.LinearColorOnly ||
				preset == PresetType.LinearColorPlusTransparency;
		}

		public static bool PresetHasRed(PresetType preset)
		{
			return PresetHasColor(preset) == true || preset == PresetType.RedOnly;
		}

		public static bool PresetHasGreen(PresetType preset)
		{
			return PresetHasColor(preset) == true;
		}

		public static bool PresetHasBlue(PresetType preset)
		{
			return PresetHasColor(preset) == true;
		}

		public static bool PresetHasAlpha(PresetType preset)
		{
			return
				preset == PresetType.ColorPlusAlpha ||
				preset == PresetType.ColorPlusTransparency ||
				preset == PresetType.LinearColorPlusAlpha ||
				preset == PresetType.LinearColorPlusTransparency ||
				preset == PresetType.AlphaOnly;
		}

		/// <summary>This method allows you to change how this texture is initially created.</summary>
		public T BeginWith<T>()
			where T : LeanCreate, new()
		{
			var instance = new T();

			Create = instance;

			return instance;
		}

		/// <summary>This method allows you to add a filter to the current texture.</summary>
		public T Then<T>()
			where T : LeanFilter, new()
		{
			var instance = new T();

			Filters.Add(instance);

			return instance;
		}

		/// <summary>This method will immediately perform the specified texture operations and return the modified texture.</summary>
		public Texture2D Generate(Texture2D existingTexture = null, bool destroyExistingIfIncompatible = true, PreviewType preview = PreviewType.None, bool previewTile = false)
		{
			var pending = TrySchedule(existingTexture, destroyExistingIfIncompatible, preview, previewTile);

			if (pending != null)
			{
				return pending.Complete();
			}

			return existingTexture;
		}

		/// <summary>This method will try and create a job to modify a texture using the current settings applied to this LeanTexture instance.</summary>
		public LeanPendingTexture TrySchedule(Texture2D existingTexture = null, bool destroyExistingIfIncompatible = true, PreviewType preview = PreviewType.None, bool previewTile = false, bool convertToGamma = false)
		{
			if (create != null)
			{
				// Extract info from the BaseOn texture
				var finalWrapModeU = settingsFrom != null ? settingsFrom.wrapModeU : wrapModeU;
				var finalWrapModeV = settingsFrom != null ? settingsFrom.wrapModeV : wrapModeV;
				var finalMipmap    = settingsFrom != null ? settingsFrom.mipmapCount > 0 : false;
				var linear         = PresetIsLinear(preset);

				var pending = create.TrySchedule(finalWrapModeU, finalWrapModeV, linear);

				if (pending != null)
				{
					if (filters != null)
					{
						foreach (var filter in filters)
						{
							if (filter != null)
							{
								filter.Schedule(pending);
							}
						}
					}

					var outputTexture  = default(Texture2D);
					var format         = GetFormat(preview);

					if (existingTexture != null)
					{
						if (existingTexture.format == format && existingTexture.mipmapCount > 0 && finalMipmap == true)
						{
							outputTexture = existingTexture;
						}
						else if (destroyExistingIfIncompatible == true)
						{
							Object.DestroyImmediate(existingTexture);
						}
					}

					if (outputTexture == null)
					{
						outputTexture = new Texture2D(pending.Size.x, pending.Size.y, format, finalMipmap, linear);
					}
					else if (outputTexture.width != pending.Size.x || outputTexture.height != pending.Size.y)
					{
						outputTexture.Resize(pending.Size.x, pending.Size.y);
					}
					
					pending.OutputTexture = outputTexture;

					if (convertToGamma == true)
					{
						pending.ConvertToGamma();
					}

					ScheduleConvert(pending, outputTexture, preview, previewTile);

					return pending;
				}
			}

			return null;
		}

		private void ScheduleConvert(LeanPendingTexture data, Texture2D outputTexture, PreviewType preview, bool previewTile)
		{
			if (preview == PreviewType.None)
			{
				switch (preset)
				{
					case PresetType.ColorPlusAlpha:
					case PresetType.LinearColorPlusAlpha:
					case PresetType.ColorPlusTransparency:
					case PresetType.LinearColorPlusTransparency:
					{
						var colors  = new NativeArray<Color32>(data.Size.x * data.Size.y, Allocator.TempJob); data.Register(colors);
						var convert = new ConvertJob_32() { IN = data.Pixels, OUT = colors };

						convert.Schedule(data.Pixels.Length, 32, data.Handle).Complete();

						outputTexture.SetPixelData(colors, 0);
					}
					break;

					case PresetType.ColorOnly:
					case PresetType.LinearColorOnly:
					case PresetType.NormalMap:
					{
						var colors  = new NativeArray<Color24>(data.Size.x * data.Size.y, Allocator.TempJob); data.Register(colors);
						var convert = new ConvertJob_24() { IN = data.Pixels, OUT = colors };

						convert.Schedule(data.Pixels.Length, 32, data.Handle).Complete();

						outputTexture.SetPixelData(colors, 0);
					}
					break;

					case PresetType.AlphaOnly:
					case PresetType.RedOnly:
					{
						var colors  = new NativeArray<byte>(data.Size.x * data.Size.y, Allocator.TempJob); data.Register(colors);
						var convert = new ConvertJob_8() { IN = data.Pixels, OUT = colors, Alpha = preset == PresetType.AlphaOnly };

						convert.Schedule(data.Pixels.Length, 32, data.Handle).Complete();

						outputTexture.SetPixelData(colors, 0);
					}
					break;
				}
			}
			else
			{
				var colors  = new NativeArray<Color24>(data.Size.x * data.Size.y, Allocator.TempJob); data.Register(colors);
				var convert = new ConvertJob_Preview() { IN = data.Pixels, OUT = colors, Preview = preview, Preset = preset, Size = data.Size, Tile = previewTile };

				convert.Schedule(data.Pixels.Length, 32, data.Handle).Complete();

				outputTexture.SetPixelData(colors, 0);
			}
		}

		private TextureFormat GetFormat(PreviewType preview)
		{
			if (preview == PreviewType.None)
			{
				switch (preset)
				{
					case PresetType.ColorOnly:       return TextureFormat.RGB24;
					case PresetType.LinearColorOnly: return TextureFormat.RGB24;
					case PresetType.NormalMap:       return TextureFormat.RGB24;
					case PresetType.AlphaOnly:       return TextureFormat.Alpha8;
					case PresetType.RedOnly:         return TextureFormat.R8;
				}

				return TextureFormat.RGBA32;
			}

			return TextureFormat.RGB24;
		}

		[BurstCompile]
		private struct ConvertJob_32 : IJobParallelFor
		{
			[ReadOnly] public NativeArray<float4> IN;
			[WriteOnly] public NativeArray<Color32> OUT;

			public void Execute(int index)
			{
				var pixel = IN[index];

				OUT[index] = new Color32() { r = FloatToByte(pixel.x), g = FloatToByte(pixel.y), b = FloatToByte(pixel.z), a = FloatToByte(pixel.w) };
			}
		}

		[BurstCompile]
		private struct ConvertJob_24 : IJobParallelFor
		{
			[ReadOnly] public NativeArray<float4> IN;
			[WriteOnly] public NativeArray<Color24> OUT;

			public void Execute(int index)
			{
				var pixel = IN[index];

				OUT[index] = new Color24() { r = FloatToByte(pixel.x), g = FloatToByte(pixel.y), b = FloatToByte(pixel.z) };
			}
		}

		[BurstCompile]
		private struct ConvertJob_8 : IJobParallelFor
		{
			[ReadOnly] public NativeArray<float4> IN;
			[WriteOnly] public NativeArray<byte> OUT;
			[ReadOnly] public bool Alpha;

			public void Execute(int index)
			{
				var pixel = IN[index];

				if (Alpha == true)
				{
					OUT[index] = FloatToByte(pixel.w);
				}
				else
				{
					OUT[index] = FloatToByte(pixel.x);
				}
			}
		}

		[BurstCompile]
		private struct ConvertJob_Preview : IJobParallelFor
		{
			[WriteOnly] public NativeArray<Color24> OUT;

			[ReadOnly] public NativeArray<float4> IN;
			[ReadOnly] public int2                Size;
			[ReadOnly] public PresetType          Preset;
			[ReadOnly] public PreviewType         Preview;
			[ReadOnly] public bool                Tile;

			public void Execute(int index)
			{
				var x  = index % Size.x;
				var y  = index / Size.x;
				var uv = new float2(x, y) / (Size - 1);

				if (Tile == true)
				{
					uv = ((uv - 0.5f) * 2.0f) + 0.5f;
				}

				var px = LeanSample.Tex2D_Point_WrapXY(IN, Size, uv);

				switch (Preview)
				{
					case PreviewType.R: px = new float4(px.x, 0.0f, 0.0f, 1.0f); break;
					case PreviewType.G: px = new float4(0.0f, px.y, 0.0f, 1.0f); break;
					case PreviewType.B: px = new float4(0.0f, 0.0f, px.z, 1.0f); break;
					case PreviewType.A: px = new float4(px.w, px.w, px.w, 1.0f); break;
				}

				switch (Preset)
				{
					case PresetType.ColorPlusTransparency:
					case PresetType.LinearColorPlusTransparency:
					{
						px.xyz *= px.w;
					}
					break;
				}

				OUT[index] = new Color24() { r = FloatToByte(px.x), g = FloatToByte(px.y), b = FloatToByte(px.z) };
			}
		}

		private static byte FloatToByte(float v)
		{
			return (byte)math.round(math.saturate(v) * 255.0f);
		}
	}
}