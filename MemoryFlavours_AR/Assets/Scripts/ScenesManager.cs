using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{   
    /// <summary>
    /// on press bring to quiz scene
    /// </summary>
    public void QuizScene()
    {
        SceneManager.LoadScene("QuizScene");
    }
    public void ArCam()
    {
        SceneManager.LoadScene("");
    }
}
