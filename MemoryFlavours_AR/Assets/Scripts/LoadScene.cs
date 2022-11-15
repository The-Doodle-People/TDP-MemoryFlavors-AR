/*
 * Author: Shi Jie, Anqi, Jessica
 * Date: 9/11/22
 * Description: Loads or quits scene when button is click
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Goes to home page
    public void HomePage()
    {
        SceneManager.LoadScene("StartPage");
    }

    // Closes the application
    public void QuitApp()
    {
        Application.Quit();
    }

    // Changes to next scene
    public void StartApp()
    {
        SceneManager.LoadScene("ARCamera");
    }

    // Go to game scene
    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
