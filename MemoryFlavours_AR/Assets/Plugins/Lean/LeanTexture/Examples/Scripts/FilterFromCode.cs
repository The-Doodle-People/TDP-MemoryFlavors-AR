using UnityEngine;
using UnityEngine.UI;

namespace Lean.Texture
{
	/// <summary>This component shows you how to use LeanTexture from code.</summary>
	[HelpURL(LeanTexture.HelpUrlPrefix + "FilterFromCode")]
	[AddComponentMenu(LeanTexture.ComponentPathPrefix + "Filter From Code")]
	public class FilterFromCode : MonoBehaviour
	{
		/// <summary>The texture that will be modified.</summary>
		public UnityEngine.Texture SourceTexture;

		/// <summary>The UI element the modified texture will be displayed using.</summary>
		public RawImage OutputImage;

		protected virtual void Start()
		{
			// Create LeanTexture instance
			var leanTexture = new LeanTexture();

			// Begin with texture
			var createTexture = leanTexture.BeginWith<LeanCreateTexture>();

			createTexture.Texture = SourceTexture;

			// Then Modify Brightness
			var filterBrightness = leanTexture.Then<LeanFilterBrightness>();

			filterBrightness.Multiplier = 2.0f;

			// Then Rotate Hue
			var filterHue = leanTexture.Then<LeanFilterHue>();

			filterHue.Offset = 0.25f;

			// Generate texture
			var generatedTexture = leanTexture.Generate(); // NOTE: You must manually destroy this texture once you've finished using it

			// Display it in the UI
			OutputImage.texture = generatedTexture;
		}
	}
}