using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TouchInteraction : MonoBehaviour
{
    public GameObject flour;
    public GameObject saltnSugar;
    TriggerCheck trigger;
    void OnTouchPress()
    {
        Debug.Log("touch");
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
