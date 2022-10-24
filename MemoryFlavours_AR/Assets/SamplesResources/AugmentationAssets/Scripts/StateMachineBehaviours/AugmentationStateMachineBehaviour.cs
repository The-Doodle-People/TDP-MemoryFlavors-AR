/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AugmentationStateMachineBehaviour : StateMachineBehaviour
{
    public string OnEnterMethodName;
    public string OnUpdateMethodName;
    public string OnExitMethodName;
    readonly Dictionary<Type, Dictionary<string, Delegate>> cachedDelegates = new Dictionary<Type, Dictionary<string, Delegate>>();

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (!string.IsNullOrEmpty(OnEnterMethodName))
            DoStateEvent(animator, OnEnterMethodName);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (!string.IsNullOrEmpty(OnExitMethodName))
            DoStateEvent(animator, OnExitMethodName);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (!string.IsNullOrEmpty(OnUpdateMethodName))
            DoStateEvent(animator, OnUpdateMethodName);
    }
    
    public abstract void DoStateEvent(Animator animator, string methodName);

    public abstract Type GetTargetType();
    
    /// <summary>
    /// Gets a delegate for the method of type T named methodName. Creates it if it does not already exist in the cache.
    /// </summary>
    /// <returns></returns>
    public Action<T> GetMethod<T>(T augmentation, string methodName)
    {
        Action<T> result = null;

        Dictionary<string, Delegate> delegateByMethodName;
        if (cachedDelegates.TryGetValue(typeof(T), out delegateByMethodName))
        {
            Delegate del;
            if (delegateByMethodName.TryGetValue(methodName, out del))
                result = del as Action<T>;
        }


        if (result == null)
        {
            var methodInfo = UnityEventBase.GetValidMethodInfo(augmentation, methodName, new Type[0]);
            if (methodInfo == null)
                Debug.LogWarning("Method \"" + methodName + "\" could not be found on object of type " + typeof(T).Name);
            else
            {
#if NETFX_CORE
                var del = methodInfo.CreateDelegate(typeof(Action<T>));
#else
                var del = Delegate.CreateDelegate(typeof(Action<T>), methodInfo, false);
#endif
                if (del == null)
                {
                    if (methodInfo.ReturnType != typeof(void))
                        Debug.LogWarning("Method \"" + methodName + "\" must have a return type of void to be used with AugmentationStateMachineBehaviour");
                    
                    if (methodInfo.GetGenericArguments().Length > 0)
                        Debug.LogWarning("Method \"" + methodName + "\" must have no arguments to be used with AugmentationStateMachineBehaviour");
                }
                else
                {
                    result = del as Action<T>;
                    if (result != null)
                        AddDelegateToCache(result, methodName);
                }
            }

        }

        return result;
    }

    /// <summary>
    /// Caches a delegate by type and method name
    /// </summary>
    void AddDelegateToCache<T>(Action<T> delegateToAdd, string methodName)
    {
        Dictionary<string, Delegate> delegateByMethodName;
        if (!cachedDelegates.TryGetValue(typeof(T), out delegateByMethodName))
        {
            delegateByMethodName = new Dictionary<string, Delegate>();
            cachedDelegates.Add(typeof(T), delegateByMethodName);
        }

        delegateByMethodName.Add(methodName, delegateToAdd);
    }
}

