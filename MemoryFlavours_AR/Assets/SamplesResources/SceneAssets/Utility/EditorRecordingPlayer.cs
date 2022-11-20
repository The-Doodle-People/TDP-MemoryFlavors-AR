/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class EditorRecordingPlayer : MonoBehaviour
{
    public string BasePath = "Assets/SamplesResources/Recordings/";
    public string RecordingName;
    public bool UseWebCam;

    class SceneConfiguration
    {
        public PlayModeType OriginalPlayModeType;
        public string OriginalSequencePath;
    }

    static SceneConfiguration sOriginalConfiguration;

    void Awake()
    {
        // If we forced PlayMode to Webcam, consider if we need to revert it back to Recording
        if (VuforiaRuntimeUtilities.IsRecordingPlayMode())
        {
            // In case we haven't already reloaded our scene (with ReloadScene()), then we need to setup the recording
            // name, and Deinit Vuforia, so that upon Scene change, we can load our sequence
            if (VuforiaConfiguration.Instance.PlayMode.SequencePath != BasePath + RecordingName || UseWebCam)
                ConfigureScene();
            // To this point, you should only get, after you have your VuforiaConfiguration.Instance.PlayMode.SequencePath
            // set up, and after the Scene reload had happened
            else if (!VuforiaApplication.Instance.IsInitialized)
                VuforiaApplication.Instance.Initialize();
        }
        else if (VuforiaRuntimeUtilities.IsWebCamPlayMode() && IsConfigurationChanged())
        {
            if (!VuforiaApplication.Instance.IsInitialized)
                VuforiaApplication.Instance.Initialize();
        }
    }

    public void RestoreOriginalConfiguration()
    {
        if (!VuforiaRuntimeUtilities.IsPlayMode())
            return;

        // If configuration is not overridden there is nothing to do.
        if (!IsConfigurationChanged())
            return;

        VuforiaConfiguration.Instance.PlayMode.PlayModeType = sOriginalConfiguration.OriginalPlayModeType;
        VuforiaConfiguration.Instance.PlayMode.SequencePath = sOriginalConfiguration.OriginalSequencePath;
        if (VuforiaApplication.Instance.IsInitialized)
            VuforiaApplication.Instance.Deinit();

        // If enabled, engine will start again. Since we are exiting the scene, we don't want to do that.
        VuforiaBehaviour.Instance.enabled = false;
        VuforiaApplication.Instance.Initialize();
    }

    void ConfigureScene()
    {
        if (VuforiaApplication.Instance.IsInitialized)
            VuforiaApplication.Instance.Deinit();

        sOriginalConfiguration = new SceneConfiguration
        {
            OriginalSequencePath = VuforiaConfiguration.Instance.PlayMode.SequencePath,
            OriginalPlayModeType = VuforiaConfiguration.Instance.PlayMode.PlayModeType
        };

        if (UseWebCam)
        {
            Debug.LogWarning($"Recording and Playback is not supported in scene {SceneManager.GetActiveScene().name}." +
                             " Please test this scene on a mobile device for the best experience.");
            VuforiaConfiguration.Instance.PlayMode.PlayModeType = PlayModeType.WEBCAM;
        }
        else
        {
            Debug.LogWarning($"Selecting recording {VuforiaConfiguration.Instance.PlayMode.SequencePath} " +
                          $"for playback in scene {SceneManager.GetActiveScene().name}.");
            VuforiaConfiguration.Instance.PlayMode.SequencePath = BasePath + RecordingName;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }

    bool IsConfigurationChanged()
    {
        var currentConfiguration = new SceneConfiguration
        {
            OriginalSequencePath = VuforiaConfiguration.Instance.PlayMode.SequencePath,
            OriginalPlayModeType = VuforiaConfiguration.Instance.PlayMode.PlayModeType
        };

        return sOriginalConfiguration != null && sOriginalConfiguration != currentConfiguration;
    }
}