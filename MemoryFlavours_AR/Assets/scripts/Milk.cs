/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for 3d evaporate milk model to pour 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for 3d milk model
/// </summary>
public class Milk : MonoBehaviour
{
    /// <summary>
    /// link the 3d model animation
    /// </summary>
    public Animator FillCupAnimator; //public variable

    /// <summary>
    /// function to pour the milk into cup
    /// </summary>
    public void FillingMilk()
    {
        //3d model pouring
        FillCupAnimator.SetBool("MilkPouring", true); // where i set the animation to be true after certain actions 

    }
}
