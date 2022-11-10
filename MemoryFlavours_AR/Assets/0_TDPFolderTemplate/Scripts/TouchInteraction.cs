using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; 

public class TouchHandler : MonoBehaviour
{
    TriggerCheck triggerSript;
    float mixCounter = 0;
    public Slider mixSlider;
    void OnTouchPress()
    {
        Debug.Log("touch");
        
        //x n y coordinated of the touch
        Vector3 rayPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        //nearclipplane is where u start seeing things
        rayPosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(rayPosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            triggerSript = gameObject.GetComponent<TriggerCheck>();
            if (hitInfo.collider.tag == "dryMix" && triggerSript.enteredBowl == true)
            {
                Debug.Log("mixing");
                mixCounter += 0.2f;
                mixSlider.value = mixCounter;
            }
        }


        //if trigger is true then mix counter
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
