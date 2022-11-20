using UnityEngine;
using CW.Common;

namespace Lean.Texture
{
	/// <summary>This is the base class for all texture creation methods.</summary>
	[System.Serializable]
	public abstract class LeanCreate
	{
		public abstract string Title
		{
			get;
		}

		public string FinalTitle
		{
			get
			{
				return "Begin With " + Title;
			}
		}

		public abstract LeanPendingTexture TrySchedule(TextureWrapMode wrapU, TextureWrapMode wrapV, bool linear);

#if UNITY_EDITOR
		public void DrawInspector(UnityEditor.SerializedProperty property, UnityEditor.SerializedObject data)
		{
			CwEditor.BeginData(property);

			DrawInspector();

			CwEditor.EndData();
		}

		protected bool ObjectExists(string propertyPath)
		{
			var property = CwEditor.GetProperty(propertyPath);

			return property != null && property.objectReferenceValue != null;
		}

		protected abstract void DrawInspector();
#endif
	}
}