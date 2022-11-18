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

    // Check if player can place table
    public bool canPlaceTable;

    // Character Animators
    public Animator boy6Animator;
    public Animator boy12Animator;
    public Animator girl6Animator;
    public Animator girl12Animator;

    // Check if table is placed
    public bool tablePlaced;

    // Check if player can interact with dimsum
    public bool canInteractWithDimsum;

    // Check if player has already interacted with the dimsum
    public bool interactedWithDimsum;

    // Animator for Table fall animation
    public Animator tableAnimator;

    // Animator for transition animation
    public Animator transitionAnimator;

    public GameObject transitionCanvas;

    // Check if player can scan postcard
    public bool canScanPostcardAgain;

    // Check if player scanned postcard the second time
    public bool postcardScanned2;

    // Start Image Target GameObject
    public GameObject startImageTarget;

    public GameObject planeFinder;

    public GameObject groundStage;

    public bool gameInProgress;

    public GameObject dimSumGame;

    public GameObject endScreen;

    public bool experienceOver;

    // Dictionary for Object Tracking
    Dictionary<GameObject, bool> trackedObjectStatus = new Dictionary<GameObject, bool>();

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
        
        if (Input.GetMouseButtonDown(0) && canPlaceTable)
        {
            gameText.text = "CLICK ON THE DIM SUM\nTO EAT IT!";
            canPlaceTable = false;
            tablePlaced = true;
            canInteractWithDimsum = true;
        }

        if (interactedWithDimsum)
        {
            StartCoroutine("PlayTableFall");
            interactedWithDimsum = false;
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
        // Turn on Start Image Target
        startImageTarget.SetActive(true);
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

            if (!postcardScanned2)
            {
                // Change game text
                gameText.text = "OH! LOOKS LIKE THERE'S\nNOTHING HERE YET!";
                // Turn on CameraOff Canvas.
                cameraOff.SetActive(true);
            }

            else if (postcardScanned2)
            {
                // Set game text to null
                gameText.text = "";
                dimSumGame.SetActive(true);
                startImageTarget.SetActive(false);
                groundStage.SetActive(false);
                mainUI.SetActive(false);
            }
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

    public void EndExperience()
    {
        gameInProgress = false;
        experienceOver = true;
        dimSumGame.SetActive(false);
        endScreen.SetActive(true);
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

    IEnumerator PlayTableFall()
    {
        planeFinder.SetActive(false);
        // Play TableFall animation
        tableAnimator.SetBool("TableFall", true);

        // Changing character animation speed according to speed of table animation
        yield return new WaitForSeconds(1f);
        boy12Animator.speed = 0.7f;
        boy6Animator.speed = 0.7f;
        girl6Animator.speed = 0.7f;
        girl12Animator.speed = 0.7f;
        yield return new WaitForSeconds(0.5f);
        boy12Animator.speed = 0.6f;
        boy6Animator.speed = 0.6f;
        girl6Animator.speed = 0.6f;
        girl12Animator.speed = 0.6f;
        yield return new WaitForSeconds(0.4f);
        boy12Animator.speed = 0.55f;
        boy6Animator.speed = 0.55f;
        girl6Animator.speed = 0.55f;
        girl12Animator.speed = 0.55f;
        yield return new WaitForSeconds(0.4f);
        boy12Animator.speed = 0.45f;
        boy6Animator.speed = 0.45f;
        girl6Animator.speed = 0.45f;
        girl12Animator.speed = 0.45f;
        yield return new WaitForSeconds(0.3f);
        boy12Animator.speed = 0.35f;
        boy6Animator.speed = 0.35f;
        girl6Animator.speed = 0.35f;
        girl12Animator.speed = 0.35f;
        yield return new WaitForSeconds(0.3f);
        boy12Animator.speed = 0.25f;
        boy6Animator.speed = 0.25f;
        girl6Animator.speed = 0.25f;
        girl12Animator.speed = 0.25f;
        yield return new WaitForSeconds(0.3f);
        boy12Animator.speed = 0.15f;
        boy6Animator.speed = 0.15f;
        girl6Animator.speed = 0.15f;
        girl12Animator.speed = 0.15f;
        yield return new WaitForSeconds(0.3f);
        boy12Animator.speed = 0f;
        boy6Animator.speed = 0f;
        girl6Animator.speed = 0f;
        girl12Animator.speed = 0f;
        //yield return new WaitForSeconds(3.5f);

        yield return new WaitForSeconds(2f);
        StartCoroutine("PlayTransition");
    }

    IEnumerator PlayTransition()
    {
        transitionAnimator.SetBool("CanTransition", true);
        yield return new WaitForSeconds(8f);
        transitionAnimator.SetBool("CanTransition", false);
        canScanPostcardAgain = true;
        gameText.text = "SCAN THE POSTCARD\nIMAGE AGAIN!";

        // Turn off Start Image Target
        //startImageTarget.SetActive(false);

        // Turn on Minigame Image Target
        //minigameMidAirStage.SetActive(true);
    }
}
