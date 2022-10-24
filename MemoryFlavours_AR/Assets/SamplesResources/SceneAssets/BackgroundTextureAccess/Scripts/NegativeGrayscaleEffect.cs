/*==============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
/// This script sets up the background shader effect
/// </summary>
public class NegativeGrayscaleEffect : MonoBehaviour
{
    Material mBackgroundMaterial;
    Material mDefaultMaterial;
    int mDefaultNumberOfDivisions;

    void Awake()
    {
        mDefaultMaterial = new Material(VuforiaConfiguration.Instance.VideoBackground.VideoBackgroundShader);
        mBackgroundMaterial = new Material(Resources.Load<Shader>("NegativeGrayscale"));
        
        mDefaultNumberOfDivisions = VuforiaConfiguration.Instance.VideoBackground.NumDivisions;
        VuforiaConfiguration.Instance.VideoBackground.NumDivisions = 20;
        
        VuforiaApplication.Instance.OnVuforiaStarted += VuforiaStarted;
        VuforiaApplication.Instance.OnVuforiaStopped += VuforiaStopped;
    }
    
    void VuforiaStarted()
    {
        VuforiaBehaviour.Instance.VideoBackground.SetVideoBackgroundMaterial(mBackgroundMaterial);
    }

    void VuforiaStopped()
    {
        VuforiaBehaviour.Instance.VideoBackground.SetVideoBackgroundMaterial(new Material(mDefaultMaterial));
    }

    void Update()
    {
        var touchX = 2.0f;
        var touchY = 2.0f;

        if (Input.GetMouseButton(0))
        {
            var touchPos = Input.mousePosition;
            // Adjust the touch point for the current orientation
            touchX = ((touchPos.x / Screen.width) - 0.5f) * 2.0f;
            touchY = ((touchPos.y / Screen.height) - 0.5f) * 2.0f;
        }

        if (mBackgroundMaterial)
        {
            // Pass the touch coordinates to the shader
            mBackgroundMaterial.SetFloat("_TouchX", touchX);
            mBackgroundMaterial.SetFloat("_TouchY", touchY);
        }
    }

    void OnDestroy()
    {
        VuforiaApplication.Instance.OnVuforiaStarted -= VuforiaStarted;
        VuforiaApplication.Instance.OnVuforiaStopped -= VuforiaStopped;
        
        VuforiaConfiguration.Instance.VideoBackground.NumDivisions = mDefaultNumberOfDivisions;
        
        if (VuforiaApplication.Instance.IsRunning)
            VuforiaStopped();
    }
}
