using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class TouchInteraction : MonoBehaviour
{
    public GameObject flour;
    public GameObject saltnSugar;
    TriggerCheck trigger;

    public Slider mixingSlider;
    public GameObject mixingUI;
    int mixClickPos = 1;
    string currentTag;
    void OnTouchPress()
    {
        
        //x n y coordinated of the touch
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        //nearclipplane is where u start seeing things
        rayPosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo;

        trigger = flour.GetComponent<TriggerCheck>();
        if (Physics.Raycast(ray, out hitInfo))
        {
            
            if (hitInfo.collider.tag == "saltnSugar" && trigger.flourEnteredBowl == true)
            {
                Debug.Log("touchedS&S");
                saltnSugar.AddComponent<Lean.Touch.LeanDragTranslate>();
            }

          
            currentTag = "mixingBtn" + mixClickPos.ToString();
            if(hitInfo.collider.tag == currentTag)
            {
                mixClickPos++;

                mixingSlider.value += 0.1f;
                if(mixingSlider.value == 1)
                {
                    mixingUI.SetActive(false);
                }
            }
            if (mixClickPos > 4)
            {
                mixClickPos = 1;
            }
        }
    }
   
}
