using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_chilli : MonoBehaviour
{
    public GameObject vbBtnChilli;
    public Animator chilliAnim;
    public AudioSource collectSound;
    //public GameObject totalScore;
    //public TextMeshProUGUI scoreText;
    //public int chilliScore;

    // Start is called before the first frame update
    void Start()
    {
        vbBtnChilli = GameObject.Find("chilliBtn");
        vbBtnChilli.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnChilli.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        chilliAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        collectSound.Play();
        chilliAnim.Play("chilliAnim");
        chilliAnim.Play("chilliDoneAnim");
        TotalScore.totalScore += 1;
        //scoreText.GetComponent<Text>().text = "INGREDIENTS: " + playerScore;
        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        chilliAnim.Play("none");
        Debug.Log("Button Released");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
