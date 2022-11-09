using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviour
{
    public GameObject canvas;
    void OnTouchPress()
    {
        //Vector3 is x,y,z
        Vector3 rayOrigin = Touchscreen.current.primaryTouch.position.ReadValue();
        rayOrigin.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(rayOrigin);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            /*
            if (hitInfo.collider.GetComponent<AstronautController>() != null)
            {
                Debug.Log("Touch detected!");
                //canvas.SetActive(true);
                hitInfo.collider.GetComponent<AstronautController>().Interact();
            }
            */
        }
    }
}
