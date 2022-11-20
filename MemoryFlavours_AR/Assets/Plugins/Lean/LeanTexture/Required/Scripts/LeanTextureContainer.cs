using UnityEngine;
using CW.Common;
using System.Collections.Generic;
using PresetType = Lean.Texture.LeanTexture.PresetType;
using PreviewType = Lean.Texture.LeanTexture.PreviewType;

namespace Lean.Texture
{
	/// <summary>This scriptable object wraps <b>LeanTexture</b>, so it can be used as an asset.</summary>
	public class LeanTextureContainer : ScriptableObject
	{
		public LeanTexture Data { get { if (data == null) data = new LeanTexture(); return data; } } [SerializeField] private LeanTexture data;
	}
}

#if UNITY_EDITOR
namespace Lean.Texture.Editor
{
	using UnityEditor;
	using TARGET = LeanTextureContainer;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(TARGET), true)]
	public class LeanTextureContainer_Editor : CwEditor
	{
		TARGET tgt; TARGET[] tgts;

		Texture2D previewTexture;

		bool dirty = true;

		PreviewType preview;

		bool previewTile;

		[MenuItem("Assets/Create/Lean/Texture")]
		public static void CreateTexture()
		{
			var path = AssetDatabase.GetAssetPath(Selection.activeObject);

			if (System.IO.File.Exists(path) == true)
			{
				path = System.IO.Path.GetDirectoryName(path) + "/" + System.IO.Path.GetFileNameWithoutExtension(path) + " (Modified).asset";
			}
			else
			{
				path += "/New Texture.asset";
			}

			var instance = ScriptableObject.CreateInstance<LeanTextureContainer>();
			var textures = new List<UnityEngine.Texture>();

			foreach (var o in Selection.objects)
			{
				if (o is UnityEngine.Texture)
				{
					textures.Add((UnityEngine.Texture)o);
				}
			}

			foreach (var texture in textures)
			{
				if (texture is Texture2D)
				{
					var texture2D   = (Texture2D)texture;
					var texturePath = AssetDatabase.GetAssetPath(texture2D);

					instance.Data.SettingsFrom = texture2D;

					if (string.IsNullOrEmpty(texturePath) == false)
					{
						var textureImporter = AssetImporter.GetAtPath(texturePath) as TextureImporter;

						if (textureImporter != null)
						{
							if (textureImporter.textureType == TextureImporterType.Default)
							{
								if (textureImporter.DoesSourceTextureHaveAlpha() == true)
								{
									if (textureImporter.sRGBTexture == true)
									{
										if (textureImporter.alphaIsTransparency == true)
										{
											instance.Data.Preset = PresetType.ColorPlusTransparency;
										}
										else
										{
											instance.Data.Preset = PresetType.ColorPlusAlpha;
										}
									}
									else
									{
										if (textureImporter.alphaIsTransparency == true)
										{
											instance.Data.Preset = PresetType.LinearColorPlusTransparency;
										}
										else
										{
											instance.Data.Preset = PresetType.LinearColorPlusAlpha;
										}
									}
								}
								else
								{
									if (textureImporter.sRGBTexture == true)
									{
										instance.Data.Preset = PresetType.ColorOnly;
									}
									else
									{
										instance.Data.Preset = PresetType.LinearColorOnly;
									}
								}
							}
							else if (textureImporter.textureType == TextureImporterType.NormalMap)
							{
								instance.Data.Preset = PresetType.NormalMap;
							}
							else if (textureImporter.textureType == TextureImporterType.SingleChannel)
							{
								if (texture2D.format == TextureFormat.R8)
								{
									instance.Data.Preset = PresetType.RedOnly;
								}
								else
								{
									instance.Data.Preset = PresetType.AlphaOnly;
								}
							}
						}
					}

					break;
				}
			}

			if (textures.Count == 1)
			{
				var createTexture = instance.Data.BeginWith<LeanCreateTexture>();

				createTexture.Texture = textures[0];
			}
			else if (textures.Count > 1)
			{
				var createTexture = instance.Data.BeginWith<LeanCreateTextureCombined>();

				createTexture.RedTexture = textures[0];

				createTexture.GreenTexture = textures[1];

				if (textures.Count > 2)
				{
					createTexture.BlueTexture = textures[2];
				}

				if (textures.Count > 3)
				{
					createTexture.AlphaTexture = textures[3];
				}
			}

			ProjectWindowUtil.CreateAsset(instance, path);
		}

		public override bool HasPreviewGUI()
		{
			return previewTexture != null;
		}

		public override GUIContent GetPreviewTitle()
		{
			if (previewTexture != null)
			{
				return new GUIContent("Preview (" + previewTexture.width + "x" + previewTexture.height + ")");
			}

			return base.GetPreviewTitle();
		}

		public override void OnPreviewSettings()
		{
			if (previewTexture != null)
			{
				EditorGUI.BeginChangeCheck();

				if (LeanTexture.PresetHasColor(tgt.Data.Preset) == true)
				{
					if (GUILayout.Toggle(preview == PreviewType.RGB, "RGB", EditorStyles.miniButton) == true)
					{
						preview = PreviewType.RGB;
					}
				}

				if (LeanTexture.PresetHasRed(tgt.Data.Preset) == true)
				{
					if (GUILayout.Toggle(preview == PreviewType.R, "R", EditorStyles.miniButton) == true)
					{
						preview = PreviewType.R;
					}
				}

				if (LeanTexture.PresetHasGreen(tgt.Data.Preset) == true)
				{
					if (GUILayout.Toggle(preview == PreviewType.G, "G", EditorStyles.miniButton) == true)
					{
						preview = PreviewType.G;
					}
				}

				if (LeanTexture.PresetHasBlue(tgt.Data.Preset) == true)
				{
					if (GUILayout.Toggle(preview == PreviewType.B, "B", EditorStyles.miniButton) == true)
					{
						preview = PreviewType.B;
					}
				}

				if (LeanTexture.PresetHasAlpha(tgt.Data.Preset) == true)
				{
					if (GUILayout.Toggle(preview == PreviewType.A, "A", EditorStyles.miniButton) == true)
					{
						preview = PreviewType.A;
					}
				}

				if (tgt.Data.HasWrapping == true)
				{
					previewTile = GUILayout.Toggle(previewTile, "Tile", EditorStyles.miniButton);
				}

				if (EditorGUI.EndChangeCheck() == true)
				{
					dirty = true;
				}

				ValidatePreview();
			}
		}

		private void ValidatePreview()
		{
			if (preview == PreviewType.None)
			{
				preview = PreviewType.RGB;
			}

			switch (tgt.Data.Preset)
			{
				case PresetType.ColorOnly:
				case PresetType.LinearColorOnly:
				{
					if (preview == PreviewType.A) preview = PreviewType.RGB;
				}
				break;

				case PresetType.NormalMap:
				{
					preview = PreviewType.RGB;
				}
				break;

				case PresetType.AlphaOnly:
				{
					preview = PreviewType.A;
				}
				break;

				case PresetType.RedOnly:
				{
					preview = PreviewType.R;
				}
				break;
			}
		}

		private static GUIStyle previewStyle;

		public override void OnPreviewGUI(Rect r, GUIStyle background)
		{
			if (previewTexture != null)
			{
				if (previewStyle == null)
				{
					previewStyle = new GUIStyle();

					previewStyle.alignment = TextAnchor.MiddleCenter;
				}

				GUI.Label(r, previewTexture, previewStyle);
			}
			else
			{
				base.OnPreviewGUI(r, background);
			}
		}

		protected override void OnInspector()
		{
			serializedObject.UpdateIfRequiredOrScript();

			GetTargets(out tgt, out tgts);

			var sCreate  = serializedObject.FindProperty("data.create");
			var sFilters = serializedObject.FindProperty("data.filters");
			var delIndex = -1;
			var delTex   = false;

			EditorGUI.BeginChangeCheck();

			if (Draw("data.preset", "This allows you to specify what kind of texture is being generated. This changes the output texture format, preview behavior, and warnings in the filter inspectors.\n\nNOTE: The <b>Color</b> presets are for gamma/sRGB textures, and the <b>LinearColor</b> presets are for linear color space textures.\n\nNOTE: The <b>PlusAlpha</b> and <b>PlusTransparency</b> presets change how the preview is rendered. If your alpha channel stores non-transparency data (e.g. smoothness, height) then you should use the <b>PlusAlpha</b> presets.") == true)
			{
				delTex = true;
			}

			Draw("data.settingsFrom", "When generating the texture, some settings like the <b>WrapMode</b> will be copied from the texture you specify here.");
			if (Any(tgts, t => t.Data.SettingsFrom == null))
			{
				BeginIndent();
					Draw("data.wrapModeU", "If <b>SettingsFrom</b> is set to None/null, then this allows you to specify the wrap mode of the generated texture.");
					Draw("data.wrapModeV", "If <b>SettingsFrom</b> is set to None/null, then this allows you to specify the wrap mode of the generated texture.");
					Draw("data.mipmapEnabled", "If <b>SettingsFrom</b> is set to None/null, then this allows you to specify the mip map creation of the generated texture.");
				EndIndent();
			}

			Separator();

			EditorGUILayout.BeginHorizontal();
				if (tgt.Data.Create != null)
				{
					EditorGUILayout.LabelField(tgt.Data.Create.FinalTitle, EditorStyles.boldLabel);
				}
				DrawChangeCreate();
			EditorGUILayout.EndHorizontal();
			BeginIndent();
				if (tgt.Data.Create != null)
				{
					tgt.Data.Create.DrawInspector(sCreate, serializedObject);
				}
			EndIndent();

			Separator();

			for (var i = 0; i < Mathf.Min(tgt.Data.Filters.Count, sFilters.arraySize); i++)
			{
				var filter = tgt.Data.Filters[i];

				EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(filter != null ? filter.FinalTitle : "MISSING", EditorStyles.boldLabel);
					if (GUILayout.Button("X", GUILayout.Width(20)) == true)
					{
						delIndex = i;
					}
				EditorGUILayout.EndHorizontal();

				BeginIndent();
					if (filter != null)
					{
						filter.DrawInspector(sFilters.GetArrayElementAtIndex(i), serializedObject);
					}
				EndIndent();

				Separator();
			}

			Separator();

			DrawAddFilter();

			Separator();

			if (delIndex >= 0)
			{
				sFilters.DeleteArrayElementAtIndex(delIndex); serializedObject.ApplyModifiedProperties(); dirty = true;
			}

			if (EditorGUI.EndChangeCheck() == true)
			{
				serializedObject.ApplyModifiedProperties();

				dirty = true;
			}

			if (delTex == true)
			{
				DestroyImmediate(previewTexture);

				dirty = true;
			}

			if (dirty == true)
			{
				Generate();
			}

			DrawExport();
		}

		protected virtual void OnEnable()
		{
			Undo.undoRedoPerformed += HandleUndoRedo;
		}

		protected virtual void OnDisable()
		{
			Undo.undoRedoPerformed -= HandleUndoRedo;
		}

		private void HandleUndoRedo()
		{
			dirty = true;

			serializedObject.Update();

			Repaint();
		}

		private static List<LeanCreate> createInstances = new List<LeanCreate>();

		private static List<LeanFilter> filterInstances = new List<LeanFilter>();

		static LeanTextureContainer_Editor()
		{
			foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (var type in assembly.GetTypes())
				{
					if (type.IsSubclassOf(typeof(LeanCreate)) == true)
					{
						createInstances.Add((LeanCreate)System.Activator.CreateInstance(type));
					}
					else if (type.IsSubclassOf(typeof(LeanFilter)) == true)
					{
						filterInstances.Add((LeanFilter)System.Activator.CreateInstance(type));
					}
				}
			}
		}

		private void DrawChangeCreate()
		{
			var clicked = false;

			if (tgt.Data.Create != null)
			{
				clicked = GUILayout.Button("Change", GUILayout.Width(60));
			}
			else
			{
				var oldColor = GUI.color;

				BeginColor(Color.green);
					clicked = GUILayout.Button("Choose Texture Creation Technique...");
				EndColor();
			}

			if (clicked == true)
			{
				var menu  = new GenericMenu();

				foreach (var instance in createInstances)
				{
					menu.AddItem(new GUIContent(instance.FinalTitle), false, () => { tgt.Data.Create = (LeanCreate)System.Activator.CreateInstance(instance.GetType()); serializedObject.Update(); Generate(); });
				}

				menu.ShowAsContext();
			}
		}

		private void DrawAddFilter()
		{
			if (tgt.Data.Create != null && Button("Add Filter") == true)
			{
				var menu  = new GenericMenu();
				var types = typeof(LeanFilter).Assembly.GetTypes();

				foreach (var instance in filterInstances)
				{
					menu.AddItem(new GUIContent(instance.FinalTitle), false, () => { tgt.Data.Filters.Add((LeanFilter)System.Activator.CreateInstance(instance.GetType())); serializedObject.Update(); Generate(); });
				}

				menu.ShowAsContext();
			}
		}

		private void DrawExport()
		{
			if (tgt.Data.Create != null)
			{
				EditorGUILayout.LabelField("Export", EditorStyles.boldLabel);

				EditorGUILayout.BeginHorizontal();
					if (Button("Save") == true)
					{
						var path = System.IO.Path.GetFullPath(AssetDatabase.GetAssetPath(tgt));

						path = System.IO.Path.ChangeExtension(path, "png");

						if (System.IO.File.Exists(path) == false || EditorUtility.DisplayDialog("Confirm Save", tgt.name + " already exists.\nDo you want to replace it?", "Yes", "No") == true)
						{
							TryExport(path);
						}
					}

					if (Button("Save As...") == true)
					{
						var path = EditorUtility.SaveFilePanel("Export " + tgt.name, System.IO.Path.GetDirectoryName(AssetDatabase.GetAssetPath(tgt)), tgt.name, "png");

						if (string.IsNullOrEmpty(path) == false)
						{
							TryExport(path);
						}
					}
				EditorGUILayout.EndHorizontal();
			}
		}

		private void TryExport(string path)
		{
			path = path.Replace('\\', '/');

			var convertToGamma = false;

			if (PlayerSettings.colorSpace == ColorSpace.Linear)
			{
				if (LeanTexture.PresetIsLinear(tgt.Data.Preset) == true)
				{
					convertToGamma = true;
				}
			}

			var pending = tgt.Data.TrySchedule(convertToGamma: convertToGamma);

			if (pending != null)
			{
				var tempTexture = pending.Complete();
				var data        = tempTexture.EncodeToPNG();
				var exists      = System.IO.File.Exists(path);

				DestroyImmediate(tempTexture);

				System.IO.File.WriteAllBytes(path, data);

				if (path.StartsWith(Application.dataPath) == true)
				{
					var local = path.Substring(Application.dataPath.Length - "Assets".Length);

					AssetDatabase.ImportAsset(local);

					Selection.activeObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Texture>(local);

					// Update texture import settings?
					if (exists == false)
					{
						var textureImporter = AssetImporter.GetAtPath(local) as TextureImporter;

						ApplySettings(textureImporter);

						textureImporter.SaveAndReimport();
					}
				}
			}
		}

		private void ApplySettings(TextureImporter target)
		{
			var settings = new TextureImporterSettings();

			if (tgt.Data.SettingsFrom != null)
			{
				var path = AssetDatabase.GetAssetPath(tgt.Data.SettingsFrom);

				if (string.IsNullOrEmpty(path) == false)
				{
					var textureImporter = (TextureImporter)AssetImporter.GetAtPath(path);

					textureImporter.ReadTextureSettings(settings);

					target.textureCompression = textureImporter.textureCompression;
					target.maxTextureSize     = textureImporter.maxTextureSize;
				}
				else
				{
					settings.wrapModeU     = tgt.Data.SettingsFrom.wrapModeU;
					settings.wrapModeV     = tgt.Data.SettingsFrom.wrapModeV;
					settings.mipmapEnabled = tgt.Data.SettingsFrom.mipmapCount > 0;
				}
			}
			
			switch (tgt.Data.Preset)
			{
				case PresetType.NormalMap: settings.textureType = TextureImporterType.NormalMap; break;
				case PresetType.AlphaOnly: settings.textureType = TextureImporterType.SingleChannel; settings.singleChannelComponent = TextureImporterSingleChannelComponent.Alpha; break;
				case PresetType.RedOnly: settings.textureType = TextureImporterType.SingleChannel; settings.singleChannelComponent = TextureImporterSingleChannelComponent.Red; break;
			}

			target.SetTextureSettings(settings);
		}

		private void Generate()
		{
			ValidatePreview();

			var convertToGamma = false;

			if (PlayerSettings.colorSpace == ColorSpace.Linear)
			{
				if (tgt.Data.Preset == PresetType.LinearColorOnly ||
					tgt.Data.Preset == PresetType.LinearColorPlusAlpha ||
					tgt.Data.Preset == PresetType.LinearColorPlusTransparency)
				{
					convertToGamma = true;
				}
			}

			var pending = tgt.Data.TrySchedule(previewTexture, true, preview, previewTile, convertToGamma);

			if (pending != null)
			{
				previewTexture = pending.Complete();
				dirty          = false;
			}
		}
	}
}
#endif