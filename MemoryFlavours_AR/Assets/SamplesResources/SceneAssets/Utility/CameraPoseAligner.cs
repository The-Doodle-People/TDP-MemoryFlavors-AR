/*============================================================================== 
Copyright (c) 2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.   
==============================================================================*/


using UnityEngine;
using Vuforia;

public class CameraPoseAligner : MonoBehaviour
{
    Transform mARCamera;
    void Start()
    {
        VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
    }

    void OnDestroy()
    {
        VuforiaApplication.Instance.OnVuforiaStarted -= OnVuforiaStarted;
    }

    void OnVuforiaStarted()
    {
        mARCamera = VuforiaBehaviour.Instance.transform;
    }

    void LateUpdate()
    {
        if (!VuforiaApplication.Instance.IsRunning)
            return;

        transform.LookAt(transform.position + mARCamera.forward, mARCamera.up);
    }
}
