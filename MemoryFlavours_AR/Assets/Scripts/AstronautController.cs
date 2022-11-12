using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautController : MonoBehaviour
{
    public GameObject canvas;

    public Animator myAnimator;

    public void StartWave()
    {
        myAnimator.SetBool("IsWaving", true);
    }

    public void Interact()
    {
        canvas.SetActive(true);
    }
}
