using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int previousSceneIndex;
    
    public int sceneIndex;

    public bool loadFadeOut;
    
    private static readonly int FadeOut = Animator.StringToHash("fadeOut");

    [SerializeField] private bool sceneChanging;
    
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

    private void OnDisable()
    {
        Debug.Log("norun");
    }

    private void SceneResponse(Scene scene, LoadSceneMode mode)
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneChanging = false;
        // if this is the refreshscene
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(ReturnToPrevious());
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != sceneIndex && !sceneChanging)
        {
            // stores current scene before load
            previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
            sceneChanging = true;
            // loads new scene
            SceneManager.LoadScene(sceneIndex);
        }
    }
    
    private void OnDestroy()
    {
        VuforiaApplication.Instance.OnVuforiaStarted -= OnVuforiaStarted;
    }

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


    private IEnumerator ReturnToPrevious()
    {
        yield return new WaitForSeconds(3f);
        sceneIndex = previousSceneIndex;
    }
}
