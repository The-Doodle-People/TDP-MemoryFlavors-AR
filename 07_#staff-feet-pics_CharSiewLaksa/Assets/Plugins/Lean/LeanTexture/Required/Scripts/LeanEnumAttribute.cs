using UnityEngine;

namespace Lean.Texture
{
    /// <summary>Enums don't seem to render properly with SerializeReference if they appear after the inspector has opened (at least in 2019.4.12f1), so we use this to work around it.</summary>
    public class LeanEnumAttribute : PropertyAttribute
    {
        public LeanEnumAttribute(System.Type newEnumType)
        {
            EnumType = newEnumType;
        }

        public readonly System.Type EnumType;
    }
}

#if UNITY_EDITOR
namespace Lean.Texture
{
	using UnityEditor;

	[CustomPropertyDrawer(typeof(LeanEnumAttribute))]
	public class LeanEnumDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var enumAttribute = (LeanEnumAttribute)attribute;

			var enumEntry = (System.Enum)System.Enum.ToObject(enumAttribute.EnumType, property.intValue);

			property.intValue = (int)(object)EditorGUI.EnumPopup(position, label, enumEntry);
		}
	}
}
#endif