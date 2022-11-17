
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
#if UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif

#if VSGIF
using AppAdvisory.VSGIF;
#endif

#if APPADVISORY_LEADERBOARD
using AppAdvisory.social;
#endif

#if APPADVISORY_ADS
using AppAdvisory.Ads;
#endif

#if VSRATE
using AppAdvisory.VSRATE;
#endif

#if VS_SHARE
using AppAdvisory.SharingSystem;
#endif

namespace AppAdvisory
{
	public static class SDEUtility 
	{
		const string menuPath = "GameObject/";

		//	public static void CreateAsset<T>(string name) where T : ScriptableObject
		//	{
		//		var asset = ScriptableObject.CreateInstance<T>();
		//		ProjectWindowUtil.CreateAsset(asset, name + ".asset");
		//	}
		//
		//	[MenuItem("Assets/Create/ADS_SETTING")]
		//	public static void CreateAdIds()
		//	{
		//		CreateAsset<ADIDS>("ADS_SETTING");
		//	}

		#if APPADVISORY_ADS
		[MenuItem ( menuPath + "APP ADVISORY/Very Simple Ads/CREATE AdsInit",false,20)]
		public static void CreateAdInits()
		{
			GameObject gameObject = new GameObject("AdsInit");
			AdsInit a = gameObject.AddComponent<AdsInit>();
			string[] guids = AssetDatabase.FindAssets("ADS_SETTING");

			#if UNITY_5_3_OR_NEWER
			//a._ADIDS =  AssetDatabase.LoadAssetAtPath<ADIDS>( AssetDatabase.GUIDToAssetPath( guids[0] ));
			a.SetADIDS(AssetDatabase.LoadAssetAtPath<ADIDS>( AssetDatabase.GUIDToAssetPath( guids[0] )));
			#else
			//a._ADIDS =  AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(ADIDS)) as ADIDS;
			a.SetADIDS(AssetDatabase.LoadAssetAtPath( AssetDatabase.GUIDToAssetPath( guids[0] ), typeof(ADIDS))  as ADIDS );
			#endif
			//			Autoselect();

//			if (GUI.changed){
//				EditorUtility.SetDirty(target);
			#if UNITY_5_3_OR_NEWER
			EditorSceneManager.MarkSceneDirty( EditorSceneManager.GetActiveScene());
			#endif
//			}
		}
		#endif


		#if APPADVISORY_LEADERBOARD
		[MenuItem ( menuPath + "APP ADVISORY/Very Simple Leaderboard/CREATE LeaderboardInit",false,20)]
		public static void CreateLeadertboardInits()
		{
			GameObject gameObject = new GameObject("LeaderboardInit");
			LeaderboardInit a = gameObject.AddComponent<LeaderboardInit>(); 
			string[] guids = AssetDatabase.FindAssets("LEADERBOARD_SETTING");

			#if UNITY_5_3_OR_NEWER
			a.leaderboardIds =  AssetDatabase.LoadAssetAtPath<LEADERBOARDIDS>( AssetDatabase.GUIDToAssetPath( guids[0] ));
			a.SetLEADERBORDIDS(AssetDatabase.LoadAssetAtPath<LEADERBOARDIDS>( AssetDatabase.GUIDToAssetPath( guids[0] )));
			#else
			a.leaderboardIds =  AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(LEADERBOARDIDS)) as LEADERBOARDIDS;
			a.SetLEADERBORDIDS(AssetDatabase.LoadAssetAtPath( AssetDatabase.GUIDToAssetPath( guids[0] ), typeof(LEADERBOARDIDS))  as LEADERBOARDIDS );
			#endif
			//			Autoselect();


			#if UNITY_5_3_OR_NEWER
			EditorSceneManager.MarkSceneDirty( EditorSceneManager.GetActiveScene());
			#endif
		}
		#endif

		private static void ThrowSettingsNotFound(string[] guids, string name) {
			if(guids.Length == 0)
				throw new System.Exception("Settings Window can't be opened ! No " + name + " Were Found ! Create one using Asset/Create/AppAdvisory/"+ name);
		}


