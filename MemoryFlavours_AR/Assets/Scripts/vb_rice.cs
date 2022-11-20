/*
 * Author: Nurul Iffah Binte Mohammad Jailani, Nien-En Josephine Ng, Nomitha Velmurugan
 * Date: 29 Oct 2022    
 * Description: Script for rice's virtual button
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_rice : MonoBehaviour
{
    public GameObject vbBtnRice; // rice virtual button
    public Animator riceAnim; //rice animator
    public AudioSource collectSound; //audio source of sound effect

    // Start is called before the first frame update
    void Start()
    {
        // to find the virtual button game object and register the virtual button behaviour
        vbBtnRice = GameObject.Find("riceBtn");
        vbBtnRice.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnRice.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        riceAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        // play the sound effect when collected
        collectSound.Play();

        // play animation of object
        riceAnim.Play("riceAnim");
        riceAnim.Play("riceDoneAnim");

        // to increase the score of objects collected
        TotalScore.totalScore += 1;

        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // to stop animation of object
        riceAnim.Play("none");
        Debug.Log("Button Released");
    }
}
