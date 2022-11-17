
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
using UnityEditor;
using System.IO;


namespace AppAdvisory
{
	
	public class ExternalPostProcess : AssetPostprocessor 
	{

		private static void CheckImportedAndDeletedAssetsNetworks(string[] assets, bool isImported) {
			foreach (string asset in assets) 
			{
				if (asset.Contains (ExternalPackagesResolver.ChartBoostFolder)) 
				{
					ScriptingSymbolsManager.SetChartboostSymbol (isImported);
				} 
				if (asset.Contains (ExternalPackagesResolver.AdmobFolder)) 
				{
					ScriptingSymbolsManager.SetAdmobSymbol (isImported);
				} 
				if (asset.Contains (ExternalPackagesResolver.AdColonyFolder)) 
				{
					ScriptingSymbolsManager.SetAdColonySymbol (isImported);
				} 
				if (asset.Contains (ExternalPackagesResolver.FacebookFolder)) 
				{
					ScriptingSymbolsManager.SetFacebookSymbol (isImported);
				}
			}
		}

		private static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			CheckImportedAndDeletedAssetsNetworks (importedAssets, true);
			CheckImportedAndDeletedAssetsNetworks (deletedAssets, false);

			if (deletedAssets.Length > 0 || importedAssets.Length > 0) {
				#region VERY SIMPLE
				ScriptingSymbolsManager.SetAllVSSymbols();
				#endregion

				/*
				#region ADS
				ScriptingSymbolsManager.SetAdColonySymbol (ExternalPackagesResolver.IsAdColonyAvailable);
				ScriptingSymbolsManager.SetAdmobSymbol (ExternalPackagesResolver.IsAdmobAvailable);
				ScriptingSymbolsManager.SetChartboostSymbol (ExternalPackagesResolver.IsChartBoostAvailable);
				ScriptingSymbolsManager.SetFacebookSymbol (ExternalPackagesResolver.IsFacebookAvailable);
				#endregion
				*/

				#region DOTWEEN
				ScriptingSymbolsManager.SetAADotweenSymbol (ExternalPackagesResolver.IsDOTweenAvailable);
				#endregion
			}
		}
	}
}