#pragma warning disable 0162 // code unreached.
using System.Reflection;


#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0618 // obslolete
#pragma warning disable 0108 
#pragma warning disable 0649 //never used
#pragma warning disable 0429 //never used

/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System;
// from the excellent http://answers.unity3d.com/questions/45186/can-i-auto-run-a-script-when-editor-launches-or-a.html

///
/// This must be added to "Editor" folder: http://unity3d.com/support/documentation/ScriptReference/index.Script_compilation_28Advanced29.html
/// Execute some code exactly once, whenever the project is opened, recompiled, or run.
///

namespace AppAdvisory
{
	[InitializeOnLoad]
	public class Autorun
	{

		private const bool DOSCIRPTINGSYMBOL = true;

		static Autorun()
		{
			EditorApplication.playmodeStateChanged -= OnPlayModeChanged;
			EditorApplication.playmodeStateChanged += OnPlayModeChanged;


			EditorApplication.update -= RunOnce;
			EditorApplication.update += RunOnce;

		}
			
		static void OnPlayModeChanged(){
			EditorApplication.update -= RunOnce;
			EditorApplication.playmodeStateChanged -= OnPlayModeChanged;
		}

		static void RunOnce() 
		{
			EditorApplication.update -= RunOnce;

			ScriptingSymbolsManager.SetAllVSSymbols ();

			SettingsWindow.showAtStartup = EditorPrefs.GetInt(SettingsWindow.PREFSHOWATSTARTUP, 1) == 1;
			if (SettingsWindow.showAtStartup) {
				//SDEUtility.OpenSettings ();
			}
				
		}
	}
}         