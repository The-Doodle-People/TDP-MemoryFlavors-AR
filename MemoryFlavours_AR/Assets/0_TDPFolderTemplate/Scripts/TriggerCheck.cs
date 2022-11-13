using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public bool flourEnteredBowl = false;
    
    Rigidbody rb;
    public Transform flourHoldArea;
    public Transform saltSugarHoldArea;
    
    
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
