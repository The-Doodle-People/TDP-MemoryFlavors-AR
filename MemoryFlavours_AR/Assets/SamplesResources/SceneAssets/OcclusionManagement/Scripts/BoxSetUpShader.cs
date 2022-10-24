/*==============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
/// This script sets up shader variables for the occlusion shaders.
/// Different parameters are used for each device orientation.
/// </summary>
public class BoxSetUpShader : MonoBehaviour
{
    bool mVideoBackgroundReady;
    bool mShaderHasBeenSetup;
    bool mEnableLogging;
    Vector2 mTexureRatio;
    Vector2 mViewportSize;
    Vector2 mViewportOrig;
    Vector2 mPrefix;
    Vector2 mInversionMultiplier;
    Material mOcclusionMaterial;


    void Awake()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized += VuforiaInitialized;
    }

    void VuforiaInitialized(VuforiaInitError obj)
    {
        mOcclusionMaterial = GetComponent<Renderer>().material;
        VuforiaBehaviour.Instance.VideoBackground.OnVideoBackgroundChanged += OnVideoBackgroundChanged;
    }

    void OnDestroy()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized -= VuforiaInitialized;
        
        if (VuforiaBehaviour.Instance != null)
            VuforiaBehaviour.Instance.VideoBackground.OnVideoBackgroundChanged -= OnVideoBackgroundChanged;
    }
    
    public void OnVideoBackgroundChanged()
    {
        // If the video background config is changed, recalculate and pass new values to shader
        SetViewportParameters();
        UpdateOcclusionMaterial();
    }

    void SetViewportParameters()
    { 
          var vbTexture = VuforiaBehaviour.Instance.VideoBackground.VideoBackgroundTexture;
          var videoModeData = VuforiaBehaviour.Instance.CameraDevice.GetVideoMode();

          var imageSize = new Vector2(videoModeData.width, videoModeData.height);
          var textureSize = new Vector2(vbTexture.width, vbTexture.height);
          
          mTexureRatio.x = imageSize.x / (float)textureSize.x;
          mTexureRatio.y = imageSize.y / (float)textureSize.y;

          // update viewport size
          Rect viewport = VuforiaBehaviour.Instance.CameraDevice.GetVideoBackgroundRectInViewPort();

          mViewportOrig.x = viewport.xMin;
          mViewportOrig.y = viewport.yMin;
          mViewportSize.x = viewport.xMax - viewport.xMin;
          mViewportSize.y = viewport.yMax - viewport.yMin;

          bool isPortrait = (VuforiaRuntimeUtilities.ScreenOrientation == ScreenOrientation.Portrait ||
                           VuforiaRuntimeUtilities.ScreenOrientation == ScreenOrientation.PortraitUpsideDown );

          Shader.DisableKeyword(isPortrait ? "PORTRAIT_OFF" : "PORTRAIT_ON");
          Shader.EnableKeyword(isPortrait ? "PORTRAIT_ON" : "PORTRAIT_OFF");

          // determine for which orientation the shaders should be set up:
          switch (VuforiaRuntimeUtilities.ScreenOrientation)
          {
              case ScreenOrientation.Portrait:
                  mPrefix.x = 1.0f;
                  mPrefix.y = 1.0f;
                  mInversionMultiplier.x = -1.0f;
                  mInversionMultiplier.y = -1.0f;
                  break;
              case ScreenOrientation.PortraitUpsideDown:
                  mPrefix.x = 0.0f;
                  mPrefix.y = 0.0f;
                  mInversionMultiplier.x = 1.0f;
                  mInversionMultiplier.y = 1.0f;
                  break;
              case ScreenOrientation.LandscapeLeft:
                  mPrefix.x = 0.0f;
                  mPrefix.y = 1.0f;
                  mInversionMultiplier.x = 1.0f;
                  mInversionMultiplier.y = -1.0f;
                  break;
              case ScreenOrientation.LandscapeRight:
                  mPrefix.x = 1.0f;
                  mPrefix.y = 0.0f;
                  mInversionMultiplier.x = -1.0f;
                  mInversionMultiplier.y = 1.0f;
                  break;
          }

          // Pass the updated values to the shader
          mOcclusionMaterial.SetFloat("_TextureRatioX", mTexureRatio.x);
          mOcclusionMaterial.SetFloat("_TextureRatioY", mTexureRatio.y);
          mOcclusionMaterial.SetFloat("_ViewportSizeX", mViewportSize.x);
          mOcclusionMaterial.SetFloat("_ViewportSizeY", mViewportSize.y);
          mOcclusionMaterial.SetFloat("_ViewportOrigX", mViewportOrig.x);
          mOcclusionMaterial.SetFloat("_ViewportOrigY", mViewportOrig.y);
          mOcclusionMaterial.SetFloat("_ScreenWidth", Screen.width);
          mOcclusionMaterial.SetFloat("_ScreenHeight", Screen.height);
          mOcclusionMaterial.SetFloat("_PrefixX", mPrefix.x);
          mOcclusionMaterial.SetFloat("_PrefixY", mPrefix.y);
          mOcclusionMaterial.SetFloat("_InversionMultiplierX", mInversionMultiplier.x);
          mOcclusionMaterial.SetFloat("_InversionMultiplierY", mInversionMultiplier.y);
    }
    
    void UpdateOcclusionMaterial()
    {
        mOcclusionMaterial.mainTexture = VuforiaBehaviour.Instance.VideoBackground.VideoBackgroundTexture;
        SetViewportParameters();
    }
}
