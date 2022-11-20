/*==============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Vuforia;

public class GroundPlaneUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Text Instructions = null;
    [SerializeField] CanvasGroup ScreenReticle = null;

    const string CHAIR_PLACEMENT_HINT = "Tap to place Chair";
    const string CHAIR_CONTROLS_WITH_PINCH = "• Touch and drag to move Chair\n• Two fingers to rotate or pinch to scale";
    const string CHAIR_CONTROLS_WITHOUT_PINCH = "• Touch and drag to move Chair\n• Two fingers to rotate";
    const string POINT_DEVICE_TO_GROUND_HINT = "Point device towards ground";
    const string MOVE_TO_ANOTHER_PLACE_HINT = "Move to get better tracking for placing an anchor";

    PointerEventData mPointerEventData;
    DevicePoseManager mDevicePoseManager;
    ProductPlacement mProductPlacement;
    TouchHandler mTouchHandler;
    PlaneFinderBehaviour mPlaneFinder;

    bool mIsAnchorTracking;

    /// <summary>
    /// The Surface Indicator should only be visible if the following conditions are true:
    /// 1. Tracking Status is Tracked or Limited (sufficient for Hit Test Anchors)
    /// 2. Ground Plane Hit was received for this frame
    /// 3. There's no active touches
    /// </summary>
    bool SurfaceIndicatorVisibilityConditionsMet => mDevicePoseManager.TargetStatus.IsTrackedOrLimited() &&
                                                    mProductPlacement.GroundPlaneHitReceived &&
                                                    Input.touchCount == 0;

    void Start()
    {
        mDevicePoseManager = FindObjectOfType<DevicePoseManager>();
        mPlaneFinder = FindObjectOfType<PlaneFinderBehaviour>();
        mProductPlacement = FindObjectOfType<ProductPlacement>();
        mTouchHandler = FindObjectOfType<TouchHandler>();
    }

    void LateUpdate()
    {
        if (mProductPlacement.GroundPlaneHitReceived && mDevicePoseManager.TargetStatus.IsTrackedAndNormal())
        {
            // We got an automatic hit test this frame, hide the onscreen reticle when we get a hit test
            ScreenReticle.alpha = 0;

            if (mIsAnchorTracking)
                Instructions.text = mTouchHandler.EnablePinchScaling ? CHAIR_CONTROLS_WITH_PINCH : CHAIR_CONTROLS_WITHOUT_PINCH;
            else
                Instructions.text = CHAIR_PLACEMENT_HINT;
        }
        else
        {
            // No automatic hit test, so set alpha based on which plane mode is active
            if (!mProductPlacement.GroundPlaneHitReceived)
                ScreenReticle.alpha = 1;

            Instructions.text = mProductPlacement.GroundPlaneHitReceived ? MOVE_TO_ANOTHER_PLACE_HINT : POINT_DEVICE_TO_GROUND_HINT;
        }

        // Surface Indicator visibility conditions rely upon GroundPlaneHitReceived,
        // so we perform this operation in LateUpdate() to ensure that it is called
        // after GroundPlaneHitReceived has been updated in Update().
        SetSurfaceIndicatorVisible(SurfaceIndicatorVisibilityConditionsMet);
    }

    // Called by Anchor_Placement's DefaultObserverEventHandler.OnTargetFound()
    public void OnAnchorFound()
    {
        mIsAnchorTracking = true;
    }

    // Called by Anchor_Placement's DefaultObserverEventHandler.OnTargetLost()
    public void OnAnchorLost()
    {
        mIsAnchorTracking = false;
    }

    /// <summary>
    /// This method can be used to set the Ground Plane surface indicator visibility.
    /// This sample will display it when the Status=TRACKED and StatusInfo=Normal.
    /// </summary>
    /// <param name="isVisible">bool</param>
    void SetSurfaceIndicatorVisible(bool isVisible)
    {
        var renderers = mPlaneFinder.PlaneIndicator.GetComponentsInChildren<Renderer>(true);
        var canvases = mPlaneFinder.PlaneIndicator.GetComponentsInChildren<Canvas>(true);

        foreach (var canvas in canvases)
            canvas.enabled = isVisible;

        foreach (var renderer in renderers)
            renderer.enabled = isVisible;
    }
}
