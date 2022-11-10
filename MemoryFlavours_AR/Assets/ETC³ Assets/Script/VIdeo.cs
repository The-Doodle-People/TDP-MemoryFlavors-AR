using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VIdeo : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer video;

    public GameObject Video;

    private void Awake()
    {
        VideoPlayBack();
    }

    public void VideoPlayBack()
    {
        video.Play();
        Video.SetActive(true);
    }
}
