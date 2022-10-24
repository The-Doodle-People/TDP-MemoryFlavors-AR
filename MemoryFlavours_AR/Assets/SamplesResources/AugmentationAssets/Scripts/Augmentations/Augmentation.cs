/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using System;
using System.Collections;
using UnityEngine;

public abstract class Augmentation : MonoBehaviour
{
    public Action OnEnterEvent = delegate { };
    public Action OnExitEvent = delegate { };
    
    protected Animator mAnimator;
    bool mActive;
    
    public virtual void Start()
    {
        mAnimator = GetComponent<Animator>();
        // Don't want to call OnEnter if we haven't called Start yet
        StartCoroutine(WaitForThen(1.0f, OnEnter));
    }

    public void Disable()
    {
        OnExit();
    }
    
    /// <summary>
    /// Restores the objects state as if the scene has been reloaded
    /// </summary>
    public virtual void Restore()
    {
        Disable();

        // Resets all animator parameters and sets animator to default state
        mAnimator.Rebind();
    }

    public virtual void OnEnter()
    {
        // intentionally left blank for derived classes to extend
    }

    public virtual void OnExit()
    {
        OnExitEvent.Invoke();
    }
    
    /// <summary>
    /// Enables or disables all renderers on a GameObject and its children
    /// </summary>
    /// <param name="enable">True to enable renderers, false to disable them</param>
    public virtual void SetRenderersEnabled(bool enable)
    {
        var components = GetComponentsInChildren<Renderer>(true);
        foreach (var component in components)
            component.enabled = enable;
    }

    /// <summary>
    /// Enables or disables all colliders on a GameObject and its children
    /// </summary>
    /// <param name="enable">True to enable colliders, false to disable them</param>
    public virtual void SetCollidersEnabled(bool enable)
    {
        var components = GetComponentsInChildren<Collider>(true);
        foreach (var component in components)
            component.enabled = enable;
    }
    
    /// <summary>
    /// Waits for a number of seconds then performs an action
    /// </summary>
    IEnumerator WaitForThen(float waitSeconds, Action action)
    {
        yield return new WaitForSeconds(waitSeconds);
        action();
    }
}