using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraController : MonoBehaviour
{

    public GameObject model;

    public void ToggleCamera()
    {
        //If Camera is Enabled, Disable it, otherwise Enable it
        if (VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.VideoBackground.StopVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = false;
            model.SetActive(false);
        }
        else
        {
            VuforiaBehaviour.Instance.VideoBackground.StartVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = true;
    model.SetActive(true);
        }
    }
}
