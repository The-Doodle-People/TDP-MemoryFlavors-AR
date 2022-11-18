using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private static AudioSource SFXCollectAudio;
    private void Start()
    {
        SFXCollectAudio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    public void CollectAudioOn()
    {
        SFXCollectAudio.Play();
    }
}
