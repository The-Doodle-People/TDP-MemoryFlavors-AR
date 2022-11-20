using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_rice : MonoBehaviour
{
    public GameObject vbBtnRice;
    public Animator riceAnim;
    public AudioSource collectSound;
    public GameObject totalScore;
    //public TextMeshProUGUI scoreText;
    //public int riceScore;

    // Start is called before the first frame update
    void Start()
    {
        vbBtnRice = GameObject.Find("riceBtn");
        vbBtnRice.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnRice.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        riceAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        riceAnim.Play("riceAnim");
        riceAnim.Play("riceDoneAnim");
        collectSound.Play();
        TotalScore.totalScore += 1;
        //scoreText.GetComponent<Text>().text = "INGREDIENTS: " + playerScore;
        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        riceAnim.Play("none");
        Debug.Log("Button Released");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
