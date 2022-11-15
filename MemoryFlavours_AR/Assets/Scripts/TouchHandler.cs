using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{
   void OnTouchPress()
    {
        // Get the x and y coordinates of the touch
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        // Set the z coordinate of the touch to the nearclipplane
        rayPosition.z = Camera.main.nearClipPlane;

        // Pass the rayPosition into the ScreenPointToRay function to create a Ray
        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo;

        // Raycast using the created Ray
        if(Physics.Raycast(ray, out hitInfo))
        {
            // Check if the hit object has an AstronautController component
            if(hitInfo.collider.GetComponent<WavesController>() != null)
            {
                hitInfo.collider.GetComponent<WavesController>().Interact();
            }
        }
    }
}
