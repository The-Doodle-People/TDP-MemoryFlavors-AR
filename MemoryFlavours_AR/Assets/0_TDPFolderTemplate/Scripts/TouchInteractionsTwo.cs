using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchInteractionsTwo : MonoBehaviour
{
    public GameObject peanutFillings;
    public GameObject blender;
    public GameObject blendedPeanuts;

    public TMP_Text stepsText;

    bool peanutTaken; // ensure that peanut are taken before blending
   

    public UIManager uiMgr;
    void OnTouchPress()
    {
        //x n y coordinated of the touch
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        //nearclipplane is where u start seeing things
        rayPosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if(hitInfo.collider.tag == "peanutFillings" && uiMgr.currentStep == 5)
            {
               
                peanutFillings.AddComponent<Lean.Touch.LeanDragTranslate>();
                peanutTaken = true;
            }

            if(hitInfo.collider.tag == "blenderCap" && uiMgr.currentStep == 5)
            {
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