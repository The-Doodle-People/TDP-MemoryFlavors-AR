/*==============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/

using UnityEngine;
using Vuforia;

public class DevicePoseUI : MonoBehaviour
{
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
        var statusMessage = "";
        
        switch (targetStatus.StatusInfo)
        {
            case StatusInfo.NORMAL:
                statusMessage = "";
                break;
            case StatusInfo.UNKNOWN:
                statusMessage = "Limited Status";
                break;
            case StatusInfo.INITIALIZING:
                statusMessage = "Move to scan";
                break;
            case StatusInfo.EXCESSIVE_MOTION:
                statusMessage = "Move slower";
                break;
            case StatusInfo.INSUFFICIENT_FEATURES:
                statusMessage = "Not enough visual features in the scene";
                break;
            case StatusInfo.INSUFFICIENT_LIGHT:
                statusMessage = "Not enough light in the scene";
                break;
            case StatusInfo.RELOCALIZING:
                // Display a relocalization message in the UI if:
                // * No AnchorBehaviours are being tracked
                // * None of the active/tracked AnchorBehaviours are in TRACKED status

                // Set the status message now and clear it if none of the conditions are met.
                statusMessage = "Point back to previously seen area and rescan to relocalize.";

                var activeAnchors = FindObjectsOfType<AnchorBehaviour>();
                // Cycle through all of the active AnchorBehaviours.
                foreach (var anchor in activeAnchors)
                {
                    if (anchor.TargetStatus.Status == Status.TRACKED)
                    {
                        // If at least one of the AnchorBehaviours has Tracked status,
                        // then don't display the relocalization message.
                        statusMessage = "";
                        break;
                    }
                }
                break;
        }

        StatusMessage.Display(statusMessage);
    }
}
