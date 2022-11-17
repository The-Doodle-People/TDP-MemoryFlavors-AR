
/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com
 * Facebook: https://facebook.com/appadvisory
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch
 ***********************************************************************************************************/




using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AppAdvisory.SharingSystem
{
	public class ShareSettings : ScriptableObject 
	{

		public bool androidForceSDCardPermission = true;
		public bool showButtonShareWhenSceneRestartIfScreenshotAvailable = true;
		public ButtonShareState state = ButtonShareState.isClosed;

		#region EDITOR

		public bool isShareFoldoutOpened = false;

		public static readonly string PATH = "Assets/_AppAdvisory/Very_Simple_Share/";
		public static readonly string NAME = "ShareSettings";

		private static string PathToAsset 
		{
			get 
			{
				return PATH + NAME + ".asset";
			}
		}

		#if UNITY_EDITOR

		[MenuItem("Assets/Create/AppAdvisory/ShareSettings")]
		public static void CreateShareSettings()
		{
			ShareSettings asset = ScriptableObject.CreateInstance<ShareSettings>();

			AssetDatabase.CreateAsset(asset, PathToAsset);
			AssetDatabase.SaveAssets();

			EditorUtility.FocusProjectWindow();

			Selection.activeObject = asset;
		}

		#endif

		#endregion
	}

}