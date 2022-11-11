using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public bool enteredBowl = false;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");

        if (other.gameObject.tag == "bowlTrigger")
        {
            enteredBowl= true;
        }

    }
    
}
