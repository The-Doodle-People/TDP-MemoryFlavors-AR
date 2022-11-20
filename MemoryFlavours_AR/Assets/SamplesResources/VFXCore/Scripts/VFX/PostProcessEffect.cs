/*========================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;

namespace VFX
{
    /// <summary>
    /// Component that applies a post-process effect onto the camera rendering,
    /// based on a custom material.
    /// This script must be attached to a Camera gameObject.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class PostProcessEffect : MonoBehaviour
    {
        public Material Material;

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (Material)
                Graphics.Blit(src, dest, Material);
        }
    }
}