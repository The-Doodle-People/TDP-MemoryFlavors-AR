using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    /// <summary>
    /// Switch to AR scene
    /// </summary>
    public void Switch()
    {
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Switch to ending scene
    /// </summary>
    public void End()
    {
        SceneManager.LoadScene(2);
    }
}
