/*
 * Author: Chao Hao
 * Last Updated: 18/11/2022 
 * Description:
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class GameManager : MonoBehaviour
{
    [Header("GameManager General")]
    public static GameManager instance;

    public int previousSceneIndex;
    
    public int sceneIndex;

    public bool loadFadeOut;
    
    private bool sceneChanging;

    private static readonly int FadeOut = Animator.StringToHash("fadeOut");

    [Header("For Quiz Scene")]
    public int quizId;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            //Set the GameManager to not to be destroyed when scenes are loaded
            DontDestroyOnLoad(gameObject);
            //Set myself as the instance
            instance = this;
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += SceneResponse;
        VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
        // Run this is playground scene
        if (loadFadeOut && sceneIndex == 0)
        {
            FindObjectOfType<SceneToggler>().gameObject.GetComponent<Animator>().SetTrigger(FadeOut);
        }
    }

    // When GameManager is Destroyed
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneResponse;
        VuforiaApplication.Instance.OnVuforiaStarted -= OnVuforiaStarted;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != sceneIndex && !sceneChanging)
        {
            // stores current scene before load
            previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // lock the function to prevent repeat calls
            sceneChanging = true;
            // loads new scene
            SceneManager.LoadScene(sceneIndex);
        }
    }

    /// <summary>
    /// Checks if sceneIndex has changed, runs custom code depending on result
    /// </summary>
    private void SceneResponse(Scene scene, LoadSceneMode mode)
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneChanging = false;
        // if this for refresh scene
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(ReturnToPrevious());
        }
    }
    
    /// <summary>
    /// Sets framerate of the Application
    /// </summary>
    private static void OnVuforiaStarted()
    {
        // Query Vuforia for recommended frame rate and set it in Unity
        var targetFps = VuforiaBehaviour.Instance.CameraDevice.GetRecommendedFPS();

        // By default, we use Application.targetFrameRate to set the recommended frame rate.
        // Google Cardboard does not use vsync, and OVR explicitly disables it. If developers 
        // use vsync in their quality settings, they should also set their QualitySettings.vSyncCount
        // according to the value returned above.
        // e.g: If targetFPS > 50 --> vSyncCount = 1; else vSyncCount = 2;
        if (Application.targetFrameRate != targetFps)
        {
            Debug.Log("Setting frame rate to " + targetFps + "fps");
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = targetFps;
        }
    }

    // code waits for 3 seconds before switching the scene back to the previous scene
    private IEnumerator ReturnToPrevious()
    {
        yield return new WaitForSeconds(3f);

        // no checks needed, as if sceneIndex == buildIndex, no scene change will happen
        sceneIndex = previousSceneIndex;
    }
}
