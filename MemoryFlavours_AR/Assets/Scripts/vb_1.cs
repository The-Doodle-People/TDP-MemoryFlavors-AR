using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_1 : MonoBehaviour
{
    public GameObject vbBtnChicken;
    public Animator chickenAnim;
    public AudioSource collectSound;
    public GameObject totalScore;
    //public TextMeshProUGUI scoreText;
    //public int chickScore;


    // Start is called before the first frame update
    void Start()
    {
        vbBtnChicken = GameObject.Find("chickenBtn");
        vbBtnChicken.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnChicken.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        chickenAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        collectSound.Play();
        chickenAnim.Play("chickAnim");
        chickenAnim.Play("chickDoneAnim");
        TotalScore.totalScore += 1;
        //scoreText.GetComponent<Text>().text = "INGREDIENTS: " + playerScore;
        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        chickenAnim.Play("none");
        Debug.Log("Button Released");
    }
}
