using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Stores the active GameManager
    /// </summary>
    public static GameManager instance;

    public bool enterMemories = true;
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

    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
