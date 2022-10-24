/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.Collections;
using UnityEngine;
using Vuforia;

public class CloudObserverEventHandler : DefaultObserverEventHandler
{
    CloudRecoBehaviour mCloudRecoBehaviour;
    CloudContentManager mCloudContentManager;

    Coroutine mResetCloudRecoCoroutine;

    protected override void Start()
    {
        base.Start();
        mCloudRecoBehaviour = FindObjectOfType<CloudRecoBehaviour>();
        mCloudContentManager = FindObjectOfType<CloudContentManager>();

        mObserverBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    public void OnReset()
    {
        Debug.Log("<color=blue>OnReset()</color>");
        OnTrackingLost();
        mCloudRecoBehaviour.ClearObservers(false);
    }
    
    /// <summary>
    /// Method called from the CloudRecoEventHandler
    /// when a new target is created
    /// </summary>
    public void TargetCreated(CloudRecoBehaviour.CloudRecoSearchResult targetSearchResult)
    {
        mCloudContentManager.HandleTargetFinderResult(targetSearchResult);
    }
    
    protected override void OnTrackingFound()
    {
        Debug.Log("<color=blue>OnTrackingFound()</color>");
        base.OnTrackingFound();
        if (mCloudRecoBehaviour)
            // Changing CloudRecoBehaviour.enabled to true will call CloudRecoObserver.Deactivate()
            // and also call all registered ICloudRecoEventHandler.OnStateChanged() with false.
            mCloudRecoBehaviour.enabled = false;

        if (mCloudContentManager)
            mCloudContentManager.ShowTargetInfo(true);
    }

    protected override void OnTrackingLost()
    {
        Debug.Log("<color=blue>OnTrackingLost()</color>");
        base.OnTrackingLost();
        if (mCloudRecoBehaviour)
            // Changing CloudRecoBehaviour.enabled to true will call CloudRecoObserver.Activate()
            // and also call all registered ICloudRecoEventHandler.OnStateChanged() with true.
            mCloudRecoBehaviour.enabled = true;

        if (mCloudContentManager)
            mCloudContentManager.ShowTargetInfo(false);
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            // Start coroutine to check if the target is EXTENDED_TRACKED for more than 3 seconds.
            // Reset Cloud Reco if that is the case
            if (mResetCloudRecoCoroutine == null)
                mResetCloudRecoCoroutine = StartCoroutine(ResetCloudReco());
        }
        if (targetStatus.Status == Status.TRACKED)
        {
            if (mResetCloudRecoCoroutine != null)
            {
                StopCoroutine(mResetCloudRecoCoroutine);
                mResetCloudRecoCoroutine = null;
            }
        }
    }

    IEnumerator ResetCloudReco()
    {
        // Set up a timer to reenable cloud reco scanning after a target is lost for 3 seconds
        yield return new WaitForSeconds(3f);

        OnReset();
    }
}
