using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class ImgSwipe : MonoBehaviour
{
    public Image one;
    public Image two;
    public Button next;


    public AudioSource oneAudio;    
    public AudioSource twoAudio;

    private void Awake()
    {
        Button nextBTM = next.GetComponent<Button>();

        nextBTM.onClick.AddListener(imagerotate);

        oneAudio = oneAudio.GetComponent<AudioSource>();
        twoAudio = twoAudio.GetComponent<AudioSource>();
    }

    public void imagerotate()
    {
        if (one.fillAmount == 0)
        {
            one.fillAmount = 1;
            oneAudio.Play();
        }
        else if (one.fillAmount == 1)
        {
            two.fillAmount = 1;
            twoAudio.Play();
        }
    }
}
