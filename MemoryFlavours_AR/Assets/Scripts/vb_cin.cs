using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_cin : MonoBehaviour
{

    public GameObject vbBtnCin;
    public Animator cinAnim;
    public AudioSource collectSound;
    //public GameObject totalScore;
    //public TextMeshProUGUI scoreText;
    //public int cinScore;

    // Start is called before the first frame update
    void Start()
    {
        vbBtnCin = GameObject.Find("cinBtn");
        vbBtnCin.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnCin.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        cinAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        collectSound.Play();
        cinAnim.Play("cinAnim");
        cinAnim.Play("cinDoneAnim");
        TotalScore.totalScore += 1;
        //scoreText.GetComponent<Text>().text = "INGREDIENTS: " + playerScore;
        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        cinAnim.Play("none");
        Debug.Log("Button Released");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
