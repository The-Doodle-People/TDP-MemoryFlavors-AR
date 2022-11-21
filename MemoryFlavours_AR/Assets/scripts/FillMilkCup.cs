/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for filling milk in cup 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to fill evaporated milk in 3d cup
/// </summary>
public class FillMilkCup : MonoBehaviour
{
    /// <summary>
    /// to link filling milk into cup animation
    /// </summary>
    public Animator FillMilkAnimator; //public variable

    /// <summary>
    /// to link the next button
    /// </summary>
    public GameObject nextButton;

    /// <summary>
    /// function to fill milk into cup
    /// </summary>
    public void FillingCup()
    {
        //for animation so that milk shows up in cup
        FillMilkAnimator.SetBool("IsFillingMilk", true); // where i set the animation to be true after certain actions 
        // when animation ends
        if (!FillMilkAnimator.GetCurrentAnimatorStateInfo(0).IsName("IsFillingMilk"))
        {
            //set next button to true for player to move on in game
            nextButton.SetActive(true);
        }

    }
}
