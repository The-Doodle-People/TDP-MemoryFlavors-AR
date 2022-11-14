using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCheck : MonoBehaviour
{
    public bool flourEnteredBowl = false;
  
    
    Rigidbody rb;
    public Transform flourHoldArea;
    public Transform saltSugarHoldArea;
    public TMP_Text step1b;
    
    public GameObject mixingUI;
    public GameObject mixingSlider;
    
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");

        if (other.gameObject.tag == "bowlTrigger" )
        {
            if (gameObject.tag == "flour")
            {
                flourEnteredBowl = true;
                adjustComponents();
                rb.transform.parent = flourHoldArea;
                gameObject.transform.position = flourHoldArea.position;
                
            }
           
            if(gameObject.tag == "saltnSugar")
            {
                
                adjustComponents();
                rb.transform.parent = saltSugarHoldArea;
                gameObject.transform.position = saltSugarHoldArea.position;
                step1b.text = "tap all the buttons to mix the dry ingredients";
               

                mixingUI.SetActive(true);
                mixingSlider.SetActive(true);
            }


        }
       

    }

    void adjustComponents()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.drag = 10;
        Destroy(gameObject.GetComponent<Lean.Touch.LeanDragTranslate>());
        Destroy(rb);
    }
    
}
