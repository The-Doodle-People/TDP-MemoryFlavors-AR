/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
/// Handles the outline around the VuMark
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class VuMarkOutline : MonoBehaviour
{
    LineRenderer mLineRenderer;

    void Awake()
    {
        mLineRenderer = GetComponentInChildren<LineRenderer>();
        SetupVuMarkBorderOutline();
    }

    /// <summary>
    /// Called by DefaultObserverEventHandler.OnTargetFound.
    /// </summary>
    public void OnVuMarkFound(VuMarkBehaviour vuMarkBehaviour)
    {
        vuMarkBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;

        SetBorderPositions(vuMarkBehaviour);
        UpdateLineRenderer(vuMarkBehaviour.TargetStatus);
    }

    /// <summary>
    /// Called by DefaultObserverEventHandler.OnTargetLost.
    /// </summary>
    public void OnVuMarkLost(VuMarkBehaviour vuMarkBehaviour)
    {
        vuMarkBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
    }

    void OnTargetStatusChanged(ObserverBehaviour observerBehaviour, TargetStatus targetStatus)
    {
        UpdateLineRenderer(targetStatus);
    }

    void UpdateLineRenderer(TargetStatus targetStatus)
    {
        if (!mLineRenderer) return;

        // Only enable line renderer when target becomes Extended Tracked or when running in Unity Editor.
        mLineRenderer.enabled = targetStatus.Status != Status.NO_POSE && (targetStatus.Status == Status.EXTENDED_TRACKED || VuforiaRuntimeUtilities.IsPlayMode());

        // If the Device Pose Observer is enabled and the target becomes Extended Tracked,
        // set the VuMark outline to green. If in Unity Editor PlayMode, set to cyan.
        // Note that on HoloLens, the Device Pose Observer is always enabled (as of Vuforia 7.2).
        if (mLineRenderer.enabled)
            mLineRenderer.material.color = (targetStatus.Status == Status.EXTENDED_TRACKED) ? Color.green : Color.cyan;
    }

    void SetupVuMarkBorderOutline()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = new Vector3(1 / transform.localScale.x,
                                                        1, 1 / transform.localScale.z);
        mLineRenderer.enabled = false;
        mLineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mLineRenderer.receiveShadows = false;
        // This shader needs to be added in the Project's Graphics Settings,
        // unless it is already in use by a Material present in the project.
        mLineRenderer.material.shader = Shader.Find("Unlit/Color");
        mLineRenderer.material.color = Color.clear;
        mLineRenderer.positionCount = 4;
        mLineRenderer.loop = true;
        mLineRenderer.useWorldSpace = false;
        var curve = new AnimationCurve();
        curve.AddKey(0.0f, 1.0f);
        curve.AddKey(1.0f, 1.0f);
        mLineRenderer.widthCurve = curve;
        mLineRenderer.widthMultiplier = 0.003f;
    }

    void SetBorderPositions(VuMarkBehaviour vuMarkBehaviour)
    {
        var vuMarkSize = vuMarkBehaviour.GetSize();
        var vuMarkExtentsX = vuMarkSize.x * 0.5f + mLineRenderer.widthMultiplier * 0.5f;
        var vuMarkExtentsZ = vuMarkSize.y * 0.5f + mLineRenderer.widthMultiplier * 0.5f;
        mLineRenderer.SetPositions(new []
                                   {
                                       new Vector3(-vuMarkExtentsX, 0.001f, vuMarkExtentsZ),
                                       new Vector3(vuMarkExtentsX, 0.001f, vuMarkExtentsZ),
                                       new Vector3(vuMarkExtentsX, 0.001f, -vuMarkExtentsZ),
                                       new Vector3(-vuMarkExtentsX, 0.001f, -vuMarkExtentsZ)
                                   });
    }
}
