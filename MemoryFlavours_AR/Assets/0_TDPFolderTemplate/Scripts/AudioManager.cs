using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager BgInstance;
    public static AudioSource BackgroundAudio;
    public static AudioSource StoreNoiseAudio;
    public UIController controller;
    public ScoreController game;

    private void Start()
    {
        BackgroundAudio = GetComponent<AudioSource>();
        StoreNoiseAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (game.gameOver == true && BackgroundAudio.isPlaying)
        {
            BackgroundAudio.Pause();
        }
        if (controller.uiActive == true && !StoreNoiseAudio.isPlaying)
        {
            StoreNoiseAudio.Play();
            Debug.Log("store noises is playing");
        }
    }
    // Update is called once per frame

}
