/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for filling syrup in cup 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to fill up syrup in cup
/// </summary>
public class FillCup : MonoBehaviour
{
    /// <summary>
    /// link animation for the syrup to be filled in cup
    /// </summary>
    public Animator FillCupAnimator; //public variable

    /// <summary>
    /// link next button when animation is done
    /// </summary>
    public GameObject nextButton;

    /// <summary>
    /// function to fill syrup in cup
    /// </summary>
    public void FillingCup()
    {
        //make fill cup animation to true 
        FillCupAnimator.SetBool("IsFilling", true); // where i set the animation to be true after certain actions 
        //when animation ends
        if (!FillCupAnimator.GetCurrentAnimatorStateInfo(0).IsName("IsFilling"))
        {
            //next button set to true
            nextButton.SetActive(true);
        }
        
    }
}
