/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using Vuforia;

namespace VFX
{
    public static class VuforiaCameraUtil
    {
        static Camera mVuforiaBehaviourCamera;
        public static Camera GetCamera()
        {
            if (mVuforiaBehaviourCamera == null)
                mVuforiaBehaviourCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
            return mVuforiaBehaviourCamera;
        }
    }
}