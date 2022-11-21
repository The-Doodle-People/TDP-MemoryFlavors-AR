/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used to mix syrup and evaporated milk
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to mix the syrup and milk
/// </summary>
public class MixDrink : MonoBehaviour
{
    /// <summary>
    /// link mix animation
    /// </summary>
    public Animator MixAnimator; //public variable

    /// <summary>
    /// link exit button
    /// </summary>
    public GameObject ExitButton;
    //public GameObject nextButton;

    /// <summary>
    /// function used for mixing bandung
    /// </summary>
    public void MixingDrink()
    {
        MixAnimator.SetBool("IsMixing", true); // where i set the animation to be true after certain actions 
        //if animation ends
        if (!MixAnimator.GetCurrentAnimatorStateInfo(0).IsName("IsFilling"))
        {
            //exit button set to true
            ExitButton.SetActive(true);
        }
    }
}
