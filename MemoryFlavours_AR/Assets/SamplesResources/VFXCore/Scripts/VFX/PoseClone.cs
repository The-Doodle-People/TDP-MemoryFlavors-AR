/*========================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;

namespace VFX
{
    public class PoseClone : MonoBehaviour
    {
        public Transform Source;
        public bool WorldUp;
        
        void Update()
        {
            if (!Source)
                return;

            var upDirection = Source.up;
            var forwardDirection = Source.forward;
            if (WorldUp)
            {
                upDirection = Vector3.up;
                forwardDirection.y = 0;
                forwardDirection.Normalize();
            }

            transform.SetPositionAndRotation(Source.position, Quaternion.LookRotation(forwardDirection, upDirection));
        }
    }
}