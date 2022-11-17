using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    public Image photoDisplay;
    public GameObject photoFrame;

    private Texture2D screenCapture;
    private bool viewingPhoto;

    public bool takePhoto;

    // Start is called before the first frame update
    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (takePhoto)
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
        }
        else
        {
            RemovePhoto();
        }
    }

    IEnumerator CapturePhoto()
    {
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0f, 0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100f);
        photoDisplay.sprite = photoSprite;

        photoFrame.SetActive(true);


    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
    }

    public void TakePhoto()
    {
        takePhoto = true;
    }

    public void TakeAgain()
    {
        takePhoto = false;
    }
}
