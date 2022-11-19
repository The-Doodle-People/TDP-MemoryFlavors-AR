using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private static AudioSource SFXCollectAudio;
    [SerializeField] private static AudioSource SFXTablePlaced;
    private void Start()
    {
        SFXCollectAudio = GetComponent<AudioSource>();
        SFXTablePlaced = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    public void CollectAudioOn()
    {
        SFXCollectAudio.Play();
    }

    public void TablePlacedAudioOn()
    {
        SFXTablePlaced.Play();   
    }
}
