/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class ReloadSceneWithRecording : MonoBehaviour
{
    public string BasePath = "Assets/SamplesResources/Recordings/";
    public string RecordingName;
    
    void Awake()
    {
        if (Application.isEditor)
        {
            // In case of delayed initialization, we just init with our own recording
            if (VuforiaConfiguration.Instance.Vuforia.DelayedInitialization)
                LoadSceneWithRecording();
            // In case we haven't already reloaded our scene (with ReloadScene()), then we need to setup the recording
            // name, and Deinit Vuforia, so that upon Scene change, we can load our sequence
            else if (VuforiaConfiguration.Instance.PlayMode.SequencePath != BasePath + RecordingName)
                VuforiaApplication.Instance.OnVuforiaInitialized += ReloadScene;
            // To this point, you should only get, after you have your VuforiaConfiguration.Instance.PlayMode.SequencePath
            // set up, and after the Scene reload had happened
            else if (!VuforiaApplication.Instance.IsInitialized)
                LoadSceneWithRecording();
        }
    }

    void ReloadScene(VuforiaInitError initError)
    {
        VuforiaApplication.Instance.OnVuforiaInitialized -= ReloadScene;
        if (VuforiaApplication.Instance.IsRunning)
            VuforiaApplication.Instance.Deinit();
        VuforiaConfiguration.Instance.PlayMode.SequencePath = BasePath + RecordingName;
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }  

    void LoadSceneWithRecording()
    {
        VuforiaConfiguration.Instance.PlayMode.SequencePath = BasePath + RecordingName;
        if (!VuforiaApplication.Instance.IsInitialized)
            VuforiaApplication.Instance.Initialize();
    }
}