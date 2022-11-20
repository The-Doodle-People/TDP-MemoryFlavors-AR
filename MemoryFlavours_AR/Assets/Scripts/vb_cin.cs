/*
 * Author: Nurul Iffah Binte Mohammad Jailani, Nien-En Josephine Ng, Nomitha Velmurugan
 * Date: 29 Oct 2022    
 * Description: Script for cinnamon's virtual button
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_cin : MonoBehaviour
{

    public GameObject vbBtnCin; // cinnamon virtual button
    public Animator cinAnim; // cinnamon animator
    public AudioSource collectSound; //audio source of sound effect

    // Start is called before the first frame update
    void Start()
    {
        // to find the virtual button game object and register the virtual button behaviour
        vbBtnCin = GameObject.Find("cinBtn");
        vbBtnCin.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnCin.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        cinAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        // play the sound effect when collected
        collectSound.Play();

        // play animation of object
        cinAnim.Play("cinAnim");
        cinAnim.Play("cinDoneAnim");

        // to increase the score of objects collected
        TotalScore.totalScore += 1;

        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // to stop animation of object
        cinAnim.Play("none");
        Debug.Log("Button Released");
    }
}
