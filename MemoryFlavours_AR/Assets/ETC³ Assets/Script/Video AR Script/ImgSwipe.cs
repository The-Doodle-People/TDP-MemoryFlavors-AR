using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ImgSwipe : MonoBehaviour
{
    public Image one;
    public Image two;
    public Button next;
    public Button audioBTN;


    public AudioSource oneAudio;    
    public AudioSource twoAudio;

    private void Awake()
    {
        Button nextBTM = next.GetComponent<Button>();

        nextBTM.onClick.AddListener(imagerotate);

        Button play = audioBTN.GetComponent<Button>();

        play.onClick.AddListener(audioPlayer);
    }

    private void imagerotate()
    {
        if (one.fillAmount == 1)
        {
            two.fillAmount = 1;
            one.fillAmount = 0;
        }
    }


    private void audioPlayer()
    {
        if (one.fillAmount == 1 || two.fillAmount == 0)
        {
            oneAudio.Play();
            twoAudio.Stop();
        }

        if (two.fillAmount == 1 || one.fillAmount == 0)
        {
            oneAudio.Stop();
            twoAudio.Play();
        }
    }
}
