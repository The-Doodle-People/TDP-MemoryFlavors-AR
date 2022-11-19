using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private static AudioSource SFXCollectAudio;
    public AudioSource SFXTablePlaced;
    public AudioSource SFXTableBreak;
    public AudioSource SFXShocked;
    
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
}
