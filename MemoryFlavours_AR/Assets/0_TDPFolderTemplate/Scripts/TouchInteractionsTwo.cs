/*
* Author: Wong Qing Wei
* Date: 20/11/2022
* Description: When touch interaction is detected script for step 4
*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchInteractionsTwo : MonoBehaviour
{
    /// <summary>
    /// Game Object to store 3d of peanut fillings
    /// </summary>
    public GameObject peanutFillings;

    /// <summary>
    /// Game object to store 3d blender
    /// </summary>
    public GameObject blender;

    /// <summary>
    /// Game Object to store 3d blended peanuts
    /// </summary>
    public GameObject blendedPeanuts;

    /// <summary>
    /// to store the UI text
    /// </summary>
    public TMP_Text stepsText;

    /// <summary>
    /// boolean to ensure that peanut are taken before blending
    /// </summary>
    bool peanutTaken;

    /// <summary>
    /// to recall UIManager script 
    /// </summary>
    public UIManager uiMgr;

    // on touch is detected
    void OnTouchPress()
    {
        //x n y coordinated of the touch
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        //nearclipplane is where u start seeing things
        rayPosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo;

        // raycast hit to where user tap on and add lean touch function to it
        if (Physics.Raycast(ray, out hitInfo))
        {
            if(hitInfo.collider.tag == "peanutFillings" && uiMgr.currentStep == 5)
            {
               
                peanutFillings.AddComponent<Lean.Touch.LeanDragTranslate>();
                peanutTaken = true;
            }
           
            if(hitInfo.collider.tag == "blenderCap" && uiMgr.currentStep == 5)
            {
                // check if fillings have taken or not
                if(peanutTaken == false)
                {
                    stepsText.text = "Put peanutFillings first before blending";
                }
                else
                {
                    blender.AddComponent<Lean.Touch.LeanDragTranslate>();
                }
            }

            if(hitInfo.collider.tag == "blendedPeanuts")
            {
                blendedPeanuts.AddComponent<Lean.Touch.LeanDragTranslate>();
            }
        }
    }
}