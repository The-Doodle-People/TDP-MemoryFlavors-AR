/*==============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class GuideViewCycler : MonoBehaviour
{
    ModelTargetBehaviour mStandardLanderTarget;
    void Awake()
    {
        mStandardLanderTarget = GetComponent<ModelTargetBehaviour>();
    }

    /// <summary>
    /// Cycles through guide views for Standard Model Targets with multiple views.
    /// </summary>
    public void CycleGuideView()
    {
        VLog.Log("cyan", "CycleGuideView() called.");

        if (mStandardLanderTarget == null) 
            return;
        
        var activeView = mStandardLanderTarget.GetActiveGuideViewIndex();
        var totalViews = mStandardLanderTarget.GetNumGuideViews();

        if (totalViews > 1 && activeView > -1)
        {
            var guideViewIndexToActivate = (activeView + 1) % totalViews;
            VLog.Log("yellow",
                     mStandardLanderTarget.TargetName + ": Activating Guide View Index " +
                     guideViewIndexToActivate + " of " +
                     totalViews + " total Guide Views.");

            mStandardLanderTarget.SetActiveGuideViewIndex(guideViewIndexToActivate);
        }
        else
        {
            VLog.Log("yellow",
                     "GuideView was not cycled." +
                     "\nActive Guide View Index = " + activeView +
                     "\nNumber of Guide Views = " + totalViews);
        }
    }

}