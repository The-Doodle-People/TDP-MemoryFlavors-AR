using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_1 : MonoBehaviour
{
    public GameObject vbBtnChicken;
    public Animator chickenAnim;


    // Start is called before the first frame update
    void Start()
    {
        vbBtnChicken = GameObject.Find("chickenBtn");
        vbBtnChicken.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnChicken.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        chickenAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        chickenAnim.Play("chickAnim");
        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        chickenAnim.Play("none");
        Debug.Log("Button Released");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
