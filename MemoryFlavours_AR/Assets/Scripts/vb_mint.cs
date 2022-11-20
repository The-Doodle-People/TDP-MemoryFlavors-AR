using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_mint : MonoBehaviour
{
    public GameObject vbBtnMint;
    public Animator mintAnim;
    public AudioSource collectSound;
    //public GameObject totalScore;
    //public TextMeshProUGUI scoreText;
    //public int mintScore;

    // Start is called before the first frame update
    void Start()
    {
        vbBtnMint = GameObject.Find("mintBtn");
        vbBtnMint.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnMint.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        mintAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        collectSound.Play();
        mintAnim.Play("mintAnim");
        mintAnim.Play("mintDoneAnim");
        TotalScore.totalScore += 1;
        //scoreText.GetComponent<Text>().text = "INGREDIENTS: " + playerScore;
        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        mintAnim.Play("none");
        Debug.Log("Button Released");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
