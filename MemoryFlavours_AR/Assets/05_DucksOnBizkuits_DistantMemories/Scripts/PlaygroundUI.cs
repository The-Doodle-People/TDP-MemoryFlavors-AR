/*
 * Author: Chao Hao
 * Last Updated: 18/11/2022 
 * Description: Script to fade into the cmera refresh scene
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaygroundUI : MonoBehaviour
{
    public Animator canvasAnims;
    private static readonly int FadeIn = Animator.StringToHash("fadeIn");

    public void RefreshCamera()
    {
        canvasAnims.SetTrigger(FadeIn);
        FindObjectOfType<GameManager>().loadFadeOut = true;
    }
}