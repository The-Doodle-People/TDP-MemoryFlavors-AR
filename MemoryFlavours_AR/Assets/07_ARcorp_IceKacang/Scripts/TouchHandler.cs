using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TouchHandler : MonoBehaviour
{
    /// <summary>
    /// Wave button
    /// </summary>
    public GameObject WaveButton;


    // When user touches screen
    void OnTouchPress()
    {
        ///Get the position of the touch input, which is the primaryTouch
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        ///Assign the nearClipPlane value of the camera as the ‘z’ coordinate
        rayPosition.z = Camera.main.nearClipPlane;

        ///The function returns a Ray, so we create a Ray variable to store it.
        Ray ray = Camera.main.ScreenPointToRay(rayPosition);

        ///RaycastHit variable to store the information of anything the raycast detects
        RaycastHit hitInfo;

        ///Function to shoot our ray
        if (Physics.Raycast(ray,out hitInfo))
        {
            ///Checking if the object we hit has an AstronautController component 
            if (hitInfo.collider.GetComponent<AstronautController>() != null)
            {
                ///Sets canvas and button to active
                WaveButton.SetActive(true);

                ///Touch is detected
                Debug.Log("Touch is detected");
            }
        }

    }

}
