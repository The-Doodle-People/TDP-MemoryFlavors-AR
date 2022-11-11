using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    // Restart confirmation tab 
    public GameObject restartConfirmation;

    // Animator for loading screen
    public Animator loadingScreen;

    // MainUI canvas
    public GameObject mainUI;

    // Start Screen canvas
    public GameObject startCanvas;

    bool experienceStarted;

    // Main Game text
    public TextMeshProUGUI gameText;

    // cameraOff canvas
    public GameObject cameraOff;

    // Check if player can scan postcard
    public bool canScanPostcardAgain;

    // Check if player scanned postcard the second time
    public bool postcardScanned2;

    void Awake()
    {
        
    }

    void Update()
    {
        // If player taps/clicks screen, Start the PlayLoading Coroutine
        if (/*Input.touchCount > 0*/ Input.GetMouseButtonDown(0) && !experienceStarted)
        {
            StartCoroutine("PlayLoading");
        }
    }

    IEnumerator PlayLoading()
    {
        experienceStarted = true;
        // Play loading animation
        loadingScreen.SetBool("experienceStart", true);
        // Wait 6.5 seconds for loading screen to fade in and out
        yield return new WaitForSeconds(6.5f);
        ToggleMainUI();
        // Stop loading animation
        loadingScreen.SetBool("experienceStart", false);
        // Turn off start canvas
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
            // Stop rendering video
            VuforiaBehaviour.Instance.VideoBackground.StopVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = false;

            // Change game text
            gameText.text = "OH! LOOKS LIKE THERE'S\nNOTHING HERE YET!";
            // Turn on CameraOff Canvas.
            cameraOff.SetActive(true);
        }

        // If Vuforia Camera is not running
        else if(!VuforiaBehaviour.Instance.enabled)
        {
            // Start rendering video
            VuforiaBehaviour.Instance.VideoBackground.StartVideoBackgroundRendering(); 
            VuforiaBehaviour.Instance.enabled = true;

            // Change game text
            gameText.text = "CLICK ANYWHERE ON THE\nSCREEN TO PLACE\nTHE TABLE!";
            // Turn off CameraOff Canvas.
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
