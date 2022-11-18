using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandung : MonoBehaviour
{
    public Animator BandungAnimator; //public variable

    public GameObject Canvas;
    public void StartPour()
    {
        BandungAnimator.SetBool("IsPouring", true); // where i set the animation to be true after certain actions 

    }

    //teacher's method, preferred as it is more clean when there is alot more canvas 
    public void Interact()
    {
        //myAnimator.SetBool("IsWaving", true); // where i set the animation to be true after certain actions
        //Canvas.SetActive(true);
    }
}
