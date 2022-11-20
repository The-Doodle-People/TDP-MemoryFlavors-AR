/*
* Author: Wong Qing Wei, Jasmine Giam
* Date: 20/11/2022
* Description: Game Manager Script
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Stores the active GameManager
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// bool to check if player is viewing the responses
    /// </summary>
    public bool enterMemories = false;
    private void Awake()
    {
        // Check whether instance is null. If null, means no GameManager
        // Check whether I am the active GameManager
        if (instance != null && instance != this)
        {
            //If there is a GameManager, and it's not me, destroy myself.
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        
    }

    //quit application
    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
