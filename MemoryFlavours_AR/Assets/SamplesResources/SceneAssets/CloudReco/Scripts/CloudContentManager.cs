/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CloudContentManager : MonoBehaviour
{
    [SerializeField] Transform CloudTarget = null;
    [SerializeField] UnityEngine.UI.Text CloudTargetInfo = null;

    [System.Serializable]
    public class AugmentationObject
    {
        public string TargetName;
        public GameObject Augmentation;
    }

    public AugmentationObject[] AugmentationObjects;
    readonly string[] mStarRatings = { "☆☆☆☆☆", "★☆☆☆☆", "★★☆☆☆", "★★★☆☆", "★★★★☆", "★★★★★" };
    Dictionary<string, GameObject> mAugmentations;
    Transform mContentManagerParent;
    Transform mCurrentAugmentation;
    
    void Start()
    {
        mAugmentations = new Dictionary<string, GameObject>();

        for (var a = 0; a < AugmentationObjects.Length; ++a)
            mAugmentations.Add(AugmentationObjects[a].TargetName, AugmentationObjects[a].Augmentation);
    }

    public void ShowTargetInfo(bool showInfo)
    {
        if (CloudTargetInfo != null)
        {
            var canvas = CloudTargetInfo.GetComponentInParent<Canvas>();
            canvas.enabled = showInfo;
        }
    }

    public void HandleTargetFinderResult(CloudRecoBehaviour.CloudRecoSearchResult targetSearchResult)
    {
        Debug.Log("<color=blue>HandleTargetFinderResult(): " + targetSearchResult.TargetName + "</color>");

        CloudTargetInfo.text =
            "Name: " + targetSearchResult.TargetName +
            "\nRating: " + mStarRatings[targetSearchResult.TrackingRating] +
            "\nMetaData: " + (targetSearchResult.MetaData.Length > 0 ? targetSearchResult.MetaData : "No") +
            "\nTarget Id: " + targetSearchResult.UniqueTargetId;

        GameObject augmentation = null;
        mAugmentations.TryGetValue(targetSearchResult.TargetName, out augmentation);

        if (augmentation == null) 
            return;

        if (augmentation.transform.parent == CloudTarget.transform) 
            return;
        
        Renderer[] augmentationRenderers;

        if (mCurrentAugmentation != null && mCurrentAugmentation.parent == CloudTarget)
        {
            mCurrentAugmentation.SetParent(mContentManagerParent);
            mCurrentAugmentation.transform.localPosition = Vector3.zero;

            augmentationRenderers = mCurrentAugmentation.GetComponentsInChildren<Renderer>();
            foreach (var renderer in augmentationRenderers)
            {
                renderer.gameObject.layer = LayerMask.NameToLayer("UI");
                renderer.enabled = true;
            }
        }

        // store reference to content manager's parent object of the augmentation
        mContentManagerParent = augmentation.transform.parent;
        // store reference to the current augmentation
        mCurrentAugmentation = augmentation.transform;

        // set new target augmentation parent to cloud target
        augmentation.transform.SetParent(CloudTarget);
        augmentation.transform.localPosition = Vector3.zero;

        augmentationRenderers = augmentation.GetComponentsInChildren<Renderer>();
        foreach (var renderer in augmentationRenderers)
        {
            renderer.gameObject.layer = LayerMask.NameToLayer("Default");
            renderer.enabled = true;
        }
    }
}
