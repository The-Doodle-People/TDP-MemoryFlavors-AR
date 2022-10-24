/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using Vuforia;

namespace VFX
{
    /// <summary>
    /// A script that makes a game object to auto-rotate
    /// to remain camera facing (screen-aligned).
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        [Tooltip("Align the billboard to the world UP-vector.")]
        public bool AlignWorldUp = false;

        void Update()
        {
            if (!VuforiaApplication.Instance.IsRunning)
                return;

            var viewerCam = VuforiaCameraUtil.GetCamera();
            var viewDir = viewerCam.transform.forward;
            var upDir = AlignWorldUp ? Vector3.up : viewerCam.transform.up;
            transform.LookAt(transform.position + viewDir, upDir);
        }
    }
}