/*
 * Author: Charlene Ngiam, Rovee
 * Date: 1 november - 20 november 2022
 * Description: the script used for touch screen 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// class for touch handler 
/// </summary>
public class TouchHandler : MonoBehaviour
{
    /// <summary>
    /// function on touch press, when player taps the screen 
    /// </summary>
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
            //if(hitInfo.collider.GetComponent<CoffeeShop>() != null)
            if (hitInfo.collider.GetComponent<AuntyController>() != null)
            {
                Debug.Log("Touch detected");
                //get aunty controller script, to link waving animation to raycast, using raycast for waving animation
                hitInfo.collider.GetComponent<AuntyController>().Interact();
            }
        }
    }
}
