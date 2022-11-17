
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
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using MyThreadPriority = System.Threading.ThreadPriority;
using UnityEditorInternal;

#if APPADVISORY_ADS
using AppAdvisory.Ads;
#endif

#if VSGIF
using AppAdvisory.VSGIF;
#endif

#if APPADVISORY_LEADERBOARD
using AppAdvisory.social;
#endif

#if VSRATE
using AppAdvisory.VSRATE; 
#endif

#if VS_SHARE
using AppAdvisory.SharingSystem;
#endif

using AppAdvisory.Utils;


namespace AppAdvisory
{
	public class SettingsWindow : EditorWindow
	{
		Vector2 scrollPos;
		float margin = 30;

		public static SettingsWindow InitWindow() 
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode)
				return null;

			SettingsWindow window = GetWindow<SettingsWindow>("AA - Settings");
			window.Show ();
			return window;
		}
			
		#region SKIN

		private static GUIStyle _toolBarButton;
		public static GUIStyle ToolBarButton
		{
			get
			{
				if (_toolBarButton == null)
				{
					_toolBarButton = new GUIStyle(EditorStyles.miniButtonRight);
					_toolBarButton.fontSize = 10;
					_toolBarButton.fontStyle = FontStyle.Bold;
					_toolBarButton.fixedHeight = EditorGUIUtility.singleLineHeight * 2f;
					_toolBarButton.stretchWidth = true;
				}
				return _toolBarButton;
			}
		}

		private static float tabMinWidth = 75;
		private static float tabWidthToRemoveText = 90;

		#endregion

		#region COMMON

		public CommonAppSettings commonAppSettings;

		public void SetCommonAppSettings(CommonAppSettings commonAppSettings) {
			this.commonAppSettings = commonAppSettings;
		}

		public bool ANDROID_AMAZON {
			get {
				bool _bool = commonAppSettings.android_Amazon;

				return _bool;
			}

			set {
				bool _bool = commonAppSettings.android_Amazon;

				if (_bool == value)
					return;

				commonAppSettings.android_Amazon = value;

				ScriptingSymbolsManager.SetAndroidAmazonSymbol (value);
			}
		}

		public VSTab OpenedTab {
			get 
			{
				return commonAppSettings.openedTab;
			}

			set {
				VSTab _tab = value;

				if (_tab == commonAppSettings.openedTab)
					return;
				
				commonAppSettings.openedTab = value;

				SetModeFromOpenedTab ();
			}
		}

		public static int TabNumber {
			get {
				int _tabNumber = 1;

				#if APPADVISORY_ADS
				_tabNumber++;
				#endif

				#if VSGIF
				_tabNumber++;
				#endif

				#if APPADVISORY_LEADERBOARD
				_tabNumber++;
				#endif

				#if VSRATE
				_tabNumber++;
				#endif

				#if VS_SHARE
				_tabNumber++;
				#endif

				#if VS_COMBO_PACK
				_tabNumber++;
				#endif

				return _tabNumber;
			}
		}


		#endregion

		#region ADS

		#if APPADVISORY_ADS

		public ReorderableList bannerNetworks;
		public ReorderableList intersitialNetworks;
		public ReorderableList videoNetworks;
		public ReorderableList rewardedVideoNetworks;

		public ADIDS adIDs;

		public void SetAdIDs(ADIDS adIds) 
		{
		this.adIDs = adIds;
		
		this.adIDs.EnableUnityAds = false;
		#if ENABLE_UNITY_ADS
			this.adIDs.EnableUnityAds = true;
		#endif

			this.adIDs.EnableAdmob = false;
		#if ENABLE_ADMOB
		this.adIDs.EnableAdmob = true;
		#endif

			this.adIDs.EnableFacebook = false;
		#if ENABLE_FACEBOOK
		this.adIDs.EnableFacebook = true;
		#endif

			this.adIDs.EnableChartboost = false;
		#if CHARTBOOST
		this.adIDs.EnableChartboost = true;
		#endif

			this.adIDs.EnableAdcolony = false;
		#if ENABLE_ADCOLONY
		this.adIDs.EnableAdcolony = true;
		#endif
			
		}

		public void InitNetworks() {
			if (this.adIDs == null)
				return;


			bannerNetworks = EditorWindowUtils.CreateReorderableEnumList<BannerNetwork> (ref this.adIDs.bannerNetworks, "Banner Networks");
			intersitialNetworks = EditorWindowUtils.CreateReorderableEnumList<InterstitialNetwork> (ref this.adIDs.interstitialNetworks, "Intersitital Networks");
			videoNetworks = EditorWindowUtils.CreateReorderableEnumList<VideoNetwork> (ref this.adIDs.videoNetworks, "Video Networks");
			rewardedVideoNetworks = EditorWindowUtils.CreateReorderableEnumList<RewardedVideoNetwork> (ref this.adIDs.rewardedVideoNetworks, "Rewarded Video Networks");
		}

		public void UpdateNetworks () 
		{
			if(bannerNetworks == null)
				InitNetworks();


			#if ENABLE_ADMOB || ENABLE_FACEBOOK
			bannerNetworks.DoLayoutList();
			#endif

			#if ENABLE_ADMOB || ENABLE_FACEBOOK || CHARTBOOST
			intersitialNetworks.DoLayoutList ();
			#endif


			#if ENABLE_UNITY_ADS || ENABLE_ADCOLONY
			//#if UNITY_ADS || ENABLE_ADCOLONY
			videoNetworks.DoLayoutList ();
			#endif

			#if ENABLE_ADMOB || ENABLE_UNITY_ADS || CHARTBOOST || ENABLE_ADCOLONY
			//#if ENABLE_ADMOB || UNITY_ADS || CHARTBOOST || ENABLE_ADCOLONY
			rewardedVideoNetworks.DoLayoutList ();
			#endif
		}


		public void DisplayAdsSettings(bool isOpened) 
		{
			adIDs.isAdsFoldoutOpened = isOpened;
			if(isOpened)
				OpenedTab = VSTab.AdsSettings;
		}

		public bool DEBUG
		{
		get
		{

		bool _bool = adIDs.DEBUG;

		return _bool;
		}

		set
		{

		bool _bool = adIDs.DEBUG;

		if(_bool == value)
		return;

		adIDs.DEBUG = value;

				ScriptingSymbolsManager.SetAADebugSymbol (value);
		}
		}

		public bool EnableChartboost
		{
		get
		{
		bool _bool = adIDs.EnableChartboost;

		return _bool;
		}

		set
		{

		bool _bool = adIDs.EnableChartboost;

		if(_bool == value)
		return;

		adIDs.EnableChartboost = value;
				ScriptingSymbolsManager.SetChartboostSymbol (value);
		}
		}

		public bool EnableUnityAds 
		{
			get 
			{
				return adIDs.EnableUnityAds;
			}
			set
			{
				bool _bool = adIDs.EnableUnityAds;

				if(_bool == value)
					return;

				adIDs.EnableUnityAds = value;
				ScriptingSymbolsManager.SetUnityAdsSymbol (value);
			}

		}

		public bool EnableAdmob
		{
		get
		{
		bool _bool = adIDs.EnableAdmob;

		return _bool;
		}

		set
		{
		bool _bool = adIDs.EnableAdmob;

		if(_bool == value)
		return;

		adIDs.EnableAdmob = value;

				ScriptingSymbolsManager.SetAdmobSymbol (value);
		}
		}

		public bool EnableAdcolony
		{
		get
		{
		bool _bool = adIDs.EnableAdcolony;

		return _bool;
		}

		set
		{
		bool _bool = adIDs.EnableAdcolony;

		if(_bool == value)
		return;

		adIDs.EnableAdcolony = value;
				ScriptingSymbolsManager.SetAdColonySymbol (value);
		}
		}

		public bool EnableFacebook
		{
		get
		{
		bool _bool = adIDs.EnableFacebook;

		return _bool;
		}

		set
		{
		bool _bool = adIDs.EnableFacebook;

		if(_bool == value)
		return;

		adIDs.EnableFacebook = value;

				ScriptingSymbolsManager.SetFacebookSymbol (value);
		}
		}

		#endif

		#endregion

		#region GIFS

		#if VSGIF

		public GIFSettings gifSettings;

		public void SetGIFs(GIFSettings gifSettings) 
		{
		this.gifSettings = gifSettings;
		}

		public void DisplayGIFSettings(bool isOpened) 
		{
			gifSettings.isGIFFoldoutOpened = isOpened;
			if(isOpened)
				OpenedTab = VSTab.GifSettings;
		}

		#endif

		#endregion

		#region LEADERBOARD

		#if APPADVISORY_LEADERBOARD

		public LEADERBOARDIDS leaderboardIDs;

		public void SetLeaderboardIDs(LEADERBOARDIDS leaderboardIDs) {
		this.leaderboardIDs = leaderboardIDs;
		}

		public void DisplayLeaderboardSettings(bool isOpened) 
		{
			leaderboardIDs.isLeaderboardIDsFoldoutOpened = isOpened;
			if(isOpened)
				OpenedTab = VSTab.LeaderboardSettings;
		}

		public bool Enable_iOS
		{
		get
		{
		bool _bool = leaderboardIDs.ENABLE_IOS;

		return _bool;
		}

		set
		{
		bool _bool = leaderboardIDs.ENABLE_IOS;

		if(_bool == value)
		return;

		leaderboardIDs.ENABLE_IOS = value;
				ScriptingSymbolsManager.SetLeaderboardIOSSymbol (value);
		}
		}

		public bool Enable_Android
		{
		get
		{
		bool _bool = leaderboardIDs.ENABLE_ANDROID;

		return _bool;
		}

		set
		{
		bool _bool = leaderboardIDs.ENABLE_ANDROID;

		if(_bool == value)
		return;

		leaderboardIDs.ENABLE_ANDROID = value;
				ScriptingSymbolsManager.SetLeaderBoardAndroidSymbol (value);

		}
		}

		#endif

		#endregion

		#region RATE

		#if VSRATE
		public RateUsSettings rateUsSettings;

		public void SetRateUsSettings(RateUsSettings rateUsSettings) 
		{
		this.rateUsSettings = rateUsSettings;
		}

		public void DisplayRateSettings(bool isOpened) 
		{
			rateUsSettings.isRateUsFoldoutOpened = isOpened;
			if(isOpened)
				OpenedTab = VSTab.RateSettings;
		}

		#endif

		#endregion

		#region SHARE

		#if VS_SHARE

		public ShareSettings shareSettings;

		public void SetShareSettings(ShareSettings shareSettings)
		{
		this.shareSettings = shareSettings;
		}

		public void DisplayShareSettings(bool isOpened) 
		{
		shareSettings.isShareFoldoutOpened = isOpened;
			if(isOpened)
				OpenedTab = VSTab.ShareSettings;
		}

		#endif

		#endregion


		private const string VS_COMBOPACK_DOC_URL = "https://docs.google.com/document/d/1biE_usbYpKoAg-SMSXC9f5muN7AvuWNe2WzKUj82K0M/edit";
		private const string VS_AD_DOC_URL = "https://docs.google.com/document/d/1jUq6sgmJeZCwMfvaZs6Q1QnlybEr4vlp9xtM70OP_ak/edit";
		private const string VS_LEADERBOARD_DOC_URL = "https://docs.google.com/document/d/1Ll3JBYFN0oBGeraVXkn6yAdxfgzS_Xmjd2TLimZ3gio/edit";
		private const string VS_SHARE_DOC_URL = "https://docs.google.com/document/d/1pqM021pnjp4qG3kiQdlx-KEEB1wx1-P4OiQrWshsfoQ/edit";
		private const string VS_GIF_DOC_URL = "https://docs.google.com/document/d/1jjK4Hn0qcxU3fUzHPS_EOSoXD9pCOS-q--qHAV0Wusg/edit";
		private const string VS_RATE_DOC_URL = "https://docs.google.com/document/d/1RtIIqT8ReXNkWDMZAGz2FPm6dap6ycg4Hu5irGbVd5c/edit";

		private const string NAME_OF_THE_GAME = "Very Simple Leaderboard";


		private const string VERYSIMPLECOMBOPACK_URL = "http://u3d.as/oWD";
		private const string VERYSIMPLEAD_URL = "http://u3d.as/oWD";
		private const string VERYSIMPLELEADERBOARD_URL = "http://u3d.as/qxf";
		private const string VERYSIMPLESHARE_URL = "http://u3d.as/u3N";
		private const string VERYSIMPLEGIF_URL = "http://u3d.as/ACQ";
		private const string VERYSIMPLERATE_URL = "http://u3d.as/Dt2";

		private const string FACEBOOK_URL = "https://facebook.com/appadvisory";
		private const string REQUEST_URL = "https://appadvisory.zendesk.com/hc/en-us/requests/new";
		private const string APPADVISORY_UNITY_CATALOG_URL = "http://bit.ly/2ee6aed";//"http://u3d.as/9cs";
		private const string COMMUNITY_URL = "https://appadvisory.zendesk.com/hc/en-us/sections/201650995-FAQ";
		private const string LINKEDIN_URL = "https://www.linkedin.com/in/ganbarouch";

		public const string PREFSHOWATSTARTUP = "AppAdvisory" + NAME_OF_THE_GAME + ".PREFSHOWATSTARTUP";

		public static bool showAtStartup;
		private static GUIStyle imgHeader;
		private static GUIStyle docHeader;

		private static bool interfaceInitialized;
		private static Texture comboPackIcon;
		private static Texture adsIcon;
		private static Texture leaderboardIcon;
		private static Texture shareIcon;
		private static Texture gifIcon;
		private static Texture onlineDocIcon;
		private static Texture moreGamesIcon;
		private static Texture rateIcon;
		private static Texture communityIcon;
		private static Texture topicIcon;
		private static Texture questionIcon;
		private static Texture facebookIcon;
		private static Texture vsrateIcon;
		private static Texture linkedinIcon;



		#region STATEMACHINE
		private Action displaySettings;

		public void SetModeWelcome() {
			displaySettings = DisplayWelcome;
		}

		public void SetModeAds() {
			displaySettings = DislayAdsSettings;
		}

		public void SetModeGif() {
			displaySettings = DisplayGIFSettings;
		}

		public void SetModeRate() {
			displaySettings = DisplayRateUsSettings;
		}

		public void SetModeShare() {
			displaySettings = DisplayShareSettings;
		}

		public void SetModeLeaderboard() {
			displaySettings = DisplayLeaderboardSettings;
		}

		public  void SetModeComboPack() {
			displaySettings = DisplayComboPack;
		}

		public void SetModeFromOpenedTab() {
			if (commonAppSettings.openedTab == VSTab.Welcome) {
				SetModeWelcome ();
			}
			#if VS_COMBO_PACK
			else if (commonAppSettings.openedTab == VSTab.ComboPack) {
				SetModeComboPack ();
			} 
			#endif
			#if APPADVISORY_ADS
			else if (commonAppSettings.openedTab == VSTab.AdsSettings) {
				SetModeAds ();
			} 
			#endif
			#if VSGIF
			else if (commonAppSettings.openedTab == VSTab.GifSettings) {
				SetModeGif ();
			} 
			#endif
			#if APPADVISORY_LEADERBOARD
			else if (commonAppSettings.openedTab == VSTab.LeaderboardSettings) {
				SetModeLeaderboard ();
			} 
			#endif
			#if VSRATE
			else if (commonAppSettings.openedTab == VSTab.RateSettings) {
				SetModeRate ();
			}
			#endif
			#if VS_SHARE
			else if (commonAppSettings.openedTab == VSTab.ShareSettings) {
				SetModeShare ();
			}
			#endif
		}

		#endregion


		void OnEnable() {
			#if APPADVISORY_ADS
			InitNetworks();
			#endif

		}

		void OnGUI () {
			scrollPos = EditorGUILayout.BeginScrollView (scrollPos);

			DisplayHeader ();

			if(displaySettings != null)
				displaySettings ();

			DisplayCommonSettings ();

			EditorWindowUtils.Space (4);

			EditorGUILayout.EndScrollView ();
		}

		void DisplayHeader() {

			InitInterface();

			if(ToolBarButton != null)
				_toolBarButton.fixedWidth = Mathf.Max(((position.width + 10f) - (TabNumber - 1) * 10) / TabNumber, tabMinWidth);


			EditorGUILayout.BeginHorizontal();


			EditorGUI.BeginChangeCheck();
			if (GUILayout.Toggle(OpenedTab.Equals(VSTab.Welcome), "Welcome", ToolBarButton))
			{ 
				if (EditorGUI.EndChangeCheck())
				{                 
					EditorGUI.FocusTextInControl(null);
				}
				OpenedTab = VSTab.Welcome;   
			}       

			#if VS_COMBO_PACK
			EditorGUI.BeginChangeCheck();
			if (GUILayout.Toggle(OpenedTab.Equals(VSTab.ComboPack), ToolBarButton.fixedWidth <= tabWidthToRemoveText ? new GUIContent(null, comboPackIcon) : new GUIContent("Combo Pack", comboPackIcon), ToolBarButton))
			{ 
				if (EditorGUI.EndChangeCheck())
				{                 
					EditorGUI.FocusTextInControl(null);
				}
				OpenedTab = VSTab.ComboPack;   
			}       
			#endif

			#if APPADVISORY_ADS
			EditorGUI.BeginChangeCheck();
			if (GUILayout.Toggle(OpenedTab.Equals(VSTab.AdsSettings), ToolBarButton.fixedWidth <= tabWidthToRemoveText ? new GUIContent(null, adsIcon) : new GUIContent("Ads", adsIcon), ToolBarButton))
			{ 
				if (EditorGUI.EndChangeCheck())
				{                 
					EditorGUI.FocusTextInControl(null);
				}
				OpenedTab = VSTab.AdsSettings;   
			} 
			#endif

			#if APPADVISORY_LEADERBOARD
			EditorGUI.BeginChangeCheck();
			if (GUILayout.Toggle(OpenedTab.Equals(VSTab.LeaderboardSettings), ToolBarButton.fixedWidth <= tabWidthToRemoveText ? new GUIContent(null, leaderboardIcon) : new GUIContent("Leaderboard", leaderboardIcon), ToolBarButton))
			{ 
				if (EditorGUI.EndChangeCheck())
				{                 
					EditorGUI.FocusTextInControl(null);
				}
				OpenedTab = VSTab.LeaderboardSettings;   
			}  
			#endif

			#if VS_SHARE
			EditorGUI.BeginChangeCheck();
			if (GUILayout.Toggle(OpenedTab.Equals(VSTab.ShareSettings), ToolBarButton.fixedWidth <= tabWidthToRemoveText ? new GUIContent(null, shareIcon) : new GUIContent("Share", shareIcon), ToolBarButton))
			{ 
				if (EditorGUI.EndChangeCheck())
				{                 
					EditorGUI.FocusTextInControl(null);
				}
				OpenedTab = VSTab.ShareSettings;   
			}    
			#endif

			#if VSRATE
			EditorGUI.BeginChangeCheck();
			if (GUILayout.Toggle(OpenedTab.Equals(VSTab.RateSettings), ToolBarButton.fixedWidth <= tabWidthToRemoveText ? new GUIContent(null, vsrateIcon) : new GUIContent("Rate", vsrateIcon), ToolBarButton))
			{ 
				if (EditorGUI.EndChangeCheck())
				{                 
					EditorGUI.FocusTextInControl(null);
				}
				OpenedTab = VSTab.RateSettings;   
			}      
			#endif

			#if VSGIF
			EditorGUI.BeginChangeCheck();
			if (GUILayout.Toggle(OpenedTab.Equals(VSTab.GifSettings), ToolBarButton.fixedWidth <= tabWidthToRemoveText ? new GUIContent(null, gifIcon) : new GUIContent("Gif", gifIcon), ToolBarButton))
			{ 
				if (EditorGUI.EndChangeCheck())
				{                 
					EditorGUI.FocusTextInControl(null);
				}
				OpenedTab = VSTab.GifSettings;   
			}      
			#endif

			EditorGUILayout.EndHorizontal();

		}

		void DisplayWelcome() {
			GUILayout.Space(30);

			EditorWindowUtils.CreateFoldout (ref commonAppSettings.isWelcomeFoldoutOpened, "WELCOME");

			if (commonAppSettings.isWelcomeFoldoutOpened) 
			{

				InitInterface ();
				//GUI.Box(new Rect(0, 60, width, 60), "", imgHeader);
				//GUILayoutUtility.GetRect(position.width, 50);
				//GUILayout.Space(30);

				//GUILayoutUtility.GetRect(position.width, 50);





//				GUILayout.BeginHorizontal();
//
//				if (Button2(adsIcon)){
//					Application.OpenURL(VERYSIMPLEAD_URL);
//				}
//
//				if (Button2(leaderboardIcon)){
//					Application.OpenURL(VERYSIMPLELEADERBOARD_URL);
//				}
//
//				if (Button2(shareIcon)){
//					Application.OpenURL(VERYSIMPLESHARE_URL);
//				}
//
//				if (Button2(gifIcon)){
//					Application.OpenURL(VERYSIMPLEGIF_URL);
//				}
//
//				if (Button2(vsrateIcon)){
//					Application.OpenURL(VERYSIMPLERATE_URL);
//				}
//
//				GUILayout.EndHorizontal();




				GUILayout.BeginVertical();
				if (Button(communityIcon,"Join the community and get access to direct download","Be informed of the latest updates.")){
					Application.OpenURL(COMMUNITY_URL);
				}

				if (Button(moreGamesIcon,"More Unity assets from us","Have a look to our Unity's Asset Store catalog!")){
					Application.OpenURL(APPADVISORY_UNITY_CATALOG_URL);
				}

				if (Button(facebookIcon,"Facebook page","Follow us on Facebook.")){
					Application.OpenURL(FACEBOOK_URL);
				}

				if (Button(questionIcon,"A request?","Don't hesitate to contact us.")){
					Application.OpenURL(REQUEST_URL);
				}

				if (Button(linkedinIcon,"My Linkedin","For professional purpose only")){
					Application.OpenURL(LINKEDIN_URL);
				}

				GUILayout.Space(3);

				//				bool show = GUILayout.Toggle(showAtStartup, "Show at startup");
				//
				//				if (show != showAtStartup){
				//					showAtStartup = show;
				//					int i = GetInt(showAtStartup);
				//					Debug.Log("toggle i = " + i);
				//					EditorPrefs.SetInt(PREFSHOWATSTARTUP, i);
				//				}


				bool show = GUILayout.Toggle(showAtStartup, "Show at startup");

				if (show != showAtStartup) {
					showAtStartup = show;
					int i = GetInt (showAtStartup);
					Debug.Log ("toggle i = " + i);
					EditorPrefs.SetInt (PREFSHOWATSTARTUP, i);
				}
				//EditorWindowUtils.CreateToggleField (ref commonAppSettings.showAtStartup, "Show at startup");

				GUILayout.EndVertical();
			}

		}


		int GetInt(bool b)
		{
			if(b)
				return 1;
			else
				return 0;
		}

		void InitInterface(){

			if (!interfaceInitialized) {
				imgHeader = new GUIStyle();
				imgHeader.normal.background = (Texture2D)Resources.Load("appadvisoryBanner");
				imgHeader.normal.textColor = Color.white;

				docHeader = new GUIStyle ();
				docHeader.normal.textColor = Color.white;
				docHeader.fontSize = 16;

				adsIcon = (Texture)Resources.Load("btn_monetization") as Texture;

				leaderboardIcon = (Texture)Resources.Load ("btn_leaderboard") as Texture;
				shareIcon = (Texture)Resources.Load("btn_share") as Texture;
				gifIcon = (Texture)Resources.Load("btn_gif") as Texture;
				vsrateIcon = (Texture)Resources.Load("btn_vsrate") as Texture;
				rateIcon = Resources.Load<Texture> ("btn_rate");
				comboPackIcon = Resources.Load<Texture> ("btn_vscombopack");

				onlineDocIcon = (Texture)Resources.Load("btn_onlinedoc") as Texture;
				communityIcon = (Texture)Resources.Load("btn_community") as Texture;
				moreGamesIcon = (Texture)Resources.Load("btn_moregames") as Texture;
				rateIcon = (Texture)Resources.Load("btn_rate") as Texture;
				questionIcon = (Texture)Resources.Load("btn_question") as Texture;
				facebookIcon = (Texture)Resources.Load("btn_facebook") as Texture;
				linkedinIcon = (Texture)Resources.Load("btn_linkedin") as Texture;

				interfaceInitialized = true;
			}
		}

		bool Button(Texture texture, string heading, string body, int space=0){

			GUILayout.BeginHorizontal();

			GUILayout.Space(54);
			GUILayout.Box(texture, GUIStyle.none, GUILayout.MaxWidth(45));
			GUILayout.Space(10);

			GUILayout.BeginVertical();
			GUILayout.Space(1);
			GUILayout.Label(heading, EditorStyles.boldLabel);
			GUILayout.Label(body);
			GUILayout.EndVertical();

			GUILayout.EndHorizontal();

			var rect = GUILayoutUtility.GetLastRect();
			EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

			bool returnValue = false;
			if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition)){
				returnValue = true;
			}

			GUILayout.Space(space);

			return returnValue;
		}

		bool Button3(Texture texture, string heading){

			GUILayout.BeginHorizontal();

			GUILayout.Space(54);
			GUILayout.Box(texture, GUIStyle.none, GUILayout.MaxWidth(45));
			GUILayout.Space(10);

			GUILayout.BeginVertical();
			GUILayout.Space(12);
			GUILayout.Label(heading, EditorStyles.boldLabel);
			GUILayout.EndVertical();

			GUILayout.EndHorizontal();

			var rect = GUILayoutUtility.GetLastRect();
			EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

			bool returnValue = false;
			if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition)){
				returnValue = true;
			}

			return returnValue;
		}

		bool Button2(Texture texture){

			GUILayout.BeginHorizontal();

			float size = 100f; //UnityEngine.Random.Range(60f,100f);

			GUILayout.Space(5);
			GUILayout.Box(texture, GUIStyle.none, GUILayout.MaxWidth(size));
			GUILayout.Space(10);


			GUILayout.EndHorizontal();

			var rect = GUILayoutUtility.GetLastRect();
			EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

			bool returnValue = false;
			if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition)){
				returnValue = true;
			}


			return returnValue;
		}

		#region COMBO PACK
		void DisplayComboPack () {
			DisplayDocAndRate ();
		}

		void DisplayDocAndRate() {
			EditorWindowUtils.Space (2);
			GUILayout.BeginHorizontal();
			

			if (Button3(rateIcon,"Rate this asset")){
				Application.OpenURL(CurrentTabUASURL);
			}
			if (Button3(onlineDocIcon,"Online documentation")){
				Application.OpenURL(CurrentTabDocURL);
			}

			GUILayout.EndHorizontal();
		}

		string CurrentTabDocURL {
			get
			{
				#if VS_COMBO_PACK
				if (commonAppSettings.openedTab == VSTab.ComboPack) {
					return VS_COMBOPACK_DOC_URL;
				} 
				#endif
				#if APPADVISORY_ADS
				if (commonAppSettings.openedTab == VSTab.AdsSettings) {
					return VS_AD_DOC_URL;
				} 
				#endif
				#if VSGIF
				if (commonAppSettings.openedTab == VSTab.GifSettings) {
					return VS_GIF_DOC_URL;
				} 
				#endif
				#if APPADVISORY_LEADERBOARD
				if (commonAppSettings.openedTab == VSTab.LeaderboardSettings) {
					return VS_LEADERBOARD_DOC_URL;
				} 
				#endif
				#if VSRATE
				if (commonAppSettings.openedTab == VSTab.RateSettings) {
					return VS_RATE_DOC_URL;
				}
				#endif
				#if VS_SHARE
				if (commonAppSettings.openedTab == VSTab.ShareSettings) {
					return VS_SHARE_DOC_URL;
				}
				#endif
				return "";
			}
		}


		string CurrentTabUASURL {
			get
			{
				#if VS_COMBO_PACK
				if (commonAppSettings.openedTab == VSTab.ComboPack) {
					return VERYSIMPLECOMBOPACK_URL;
				} 
				#endif
				#if APPADVISORY_ADS
				if (commonAppSettings.openedTab == VSTab.AdsSettings) {
					return VERYSIMPLEAD_URL;
				} 
				#endif
				#if VSGIF
				if (commonAppSettings.openedTab == VSTab.GifSettings) {
					return VERYSIMPLEGIF_URL;
				} 
				#endif
				#if APPADVISORY_LEADERBOARD
				if (commonAppSettings.openedTab == VSTab.LeaderboardSettings) {
					return VERYSIMPLELEADERBOARD_URL;
				} 
				#endif
				#if VSRATE
				if (commonAppSettings.openedTab == VSTab.RateSettings) {
					return VERYSIMPLERATE_URL;
				}
				#endif
				#if VS_SHARE
				if (commonAppSettings.openedTab == VSTab.ShareSettings) {
					return VERYSIMPLESHARE_URL;
				}
				#endif
				return "";
			}
		}

			
		#endregion

		#region COMMON SETTINGS

		void DisplayCommonSettings() 
		{
			EditorWindowUtils.Space (2);
			commonAppSettings.isCommonFoldoutOpened = EditorGUILayout.Foldout(commonAppSettings.isCommonFoldoutOpened, "COMMON SETTINGS");

			if(commonAppSettings.isCommonFoldoutOpened) 
			{
				EditorWindowUtils.Space (2);

				EditorGUILayout.BeginHorizontal();
				GUILayout.Space(margin);
				ANDROID_AMAZON = EditorGUILayout.BeginToggleGroup (EditorWindowUtils.CreateGUIContentFromDescriptionAndTooltip("ANDROID_AMAZON", "check it if you want to target the Amazon app shop"), ANDROID_AMAZON);
				EditorGUILayout.EndToggleGroup ();
				EditorGUILayout.EndVertical();

				#if VSGIF || VSRATE || VS_SHARE
				EditorWindowUtils.Space (2);

				EditorWindowUtils.CreateTextField (ref commonAppSettings.iOS_ID, "iOS ID", "The iOS ID of your app (ex : 925288648)", margin); 

				EditorWindowUtils.CreateTextField (ref commonAppSettings.amazon_ID, "Amazon ID", "The Amazon ID of your app (ex B01MD1OAMS)", margin); 

				EditorWindowUtils.CreateTextField (ref commonAppSettings.android_Bundle_ID, "Android Bundle ID", "The Android bundle ID of your app (ex : com.appadvisory.pixeltank)", margin); 

				EditorWindowUtils.CreateTextField (ref commonAppSettings.shareTextBeforeUrl, "Share Text before URL", "The Text you want to display before your URL", margin); 

				EditorWindowUtils.CreateTextField (ref commonAppSettings.shareTextAfterUrl, "Share Text after URL", "The Text you want to display after your URL", margin); 
				#endif
			}

			if (GUI.changed)
			{
				EditorUtility.SetDirty(commonAppSettings); 

			}
		}

		#endregion

		#region ADS SETTINGS

		void DislayAdsSettings()
		{
			#if APPADVISORY_ADS

			#if !UNITY_IOS && !UNITY_ANDROID
			EditorGUILayout.LabelField("Please remember that no ads can be displayed on the editor");
			#endif





			DisplayDocAndRate ();
			EditorWindowUtils.Space (2);

			adIDs.isAdsFoldoutOpened = EditorGUILayout.Foldout(adIDs.isAdsFoldoutOpened, "ADS SETTINGS");

			if(adIDs.isAdsFoldoutOpened) 
			{
			EditorWindowUtils.Space (2);

			#region SDK BUTTONS

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space (margin);
			if(GUILayout.Button("GET\nADMOB\nSDK",  GUILayout.Width(100), GUILayout.Height(50)))
			{
			Application.OpenURL("https://github.com/googleads/googleads-mobile-unity");
			}

			if(GUILayout.Button("GET\nCHARTBOOST\nSDK",  GUILayout.Width(100), GUILayout.Height(50)))
			{
			Application.OpenURL("https://answers.chartboost.com/hc/en-us/articles/201219745-Unity-SDK-Download");
			}

			if(GUILayout.Button("GET\nADCOLONY\nSDK",  GUILayout.Width(100), GUILayout.Height(50)))
			{
			Application.OpenURL("https://github.com/AdColony");
			}
			if(GUILayout.Button("GET\nFACEBOOK\nSDK",  GUILayout.Width(100), GUILayout.Height(50)))
			{
			Application.OpenURL("https://developers.facebook.com/docs/unity");
			}

			EditorGUILayout.EndHorizontal();			

			#endregion

			#region ADS OPTIONS TOGGLES

			EditorWindowUtils.Space (2);

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space (margin);
			DEBUG = EditorGUILayout.BeginToggleGroup(EditorWindowUtils.CreateGUIContentFromDescriptionAndTooltip("DEBUG", "Check it iif you want to debug rewarded ads"), DEBUG);
			EditorGUILayout.EndToggleGroup();
			EditorGUILayout.EndHorizontal();

			EditorWindowUtils.Space (1);
			
			EditorWindowUtils.CreateToggleField (ref adIDs.LoadNextSceneWhenAdLoaded,"Load Next Scene When Ad(s) Ready ", "Check it if you want to use a loading scene and launch the game scene when ads are ready", margin);

			EditorWindowUtils.CreateToggleField (ref adIDs.RandomizeNetworks, "Randomize Networks", "Check it if you want to randomize the order of the networks", margin);

			EditorWindowUtils.CreateToggleField(ref adIDs.showInterstitialFirstRun, "Show interstitial at first run", "Check it if you want to display an Interstitial at the first run", margin);

			EditorWindowUtils.CreateToggleField(ref adIDs.showBannerAtRun, "Show banner at run", "Check it if you want to show banner at run", margin);

			EditorWindowUtils.CreateToggleField(ref adIDs.ShowIntertitialAtStart, "Show interstitial at start ", "Check it if you want to display interstitals ads at launch", margin);

			EditorWindowUtils.Space (1);

			#endregion

			UpdateNetworks();

			#region MEDIATION NETWORK TOGGLES
			

			
			EditorWindowUtils.CreateFoldout(ref adIDs.isMediationNetworkFoldoutOpened, "MEDIATION NETWORKS", margin);

			if(adIDs.isMediationNetworkFoldoutOpened) 
			{

					EditorGUILayout.BeginHorizontal();
					GUILayout.Space (margin * 2);
					EnableUnityAds = EditorGUILayout.BeginToggleGroup(EditorWindowUtils.CreateGUIContentFromDescriptionAndTooltip("Enable UnityAds", "Check it iif you want to use Unity ads"), EnableUnityAds);
					EditorGUILayout.EndToggleGroup();
					EditorGUILayout.EndHorizontal();

					EditorWindowUtils.Space (1);


			EditorGUILayout.BeginHorizontal();
			GUILayout.Space (margin * 2);
			EnableChartboost = EditorGUILayout.BeginToggleGroup(new GUIContent("Enable Chartboost    [?]", "Check it to use Chartboost. Download the Chartboost SDK here: https://answers.chartboost.com/hc/en-us/"), EnableChartboost);
			EditorGUILayout.EndToggleGroup();
			EditorGUILayout.EndHorizontal();
			EditorWindowUtils.Space (1);

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space (margin * 2);
			EnableAdcolony = EditorGUILayout.BeginToggleGroup(new GUIContent("Enable Adcolony    [?]", "Check it to use ADColony. Download the Adcolony SDK here: https://github.com/AdColony"), EnableAdcolony);
			EditorGUILayout.EndToggleGroup();
			EditorGUILayout.EndHorizontal();
			EditorWindowUtils.Space (1);

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space (margin * 2);
			EnableAdmob = EditorGUILayout.BeginToggleGroup(new GUIContent("Enable Admob    [?]", "Check it to use Admob (iAD will be disabled)"), EnableAdmob);
			EditorGUILayout.EndToggleGroup();
			EditorGUILayout.EndHorizontal();
			EditorWindowUtils.Space (1);

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space (margin * 2);
			EnableFacebook = EditorGUILayout.BeginToggleGroup(new GUIContent("Enable Facebook    [?]", "Check it to use Facebook (iAD will be disabled)"), EnableFacebook);
			EditorGUILayout.EndToggleGroup();
			EditorGUILayout.EndHorizontal();
			EditorWindowUtils.Space (1);

			}

			EditorWindowUtils.Space (1);
			
			#endregion

			#region ADS OPTION ENUM

			EditorWindowUtils.CreateEnumPopup<InterstitialNetwork>(ref adIDs.interstitialNetworkToShowAtRun, "Interstitial to show at run", 300, margin);


			//EditorWindowUtils.CreateEnumPopup<BannerNetwork>(ref adIDs.bannerNetwork, "Banner network", 300, margin);

			#if ENABLE_FACEBOOK || ENABLE_ADMOB

			EditorWindowUtils.CreateEnumPopup<ChildrenPrivacy>(ref adIDs.childrenPrivacy, "Children's Privacy", 300, margin);

			#endif

			EditorWindowUtils.Space (2);

			#endregion


			#if ENABLE_FACEBOOK 
			//if(adIDs.bannerNetwork == BannerNetwork.Facebook)
			//{
			
			if(bannerNetworks.list.Contains(BannerNetwork.Facebook))
			{
			EditorWindowUtils.CreateEnumPopup<AudienceNetwork.AdSize>(ref adIDs.FacebookBannerSize, "Facebook Banner Size", 300, margin);
			EditorWindowUtils.CreateEnumPopup<FacebookBannerPosition>(ref adIDs.FacebookBannerPosition, "Facebook Banner Position", 300, margin);
			}

			#endif

			#if ENABLE_ADMOB
			EditorWindowUtils.CreateToggleField (ref adIDs.lookForGameAds, "Look for Game Ads", "Check it if you want to add Game as one of your ads keyword ", margin);
			EditorWindowUtils.CreateTextField(ref adIDs.testDeviveId, "Test Device ID", "Build you project on a device and check the log to retrieve it", margin);

			//if(adIDs.bannerNetwork == BannerNetwork.Admob)
			//{
			if(bannerNetworks.list.Contains(BannerNetwork.Admob)) 
			{
			EditorWindowUtils.CreateEnumPopup<GoogleMobileAds.Api.AdPosition>(ref adIDs.AdmobBannerPosition, "Admob Banner Position", 300, margin);
			}
			#endif


			/*
			#region BANNER NETWORKS
			#if ENABLE_ADMOB || ENABLE_FACEBOOK
			
			EditorWindowUtils.Space (2);

			EditorWindowUtils.CreateFoldout(ref adIDs.IsBannerNetworksOpened, "BANNER NETWORKS", margin);
			#endif

			if (adIDs.IsBannerNetworksOpened) 
			{

			#if ENABLE_ADMOB
			EditorWindowUtils.CreateToggleField(ref adIDs.useAdmobAsBannerNetwork, "Admob", "Check it if you want to use Admob as one of your BANNER network", margin * 2);
			#endif


			#if ENABLE_FACEBOOK
			EditorWindowUtils.CreateToggleField(ref adIDs.useFacebookAsBannerNetwork, "Facebook", "Check it if you want to use Facebook as one of your BANNER network", margin * 2);
			#endif
			}

			#endregion

				#region INTERSTITIAL NETWORKS 
			
			#if ENABLE_ADMOB || CHARTBOOST || ENABLE_FACEBOOK 

			EditorWindowUtils.Space (2);

			EditorWindowUtils.CreateFoldout(ref adIDs.IsInterstitialNetworksOpened, "INTERSTITIAL NETWORKS", margin);

			if (adIDs.IsInterstitialNetworksOpened) 
			{
			#if ENABLE_ADMOB
			EditorWindowUtils.CreateToggleField(ref adIDs.useAdmobAsInterstitialNetwork, "Admob", "Check it if you want to use Admob as one of your INTERSTITIAL network", margin * 2);
			#endif

			#if CHARTBOOST
			EditorWindowUtils.CreateToggleField (ref adIDs.useChartboostAsInterstitialNetwork, "Chartboost", "Check it if you want to use Chartboost as one of your INTERSTITIAL network", margin * 2);
			#endif

			#if ENABLE_FACEBOOK
			EditorWindowUtils.CreateToggleField (ref adIDs.useFacebookAsInterstitialNetwork, "Facebook", "Check it if you want to use Facebook as one of your INTERSTITIAL network", 2 * margin);
			#endif
			}

			#endif

				#endregion

			#region VIDEO NETWORKS

			#if ENABLE_UNITY_ADS || ENABLE_ADCOLONY 
			//#if UNITY_ADS || ENABLE_ADCOLONY 

			EditorWindowUtils.Space (2);

			EditorWindowUtils.CreateFoldout(ref adIDs.IsVideoNetworksOpened, "VIDEO NETWORKS", margin);

			if (adIDs.IsVideoNetworksOpened) 
			{
			#if ENABLE_UNITY_ADS
			//#if UNITY_ADS
			EditorWindowUtils.CreateToggleField (ref adIDs.useUnityAdsAsBannerNetwork, "Unity Ads", "Check it if you want to use Unity Ads as one of your VIDEO network", margin * 2);
			#endif

			#if ENABLE_ADCOLONY
			EditorWindowUtils.CreateToggleField(ref adIDs.useAdColonyAsBannerNetwork, "AdColony", "Check it if you want to use AdColony as one of your VIDEO network", margin * 2);
			#endif
			}

			#endif

				#endregion

			#region REWARDED VIDEO NETWORK

			#if ENABLE_ADMOB || ENABLE_UNITY_ADS || CHARTBOOST || ENABLE_ADCOLONY
			//#if ENABLE_ADMOB || UNITY_ADS || CHARTBOOST || ENABLE_ADCOLONY

			EditorWindowUtils.Space (2);

			EditorWindowUtils.CreateFoldout(ref adIDs.IsRewardedVideoNetworksOpened, "REWARDED VIDEO NETWORKS", margin);

			if (adIDs.IsRewardedVideoNetworksOpened) 
			{
			#if ENABLE_ADMOB
			EditorWindowUtils.CreateToggleField(ref adIDs.useAdmobAsRewardedVideoNetwork, "Admob", "Check it if you want to use Admob as one of your REWARDED VIDEO network", margin * 2);
			#endif

			#if ENABLE_UNITY_ADS
			//#if UNITY_ADS
			EditorWindowUtils.CreateToggleField(ref adIDs.useUnityAdsAsRewardedVideoNetwork, "Unity Ads ", "Check it if you want to use Unity Ads as one of your REWARDED VIDEO network", margin * 2);
			#endif

			#if CHARTBOOST
			EditorWindowUtils.CreateToggleField (ref adIDs.useChartboostAsRewardedVideoNetwork, "Chartboost", "Check it if you want to use Chartboost as one of your REWARDED VIDEO network", margin * 2);
			#endif

			#if ENABLE_ADCOLONY
			EditorWindowUtils.CreateToggleField(ref adIDs.useAdColonyAsRewardedVideoNetwork, "AdColony", "Check it if you want to use AdColony as one of your REWARDED VIDEO network", margin * 2);
			#endif
			}

			EditorWindowUtils.Space (2);

			#endif

			#endregion
			
			*/
			#if ENABLE_ADMOB

			EditorWindowUtils.CreateFoldout(ref adIDs.IsAdmobSettingsOpened, "ADMOB", margin);

			if(adIDs.IsAdmobSettingsOpened)
			{
			EditorWindowUtils.CreateFoldout(ref adIDs.IsAdmobIOSSettingsOpened, "iOS IDs", margin * 2);
			if(adIDs.IsAdmobIOSSettingsOpened)
			{
			EditorWindowUtils.CreateTextField(ref adIDs.AdmobBannerIdIOS, "Banner ID", "Please enter your Admob BANNER Id for iOS", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdmobInterstitialIdIOS, "Interstitial ID", "Please enter your Admob INTERSTITIAL Id for iOS", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdmobRewardedVideoIdIOS, "Rewarded Video ID", "Please enter your Admob REWARDED VIDEO Id for iOS", margin * 3);
			}

			EditorWindowUtils.CreateFoldout(ref adIDs.IsAdmobANDROIDSettingsOpened, "ANDROID IDs", margin * 2);
			if(adIDs.IsAdmobANDROIDSettingsOpened)
			{
			EditorWindowUtils.CreateTextField(ref adIDs.AdmobBannerIdANDROID, "Banner ID", "Please enter your Admob BANNER Id for Android", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdmobInterstitialIdANDROID, "Interstitial ID", "Please enter your Admob INTERSTITIAL Id for Android", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdmobRewardedVideoIdANDROID, "Rewarded Video ID", "Please enter your Admob REWARDED VIDEO Id for Android", margin * 3);
			}

			EditorWindowUtils.CreateFoldout(ref adIDs.IsAdmobAMAZONSettingsOpened, "AMAZON IDs", margin * 2);
			if(adIDs.IsAdmobAMAZONSettingsOpened)
			{
			EditorWindowUtils.CreateTextField(ref adIDs.AdmobBannerIdAMAZON, "Banner ID", "Please enter your Admob BANNER Id for Amazon  - Could be the same as Android", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdmobInterstitialIdAMAZON, "Interstitial ID", "Please enter your Admob INTERSTITIAL Id for Amazon  - Could be the same as Android", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdmobRewardedVideoIdAMAZON, "Rewarded Video ID", "Please enter your Admob REWARDED VIDEO Id for Amazon  - Could be the same as Android", margin * 3);
			}
			}

			EditorWindowUtils.Space(2);
			#endif

			#if ENABLE_FACEBOOK

			EditorWindowUtils.CreateFoldout(ref adIDs.IsFacebookSettingsOpened, "FACEBOOK", margin);

			if(adIDs.IsFacebookSettingsOpened) {

			EditorWindowUtils.CreateFoldout(ref adIDs.IsFacebookIOSSettingsOpened, "iOS Placement IDs", margin * 2);
			if(adIDs.IsFacebookIOSSettingsOpened)
			{
			EditorWindowUtils.CreateTextField(ref adIDs.FacebookBannerIdIOS, "Banner ID", "Please enter your Facebook BANNER Placement Id for iOS", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.FacebookInterstitialIdIOS, "Interstitial ID", "Please enter your Facebbok INTERSTITIAL Placement Id for iOS", margin * 3);
			}

			EditorWindowUtils.CreateFoldout(ref adIDs.IsFacebookANDROIDSettingsOpened, "ANDROID Placement IDs", margin * 2);
			if(adIDs.IsFacebookANDROIDSettingsOpened)
			{
			EditorWindowUtils.CreateTextField(ref adIDs.FacebookBannerIdANDROID, "Banner ID", "Please enter your Facebook BANNER Placement Id for Android", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.FacebookInterstitialIdANDROID, "Interstitial ID", "Please enter your Facebbok INTERSTITIAL Placement Id for Android", margin * 3);
			}

			EditorWindowUtils.CreateFoldout(ref adIDs.IsFacebookAMAZONSettingsOpened, "AMAZON Placement IDs", margin * 2);
			if(adIDs.IsFacebookAMAZONSettingsOpened)
			{
			EditorWindowUtils.CreateTextField(ref adIDs.FacebookBannerIdAMAZON, "Banner ID", "Please enter your Facebook BANNER Placement Id for Amazon", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.FacebookInterstitialIdAMAZON, "Interstitial ID", "Please enter your Facebbok INTERSTITIAL Placement Id for Amazon", margin * 3);
			}
			}
			EditorWindowUtils.Space(2);

			#endif

			#if ENABLE_UNITY_ADS
			//#if UNITY_ADS

			EditorWindowUtils.CreateFoldout(ref adIDs.IsUnityAdsSettingsOpened, "UNITY ADS", margin);

			if(adIDs.IsUnityAdsSettingsOpened)
			{
			EditorWindowUtils.CreateTextField(ref adIDs.rewardedVideoZoneUnityAds, "Rewarded video zone unity ads", "If you don't know what it is, have a look to the Unity Ads documentation: https://unityads.unity3d.com", margin * 3);
			}
			EditorWindowUtils.Space(2);

			#endif

			#if CHARTBOOST

			EditorWindowUtils.CreateFoldout(ref adIDs.IsChartboostSettingsOpened, "CHARTBOOST", margin);

			if(adIDs.IsChartboostSettingsOpened)
			{
			EditorWindowUtils.CreateFoldout(ref adIDs.IsChartboostIOSSettingsOpened, "iOS IDs", margin * 2);
			if(adIDs.IsChartboostIOSSettingsOpened) 
			{
			EditorWindowUtils.CreateTextField(ref adIDs.ChartboostAppIdIOS, "App ID", "Please enter your Chartboost App Id for iOS - Find it on Chartboost.com", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.ChartboostAppSignatureIOS, "App Signature ID", "Please enter your Chartboost App Signature Id for iOS - Find it on Chartboost.com", margin * 3);
			}

			EditorWindowUtils.CreateFoldout(ref adIDs.IsChartboostAndroidSettingsOpened, "Android IDs", margin * 2);
			if(adIDs.IsChartboostAndroidSettingsOpened) 
			{
			EditorWindowUtils.CreateTextField(ref adIDs.ChartboostAppIdAndroid, "App ID", "Please enter your Chartboost App Id for Android - Find it on Chartboost.com", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.ChartboostAppSignatureAndroid, "App Signature ID", "Please enter your Chartboost App Signature Id for Android - Find it on Chartboost.com", margin * 3);
			}

			EditorWindowUtils.CreateFoldout(ref adIDs.IsChartboostAmazonSettingsOpened, "Amazon IDs", margin * 2);
			if(adIDs.IsChartboostAmazonSettingsOpened) 
			{
			EditorWindowUtils.CreateTextField(ref adIDs.ChartboostAppIdAmazon, "App ID", "Please enter your Chartboost App Id for Amazon - Find it on Chartboost.com", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.ChartboostAppSignatureAmazon, "App Signature ID", "Please enter your Chartboost App Signature Id for Amazon - Find it on Chartboost.com", margin * 3);
			}
			}

			EditorWindowUtils.Space(2);
			#endif


			#if ENABLE_ADCOLONY
			EditorWindowUtils.CreateFoldout(ref adIDs.IsADColonySettingsOpened, "ADCOLONY", margin);

			if(adIDs.IsADColonySettingsOpened)
			{
			EditorWindowUtils.CreateFoldout(ref adIDs.IsADColonyIOSSettingsOpened, "iOS IDs", margin * 2);
			if(adIDs.IsADColonyIOSSettingsOpened) 
			{
			EditorWindowUtils.CreateTextField(ref adIDs.AdColonyAppID_iOS, "App ID", "Please enter your ADColony App ID for iOS", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdColonyInterstitialVideoZoneKEY_iOS, "Interstitial Video Zone Key", "Please enter your ADColony Interstitial Video Zone Key for iOS", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdColonyInterstitialVideoZoneID_iOS, "Interstitial Video Zone ID", "Please enter your ADColony Interstitial Video Zone ID for iOS", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdColonyRewardedVideoZoneKEY_iOS, "Rewarded Video Zone Key", "Please enter your ADColony Rewarded Video Zone Key for iOS", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdColonyRewardedVideoZoneID_iOS, "Rewarded Video Zone ID", "Please enter your ADColony Rewarded Video Zone ID for iOS", margin * 3);
			}

			EditorWindowUtils.CreateFoldout(ref adIDs.IsADColonyAndroidSettingsOpened, "Android IDs", margin * 2);
			if(adIDs.IsADColonyAndroidSettingsOpened) 
			{
			EditorWindowUtils.CreateTextField(ref adIDs.AdColonyAppID_ANDROID, "App ID", "Please enter your ADColony App ID for ANDROID", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdColonyInterstitialVideoZoneKEY_ANDROID, "Interstitial Video Zone Key", "Please enter your ADColony Interstitial Video Zone Key for ANDROID", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdColonyInterstitialVideoZoneID_ANDROID, "Interstitial Video Zone ID", "Please enter your ADColony Interstitial Video Zone ID for ANDROID", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdColonyRewardedVideoZoneKEY_ANDROID, "Rewarded Video Zone Key", "Please enter your ADColony Rewarded Video Zone Key for ANDROID", margin * 3);

			EditorWindowUtils.CreateTextField(ref adIDs.AdColonyRewardedVideoZoneID_ANDROID, "Rewarded Video Zone ID", "Please enter your ADColony Rewarded Video Zone ID for ANDROID", margin * 3);
			}
			}

			#endif
			}

			if (GUI.changed)
			{
			EditorUtility.SetDirty(adIDs); 

			}

			#endif
		}

		#endregion

		#region GIFSETTINGS

		void DisplayGIFSettings() 
		{
			#if VSGIF

			DisplayDocAndRate ();
			EditorWindowUtils.Space (2);
			gifSettings.isGIFFoldoutOpened = EditorGUILayout.Foldout(gifSettings.isGIFFoldoutOpened, "GIFs SETTINGS");

			if (gifSettings.isGIFFoldoutOpened) 
			{
			EditorWindowUtils.Space (2);

			EditorWindowUtils.CreateIntSlider(ref gifSettings.width, 8, 2048, "Width", 400, margin);

			EditorWindowUtils.CreateIntSlider(ref gifSettings.framePerSecond, 1, 30, "Frame Per Second", 400, margin);

			EditorWindowUtils.CreateIntSlider(ref gifSettings.repeat, -1, 10, "Repeat", 400, margin);

			EditorWindowUtils.CreateIntSlider(ref gifSettings.quality, 1, 100, "Quality", 400, margin);

			EditorWindowUtils.CreateFloatSlider(ref gifSettings.bufferSize, 0.1f, 10f, "Buffer Size", 400, margin);

			EditorWindowUtils.CreateEnumPopup<MyThreadPriority>(ref gifSettings.WorkerPriority, "Worker Priority", 300, margin);

			}

			if (GUI.changed)
			{
			EditorUtility.SetDirty(gifSettings); 

			}

			#endif
		}

		#endregion

		#region LEADERBOARD SETTINGS

		void DisplayLeaderboardSettings() 
		{
			#if APPADVISORY_LEADERBOARD

			DisplayDocAndRate ();
			EditorWindowUtils.Space (2);
			EditorWindowUtils.CreateFoldout(ref leaderboardIDs.isLeaderboardIDsFoldoutOpened, "LEADERBOARD SETTINGS");

			if (leaderboardIDs.isLeaderboardIDsFoldoutOpened) 
			{
			if(leaderboardIDs.FIRST_TIME)
			{
			Debug.Log("*********** APP_ADVISORY_FIRST_TIME_LEADERBORD ***********");

					ScriptingSymbolsManager.SetVSLeaderboardSymbol(true);

			leaderboardIDs.FIRST_TIME = false;
			}

			#region BUTTON

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(margin);

			if(GUILayout.Button("GET\nGoogle Play Game\nSDK",  GUILayout.Width(150), GUILayout.Height(50)))
			{
			Application.OpenURL("https://github.com/playgameservices/play-games-plugin-for-unity");
			}
			EditorGUILayout.EndHorizontal();

			#endregion

			EditorWindowUtils.Space(2);


			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(margin);
			Enable_iOS = EditorGUILayout.BeginToggleGroup(new GUIContent("Enable iOS Game Center   [?]", "Activate if you want to use Game Center Leaderboard"), Enable_iOS);
			EditorGUILayout.EndToggleGroup();
			EditorGUILayout.EndHorizontal();

			EditorWindowUtils.Space(1);

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(margin);
			Enable_Android = EditorGUILayout.BeginToggleGroup(new GUIContent("Enable Android Google Play Game Services   [?]", "Activate if you want to use Google Play Game Services Leaderboard"), Enable_Android);
			EditorGUILayout.EndToggleGroup();
			EditorGUILayout.EndHorizontal();

			EditorWindowUtils.Space(1);

			#if VSLEADERBOARD_ENABLE_IOS
			var stringIos = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
			if(!stringIos.Contains("APPADVISORY_LEADERBOARD"))
			{
			stringIos = "APPADVISORY_LEADERBOARD" + ";" + stringIos;

			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS,stringIos);
			}
			#endif

			#if VSLEADERBOARD_ENABLE_ANDROID
			var stringAndroid = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
			if(!stringAndroid.Contains("APPADVISORY_LEADERBOARD"))
			{
			stringAndroid = "APPADVISORY_LEADERBOARD" + ";" + stringAndroid;

			PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android,stringAndroid);
			}
			#endif

			#if VSLEADERBOARD_ENABLE_IOS
			EditorWindowUtils.CreateTextField(ref leaderboardIDs.LEADERBOARDID_IOS, "Game Center Leaderboard Id", "Find it on Itunes Connect console", margin);
			#endif

			#if VSLEADERBOARD_ENABLE_ANDROID
			EditorWindowUtils.CreateTextField(ref leaderboardIDs.LEADERBOARDID_ANDROID, "Google Play Game Leaderboard Id", "Find it on Google Play Game console", margin);
			#endif
			}

			if (GUI.changed)
			{
			EditorUtility.SetDirty(leaderboardIDs); 

			}

			#endif		
		}

		#endregion

		#region RATE US SETTINGS

		void DisplayRateUsSettings() 
		{
			#if VSRATE

			DisplayDocAndRate ();
			EditorWindowUtils.Space (2);
			EditorWindowUtils.CreateFoldout(ref rateUsSettings.isRateUsFoldoutOpened , "RATE US SETTINGS");

			if (rateUsSettings.isRateUsFoldoutOpened) 
			{

			//EditorWindowUtils.Space (1);

			EditorWindowUtils.CreateLabelField("Number of Stars to Accept Review", "", margin);

			EditorWindowUtils.CreateFloatSlider(ref rateUsSettings.numberOfStarsToAcceptReview, 1f, 5f, "", 250, margin);

			EditorWindowUtils.CreateTextField(ref rateUsSettings.email, "email", "The email adress", margin);

			EditorWindowUtils.CreateTextField(ref rateUsSettings.subject, "subject", "The subect of the email", margin);

			EditorWindowUtils.CreateTextField(ref rateUsSettings.body, "body", "The body of the email", margin);
			}

			if (GUI.changed)
			{
			EditorUtility.SetDirty(rateUsSettings); 

			}

			#endif
		}

		#endregion

		#region SHARE SETTINGS

		void DisplayShareSettings() 
		{
			#if VS_SHARE

			DisplayDocAndRate ();
			EditorWindowUtils.Space (2);
			EditorWindowUtils.CreateFoldout(ref shareSettings.isShareFoldoutOpened , "SHARE SETTINGS");

			if (shareSettings.isShareFoldoutOpened) 
			{
			EditorWindowUtils.CreateToggleField(ref shareSettings.androidForceSDCardPermission, "Android Force SDCard Permission", "Check it to force the SD Card Permission on Android", margin);

			EditorWindowUtils.CreateToggleField(ref shareSettings.showButtonShareWhenSceneRestartIfScreenshotAvailable, "Show Share Button When Scene Restart If Screenshot Available", "Check it to show the share button when the scene restarts ff the screenshot is available", margin);

			EditorWindowUtils.CreateEnumPopup<ButtonShareState>(ref shareSettings.state, "Share Button State", 300, margin);
			}

			if (GUI.changed)
			{
			EditorUtility.SetDirty(shareSettings); 
			}

			#endif
		}

		#endregion


	}
}