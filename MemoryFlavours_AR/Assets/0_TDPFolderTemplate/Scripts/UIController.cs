using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject restartConfirmation;
    public Animator loadingScreen;
    public GameObject mainUI;
    public GameObject startCanvas;
    bool experienceStarted;
    public TextMeshProUGUI gameText;
    public GameObject cameraOff;

    void Awake()
    {
        
    }

    void Update()
    {
        if (/*Input.touchCount > 0*/ Input.GetMouseButtonDown(0) && !experienceStarted)
        {
            StartCoroutine("PlayLoading");
        }
    }

    IEnumerator PlayLoading()
    {
        experienceStarted = true;
        loadingScreen.SetBool("experienceStart", true);
        yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(4.5f);
        ToggleMainUI();
        loadingScreen.SetBool("experienceStart", false);
        startCanvas.SetActive(false);
    }

    public void ToggleMainUI()
    {
        if (!mainUI.activeSelf)
        {
            mainUI.SetActive(true);
        }
        
        else if (mainUI.activeSelf)
        {
            mainUI.SetActive(false);
        }
    }

    public void ToggleCamera()
    {
        // Check if Vuforia is running
        if (VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.VideoBackground.StopVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = false;
            gameText.text = "OH! LOOKS LIKE THERE'S\nNOTHING HERE YET!";
            cameraOff.SetActive(true);
        }

        else if(!VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.VideoBackground.StartVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = true;
            gameText.text = "CLICK ANYWHERE ON THE\nSCREEN TO PLACE\nTHE TABLE!";
            cameraOff.SetActive(false);
        }
    }

    public void ToggleRestart()
    {
        if (!restartConfirmation.activeSelf)
        {
            restartConfirmation.SetActive(true);
        }

        else if (restartConfirmation.activeSelf)
        {
            restartConfirmation.SetActive(false);
        }
    }
}
