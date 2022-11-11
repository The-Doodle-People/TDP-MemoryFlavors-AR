using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class Video : MonoBehaviour
{

    public Material playButtonMaterial;
    public Material pauseButtonMaterial;
    public Renderer playButtonRenderer;
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            audioSource.Pause();
            playButtonRenderer.material = playButtonMaterial;
        }
        else
        {
            videoPlayer.Play();
            audioSource.Play();
            playButtonRenderer.material = pauseButtonMaterial;
        }

        Debug.Log("Play Click!!");
    }

    public void Play()
    {
        Debug.Log("Play");
        playButtonRenderer.material = pauseButtonMaterial;
    }
}