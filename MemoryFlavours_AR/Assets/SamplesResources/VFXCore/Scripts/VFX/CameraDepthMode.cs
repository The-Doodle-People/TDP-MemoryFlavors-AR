/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;

namespace VFX
{
    /// <summary>
    /// Component that enables selecting camera depth texture mode.
    /// This script must be attached to a Camera game-object.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraDepthMode : MonoBehaviour
    {
        [Tooltip("Camera depth texture mode.")]
        public DepthTextureMode depthMode = DepthTextureMode.Depth;
        
        void Awake()
        {
            // Turn on depth rendering for the camera,
            // so that the shader can access it via _CameraDepthTexture
            GetComponent<Camera>().depthTextureMode = depthMode;
        }
    }
}