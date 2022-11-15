using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    public GameObject canvas;

    public Animator myAnimator;

    public void StartWave()
    {
        myAnimator.SetBool("IsMoving", true);
    }

    public void Interact()
    {
        canvas.SetActive(true);
    }
}
