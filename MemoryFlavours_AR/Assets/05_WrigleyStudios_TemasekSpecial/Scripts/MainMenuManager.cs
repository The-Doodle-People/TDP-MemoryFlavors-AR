using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This scripts controls all the buttons in the app handling the transistion from scene to scene
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject infoMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAR()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void Infomenu()
    {
        infoMenu.SetActive(true);
    }

    public void back()
    {
        infoMenu.SetActive(false);
    }
}
