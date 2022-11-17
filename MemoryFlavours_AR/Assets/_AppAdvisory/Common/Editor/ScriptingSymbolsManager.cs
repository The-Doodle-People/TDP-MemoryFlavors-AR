
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
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AppAdvisory
{
	public class ScriptingSymbolsManager
	{
		private static readonly string UNITY_ADS = "ENABLE_UNITY_ADS";


		private static readonly string CHARTBOOST = "CHARTBOOST";
		private static readonly string ADCOLONY = "ENABLE_ADCOLONY";
		private static readonly string ADMOB = "ENABLE_ADMOB";
		private static readonly string FACEBOOK = "ENABLE_FACEBOOK";
		private static readonly string ANDROID_AMAZON = "ANDROID_AMAZON";
		private static readonly string AADEBUG = "AADEBUG";

		private static readonly string LEADERBOARD_IOS = "VSLEADERBOARD_ENABLE_IOS";
		private static readonly string LEADERBOARD_ANDROID = "VSLEADERBOARD_ENABLE_ANDROID";
		private static readonly string VS_LEADERBOARD = "VSLEADERBOARD";

		private static readonly string AA_DOTWEEN = "AADOTWEEN";

		private static readonly string AA_COMBO_PACK = "VS_COMBO_PACK";
		private static readonly string AA_ADS = "APPADVISORY_ADS";
		private static readonly string AA_GIF = "VSGIF";
		private static readonly string AA_LEADERBOARD = "APPADVISORY_LEADERBOARD";
		private static readonly string AA_RATE = "VSRATE";
		private static readonly string AA_SHARE = "VS_SHARE";


		public static void SetAllVSSymbols() {
			SetAAComboPackSymbol (ExternalPackagesResolver.IsVSComboPackAvailable);
			SetAAAdsSymbol (ExternalPackagesResolver.IsVSAdsAvailable);
			SetAAGifSymbol (ExternalPackagesResolver.IsVSGifsAvailable);
			SetAALeaderboardSymbol (ExternalPackagesResolver.IsVSLeaderboardAvailable);
			SetAARateSymbol (ExternalPackagesResolver.IsVSRateAvailable);
			SetAAShareSymbol (ExternalPackagesResolver.IsVSShareAvailable);
		}

		public static void SetAADotweenSymbol(bool isActivate) {
			SetScriptingSymbolsAll (AA_DOTWEEN, isActivate);
		}

		public static void SetAAAdsSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (AA_ADS, isActivate);
			SetScriptingSymbolsAll (AA_ADS, isActivate);
		}

		public static void SetAAComboPackSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (AA_COMBO_PACK, isActivate);
			SetScriptingSymbolsAll (AA_COMBO_PACK, isActivate);
		}

		public static void SetAALeaderboardSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (AA_LEADERBOARD, isActivate);
			SetScriptingSymbolsAll (AA_LEADERBOARD, isActivate);
		}

		public static void SetAAGifSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (AA_GIF, isActivate);
			SetScriptingSymbolsAll (AA_GIF, isActivate);
		}

		public static void SetAARateSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (AA_RATE, isActivate);
			SetScriptingSymbolsAll (AA_RATE, isActivate);
		}

		public static void SetAAShareSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (AA_SHARE, isActivate);
			SetScriptingSymbolsAll (AA_SHARE, isActivate);
		}

		public static void SetLeaderboardIOSSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (LEADERBOARD_IOS, isActivate);
			SetScriptingSymbolsAll (LEADERBOARD_IOS, isActivate);
		}

		public static void SetLeaderBoardAndroidSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (LEADERBOARD_ANDROID, isActivate);
			SetScriptingSymbolsAll(LEADERBOARD_ANDROID, isActivate);
		}


		public static void SetVSLeaderboardSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (VS_LEADERBOARD, isActivate);
			SetScriptingSymbolsAll(VS_LEADERBOARD, isActivate);
		}

		public static void SetAADebugSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (AADEBUG, isActivate);
			SetScriptingSymbolsAll (AADEBUG, isActivate);
		}

		public static void SetAndroidAmazonSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (ANDROID_AMAZON, isActivate);
			SetScriptingSymbolsAll (ANDROID_AMAZON, isActivate);
		}

		public static void SetUnityAdsSymbol(bool isActivate) {
			SetScriptingSymbolsMobile (UNITY_ADS, isActivate);
		}

		public static void SetChartboostSymbol(bool isActivate)
		{
			//SetScriptingSymbolsMobile (CHARTBOOST, isActivate);
			SetScriptingSymbolsAll (CHARTBOOST, isActivate);
		}

		public static void SetAdColonySymbol(bool isActivate) {
			//SetScriptingSymbolsMobile(ADCOLONY, isActivate);
			SetScriptingSymbolsAll(ADCOLONY, isActivate);
		}

		public static void SetAdmobSymbol(bool isActivate) {
			//SetScriptingSymbolsMobile(ADMOB, isActivate);
			SetScriptingSymbolsAll(ADMOB, isActivate);
		}

		public static void SetFacebookSymbol(bool isActivate) {
			//SetScriptingSymbolsMobile(FACEBOOK, isActivate);
			SetScriptingSymbolsAll(FACEBOOK, isActivate);
		}


		private static bool IsPlatformSupported(BuildTargetGroup target) {
			var moduleManager = System.Type.GetType("UnityEditor.Modules.ModuleManager,UnityEditor.dll");
			var isPlatformSupportLoaded = moduleManager.GetMethod("IsPlatformSupportLoaded", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
			var getTargetStringFromBuildTarget = moduleManager.GetMethod("GetTargetStringFromBuildTargetGroup", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
			return (bool)isPlatformSupportLoaded.Invoke(null,new object[] {(string)getTargetStringFromBuildTarget.Invoke(null, new object[] {target})});

		}


		public static IEnumerable<T> GetValues<T>() {
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		private static void SetScriptingSymbolsMobile(string symbol, bool isActivate)
		{
			SetScriptingSymbol(symbol, BuildTargetGroup.Android, isActivate);

			#if UNITY_5 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3_OR_NEWER
			SetScriptingSymbol(symbol, BuildTargetGroup.iOS, isActivate);
			#else
			SetScriptingSymbol(symbol, BuildTargetGroup.iPhone, isActivate);
			#endif 
		}

		private static void SetScriptingSymbolsAll(string symbol, bool isActivate)
		{

			#if UNITY_5_6_OR_NEWER
			foreach (BuildTargetGroup target in GetValues<BuildTargetGroup>()) 
			{

				if (IsPlatformSupported (target)) 
				{
					SetScriptingSymbol (symbol, target, isActivate);
				}

			}
			#else
			SetScriptingSymbol (symbol, BuildTargetGroup.Android, isActivate);
			SetScriptingSymbol (symbol, BuildTargetGroup.iOS, isActivate); 
			SetScriptingSymbol (symbol, BuildTargetGroup.WSA, isActivate);
			#if !UNITY_5_5_OR_NEWER
			#if !UNITY5_0 && !UNITY_5_1
			SetScriptingSymbol (symbol, BuildTargetGroup.Nintendo3DS, isActivate);
			#endif
			SetScriptingSymbol (symbol, BuildTargetGroup.PS3, isActivate);
			SetScriptingSymbol (symbol, BuildTargetGroup.XBOX360, isActivate);
			#endif
			SetScriptingSymbol (symbol, BuildTargetGroup.PS4, isActivate);
			SetScriptingSymbol (symbol, BuildTargetGroup.PSM, isActivate);
			SetScriptingSymbol (symbol, BuildTargetGroup.PSP2, isActivate);
			SetScriptingSymbol (symbol, BuildTargetGroup.SamsungTV, isActivate); 
			SetScriptingSymbol (symbol,BuildTargetGroup.Standalone, isActivate);
			SetScriptingSymbol (symbol, BuildTargetGroup.Tizen, isActivate);
			#if !UNITY5_0 && !UNITY_5_1
			SetScriptingSymbol (symbol, BuildTargetGroup.tvOS, isActivate);
			SetScriptingSymbol (symbol, BuildTargetGroup.WiiU, isActivate);
			#endif
			SetScriptingSymbol (symbol, BuildTargetGroup.WebGL, isActivate);
			SetScriptingSymbol (symbol, BuildTargetGroup.XboxOne, isActivate);

			#endif
		}

		private static void SetScriptingSymbol(string symbol, BuildTargetGroup target, bool isActivate)
		{
			var s = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);

			if(isActivate && (s.Contains(symbol) || s.Contains(symbol + ";")))
				return;

			s = s.Replace(symbol + ";","");

			s = s.Replace(symbol,"");

			if(isActivate)
				s = symbol + ";" + s;

			PlayerSettings.SetScriptingDefineSymbolsForGroup(target,s);
		}
	}
}