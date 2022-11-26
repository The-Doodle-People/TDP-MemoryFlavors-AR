using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraController : MonoBehaviour
{   
    /// <summary>
    /// Variables for cameras
    /// </summary>
    public GameObject arCamera;
   
    /// <summary>
    /// off and on button
    /// </summary>
    public GameObject offButton;
    public GameObject onButton;

    /// <summary>
    /// toggle ar camera off
    /// </summary>
    /// 
    public void ToggleCamera()
    {   
        // Check if Vuforia is running
        if(VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.VideoBackground.StopVideoBackgroundRendering();
            offButton.SetActive(false);
            onButton.SetActive(true);
            VuforiaBehaviour.Instance.enabled = false;

        }
       
    }
    /// <summary>
    /// toggle ar camera on
    /// </summary>
    public void OnCamera()
    {
        if (VuforiaBehaviour.Instance.enabled == false)
        {
            VuforiaBehaviour.Instance.VideoBackground.StartVideoBackgroundRendering();
            offButton.SetActive(true);
            onButton.SetActive(false);
            VuforiaBehaviour.Instance.enabled = true;
        }
    }
}
