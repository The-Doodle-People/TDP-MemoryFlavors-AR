using System.Collections;
using System.Collections.Generic;
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