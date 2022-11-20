using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GUILoadSceneAdd : MonoBehaviour {

public string LevelToLoad = "";
    public string LevelToUnLoad = "";

    public void compute_levelload(){
        SceneManager.LoadScene((LevelToLoad), LoadSceneMode.Additive);
    }

    public void compute_levelunload()
    {
        SceneManager.UnloadSceneAsync(LevelToUnLoad);
    }
}

