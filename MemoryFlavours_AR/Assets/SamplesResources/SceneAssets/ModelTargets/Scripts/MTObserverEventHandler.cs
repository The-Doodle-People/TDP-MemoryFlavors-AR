/*==============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/

using UnityEngine;
using Vuforia;

public class MTObserverEventHandler : DefaultObserverEventHandler
{
    [SerializeField] GameObject ScaleWarningPopup = null;

    protected override void HandleTargetStatusChanged(Status previousStatus, Status newStatus)
    {
        base.HandleTargetStatusChanged(previousStatus, newStatus);
        
        if (mPreviousTargetStatus.StatusInfo == StatusInfo.WRONG_SCALE)
            EnableScaleWarningPopup();
    }

    void EnableScaleWarningPopup()
    {
        if (ScaleWarningPopup != null)
            ScaleWarningPopup.SetActive(true);
    }
}