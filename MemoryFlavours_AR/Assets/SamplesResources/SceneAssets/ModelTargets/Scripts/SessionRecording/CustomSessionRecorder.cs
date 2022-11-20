/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using System;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomSessionRecorder : MonoBehaviour
{
    public GameObject RecordButton;
    public GameObject StopButton;
    public GameObject ShareButton;
    public GameObject InfoPopup;
    public Text Timer;
    public Text InfoText;
    
    const string TIMER_ZERO = "00:00";
    const string TIMER_FORMAT_MMSS = @"mm\:ss";
    const string TIMER_FORMAT_HHMMSS = @"hh\:mm\:ss";
    const string ERROR_PREFIX_COULD_NOT_START_RECORDING = "Could not start recording: ";
    const string ERROR_PREFIX_RECORDING_SHUTDOWN = "Recording stopped: ";
    const string ERROR_SOURCE_NOT_AVAILABLE = "sources not available.";
    const string ERROR_NO_VALID_PATH = "unable to retrieve a valid path to store recording data.";
    const string ERROR_SOURCE_OPERATION = "source operation error.";
    const string ERROR_NO_DISC_SPACE = "insufficient free disk space.";
    const string ERROR_UNKNOWN_ORIENTATION = "unknown device orientation.";
    const string ERROR_ORIENTATION_CHANGED = "cannot change orientation while recording.";
    const string ERROR_ANOTHER_RECORDING_RUNNING = "another recording is in progress.";
    const string ERROR_EDITOR_PLAY_MODE = "recording is not supported from Unity Editor Play Mode.";
    const string ERROR_SHARE_FAILED = "Error while sharing the recording.";
    const string ERROR_UNKNOWN = "unknown error. Please check the log for more info.";
    const string INFO_RECORDING_SAVED = "Vuforia session recording saved.";
    const string INFO_EXITING_RECORDING_DELETED = "All the existing recordings have been deleted.";
    const string INFO_COULD_NOT_DELETE_RECORDING = "Could not delete previous recordings. Please check the log for more info.";

    SessionRecorderBehaviour mRecorder;
    DateTime mStartTime;

    void Awake()
    {
        mRecorder = GetComponent<SessionRecorderBehaviour>();
    }

    void Update()
    {
        if (mRecorder.GetRecordingStatus() == RecordingStatus.RUNNING)
            UpdateTimer();
    }


    public void RecordingStarted(RecordingStartError error)
    {
        switch (error)
        {
            case RecordingStartError.NONE:
                RecordButton.SetActive(false);
                StopButton.SetActive(true);
                StartTimer();
                break;
            case RecordingStartError.DATA_FLAGS_NOT_SUPPORTED:
                ShowInfoPopup(ERROR_PREFIX_COULD_NOT_START_RECORDING + ERROR_SOURCE_NOT_AVAILABLE);
                break;
            case RecordingStartError.FILE_CREATION:
                ShowInfoPopup(ERROR_PREFIX_COULD_NOT_START_RECORDING + ERROR_NO_VALID_PATH);
                break;
            case RecordingStartError.DATA_SOURCE:
                ShowInfoPopup(ERROR_PREFIX_COULD_NOT_START_RECORDING + ERROR_SOURCE_OPERATION);
                break;
            case RecordingStartError.INSUFFICIENT_FREE_SPACE:
                ShowInfoPopup(ERROR_PREFIX_COULD_NOT_START_RECORDING + ERROR_NO_DISC_SPACE);
                break;
            case RecordingStartError.UNKNOWN_ORIENTATION:
                ShowInfoPopup(ERROR_PREFIX_COULD_NOT_START_RECORDING + ERROR_UNKNOWN_ORIENTATION);
                break;
            case RecordingStartError.ANOTHER_RECORDING_RUNNING:
                ShowInfoPopup(ERROR_PREFIX_COULD_NOT_START_RECORDING + ERROR_ANOTHER_RECORDING_RUNNING);
                break;
            case RecordingStartError.UNITY_PLAYMODE_NOT_SUPPORTED:
                ShowInfoPopup(ERROR_PREFIX_COULD_NOT_START_RECORDING + ERROR_EDITOR_PLAY_MODE);
                break;
            default:
                ShowInfoPopup(ERROR_PREFIX_COULD_NOT_START_RECORDING + ERROR_UNKNOWN);
                break;
        }
    }

    public void RecordingStopped(RecordingStatusInfo info)
    {
        switch (info)
        {
            case RecordingStatusInfo.SUCCESS:
                ShowShareDialog();
                break;
            case RecordingStatusInfo.ERROR_DATA_SOURCE:
                ShowInfoPopup(ERROR_PREFIX_RECORDING_SHUTDOWN + ERROR_SOURCE_OPERATION);
                break;
            case RecordingStatusInfo.ERROR_INSUFFICIENT_FREE_SPACE:
                ShowInfoPopup(ERROR_PREFIX_RECORDING_SHUTDOWN + ERROR_NO_DISC_SPACE);
                break;
            case RecordingStatusInfo.ERROR_ORIENTATION_CHANGED:
                ShowInfoPopup(ERROR_PREFIX_RECORDING_SHUTDOWN + ERROR_ORIENTATION_CHANGED);
                break;
            default:
                ShowInfoPopup(ERROR_PREFIX_RECORDING_SHUTDOWN + ERROR_UNKNOWN);
                break;

        }
        
        RecordButton.SetActive(true);
        StopButton.SetActive(false);
        StopTimer();
    }

    public void StorageCleaned(bool cleaned)
    {
        if (cleaned)
            ShowInfoPopup(INFO_EXITING_RECORDING_DELETED);
        else
            ShowInfoPopup(INFO_COULD_NOT_DELETE_RECORDING);
    }

    public void ShareRequested(bool success)
    {
        if (!success)
            ShowInfoPopup(ERROR_SHARE_FAILED);
    }

    void ShowInfoPopup(string message)
    {
        InfoText.text = message;
        ShareButton.SetActive(false);
        InfoPopup.SetActive(true);
    }

    void ShowShareDialog()
    {
        InfoText.text = INFO_RECORDING_SAVED;
        ShareButton.SetActive(true);
        InfoPopup.SetActive(true);
    }

    void StartTimer()
    {
        Timer.gameObject.SetActive(true);
        mStartTime = DateTime.Now;
        Timer.text = TIMER_ZERO;
    }

    void UpdateTimer()
    {
        var deltaTime = DateTime.Now - mStartTime;
        Timer.text = deltaTime.ToString(deltaTime.Hours < 1 ? TIMER_FORMAT_MMSS : TIMER_FORMAT_HHMMSS);
    }

    void StopTimer()
    {
        Timer.gameObject.SetActive(false);
        Timer.text = TIMER_ZERO;
    }
}
