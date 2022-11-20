/*
 * Author: Nurul Iffah Binte Mohammad Jailani, Nien-En Josephine Ng, Nomitha Velmurugan
 * Date: 29 Oct 2022    
 * Description: Unused script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardamomController : MonoBehaviour
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
