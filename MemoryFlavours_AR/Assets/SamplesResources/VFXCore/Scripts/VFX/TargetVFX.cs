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
    /// A specialized version of ShaderVFX,
    /// that uses a Vuforia Target (Observer) to automatically
    /// configure center, axis and scale of the VFX.
    /// </summary>
    public class TargetVFX : ShaderVFX
    {
        [Header("General Settings")]
        public ObserverBehaviour Observer;
        public bool IsTargetShownOnTargetFound = true;
        public float ShowDelay;
        
        [Header("Coordinate System")]
        [Tooltip("The center property name in the shader.")]
        public string ShaderCenter = "_Center";

        [Tooltip("The axis X property name in the shader.")]
        public string ShaderAxisX = "_AxisX";

        [Tooltip("The axis Y property name in the shader.")]
        public string ShaderAxisY = "_AxisY";

        [Tooltip("The axis Z property name in the shader.")]
        public string ShaderAxisZ = "_AxisZ";

        [Tooltip("The scale property name in the shader.")]
        public string ShaderScale = "_Scale";

        [Tooltip("The center point for the visual effect (coords in local target reference).")]
        public Vector3 Center = Vector3.zero;

        [Tooltip("The center mode used for the visual effect.")]
        public VFXCenterMode CenterMode = VFXCenterMode.TARGET;

        ObserverBehaviour mObserver;
        TargetStatus mPreviousTargetStatus = TargetStatus.NotObserved;

        protected override void Awake()
        {
            base.Awake();
            if (Observer)
                mObserver = Observer;
            else
            {
                // Try finding observer in scene hierarchy
                var parentObserver = GetComponentInParent<ObserverBehaviour>();
                if (parentObserver)
                    mObserver = parentObserver;
            }

            if (!mObserver)
                Debug.LogWarning("Vuforia Observer not found!");
            mObserver.OnTargetStatusChanged += TargetStatusChanged;
        }
        
        protected override void Update()
        {
            base.Update();

            if (!mObserver || !VuforiaApplication.Instance.IsRunning)
                return;
            
            UpdateShaderCenterAndAxis();
            UpdateShaderScale();
        }

        void OnDestroy()
        {
            if (mObserver != null)
                mObserver.OnTargetStatusChanged -= TargetStatusChanged;
        }

        void UpdateShaderScale()
        {
            MaterialUtil.TrySetVector4Property(mSharedMaterial, ShaderScale, VuforiaObserverUtil.GetTargetSize(mObserver));
        }

        void TargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
        {
            if (IsUnTracked(mPreviousTargetStatus) && IsTracked(status))
                OnTargetFound();

            mPreviousTargetStatus = status;
        }

        bool IsTracked(TargetStatus targetStatus)
        {
            return targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED ||
                   targetStatus.Status == Status.LIMITED;
        }
        
        bool IsUnTracked(TargetStatus targetStatus)
        {
            return targetStatus.Status == Status.NO_POSE;
        }
        
        void OnTargetFound()
        {
            if (IsTargetShownOnTargetFound)
            {
                if (ShowDelay > Mathf.Epsilon)
                    Invoke(nameof(Play), ShowDelay);
                else
                    Play();
            }
        }

        void UpdateShaderCenterAndAxis()
        {
            if (!mObserver || !mSharedMaterial)
                return;
            

            MaterialUtil.TrySetVector4Property(mSharedMaterial, ShaderCenter, GetCenterPointWCS());

            GetAxisVectors(out Vector3 aX, out Vector3 aY, out Vector3 aZ);
            MaterialUtil.TrySetVector4Property(mSharedMaterial, ShaderAxisX, aX);
            MaterialUtil.TrySetVector4Property(mSharedMaterial, ShaderAxisY, aY);
            MaterialUtil.TrySetVector4Property(mSharedMaterial, ShaderAxisZ, aZ);
        }

        void GetAxisVectors(out Vector3 axisX, out Vector3 axisY, out Vector3 axisZ)
        {
            axisX = Vector3.right;
            axisY = Vector3.up;
            axisZ = Vector3.forward;

            Camera cam;
            if (CenterMode == VFXCenterMode.TARGET && mObserver != null)
            {
                axisX = mObserver.transform.right;
                axisY = mObserver.transform.up;
                axisZ = mObserver.transform.forward;
            }
            else if (CenterMode == VFXCenterMode.VIEWER && (cam = VuforiaCameraUtil.GetCamera()) != null)
            {
                axisX = cam.transform.right;
                axisY = cam.transform.up;
                axisZ = cam.transform.forward;
            }
        }

        Vector3 GetCenterPointWCS()
        {
            var centerPointWCS = Center;

            Camera cam;
            if (CenterMode == VFXCenterMode.TARGET && mObserver != null)
                centerPointWCS = mObserver.transform.TransformPoint(Center);
            else if (CenterMode == VFXCenterMode.VIEWER && (cam = VuforiaCameraUtil.GetCamera()) != null)
                centerPointWCS = cam.transform.TransformPoint(Center);
            
            return centerPointWCS;
        }
    }
}
