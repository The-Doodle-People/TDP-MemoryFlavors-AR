using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_carda : MonoBehaviour
{
    public GameObject vbBtnCarda;
    public Animator cardaAnim;

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
        cardaAnim.Play("cardaAnim");
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
