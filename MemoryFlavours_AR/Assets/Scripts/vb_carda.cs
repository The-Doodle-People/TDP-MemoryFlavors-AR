/*
 * Author: Nurul Iffah Binte Mohammad Jailani, Nien-En Josephine Ng, Nomitha Velmurugan
 * Date: 29 Oct 2022    
 * Description: Script for cardamom's virtual button
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_carda : MonoBehaviour
{
    public GameObject vbBtnCarda; // cardamom virtual button
    public Animator cardaAnim; //cardamom animator
    public AudioSource collectSound; //audio source of sound effect

    // Start is called before the first frame update
    void Start()
    {
        // to find the virtual button game object and register the virtual button behaviour
        vbBtnCarda = GameObject.Find("cardaBtn");
        vbBtnCarda.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnCarda.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        cardaAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        // play the sound effect when collected
        collectSound.Play();

        // play animation of object
        cardaAnim.Play("cardaAnim");
        cardaAnim.Play("cardaDoneAnim");

        // to increase the score of objects collected
        TotalScore.totalScore += 1;

        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // to stop animation of object
        cardaAnim.Play("none");
        Debug.Log("Button Released");
    }
}
