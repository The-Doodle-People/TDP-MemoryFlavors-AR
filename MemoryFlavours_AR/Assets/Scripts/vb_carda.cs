using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_carda : MonoBehaviour
{
    public GameObject vbBtnCarda;
    public Animator cardaAnim;
    public AudioSource collectSound;
    //public GameObject totalScore;
    //public TextMeshProUGUI scoreText;
    //public static int cardaScore;

    // Start is called before the first frame update
    void Start()
    {
        vbBtnCarda = GameObject.Find("cardaBtn");
        vbBtnCarda.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnCarda.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        cardaAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        collectSound.Play();
        cardaAnim.Play("cardaAnim");
        cardaAnim.Play("cardaDoneAnim");
        TotalScore.totalScore += 1;
        //scoreText.GetComponent<Text>().text = "INGREDIENTS: " + playerScore;
        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        cardaAnim.Play("none");
        Debug.Log("Button Released");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
