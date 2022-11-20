/*
 * Author: Nurul Iffah Binte Mohammad Jailani, Nien-En Josephine Ng, Nomitha Velmurugan
 * Date: 29 Oct 2022    
 * Description: Script for mint leaves's virtual button
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_mint : MonoBehaviour
{
    public GameObject vbBtnMint; // mint leaves virtual button
    public Animator mintAnim; // mint leaves animator
    public AudioSource collectSound; //audio source of sound effect

    // Start is called before the first frame update
    void Start()
    {
        // to find the virtual button game object and register the virtual button behaviour
        vbBtnMint = GameObject.Find("mintBtn");
        vbBtnMint.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnMint.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        mintAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        // play the sound effect when collected
        collectSound.Play();

        // play animation of object
        mintAnim.Play("mintAnim");
        mintAnim.Play("mintDoneAnim");

        // to increase the score of objects collected
        TotalScore.totalScore += 1;

        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // to stop animation of object
        mintAnim.Play("none");
        Debug.Log("Button Released");
    }
}
