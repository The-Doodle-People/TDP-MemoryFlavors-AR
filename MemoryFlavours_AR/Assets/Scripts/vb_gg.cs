using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class vb_gg : MonoBehaviour
{

    public GameObject vbBtnGG;
    public Animator ggAnim;
    public AudioSource collectSound;
    public TextMeshProUGUI scoreText;
    public int playerScore;

    // Start is called before the first frame update
    void Start()
    {
        vbBtnGG = GameObject.Find("ggBtn");
        vbBtnGG.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnGG.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        ggAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        collectSound.Play();
        ggAnim.Play("ggAnim");
        ggAnim.Play("ggDoneAnim");
        playerScore += 1;
        scoreText.GetComponent<Text>().text = "INGREDIENTS: " + playerScore;
        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        ggAnim.Play("none");
        Debug.Log("Button Released");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
