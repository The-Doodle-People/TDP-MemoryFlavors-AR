using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAndSendScreenshotFromScript : MonoBehaviour 
{
	public void OnClicked () 
    {
		AppAdvisory.SharingSystem.VSSHARE.self.useCustomShareText = true;
		AppAdvisory.SharingSystem.VSSHARE.self.customShareText = "YOUR TEXT HERE";
		AppAdvisory.SharingSystem.VSSHARE.DOTakeScreenShot();
		AppAdvisory.SharingSystem.VSSHARE.OnScreenshotTaken -= AppAdvisory_SharingSystem_VSSHARE_OnScreenshotTaken;
		AppAdvisory.SharingSystem.VSSHARE.OnScreenshotTaken += AppAdvisory_SharingSystem_VSSHARE_OnScreenshotTaken; 	}

	void AppAdvisory_SharingSystem_VSSHARE_OnScreenshotTaken(Texture2D tex)
	{
		AppAdvisory.SharingSystem.VSSHARE.DOShareScreenshot(AppAdvisory.SharingSystem.VSSHARE.self.customShareText, ShareType.Native);
	}
}
