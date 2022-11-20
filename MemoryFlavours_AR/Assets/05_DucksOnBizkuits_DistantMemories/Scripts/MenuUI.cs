/*
 * Author: Chao Hao
 * Last Updated: 18/11/2022 
 * Description: Script to apply onto the pause menu UI such as audio mute, pause and resume functions
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class MenuUI : MonoBehaviour
{
    public AudioSource audio;
    // to toggle AR cam
    private AnchorBehaviour[] groundPlane;
    private ImageTargetBehaviour imageTarget;

    
    
    public void OnPause()
    {
        Time.timeScale = 0;
        audio.Pause();
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
        Time.timeScale = 1f;
        audio.Play();
        
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
            FindObjectOfType<GameManager>().sceneIndex = 0;
        }
        else
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        FindObjectOfType<GameManager>().sceneIndex = 2;
    }

}
