
/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com
 * Facebook: https://facebook.com/appadvisory
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch
 ***********************************************************************************************************/

#pragma warning disable 0162 // code unreached.
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0618 // obslolete
#pragma warning disable 0108 
#pragma warning disable 0649 //never used


using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEditorInternal;


namespace AppAdvisory.Utils
{
	public class EditorWindowUtils  
	{

		private static readonly string TOOLTIPSYMBOL = "    [?]";

		public static GUIContent CreateGUIContentFromDescriptionAndTooltip (string description, string tooltip = "") {
			GUIContent guiContent;
			if (String.IsNullOrEmpty (tooltip)) 
			{
				guiContent = new GUIContent (description);
			} else 
			{
				guiContent = new GUIContent (description + TOOLTIPSYMBOL, tooltip);
			}
			return guiContent;

		}

		public static void Space(uint spacing = 1) 
		{
			for (uint i = 0; i < spacing; i++) 
			{
				EditorGUILayout.Space ();
			}
		}


		public static ReorderableList CreateReorderableEnumList<T>(ref List<T> enumList, string title) {

			ReorderableList banners = new ReorderableList(enumList, typeof(T), true, true, true, true);
			banners.drawElementCallback =  
				(Rect rect, int index, bool isActive, bool isFocused) => {
				T element = (T) banners.list[index];
				rect.y += 2;
				element = (T)(System.Object) EditorGUI.EnumPopup(new Rect(rect.x, rect.y, 300, EditorGUIUtility.singleLineHeight), (Enum)(System.Object)element);
				banners.list[index] = element;
			};

			banners.drawHeaderCallback = (Rect rect) => {  
				EditorGUI.LabelField(rect, title);
			};
			return banners;
		}


		public static void CreateLabelField(string description, string tooltip = "", float horizontalSpacing = 5, uint verticalSpacing = 1)
		{
			GUIContent guiContent = CreateGUIContentFromDescriptionAndTooltip (description, tooltip);

			EditorGUILayout.BeginHorizontal ();
			GUILayout.Space (horizontalSpacing);
			EditorGUILayout.LabelField (guiContent);
			EditorGUILayout.EndHorizontal ();

			Space (verticalSpacing);
		}


		public static void CreateTextField(ref string text, string description, string tooltip = "", float horizontalSpacing = 5, uint verticalSpacing = 1) 
		{
			CreateTextFieldOnLine (ref text, description, tooltip, horizontalSpacing, verticalSpacing);
			return;

			CreateLabelField (description, tooltip, horizontalSpacing, verticalSpacing);
			
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space (horizontalSpacing);
			text = EditorGUILayout.TextField(text);
			EditorGUILayout.EndHorizontal();
			Space (verticalSpacing);
		}

		public static void CreateTextFieldOnLine(ref string text, string description, string tooltip = "", float horizontalSpacing = 5, uint verticalSpacing = 1) {
			GUIContent guiContent = CreateGUIContentFromDescriptionAndTooltip (description, tooltip);

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space (horizontalSpacing);
			text = EditorGUILayout.TextField(description, text);
			EditorGUILayout.EndHorizontal();
			Space (verticalSpacing);
		}


		public static void CreateToggleField(ref bool boolean, string description, string tooltip = "", float horizontalSpacing = 5, uint verticalSpacing = 1)
		{
			GUIContent guiContent = CreateGUIContentFromDescriptionAndTooltip (description, tooltip);

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space (horizontalSpacing);
			boolean = EditorGUILayout.BeginToggleGroup (guiContent, boolean);
			EditorGUILayout.EndToggleGroup ();
			EditorGUILayout.EndHorizontal();
			Space (verticalSpacing);
		}

		public static void CreateIntSlider(ref int value, int min, int max, string description, float width = 400, float horizontalSpacing = 5, uint verticalSpacing = 1) {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(horizontalSpacing);
			value= EditorGUILayout.IntSlider(description, value, min, max, GUILayout.Width(width));
			EditorGUILayout.EndHorizontal();
			Space (verticalSpacing);
		}

		public static void CreateFloatSlider(ref float value, float min, float max, string description, float width = 400, float margin = 5, uint verticalSpacing = 1) {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(margin);
			value= EditorGUILayout.Slider(description, value, min, max, GUILayout.Width(width));
			EditorGUILayout.EndHorizontal();

			Space (verticalSpacing);
		}

		public static void CreateEnumPopup<T>(ref T value, string description, float width = 300, float margin = 5, uint verticalSpacing = 1) where T : struct, IFormattable, IConvertible, IComparable {
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(margin);

			value = (T) (System.Object) EditorGUILayout.EnumPopup (description, (Enum)(System.Object)value, GUILayout.Width (width));
			EditorGUILayout.EndHorizontal();

			Space (verticalSpacing);
		}

		public static void CreateFoldout(ref bool boolean, string description, float horizontalSpacing = 5, uint verticalSpacing = 1) 
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.Space (horizontalSpacing);
			boolean = EditorGUILayout.Foldout(boolean, description);
			EditorGUILayout.EndHorizontal();

			Space (verticalSpacing);
		}

		public static void CreateToolbarButton<Enum>(GUIContent content, Enum thisItem, ref Enum activeItem, GUIStyle style)
		{
			EditorGUI.BeginChangeCheck();
			if (GUILayout.Toggle(thisItem.Equals(activeItem), content, style))
			{ 
				if (EditorGUI.EndChangeCheck())
				{                 
					EditorGUI.FocusTextInControl(null);
				}

				activeItem = thisItem;   
			}           
		}


	}
}