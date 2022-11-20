/*==============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/

using System;
using System.Timers;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

/// <summary>
/// Checks for Anchor Feature support. Handles DeviceObserver resetting.
/// Unconfigures anchors before reset.
/// </summary>
public class DevicePoseManager: MonoBehaviour
{
    [Serializable]
    public class DevicePoseResetEvent : UnityEvent { }

    public AnchorBehaviour AnchorBehaviour;
    public DevicePoseResetEvent DevicePoseReset;
    public TargetStatus TargetStatus = TargetStatus.NotObserved;

    const int RELOCALIZATION_TIMER = 10000;
    const string UNSUPPORTED_DEVICE_TITLE = "Unsupported Device";
    const string UNSUPPORTED_DEVICE_BODY = "This device does not support anchors and hit test functionality. " +
                                           "Please check the list of supported Ground Plane devices on our site: " +
                                           "\n\nhttps://library.vuforia.com/articles/Solution/ground-plane-supported-devices.html";

    Timer mTimer;
    bool mTimerFinished;

    void Start()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized += OnVuforiaInitialized;
        VuforiaBehaviour.Instance.DevicePoseBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;

        // Setup a timer to restart the DeviceTracker if tracking does not receive
        // status change from StatusInfo.RELOCALIZATION after 10 seconds.
        mTimer = new Timer(RELOCALIZATION_TIMER);
        mTimer.Elapsed += TimerFinished;
        mTimer.AutoReset = false;
    }

    void Update()
    {
        // The timer runs on a separate thread and we need to ResetTrackers on the main thread.
        if (mTimerFinished)
        {
            ResetDevicePose();
            DevicePoseReset?.Invoke();
            mTimerFinished = false;
        }
    }

    void OnDestroy()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized -= OnVuforiaInitialized;
        if (VuforiaBehaviour.Instance != null)
            VuforiaBehaviour.Instance.DevicePoseBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
    }

    /// <summary>
    /// This method stops and restarts the DevicePoseBehaviour.
    /// It is called by the UI Reset Button and when RELOCALIZATION status has
    /// not changed for 10 seconds.
    /// </summary>
    public void ResetDevicePose()
    {
        Debug.Log("ResetDevicePose() called.");

        // We should Unconfigure Anchor before resetting DeviceObserver. Because DevicePoseBehaviour.Reset()
        // will destroy the configured AnchorBehaviours in the scene which we don't want in this case.
        if (AnchorBehaviour != null)
            AnchorBehaviour.UnconfigureAnchor();

        VuforiaBehaviour.Instance.DevicePoseBehaviour.Reset();
    }


    /// <summary>
    /// This is a C# delegate method for the Timer:
    /// ElapsedEventHandler(object sender, ElapsedEventArgs e)
    /// </summary>
    /// <param name="source">System.Object</param>
    /// <param name="e">ElapsedEventArgs</param>
    void TimerFinished(System.Object source, ElapsedEventArgs e)
    {
        mTimerFinished = true;
    }

    void OnVuforiaInitialized(VuforiaInitError initError)
    {
        if (initError != VuforiaInitError.NONE)
            return;

        Debug.Log("OnVuforiaInitialized() called.");

        if (VuforiaBehaviour.Instance.World.AnchorsSupported)
        {
            if (!VuforiaBehaviour.Instance.DevicePoseBehaviour.enabled)
            {
                Debug.LogError("The Ground Plane feature requires the Device Tracking to be started. " +
                               "Please enable it in the Vuforia Configuration or start it at runtime through the scripting API.");
                return;
            }

            Debug.Log("DevicePoseBehaviour is Active");
        }
        else
        {
            MessageBox.DisplayMessageBox(UNSUPPORTED_DEVICE_TITLE, UNSUPPORTED_DEVICE_BODY, false, null);
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        Debug.Log("DevicePoseManager.OnTargetStatusChanged(" + targetStatus.Status + ", " + targetStatus.StatusInfo + ")");
        TargetStatus = targetStatus;
        if (targetStatus.StatusInfo != StatusInfo.RELOCALIZING)
        {
            // If the timer is running and the status is no longer Relocalizing, then stop the timer
            if (mTimer.Enabled)
                mTimer.Stop();
        }
        else
        {
            // Start a 10 second timer to Reset Device Tracker
            if (!mTimer.Enabled)
                mTimer.Start();
        }
    }
}