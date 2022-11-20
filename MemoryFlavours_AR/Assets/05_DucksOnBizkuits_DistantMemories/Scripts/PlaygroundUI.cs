/*
 * Author: Chao Hao
 * Last Updated: 18/11/2022 
 * Description: Script to fade into the camera refresh scene
 */

using System.Collections;
using TMPro;
using UnityEngine;

public class PlaygroundUI : MonoBehaviour
{
    public Animator canvasAnims;
    private static readonly int FadeIn = Animator.StringToHash("fadeIn");

    public TextMeshProUGUI responseText;

    public void RefreshCamera()
    {
        canvasAnims.SetTrigger(FadeIn);
        FindObjectOfType<GameManager>().loadFadeOut = true;
    }

    public void StartFade()
    {
        responseText.gameObject.SetActive(true);
        responseText.color = new Color(255, 255, 255, 162);
        
        StopCoroutine(ChildToggleResponse());
        StartCoroutine(ChildToggleResponse());
    }

    private IEnumerator ChildToggleResponse()
    {
        yield return new WaitForSeconds(2f);

        responseText.gameObject.SetActive(false);
    }
}