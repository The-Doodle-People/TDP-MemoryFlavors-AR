/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for muting audio
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class to mute audio 
/// </summary>
public class MuteAudio : MonoBehaviour
{
    /// <summary>
    /// mute toggle button
    /// </summary>
    /// <param name="muted"></param>
    public void MuteToggle(bool muted)
    {
        //if toggle is on, it is muted
        if (muted)
        {
            AudioListener.volume = 0;
        }
        // if toggle is off, it is not muted
        else
        {
            AudioListener.volume = 1;
        }
    }
}
