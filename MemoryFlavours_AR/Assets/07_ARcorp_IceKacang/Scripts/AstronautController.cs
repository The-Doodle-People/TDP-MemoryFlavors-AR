using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautController : MonoBehaviour
{
    ///Animator component for astronaut object
    public Animator myAnimator;

    //function to call when iswaving bool in animator = true
    public void StartWave()
    {
        myAnimator.SetBool("IsWaving", true);
    }
}
