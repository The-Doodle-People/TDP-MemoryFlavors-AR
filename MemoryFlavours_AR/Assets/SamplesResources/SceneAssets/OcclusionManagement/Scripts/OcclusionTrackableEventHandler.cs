/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;

public class OcclusionTrackableEventHandler : DefaultObserverEventHandler
{
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        // Hide the sides of the MultiTarget
        var rendererComponents = GetComponentsInChildren<Renderer>();
        foreach (var component in rendererComponents)
        {
            if (component.name.Contains("Box."))
                component.enabled = false;
        }
    }
}
