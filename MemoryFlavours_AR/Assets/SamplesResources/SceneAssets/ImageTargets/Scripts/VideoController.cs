/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[RequireComponent(typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    public Button PlayButton;
    public RectTransform ProgressBar;

    VideoPlayer mVideoPlayer;

    void Start()
    {
        mVideoPlayer = GetComponent<VideoPlayer>();

        // Setup Delegates
        mVideoPlayer.errorReceived += HandleMVideoError;
        mVideoPlayer.started += HandleStartedEvent;
        mVideoPlayer.prepareCompleted += HandlePrepareCompleted;
        mVideoPlayer.seekCompleted += HandleSeekCompleted;
        mVideoPlayer.loopPointReached += HandleLoopPointReached;

        LogClipInfo();
    }

    void Update()
    {
        if (mVideoPlayer.isPlaying)
        {
            ShowPlayButton(false);

            if (mVideoPlayer.frameCount < float.MaxValue)
            {
                var frame = (float) mVideoPlayer.frame;
                var count = (float) mVideoPlayer.frameCount;

                if (ProgressBar != null)
                {
                    var progressPercentage = count > 0 ? frame / count * 100.0f : 0f;
                    ProgressBar.sizeDelta = new Vector2(progressPercentage, ProgressBar.sizeDelta.y);
                }
            }

        }
        else
            ShowPlayButton(true);
    }
    
    void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause(" + pause + ") called.");
        if (pause)
            Pause();
    }
    
    public void Play()
    {
        Debug.Log("Play Video");
        PauseAudio(false);
        mVideoPlayer.Play();
        ShowPlayButton(false);
    }

    public void Pause()
    {
        if (mVideoPlayer)
        {
            Debug.Log("Pause Video");
            PauseAudio(true);
            mVideoPlayer.Pause();
            ShowPlayButton(true);
        }
    }
    
    void PauseAudio(bool pause)
    {
        for (ushort trackNumber = 0; trackNumber < mVideoPlayer.audioTrackCount; ++trackNumber)
        {
            if (pause)
                mVideoPlayer.GetTargetAudioSource(trackNumber).Pause();
            else
                mVideoPlayer.GetTargetAudioSource(trackNumber).UnPause();
        }
    }
    
    void ShowPlayButton(bool enable)
    {
        PlayButton.enabled = enable;
        PlayButton.GetComponent<Image>().enabled = enable;
    }

    void LogClipInfo()
    {
        if (mVideoPlayer.clip == null) 
            return;
        
        var clip = mVideoPlayer.clip;
        Debug.Log( "\nName: " + clip.name +
                   "\nAudioTracks: " + clip.audioTrackCount +
                   "\nFrames: " + clip.frameCount +
                   "\nFPS: " + clip.frameRate +
                   "\nHeight: " + clip.height +
                   "\nWidth: " + clip.width +
                   "\nLength: " + clip.length +
                   "\nPath: " + clip.originalPath);
    }

    void HandleMVideoError(VideoPlayer video, string errorMsg)
    {
        Debug.LogError("Error: " + video.clip.name + "\nError Message: " + errorMsg);
    }

    void HandleStartedEvent(VideoPlayer video)
    {
        Debug.Log("Started: " + video.clip.name);
    }

    void HandlePrepareCompleted(VideoPlayer video)
    {
        Debug.Log("Prepare Completed: " + video.clip.name);
    }

    void HandleSeekCompleted(VideoPlayer video)
    {
        Debug.Log("Seek Completed: " + video.clip.name);
    }

    void HandleLoopPointReached(VideoPlayer video)
    {
        Debug.Log("Loop Point Reached: " + video.clip.name);

        ShowPlayButton(true);
    }
}
