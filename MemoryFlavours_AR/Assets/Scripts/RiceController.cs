using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceController : MonoBehaviour
{
    public GameObject canvas;
    public Animator myAnimator;
    public AudioSource collectSound;

    public void StartAnimate()
    {
        myAnimator.SetBool("HasPressed", true);
        collectSound.Play();
    }
    public void Interact()
    {
        canvas.SetActive(true);
    }
}
