/*
 * Author: Chao Hao
 * Last Updated: 18/11/2022 
 * Description: Script to apply onto the pause menu UI such as audio mute, pause and resume functions
 */

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class MenuUI : MonoBehaviour
{
    public AudioSource sound;
    // to toggle AR cam
    private AnchorBehaviour[] groundPlane;
    private ImageTargetBehaviour imageTarget;

    public Camera camera;

    public void Start()
    {
        camera = Camera.main;
    }
    
    public void OnPause()
    {
        camera.GetComponent<AudioListener>().enabled = false;
        sound.Pause();
        Time.timeScale = 0;
        groundPlane = FindObjectsOfType<AnchorBehaviour>();
        imageTarget = FindObjectOfType<ImageTargetBehaviour>();
        
        foreach (var plane in groundPlane)
        {
            plane.gameObject.SetActive(false);
        }
        imageTarget.gameObject.SetActive(false);
    }

    public void OnResume()
    {
        camera.GetComponent<AudioListener>().enabled = true;
        Time.timeScale = 1f;
        sound.Play();
        
        foreach (var plane in groundPlane)
        {
            plane.gameObject.SetActive(true);
        }
        imageTarget.gameObject.SetActive(true);
    }

    public void OnReturnGame()
    {
        OnResume();
        var gameManager = FindObjectOfType<GameManager>();
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            gameManager.sceneIndex = 0;
        }
        else
        {
            #if UNITY_EDITOR
            
            EditorApplication.ExitPlaymode();
            
            #endif
            Application.Quit();
        }
    }

    public void StartGame()
    {
        FindObjectOfType<GameManager>().sceneIndex = 2;
    }

}
