using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_chilli : MonoBehaviour
{
    public GameObject vbBtnChilli;
    public Animator chilliAnim;

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
        chilliAnim.Play("chilliAnim");
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
