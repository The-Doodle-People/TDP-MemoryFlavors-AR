using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TakeScreenshot : MonoBehaviour
{
    public GameObject timeTxt;
    public AudioSource cam;
    /// <summary>
    /// Play camera shutter SFX and starts coroutine
    /// </summary>
    public void TakeAPic()
    {
        cam.Play();
        StartCoroutine("Capture");
    }
    /// <summary>
    /// Coroutine to take a picture
    /// </summary>
    /// <returns></returns>
    IEnumerator Capture()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Screenshot" + timeStamp + ".png";
        string pathToSave = fileName;
        ScreenCapture.CaptureScreenshot(pathToSave);
        yield return new WaitForEndOfFrame();
        SavedToPhone();
    }
    /// <summary>
    /// Text to prove that photo has been taken
    /// </summary>
    async public void SavedToPhone()
    {
        timeTxt.SetActive(true);
        await Task.Delay(2000);
        timeTxt.SetActive(false);
    }
}
