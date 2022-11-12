using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraController : MonoBehaviour
{
    public void ToggleCamera()
    {
        // Check if Vuforia is running
        if(VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.VideoBackground.StopVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = false;
        }
    }
}
