using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //Load Menu
    public void LoadScene0()
    {
        SceneManager.LoadScene(0);
    }
    //Load Main Scene
    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }
    //load GroundPlaneScene
    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }
}
