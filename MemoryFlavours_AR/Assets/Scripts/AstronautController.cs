using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautController : MonoBehaviour
{
    public Animator myAnimator;
    public GameObject canvas;

    public void StartWave()
    {
        myAnimator.SetBool("IsWaving", true);
    }

    public void Interact()
    {
        canvas.SetActive(true);
    }
}
