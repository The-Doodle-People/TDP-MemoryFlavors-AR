using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Restart confirmation tab 
    public GameObject restartConfirmation;

    // Quit Confirmation tab
    public GameObject quitConfirmation;

    // Game Restart Confirmation Tab
    public GameObject gameRestartConfirmation;

    // Game Quit Confirmation tab
    public GameObject gameQuitConfirmation;

    // End Restart confirmation tab 
    public GameObject endRestartConfirmation;

    // End Quit Confirmation tab
    public GameObject endQuitConfirmation;

    // Animator for loading screen
    public Animator loadingScreen;

    // MainUI canvas
    public GameObject mainUI;

    //check if MainUI is active
    public bool uiActive;

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

    // Check if can scan postcard
    public bool canScanPostcard;

    // Check if postcard is scanned
    public bool postcardScanned;

    // Check if table is placed
    public bool tablePlaced;

    // Check if player can interact with dimsum
    public bool canInteractWithDimsum;

    // Check if player has already interacted with the dimsum
    public bool interactedWithDimsum;

    // GameObject for animated arrow
    public GameObject arrowObject;

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

    // Ground plane objects
    public GameObject planeFinder;
    public GameObject groundStage;

    public bool animationInProgress;

    // Arrow UI Image
    public GameObject arrowImage;

    // Check if game is in progress
    public bool gameInProgress;

    // Dim Sum game canvas
    public GameObject dimSumGame;

    // End screen canvas
    public GameObject endScreen;

    public bool experienceOver;

    // Dictionary for Object Tracking
    Dictionary<GameObject, bool> trackedObjectStatus = new Dictionary<GameObject, bool>();

    public SFXManager sfx;

    void Awake()
    {
        
    }

    void Update()
    {
        // If player taps/clicks screen, Start the PlayLoading Coroutine
        if (/*Input.touchCount > 0*/ Input.GetMouseButtonDown(0) && !experienceStarted)
        {
            StartCoroutine("PlayLoading");
            sfx.SplashPageAudioOn();
        }
        
        /*if (Input.GetMouseButtonDown(0) && canPlaceTable && groundStage.activeSelf)
        {
            gameText.text = "CLICK ON THE DIM SUM\nTO EAT IT!";
            canPlaceTable = false;
            tablePlaced = true;
            canInteractWithDimsum = true;
        }*/

        if (interactedWithDimsum)
        {
            interactedWithDimsum = false;
            StartCoroutine("PlayTableFall");
            arrowObject.SetActive(false);
        }
    }

    public void OnGroundPlanePlaced()
    {
        gameText.text = "CLICK ON THE DIM SUM\nTO EAT IT!";
        canPlaceTable = false;
        tablePlaced = true;
        canInteractWithDimsum = true;
        // Play Store noise audio
        sfx.StoreNoiseOn();
        //play table placed audio
        sfx.TablePlacedAudioOn();

    }

    IEnumerator PlayLoading()
    {
        experienceStarted = true;
        // Play loading animation
        loadingScreen.SetBool("experienceStart", true);
        
        // Wait 6.5 seconds for loading screen to fade in
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
            uiActive = true;
            Debug.Log("ui active");
        }
        
        else if (mainUI.activeSelf)
        {
            mainUI.SetActive(false);
            uiActive = false;
            Debug.Log("ui inactive");
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
                startImageTarget.SetActive(false);
                groundStage.SetActive(false);
                planeFinder.SetActive(false);
                sfx.StoreNoiseOff();
            }

            else if (postcardScanned2)
            {
                // Set game text to null
                gameText.text = "";
                dimSumGame.SetActive(true);
                startImageTarget.SetActive(false);
                groundStage.SetActive(false);
                mainUI.SetActive(false);
                sfx.SadViolinBGMPause();
            }
        }

        // If Vuforia Camera is not running
        else if(!VuforiaBehaviour.Instance.enabled)
        {
            // Start rendering video
            VuforiaBehaviour.Instance.VideoBackground.StartVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = true;

            if (!postcardScanned)
            {
                Debug.Log("Change Text");
                // turn off table ground plane
                groundStage.SetActive(false);
                startImageTarget.SetActive(true);
                // Change Game text
                gameText.text = "SCAN THE\nPOSTCARD IMAGE!";
                canPlaceTable = false;
                // Turn off CameraOff Canvas.
                cameraOff.SetActive(false);
            }

            else if (postcardScanned && canPlaceTable)
            {
                // Set active table ground plane
                groundStage.SetActive(true);
                planeFinder.SetActive(true);
                startImageTarget.SetActive(true);
                // Change Game text
                gameText.text = "FIND A FLAT SURFACE\nAND CLICK ANYWHERE \nON THE SCREEN TO\nPLACE THE TABLE!";
                // Turn off CameraOff Canvas.
                cameraOff.SetActive(false);
            }

            else if (tablePlaced && canInteractWithDimsum)
            {
                // Set active table ground plane
                groundStage.SetActive(true);
                gameText.text = "CLICK ON THE DIM SUM\nTO EAT IT!";
                canPlaceTable = false;
                tablePlaced = true;
                canInteractWithDimsum = true;
                startImageTarget.SetActive(true);
                // Play Store noise audio
                sfx.StoreNoiseOn();
                // Turn off CameraOff Canvas.
                cameraOff.SetActive(false);
            }

            else if (animationInProgress && !canScanPostcardAgain)
            {
                // Set active table ground plane
                groundStage.SetActive(true);
                startImageTarget.SetActive(true);
                gameText.text = "SCAN THE POSTCARD\nIMAGE AGAIN!";
                // Turn off CameraOff Canvas.
                cameraOff.SetActive(false);
            }

            else if (canScanPostcardAgain)
            {
                // Set active table ground plane
                groundStage.SetActive(true);
                startImageTarget.SetActive(true);
                canScanPostcardAgain = true;
                gameText.text = "SCAN THE POSTCARD\nIMAGE AGAIN!";
                sfx.SadViolinBGMUnPause();
                // Turn off CameraOff Canvas.
                cameraOff.SetActive(false);
            }

            else if (postcardScanned2)
            {
                // Set active table ground plane
                groundStage.SetActive(true);
                startImageTarget.SetActive(true);
                postcardScanned2 = true;
                gameText.text = "TOGGLE YOUR\nCAMERA!";
                arrowImage.SetActive(true);
                sfx.SadViolinBGMUnPause();
                // Turn off CameraOff Canvas.
                cameraOff.SetActive(false);
            }
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

    public void ToggleEndRestart()
    {
        if (!endRestartConfirmation.activeSelf)
        {
            endRestartConfirmation.SetActive(true);
        }

        else if (endRestartConfirmation.activeSelf)
        {
            endRestartConfirmation.SetActive(false);
        }
    }

    public void ToggleGameRestart()
    {
        if (!gameRestartConfirmation.activeSelf)
        {
            gameRestartConfirmation.SetActive(true);
        }

        else if (gameRestartConfirmation.activeSelf)
        {
            gameRestartConfirmation.SetActive(false);
        }
    }

    public void RestartConfirm()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void ToggleQuit()
    {
        if (!quitConfirmation.activeSelf)
        {
            quitConfirmation.SetActive(true);
        }

        else if (quitConfirmation.activeSelf)
        {
            quitConfirmation.SetActive(false);
        }
    }

    public void ToggleGameQuit()
    {
        if (!gameQuitConfirmation.activeSelf)
        {
            gameQuitConfirmation.SetActive(true);
        }

        else if (gameQuitConfirmation.activeSelf)
        {
            gameQuitConfirmation.SetActive(false);
        }
    }

    public void ToggleEndQuit()
    {
        if (!endQuitConfirmation.activeSelf)
        {
            endQuitConfirmation.SetActive(true);
        }

        else if (endQuitConfirmation.activeSelf)
        {
            endQuitConfirmation.SetActive(false);
        }
    }


    public void QuitConfirm()
    {
        Application.Quit();
    }

    IEnumerator PlayTableFall()
    {
        animationInProgress = true;

        planeFinder.SetActive(false);
        // Play TableFall animation
        tableAnimator.SetBool("TableFall", true);

        yield return new WaitForSeconds(0.3f);
        // Play shocked audio
        sfx.ShockedAudioOn();

        yield return new WaitForSeconds(0.4f);
        // Play TableBreak audio
        sfx.TableBreakAudioOn();

        // Changing character animation speed according to speed of table animation
        yield return new WaitForSeconds(0.3f);
        boy12Animator.speed = 0.7f;
        boy6Animator.speed = 0.7f;
        girl6Animator.speed = 0.7f;
        girl12Animator.speed = 0.7f;
        sfx.storeNoiseAudio.pitch = 0.7f;
        
        yield return new WaitForSeconds(0.5f);
        boy12Animator.speed = 0.6f;
        boy6Animator.speed = 0.6f;
        girl6Animator.speed = 0.6f;
        girl12Animator.speed = 0.6f;
        sfx.storeNoiseAudio.pitch = 0.6f;
        yield return new WaitForSeconds(0.4f);
        boy12Animator.speed = 0.55f;
        boy6Animator.speed = 0.55f;
        girl6Animator.speed = 0.55f;
        girl12Animator.speed = 0.55f;
        sfx.storeNoiseAudio.pitch = 0.55f;
        yield return new WaitForSeconds(0.4f);
        boy12Animator.speed = 0.45f;
        boy6Animator.speed = 0.45f;
        girl6Animator.speed = 0.45f;
        girl12Animator.speed = 0.45f;
        sfx.storeNoiseAudio.pitch = 0.45f;
        yield return new WaitForSeconds(0.3f);
        boy12Animator.speed = 0.35f;
        boy6Animator.speed = 0.35f;
        girl6Animator.speed = 0.35f;
        girl12Animator.speed = 0.35f;
        sfx.storeNoiseAudio.pitch = 0.35f;
        yield return new WaitForSeconds(0.3f);
        boy12Animator.speed = 0.25f;
        boy6Animator.speed = 0.25f;
        girl6Animator.speed = 0.25f;
        girl12Animator.speed = 0.25f;
        sfx.storeNoiseAudio.pitch = 0.25f;
        yield return new WaitForSeconds(0.3f);
        boy12Animator.speed = 0.15f;
        boy6Animator.speed = 0.15f;
        girl6Animator.speed = 0.15f;
        girl12Animator.speed = 0.15f;
        sfx.storeNoiseAudio.pitch = 0.15f;
        yield return new WaitForSeconds(0.3f);
        boy12Animator.speed = 0f;
        boy6Animator.speed = 0f;
        girl6Animator.speed = 0f;
        girl12Animator.speed = 0f;
        sfx.StoreNoiseOff();
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
        sfx.SadViolinBGMOn();
        animationInProgress = false;

        // Turn off Start Image Target
        //startImageTarget.SetActive(false);

        // Turn on Minigame Image Target
        //minigameMidAirStage.SetActive(true);
    }
}
