using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_gm : MonoBehaviour
{
    public GameObject vbBtnGM;
    public Animator gmAnim;

    // Start is called before the first frame update
    void Start()
    {
        vbBtnGM = GameObject.Find("gmBtn");
        vbBtnGM.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        vbBtnGM.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
        gmAnim.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        gmAnim.Play("gmAnim");
        Debug.Log("Button Pressed");

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        gmAnim.Play("none");
        Debug.Log("Button Released");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
