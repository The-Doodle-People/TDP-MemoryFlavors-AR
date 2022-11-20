/*==============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
==============================================================================*/

using UnityEngine;
using UnityEngine.Events;
using Vuforia;

/// <summary>
/// This MonoBehaviour implements the Cloud Reco Event handling for this sample.
/// It registers itself at the CloudRecoBehaviour and is notified of new search results as well as error messages
/// The current state is visualized and new results are enabled using the TargetFinder API.
/// </summary>
public class CloudRecoEventHandler : MonoBehaviour
{
    CloudRecoBehaviour mCloudRecoBehaviour;
    
    /// <summary>
    /// Can be set in the Unity inspector to reference a ImageTargetBehaviour 
    /// that is used for augmentations of new cloud reco results.
    /// </summary>
    [Tooltip("Here you can set the ImageTargetBehaviour from the scene that will be used to " +
             "augment new cloud reco search results.")]
    public ImageTargetBehaviour ImageTargetBehaviour;
    public UnityEngine.UI.Image CloudActivityIcon;
    public UnityEngine.UI.Image CloudIdleIcon;

    public UnityEvent<CloudRecoBehaviour.CloudRecoSearchResult> OnTargetCreated = new UnityEvent<CloudRecoBehaviour.CloudRecoSearchResult>();

    /// <summary>
    /// Register for events at the CloudRecoBehaviour
    /// </summary>
    void Start()
    {
        // Register this event handler at the CloudRecoBehaviour
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        if (mCloudRecoBehaviour)
        {
            mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
            mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
            mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        }

        if (CloudActivityIcon)
            CloudActivityIcon.enabled = false;
    }

    void Update()
    {
        if (mCloudRecoBehaviour != null && mCloudRecoBehaviour.CloudRecoInitialized)
            SetCloudActivityIconVisible(mCloudRecoBehaviour.Requesting);

        if (CloudIdleIcon != null)
            CloudIdleIcon.color = mCloudRecoBehaviour != null && mCloudRecoBehaviour.RecoStarted ? Color.white : Color.gray;
    }

    /// <summary>
    /// Called when CloudRecoObserver has been initialized successfully
    /// </summary>
    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
    }

    /// <summary>
    /// when we start scanning, unregister Observer from the ImageTargetBehaviour, 
    /// then delete all Observers
    /// </summary>
    public void OnStateChanged(bool scanning)
    {
        Debug.Log("<color=blue>OnStateChanged(): </color>" + scanning);

        // Changing CloudRecoBehaviour.CloudRecoEnabled to false will call:
        // 1. CloudRecoObserver.Deactivate()
        // 2. All registered ICloudRecoEventHandler.OnStateChanged() with false.

        // Changing CloudRecoBehaviour.CloudRecoEnabled to true will call:
        // 1. CloudRecoObserver.Activate()
        // 2. All registered ICloudRecoEventHandler.OnStateChanged() with true.
    }

    /// <summary>
    /// Handles new search results
    /// </summary>
    /// <param name="cloudRecoSearchResult"></param>
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        Debug.Log("<color=blue>OnNewSearchResult(): </color>" + cloudRecoSearchResult.TargetName);

        // This code demonstrates how to reuse an ImageTargetBehaviour for new search results
        // and modifying it according to the metadata. Depending on your application, it can
        // make more sense to duplicate the ImageTargetBehaviour using Instantiate() or to
        // create a new ImageTargetBehaviour for each new result. Vuforia will return a new
        // object with the right script automatically if you use:
        // CloudRecoBehaviour.EnableTracking(CloudRecoSearchResult result, string gameObjectName)

        // Check if the metadata isn't null
        if (cloudRecoSearchResult.MetaData == null)
            Debug.Log("Target metadata not available.");
        else
        {
            Debug.Log("MetaData: " + cloudRecoSearchResult.MetaData);
            Debug.Log("TargetName: " + cloudRecoSearchResult.TargetName);
            Debug.Log("TrackingRating: " + cloudRecoSearchResult.TrackingRating);
            Debug.Log("UniqueTargetId: " + cloudRecoSearchResult.UniqueTargetId);
        }

        // Changing CloudRecoBehaviour.enabled to false will call CloudRecoObserver.Deactivate()
        // and also call all registered ICloudRecoEventHandler.OnStateChanged() with false.
        mCloudRecoBehaviour.enabled = false;
        // Clear any existing observers
        mCloudRecoBehaviour.ClearObservers(false);
        // Enable the new result with the same ImageTargetBehaviour:
        mCloudRecoBehaviour.EnableObservers(cloudRecoSearchResult, ImageTargetBehaviour.gameObject);
        // Pass the CloudRecoSearchResult to the ObserverEventHandler for processing
        OnTargetCreated?.Invoke(cloudRecoSearchResult);
    }
    
    void SetCloudActivityIconVisible(bool visible)
    {
        if (!CloudActivityIcon) 
            return;
        
        CloudActivityIcon.enabled = visible;
    }
}
