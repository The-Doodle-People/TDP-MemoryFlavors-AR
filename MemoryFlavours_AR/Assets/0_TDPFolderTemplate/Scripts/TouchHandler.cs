using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchHandler : MonoBehaviour
{
    void OnTouchPress()
    {
        // Get the x and y coordinates of the touch
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        // Set the z coordinate of the touch to the nearclipping 
        rayPosition.z = Camera.main.nearClipPlane;

        // Pass the rayPosition into the ScreenPoint ToRay function to create a Ray
        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo;

        // Raycast using the created Ray
        if (Physics.Raycast(ray, out hitInfo))
        {
            // Check if the hit object has an AstronautController component
            if (hitInfo.collider.tag == "Dimsum")
            {
                // If it does, print message
                Debug.Log("Touch detected");
                
            }
        }
    }
}
