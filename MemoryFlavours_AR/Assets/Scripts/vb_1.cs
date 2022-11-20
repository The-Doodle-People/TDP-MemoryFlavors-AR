/*
 * Author: Nurul Iffah Binte Mohammad Jailani, Nien-En Josephine Ng, Nomitha Velmurugan
 * Date: 29 Oct 2022    
 * Description: Script for chicken's virtual button
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_1 : MonoBehaviour
{
    public GameObject vbBtnChicken; // chicken virtual button
    public Animator chickenAnim; // chicken animator
    public AudioSource collectSound; //audio source of sound effect


    // Start is called before the first frame update
    void Start()
    {
        // to find the virtual button game object and register the virtual button behaviour
        vbBtnChicken = GameObject.Find("chickenBtn");
        vbBtnChicken.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnChicken.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        chickenAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        // play the sound effect when collected
        collectSound.Play();

        // play animation of object
        chickenAnim.Play("chickAnim");
        chickenAnim.Play("chickDoneAnim");

        // to increase the score of objects collected
        TotalScore.totalScore += 1;

        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // to stop animation of object
        chickenAnim.Play("none");
        Debug.Log("Button Released");
    }
}
