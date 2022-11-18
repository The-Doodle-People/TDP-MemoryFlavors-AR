using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillCup : MonoBehaviour
{
    public Animator FillCupAnimator; //public variable

    public GameObject nextButton;
    public void FillingCup()
    {
        FillCupAnimator.SetBool("IsFilling", true); // where i set the animation to be true after certain actions 
        if (!FillCupAnimator.GetCurrentAnimatorStateInfo(0).IsName("IsFilling"))
        {
            nextButton.SetActive(true);
        }
        
    }

    //teacher's method, preferred as it is more clean when there is alot more canvas 
    public void Interact()
    {
        //myAnimator.SetBool("IsWaving", true); // where i set the animation to be true after certain actions
        //Canvas.SetActive(true);
    }
}
