using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TakeScreenshot : MonoBehaviour
{
    public GameObject timeTxt;
    public void TakeAPic()
    {
        StartCoroutine("Capture");
    }

    IEnumerator Capture()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".png";
        string pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();
        SavedToPhone();
    }

    async public void SavedToPhone()
    {
        timeTxt.SetActive(true);
        await Task.Delay(2000);
        timeTxt.SetActive(false);
    }
}
