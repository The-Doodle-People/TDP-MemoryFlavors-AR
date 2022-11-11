using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UIController : MonoBehaviour
{
    public GameObject restartConfirmation;
    public Animator loadingScreen;
    public GameObject mainUI;
    public GameObject startCanvas;
    bool experienceStarted;

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
        }

        else if(!VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.VideoBackground.StartVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = true;
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
