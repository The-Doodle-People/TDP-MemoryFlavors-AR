/*
 * Author: Chao Hao
 * Last Updated: 18/11/2022 
 * Description: Script to fade back into game scene after refreshing
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneToggler : MonoBehaviour
{
    public int nextScene = 1;
    private GameManager gameManager;
    
    private void Start()
    {
        Debug.Log("startttt");
        gameManager = FindObjectOfType<GameManager>();
    }

    public void InsertScene()
    {
        Debug.Log("issue");
        gameManager.sceneIndex = nextScene;
    }

    public void UnToggleFadeOut()
    {
        gameManager.loadFadeOut = false;
    }
}
