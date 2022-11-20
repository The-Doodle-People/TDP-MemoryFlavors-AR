using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{
    void OnTouchPress()
    {
        Debug.Log("OnTouchPress detected");
        // ReadValue will get the x and y value and store inside rayOrigin
        // Get the x and y position of the touch (teacher's comment)
        Vector3 rayOrigin = Touchscreen.current.primaryTouch.position.ReadValue();
        // Get the z value and store inside rayOrigin
        // Set the z position of the ray origin (teacher's comment)
        rayOrigin.z = Camera.main.nearClipPlane;

        // Create the ray using the rayOrigin (teacher's comment)
        Ray ray = Camera.main.ScreenPointToRay(rayOrigin);
        RaycastHit hitInfo;

        Debug.Log("raycast");
        // Perform the raycast (teacher's comment)
        // and the if-statement will only happen if raycast hit something
        if (Physics.Raycast(ray, out hitInfo))
        {
            // Check if the hit object has an CardamomController component (teacher's comment)
            if (hitInfo.collider.GetComponent<RiceController>() != null)
            {
                hitInfo.collider.GetComponent<RiceController>().Interact();
            }
        }

    }
}
