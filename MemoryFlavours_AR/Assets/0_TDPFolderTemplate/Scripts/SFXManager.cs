using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource SFXCollectAudio;
    public AudioSource SFXTablePlaced;
    public AudioSource SFXTableBreak;
    public AudioSource SFXShocked;
    public AudioSource storeNoiseAudio;
    
    // Update is called once per frame
    public void CollectAudioOn()
    {
        SFXCollectAudio.Play();
    }

    public void TablePlacedAudioOn()
    {
        SFXTablePlaced.Play();
    }
    public void TableBreakAudioOn()
    {
        SFXTableBreak.Play();
    }
    public void ShockedAudioOn()
    {
        SFXShocked.Play();
    }

    public void StoreNoiseOn()
    {
        storeNoiseAudio.Play();
    }

    public void StoreNoiseOff()
    {
        storeNoiseAudio.Stop();
    }
}
