
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
using System.IO;


namespace AppAdvisory
{
	public class ExternalPackagesResolver  
	{
		public static readonly string ChartBoostFolder = "Assets/Chartboost";
		public static readonly string AdColonyFolder = "Assets/AdColony";
		public static readonly string AdmobFolder = "Assets/GoogleMobileAds";
		public static readonly string FacebookFolder = "Assets/AudienceNetwork";

	

		public static readonly string VSComboPackFolder = "Assets/_AppAdvisory/Very_Simple_Combo_Pack";
		public static readonly string VSAdsFolder = "Assets/_AppAdvisory/Very_Simple_Ads";
		public static readonly string VSGifFolder = "Assets/_AppAdvisory/Very_Simple_GIFs";
		public static readonly string VSLeaderboardFolder = "Assets/_AppAdvisory/Very_Simple_Leaderboard";
		public static readonly string VSShareFolder = "Assets/_AppAdvisory/Very_Simple_Share";
		public static readonly string VSRateFolder = "Assets/_AppAdvisory/Very_Simple_Rate";

		private static readonly string DOTweenFolder = "Assets/Demigiant";


		private static readonly string ChartBoostNamespaceName = "ChartboostSDK";
		private static readonly string AdColonyNamespaceName = "Assets/AdColony";
		private static readonly string AdmobNamespaceName = "Assets/GoogleMobileAds";
		private static readonly string FacebookNamespaceName = "Assets/AudienceNetwork";


		private static readonly string UnityAdsFolder = "Assets/UnityAds";

		public static bool IsUnityAdsAvailable {
			get 
			{
				return Directory.Exists (UnityAdsFolder);
			}
		}


		public static bool IsVSComboPackAvailable {
			get 
			{
				return Directory.Exists (VSComboPackFolder);
			}
		}

		public static bool IsVSAdsAvailable {
			get 
			{
				return Directory.Exists (VSAdsFolder);
			}
		}

		public static bool IsVSGifsAvailable {
			get 
			{
				return Directory.Exists (VSGifFolder);
			}
		}

		public static bool IsVSLeaderboardAvailable {
			get 
			{
				return Directory.Exists (VSLeaderboardFolder);
			}
		}

		public static bool IsVSRateAvailable {
			get 
			{
				return Directory.Exists (VSRateFolder);
			}
		}

		public static bool IsVSShareAvailable {
			get 
			{
				return Directory.Exists (VSShareFolder);
			}
		}
			
		public static bool IsChartBoostAvailable {
			get 
			{
				return Directory.Exists (ChartBoostFolder);
			}
		}

		public static bool IsAdmobAvailable {
			get 
			{
				return Directory.Exists (AdmobFolder);
			}
		}

		public static bool IsFacebookAvailable {
			get 
			{
				return Directory.Exists (FacebookFolder);
			}
		}

		public static bool IsAdColonyAvailable {
			get 
			{
				return Directory.Exists (AdColonyFolder);
			}
		}

		public static bool IsDOTweenAvailable {
			get 
			{
				return Directory.Exists (DOTweenFolder);
			}
		}

		public static bool IsChartBoostNamespaceAvailable {
			get 
			{
				return ReflectionUtils.NamespaceExists (ChartBoostNamespaceName);
			}

		}

		public static bool IsAdColonyNamespaceAvailable {
			get 
			{
				return ReflectionUtils.NamespaceExists (AdColonyNamespaceName);
			}

		}

		public static bool IsAdmobNamespaceAvailable {
			get 
			{
				return ReflectionUtils.NamespaceExists (AdmobNamespaceName);
			}

		}

		public static bool IsFacebookNamespaceAvailable {
			get 
			{
				return ReflectionUtils.NamespaceExists (FacebookNamespaceName);
			}

		}

	}
}