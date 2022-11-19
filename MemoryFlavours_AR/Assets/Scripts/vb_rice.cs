using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_rice : MonoBehaviour
{
    public GameObject vbBtnRice;
    public Animator riceAnim;

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
