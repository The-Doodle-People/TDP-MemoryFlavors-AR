using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_cin : MonoBehaviour
{

    public GameObject vbBtnCin;
    public Animator cinAnim;

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
        cinAnim.Play("cinAnim");
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
