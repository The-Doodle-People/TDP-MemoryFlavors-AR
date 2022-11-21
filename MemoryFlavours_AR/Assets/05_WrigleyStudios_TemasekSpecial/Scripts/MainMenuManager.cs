/*
 * Author: Wrigley Studios
 * Date: 20/11/22
 * Description: The main menu Manager that handle the main menu scene
 */

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

    /// <summary>
    /// To start the AR expereiences
    /// </summary>
    public void StartAR()
    {
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Quits the application
    /// </summary>
    public void QuitApplication()
    {
        Application.Quit();
    }

    /// <summary>
    /// Open how to use information
    /// </summary>
    public void Infomenu()
    {
        infoMenu.SetActive(true);
    }

    /// <summary>
    /// Close how to use information
    /// </summary>
    public void back()
    {
        infoMenu.SetActive(false);
    }
}
