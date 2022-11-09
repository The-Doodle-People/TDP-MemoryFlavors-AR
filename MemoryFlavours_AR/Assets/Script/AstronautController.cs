using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautController : MonoBehaviour
{
    public GameObject canvas;

    // The animator component of the Astronaut object.
    public Animator myAnimator;

    // The function that will be called to set the IsWaving bool in the Animator to true.
    public void StartWave()
    {
        // Set the IsWaving bool to true here.
        myAnimator.SetBool("IsWaving", true);
    }

    public void Interact()
    {
        canvas.SetActive(true);
    }

   
}
