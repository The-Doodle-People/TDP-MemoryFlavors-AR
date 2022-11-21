/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for the bandung (pouring syrup)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for 3d syrup model to pour
/// </summary>
public class Bandung : MonoBehaviour
{
    /// <summary>
    /// link the animation (pouring animation) 
    /// </summary>
    public Animator BandungAnimator; //public variable

    //public GameObject Canvas;

    /// <summary>
    /// function to set pouring animation to true 
    /// </summary>
    public void StartPour()
    {
        BandungAnimator.SetBool("IsPouring", true); // where i set the animation to be true after certain actions 

    }
}
