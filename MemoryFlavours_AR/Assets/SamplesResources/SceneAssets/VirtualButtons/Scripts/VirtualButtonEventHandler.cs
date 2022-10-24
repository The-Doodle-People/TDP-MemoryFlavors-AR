/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

/// <summary>
/// This class implements the IVirtualButtonEventHandler interface and
/// contains the logic to start animations depending on what 
/// virtual button has been pressed.
/// </summary>
public class VirtualButtonEventHandler : MonoBehaviour
{
    public Material VirtualButtonDefault;
    public Material VirtualButtonPressed;
    public float ButtonReleaseTimeDelay;
    public UnityEvent OnVirtualButtonPressed = new UnityEvent();
    public UnityEvent OnVirtualButtonReleased = new UnityEvent();

    VirtualButtonBehaviour[] mVirtualButtonBehaviours;
   
    void Awake()
    {
        // Register with the virtual buttons ObserverBehaviour
        mVirtualButtonBehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (var i = 0; i < mVirtualButtonBehaviours.Length; ++i)
        {
            mVirtualButtonBehaviours[i].RegisterOnButtonPressed(OnButtonPressed);
            mVirtualButtonBehaviours[i].RegisterOnButtonReleased(OnButtonReleased);
        }
    }

    void Destroy()
    {
        // Register with the virtual buttons ObserverBehaviour
        mVirtualButtonBehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (var i = 0; i < mVirtualButtonBehaviours.Length; ++i)
        {
            mVirtualButtonBehaviours[i].UnregisterOnButtonPressed(OnButtonPressed);
            mVirtualButtonBehaviours[i].UnregisterOnButtonReleased(OnButtonReleased);
        }
    }
    
    /// <summary>
    /// Called when the virtual button has just been pressed:
    /// </summary>
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("OnButtonPressed: " + vb.VirtualButtonName);
        SetVirtualButtonMaterial(VirtualButtonPressed);
        StopAllCoroutines();
        OnVirtualButtonPressed?.Invoke();
    }

    /// <summary>
    /// Called when the virtual button has just been released:
    /// </summary>
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("OnButtonReleased: " + vb.VirtualButtonName);
        SetVirtualButtonMaterial(VirtualButtonDefault);
        StartCoroutine(DelayOnButtonReleasedEvent(ButtonReleaseTimeDelay));
    }
    
    void SetVirtualButtonMaterial(Material material)
    {
        // Set the Virtual Button material
        for (var i = 0; i < mVirtualButtonBehaviours.Length; ++i)
        {
            if (material != null)
                mVirtualButtonBehaviours[i].GetComponent<MeshRenderer>().sharedMaterial = material;
        }
    }

    IEnumerator DelayOnButtonReleasedEvent(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        OnVirtualButtonReleased?.Invoke();
    }
}
