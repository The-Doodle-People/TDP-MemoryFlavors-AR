
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


namespace AppAdvisory
{
	public enum VSTab 
	{
		Welcome,

		#if VS_COMBO_PACK
		ComboPack,
		#endif

		#if APPADVISORY_ADS
		AdsSettings,
		#endif

		#if VSGIF
		GifSettings,
		#endif

		#if APPADVISORY_LEADERBOARD
		LeaderboardSettings,
		#endif

		#if VSRATE
		RateSettings,
		#endif

		#if VS_SHARE
		ShareSettings
		#endif
	}
}