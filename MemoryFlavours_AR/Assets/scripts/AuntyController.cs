/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for the aunty model 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class for aunty controller
/// </summary>
public class AuntyController : MonoBehaviour
{

    /// <summary>
    /// to link aunty's animation (waving)
    /// </summary>
    public Animator myAnimator; //public variable

    public GameObject Canvas;

    /// <summary>
    /// function to set animation to true 
    /// </summary>
    public void StartWave()
    {
        myAnimator.SetBool("IsWaving", true); // where i set the animation to be true after certain actions 

    }

    /// <summary>
    /// function to link it to raycasting 
    /// </summary>
    public void Interact()
    {
        myAnimator.SetBool("IsWaving", true); // where i set the animation to be true after certain actions
        //Canvas.SetActive(true);
    }
}
