
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


#pragma warning disable 0162 // code unreached.
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0618 // obslolete
#pragma warning disable 0108 
#pragma warning disable 0649 //never used

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System;


namespace AppAdvisory
{
	[CustomEditor(typeof(CommonAppSettings))]
	public class SDInspectorGUI : Editor
	{
		void OnEnable()
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode)
				return;

			SettingsWindow window = SDEUtility.OpenSettings ();
			//window.DisplayCommonSettings (true);
		}

		public override void OnInspectorGUI ()
		{
			
		}
	}
}