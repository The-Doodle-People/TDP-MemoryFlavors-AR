using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_cinnamon : MonoBehaviour
{
    public GameObject vbBtnCinnamon;
    public Animator cinnamonAnim;


    // Start is called before the first frame update
    void Start()
    {
        vbBtnCinnamon = GameObject.Find("cinnamonBtn");
        vbBtnCinnamon.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnCinnamon.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        vbBtnCinnamon.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        cinnamonAnim.Play("cinnamonAnim");
        Debug.Log("Cinnamon Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        cinnamonAnim.Play("none");
        Debug.Log("Button Released");
    }
 
}
