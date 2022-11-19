using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager BgInstance;
    public static AudioSource BackgroundAudio;
    public static AudioSource tablePlaced;
    
    public ScoreController game;

    private void Start()
    {
        BackgroundAudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (game.gameOver == true && BackgroundAudio.isPlaying)
        {
            BackgroundAudio.Pause();
        }
    }
    public void TablePlaced()
    {
        tablePlaced = GetComponent<AudioSource>();
        tablePlaced.Play();
    }
    // Update is called once per frame

}
