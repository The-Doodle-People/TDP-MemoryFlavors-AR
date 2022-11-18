using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : MonoBehaviour
{
    public Animator FillCupAnimator; //public variable

    
    public void FillingMilk()
    {
        FillCupAnimator.SetBool("MilkPouring", true); // where i set the animation to be true after certain actions 

    }

    //teacher's method, preferred as it is more clean when there is alot more canvas 
    public void Interact()
    {
        //myAnimator.SetBool("IsWaving", true); // where i set the animation to be true after certain actions
        //Canvas.SetActive(true);
    }
}
