using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UIController : MonoBehaviour
{
    public GameObject restartConfirmation;

    public void ToggleCamera()
    {
        // Check if Vuforia is running
        if (VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.VideoBackground.StopVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = false;
        }

        else if(!VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.VideoBackground.StartVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = true;
        }
    }

    public void ToggleRestart()
    {
        if (!restartConfirmation.activeSelf)
        {
            restartConfirmation.SetActive(true);
        }

        else if (restartConfirmation.activeSelf)
        {
            restartConfirmation.SetActive(false);
        }
    }
}
