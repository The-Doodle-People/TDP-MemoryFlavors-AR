/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler which manages the instances of VuMark in the scene.
/// </summary>
public class VuMarkHandler : MonoBehaviour
{
    public AugmentationObject[] AugmentationObjects;

    /// <summary>
    /// Define the number of persistent child objects of the VuMarkBehaviour. When
    /// destroying the instance-specific augmentations, it will start after this value.
    /// Persistent Children:
    /// 1. Canvas -- displays info about the VuMark
    /// 2. LineRenderer -- displays border outline around VuMark
    /// </summary>
    const int PERSISTENT_NUMBER_OF_CHILDREN = 2;

    /// <summary>
    /// Cache VuMark texture information so we don't have to create a new texture everytime
    /// </summary>
    static Dictionary<string, Texture2D> sVuMarkInstanceTextures;
    Dictionary<string, GameObject> mVuMarkAugmentationObjects;

    void Start()
    {
        sVuMarkInstanceTextures = new Dictionary<string, Texture2D>();
        mVuMarkAugmentationObjects = new Dictionary<string, GameObject>();

        foreach (var augmentationObject in AugmentationObjects)
            mVuMarkAugmentationObjects.Add(augmentationObject.VuMarkID, augmentationObject.Augmentation);

        VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
        VuforiaApplication.Instance.OnVuforiaStopped += OnVuforiaStopped;
    }

    void OnVuforiaStarted()
    {
        VuforiaBehaviour.Instance.SetMaximumSimultaneousTrackedImages(10);
    }
    
    void OnVuforiaStopped()
    {
        VuforiaBehaviour.Instance.SetMaximumSimultaneousTrackedImages(4);
    }

    void OnDestroy()
    {
        VuforiaApplication.Instance.OnVuforiaStarted -= OnVuforiaStarted;
        VuforiaApplication.Instance.OnVuforiaStopped -= OnVuforiaStopped;
        
        if (VuforiaApplication.Instance.IsRunning)
            OnVuforiaStopped();
    }

    /// <summary>
    /// Registered to VuMark objects DefaultEventHandler.OnTargetFound.
    /// At this point an instance of VuMark is being tracked so content can be displayed.
    /// </summary>
    /// <param name="vuMarkBehaviour"></param>
    public void OnVuMarkFound(VuMarkBehaviour vuMarkBehaviour)
    {
        Debug.Log($"<color=cyan>VuMark: {vuMarkBehaviour.TargetName}, ID:{vuMarkBehaviour.InstanceId} tracked</color>" );

        // New VuMarks are created from a template behaviour, so we check if there are existing augmentations
        // attached to the current behaviour and destroy them before instantiating a new one.
        DestroyChildAugmentations(vuMarkBehaviour);

        // Get a copy of original augmentation. Assign it to VuMark as content
        var augmentation = GetVuMarkAugmentation(VuMarkUtilities.GetVuMarkId(vuMarkBehaviour));
        if (augmentation != null)
            augmentation.transform.SetParent(vuMarkBehaviour.transform, false);
    }

    /// <summary>
    /// Registered to VuMark objects DefaultEventHandler.OnTargetLost.
    /// </summary>
    /// <param name="vuMarkBehaviour"></param>
    public void OnVuMarkLost(VuMarkBehaviour vuMarkBehaviour)
    {
        Debug.Log($"<color=cyan>VuMark: {vuMarkBehaviour.TargetName}, ID:{vuMarkBehaviour.InstanceId} lost</color>" );

        DestroyChildAugmentations(vuMarkBehaviour);
    }

    public static string GetNumericVuMarkDescription(VuMarkBehaviour vuMarkTarget)
    {
        if (int.TryParse(VuMarkUtilities.GetVuMarkId(vuMarkTarget), NumberStyles.Integer, CultureInfo.InvariantCulture, out var vuMarkIdNumeric))
        {
            // Change the description based on the VuMark ID
            switch (vuMarkIdNumeric % 4)
            {
                case 1:
                    return "Astronaut";
                case 2:
                    return "Drone";
                case 3:
                    return "Fissure";
                case 0:
                    return "Oxygen Tank";
                default:
                    return "Unknown";
            }
        }

        return string.Empty; // if VuMark DataType is byte or string
    }

    /// <summary>
    /// Retrieves the VuMark texture from the cache if it exists. Otherwise creates the
    /// texture and caches it before returning.
    /// </summary>
    /// <param name="vuMarkTarget">VuMark target to retrieve texture</param>
    /// <returns></returns>
    public static Texture2D RetrieveStoredTextureForVuMarkTarget(VuMarkBehaviour vuMarkTarget)
    {
        Texture2D vuMarkTexture;
        sVuMarkInstanceTextures.TryGetValue( VuMarkUtilities.GetVuMarkId(vuMarkTarget), out vuMarkTexture);
        if (vuMarkTexture == null)
        {
            vuMarkTexture = VuMarkUtilities.GenerateTextureFromVuMarkInstanceImage(vuMarkTarget);
            sVuMarkInstanceTextures.Add(VuMarkUtilities.GetVuMarkId(vuMarkTarget), vuMarkTexture);
        }

        return vuMarkTexture;
    }

    GameObject GetVuMarkAugmentation(string vuMarkId)
    {
        GameObject sourceAugmentation;
        mVuMarkAugmentationObjects.TryGetValue(vuMarkId, out sourceAugmentation);

        if (sourceAugmentation == null)
            return null;

        return Instantiate(sourceAugmentation);
    }

    void DestroyChildAugmentations(VuMarkBehaviour vuMarkBehaviour)
    {
        // We don't want to destroy our common augmentations which we refer as PERSISTENT_NUMBER_OF_CHILDREN.
        // Those are 'Canvas' and 'LineRenderer'
        for (var x = PERSISTENT_NUMBER_OF_CHILDREN; x < vuMarkBehaviour.transform.childCount; x++)
            Destroy(vuMarkBehaviour.transform.GetChild(x).gameObject);
    }

}
