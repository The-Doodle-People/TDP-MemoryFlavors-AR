using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillMilkCup : MonoBehaviour
{
    public Animator FillMilkAnimator; //public variable

    public GameObject nextButton;
    public void FillingCup()
    {
        FillMilkAnimator.SetBool("IsFillingMilk", true); // where i set the animation to be true after certain actions 
        if (!FillMilkAnimator.GetCurrentAnimatorStateInfo(0).IsName("IsFillingMilk"))
        {
            nextButton.SetActive(true);
        }

    }
}
