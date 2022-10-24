/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using System.Collections;
using UnityEngine;
using Vuforia;

namespace VFX
{
    public class FocusMode : MonoBehaviour
    {
        [Tooltip("The camera focus mode.")]
        public Vuforia.FocusMode SelectedFocusMode = Vuforia.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;

        const float DELAY = 0.5f;
        
        void Start()
        {
            VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
        }

        void OnDestroy()
        {
            if (VuforiaApplication.Instance != null)
                VuforiaApplication.Instance.OnVuforiaStarted -= OnVuforiaStarted;
        }
        
        void OnVuforiaStarted()
        {
            StartCoroutine(SetCameraFocusMode());
        }

        IEnumerator SetCameraFocusMode()
        {
            yield return new WaitForSeconds(DELAY);
            if (VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(SelectedFocusMode))
                Debug.Log("Successfully set camera focus mode to: " + SelectedFocusMode);
            else
                Debug.LogWarning("Failed to set camera focus mode to: " + SelectedFocusMode);
        }
    }
}