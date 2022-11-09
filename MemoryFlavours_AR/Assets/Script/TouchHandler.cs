using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{
    void OnTouchPress()
    {
        // Get the X and Y position of the touch
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        // Set the z position of the ray origin
        rayPosition.z = Camera.main.nearClipPlane;

        // Create the ray using rayOrigin
        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo;

        // Perform the raycast
        if(Physics.Raycast(ray, out hitInfo))
        {
            // Check if the hit object has an AstronautController component
            if(hitInfo.collider.GetComponent<AstronautController>() != null)
            {
                hitInfo.collider.GetComponent<AstronautController>().Interact();
                Debug.Log("Touch detected!");
            }
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
