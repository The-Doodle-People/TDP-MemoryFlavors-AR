using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_onion : MonoBehaviour
{
    public GameObject vbBtnOnion;
    public Animator onionAnim;
    public AudioSource collectSound;
    //public GameObject totalScore;
    //public TextMeshProUGUI scoreText;
    //public int onionScore;

    // Start is called before the first frame update
    void Start()
    {
        vbBtnOnion = GameObject.Find("onionBtn");
        vbBtnOnion.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnOnion.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        onionAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        collectSound.Play();
        onionAnim.Play("onionAnim");
        onionAnim.Play("onionDoneAnim");
        TotalScore.totalScore += 1;
        //scoreText.GetComponent<Text>().text = "INGREDIENTS: " + playerScore;
        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        onionAnim.Play("None");
        Debug.Log("Button Released");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
