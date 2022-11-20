/*========================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using System.Collections;
using UnityEngine;

namespace VFX
{
    public class LookAtTarget : MonoBehaviour
    {
        [Tooltip("The target transform.")]
        public Transform Target;

        [Tooltip("Offset from target.")]
        public Vector3 Offset = Vector3.up * 10;
        
        [Tooltip("Should this object follow the target rotation.")]
        public bool FollowTargetRotation = true;

        [Tooltip("Should motion be smoothed over time.")]
        public bool SmoothMotion = true;

        [Tooltip("Motion smoothness factor.")]
        [Range(0, 1)]
        public float Smoothness = 0.5f;
        
        Vector3 mTargetPos = Vector3.zero;
        Vector3 mTargetForward = Vector3.forward;
        Vector3 mTargetRight = Vector3.right;
        Vector3 mTargetUp = Vector3.up;

        const float MAX_SMOOTH_DISTANCE = 2.0f;
        const float FLY_DURATION = 1.5f;
        
        void Start()
        {
            mTargetPos = Target.position;
            mTargetForward = Target.forward;
            mTargetRight = Target.right;
        }

        void Update()
        {
            var u = SmoothMotion ? (4 * (1 - Smoothness) * Time.smoothDeltaTime) : 1;
            u = Mathf.Clamp(u, 0.01f, 0.99f);
            mTargetPos = Vector3.Lerp(mTargetPos, Target.position, u);
            mTargetForward = Vector3.Lerp(mTargetForward, Target.forward, u);
            mTargetRight = Vector3.Lerp(mTargetRight, Target.right, u);
            mTargetUp = Vector3.Lerp(mTargetUp, Target.up, u);

            if (Vector3.Distance(mTargetPos, Target.position) > MAX_SMOOTH_DISTANCE)
            {
                mTargetPos = Target.position;
                mTargetForward = Target.forward;
                mTargetRight = Target.right;
            }
            
            var toTarget = (mTargetPos - transform.position).normalized;
            // Compute position at Offset from target
            var offsetPos = mTargetPos + Offset.x * Vector3.right + Offset.y * Vector3.up + Offset.z * Vector3.forward;
            // Compute UP dir
            var upDir = Vector3.Cross(toTarget, Vector3.right).normalized;

            if (FollowTargetRotation)
            {
                offsetPos = mTargetPos + Offset.x * mTargetRight + Offset.y * mTargetUp + Offset.z * mTargetForward;
                upDir = Vector3.Cross(toTarget, mTargetRight).normalized;
            }
            
            transform.position = offsetPos;
            transform.rotation = Quaternion.LookRotation(toTarget, upDir);
        }


        public void FlyToOffset(Transform offsetTransform)
        {
            var offset = Target.InverseTransformPoint(offsetTransform.position);
            FlyToOffset(offset);
        }

        public void FlyToOffset(string offsetString)
        {
            var comps = offsetString.Split(',');
            if (comps == null || comps.Length < 3)
                return;

            if (!float.TryParse(comps[0].Trim(), out var x))
                return;

            if (!float.TryParse(comps[1].Trim(), out var y))
                return;

            if (!float.TryParse(comps[2].Trim(), out var z))
                return;

            FlyToOffset(new Vector3(x, y, z));
        }

        public void FlyToOffset(Vector3 offset)
        {
            StartCoroutine(FlyToOffsetCoroutine(offset));
        }


        IEnumerator FlyToOffsetCoroutine(Vector3 offset)
        {
            var startOffset = Offset;
            float smoothTime = 0;
            while (smoothTime < FLY_DURATION)
            {
                var u = Mathf.Clamp01(smoothTime / FLY_DURATION);
                Offset = Vector3.Lerp(startOffset, offset, u);
                smoothTime += Time.smoothDeltaTime;
                yield return null;
            }
            Offset = offset;
        }
    }
}
