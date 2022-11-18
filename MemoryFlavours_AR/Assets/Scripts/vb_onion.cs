using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_onion : MonoBehaviour
{
    public GameObject vbBtnOnion;
    public Animator onionAnim;

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
        onionAnim.Play("onionAnim");
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
