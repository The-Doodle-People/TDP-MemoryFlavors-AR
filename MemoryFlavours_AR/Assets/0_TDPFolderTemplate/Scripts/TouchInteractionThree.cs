using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchInteractionThree : MonoBehaviour
{
    public GameObject kuehMold;
    public GameObject dough;
    public GameObject peanutFilling;
    TriggerCheck trigger;

    public UIManager managerUI;

    public GameObject steamerCap;
    public GameObject kueh;



    void OnTouchPress()
    {
        //x n y coordinates of the touch 
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        //nearclipplane is where u start seeing things 
        rayPosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo;

        trigger = dough.GetComponent<TriggerCheck>();
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag == "dough")
            {
                dough.AddComponent<Lean.Touch.LeanDragTranslate>();
            }
            if(hitInfo.collider.tag=="fillings" && trigger.doughInMold == true)
            {
                Debug.Log("on peaunt filling");
                peanutFilling.AddComponent<Lean.Touch.LeanDragTranslate>();
            }

        }

        trigger = kuehMold.GetComponent<TriggerCheck>();
        if(Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag == "kueh")
            {
                kueh.AddComponent<Lean.Touch.LeanDragTranslate>();
            }
            if (hitInfo.collider.tag == "steamerCap" && trigger.kuehInSteamer == true)
            {
                Debug.Log("kueh ready for steaming");
                steamerCap.AddComponent<Lean.Touch.LeanDragTranslate>();
            }
        }

    }

}
