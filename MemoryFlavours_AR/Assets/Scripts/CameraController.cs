/*
 * Author: Shi Jie, Jessica Lim, Anqi
 * Date: 11/11/22
 * Description: Toggles AR Camera on and off
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Store GameObject to stop displaying it when camera is off
    /// </summary>
    public List<GameObject> storeGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Toggle camera on and off
    public void ToggleCamera()
    {
        // if true, stop AR features
        if (VuforiaBehaviour.Instance.enabled)
        {
            VuforiaBehaviour.Instance.VideoBackground.StopVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = false; // stop tracking
            foreach (GameObject x in storeGameObjects)
            {
                x.SetActive(false);
            }
        }

        else
        {
            VuforiaBehaviour.Instance.VideoBackground.StartVideoBackgroundRendering();
            VuforiaBehaviour.Instance.enabled = true; // starts tracking
            foreach (GameObject x in storeGameObjects)
            {
                x.SetActive(true);
            }
        }
    }
}
