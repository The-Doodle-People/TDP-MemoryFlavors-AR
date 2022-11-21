/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used to quit game
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for quiting game
/// </summary>
public class QuitGame : MonoBehaviour
{
    /// <summary>
    /// function to quit game
    /// </summary>
    public void Quit()
    {
        Application.Quit(); //quit application
    }
}