		[MenuItem("Tools/APP ADVISORY/SETTINGS", false, 0)]
		[MenuItem("Window/APP ADVISORY/SETTINGS", false, 0)]
		public static SettingsWindow OpenSettings()
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode)
				return null;


			SettingsWindow window = SettingsWindow.InitWindow ();

			#region COMMON
			CommonAppSettings commonAppSettings;

			string[] commonAppSettingsGuids = AssetDatabase.FindAssets(CommonAppSettings.NAME + " t:ScriptableObject");

			ThrowSettingsNotFound(commonAppSettingsGuids, CommonAppSettings.NAME);

			#if UNITY_5_3_OR_NEWER
			commonAppSettings = AssetDatabase.LoadAssetAtPath<CommonAppSettings>( AssetDatabase.GUIDToAssetPath( commonAppSettingsGuids[0] ));
			Selection.activeObject = commonAppSettings;
			#else
			commonAppSettings = AssetDatabase.LoadAssetAtPath( AssetDatabase.GUIDToAssetPath( commonAppSettingsGuids[0] ), typeof(CommonAppSettings)) ;
			Selection.activeObject = commonAppSettings;
			#endif
			window.SetCommonAppSettings(commonAppSettings);

			#endregion

			#region ADS

			#if APPADVISORY_ADS

			ADIDS adIDs;
			string[] adIDsGuids = AssetDatabase.FindAssets("ADS_SETTING" + " t:ScriptableObject");

			ThrowSettingsNotFound(adIDsGuids, ADIDS.NAME);

			#if UNITY_5_3_OR_NEWER
			adIDs = AssetDatabase.LoadAssetAtPath<ADIDS>( AssetDatabase.GUIDToAssetPath( adIDsGuids[0] ));
			#else
			adIDs = AssetDatabase.LoadAssetAtPath( AssetDatabase.GUIDToAssetPath( adIDsGuids[0] ), typeof(ADIDS)) ;
			#endif
			window.SetAdIDs(adIDs);
		
			#endif

			#endregion

			#region GIFs

			#if VSGIF

			GIFSettings gifSettings;
			string[] gifsGuids = AssetDatabase.FindAssets(GIFSettings.NAME + " t:ScriptableObject");

			ThrowSettingsNotFound(gifsGuids, GIFSettings.NAME);

			#if UNITY_5_3_OR_NEWER
			gifSettings = AssetDatabase.LoadAssetAtPath<GIFSettings>( AssetDatabase.GUIDToAssetPath( gifsGuids[0] ));
			#else
			gifSettings = AssetDatabase.LoadAssetAtPath( AssetDatabase.GUIDToAssetPath( gifsGuids[0] ), typeof(GIFSettings));
			#endif
			window.SetGIFs(gifSettings);

			#endif

			#endregion

			#region LEADERBOARD

			#if APPADVISORY_LEADERBOARD

			LEADERBOARDIDS leaderboardIDs;

			string[] leaderboardIDsGuids = AssetDatabase.FindAssets(LEADERBOARDIDS.NAME+ " t:ScriptableObject");

			ThrowSettingsNotFound(leaderboardIDsGuids, LEADERBOARDIDS.NAME);

			#if UNITY_5_3_OR_NEWER
			leaderboardIDs = AssetDatabase.LoadAssetAtPath<LEADERBOARDIDS>( AssetDatabase.GUIDToAssetPath( leaderboardIDsGuids[0] ));
			#else
			leaderboardIDs = AssetDatabase.LoadAssetAtPath( AssetDatabase.GUIDToAssetPath( leaderboardIDsGuids[0] ), typeof(LEADERBOARDIDS));
			#endif
			window.SetLeaderboardIDs(leaderboardIDs);
		
			#endif

			#endregion

			#region RATEUS

			#if VSRATE

			RateUsSettings rateUsSettings;

			string[] rateUsSettingsGuids = AssetDatabase.FindAssets(RateUsSettings.NAME + " t:ScriptableObject");

			ThrowSettingsNotFound(rateUsSettingsGuids, RateUsSettings.NAME);

			#if UNITY_5_3_OR_NEWER
			rateUsSettings = AssetDatabase.LoadAssetAtPath<RateUsSettings>( AssetDatabase.GUIDToAssetPath( rateUsSettingsGuids[0] ));
			#else
			rateUsSettings = AssetDatabase.LoadAssetAtPath( AssetDatabase.GUIDToAssetPath( rateUsSettingsGuids[0] ), typeof(RateUsSettings));
			#endif
			window.SetRateUsSettings(rateUsSettings);

			#endif

			#endregion

			#region SHARE

			#if VS_SHARE

			ShareSettings shareSettings;

			string[] shareSettingsGuids = AssetDatabase.FindAssets(ShareSettings.NAME + " t:ScriptableObject");

			ThrowSettingsNotFound(shareSettingsGuids, ShareSettings.NAME);

			#if UNITY_5_3_OR_NEWER
			shareSettings = AssetDatabase.LoadAssetAtPath<ShareSettings>( AssetDatabase.GUIDToAssetPath( shareSettingsGuids[0] ));
			#else
			shareSettings = AssetDatabase.LoadAssetAtPath( AssetDatabase.GUIDToAssetPath( shareSettingsGuids[0] ), typeof(ShareSettings)) ;
			#endif

			window.SetShareSettings(shareSettings);

			#endif

			#endregion
			window.SetModeFromOpenedTab ();
			return window;
		}

	}
}