/*==============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class MidAirUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Text Instructions = null;

    const string DRONE_PLACEMENT_HINT = "Tap to place Drone";
    const string BETTER_TRACKING_IS_NEEDED = "Need better tracking to place a Mid-Air Anchor";

    void Start()
    {
        VuforiaBehaviour.Instance.DevicePoseBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    void OnDestroy()
    {
        if (VuforiaBehaviour.Instance != null)
            VuforiaBehaviour.Instance.DevicePoseBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
    }

    public void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        Instructions.text = targetStatus.IsTrackedAndNormal() ? DRONE_PLACEMENT_HINT : BETTER_TRACKING_IS_NEEDED;
    }
}
