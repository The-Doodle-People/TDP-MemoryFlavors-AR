/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    [Header("Area Target Position")]
    public Transform AreaTargetTransform;
    
    [Header("Navigation Agent and its ARCamera")]
    public Transform ArCameraTransform;
    public NavMeshAgent NavigationAgent;
    
    [Header("Navigation Line")]
    public LineRenderer NavigationLine;
    
    Vector3 mAreaTargetOriginalPosition;
    Vector3? mCurrentDestination;

    bool mHasFoundAreaTarget;

    const float DISTANCE_THRESHOLD = 1.5f;

    void Awake()
    {
        mAreaTargetOriginalPosition = AreaTargetTransform.transform.position;
    }

    void Update()
    {
        UpdateNavigationAgentPosition();
        UpdateNavigationLineVisibility();
        UpdateNavigationLinePath();
    }

    void UpdateNavigationAgentPosition()
    {
        // updates the navigation Agent's position inside the NavMesh, based on the ARCamera's current position
        // in relation to the AreaTarget's current position
        var arCamPositionInAreaTarget = AreaTargetTransform.InverseTransformPoint(ArCameraTransform.position);
        NavigationAgent.transform.localPosition = arCamPositionInAreaTarget + mAreaTargetOriginalPosition;
    }
    
    void UpdateNavigationLineVisibility()
    {
        NavigationLine.enabled = mHasFoundAreaTarget && mCurrentDestination.HasValue;

        if (NavigationLine.enabled)
        {
            bool isCloseToDestination =
                Vector3.Distance(NavigationAgent.transform.position, mCurrentDestination.Value) < DISTANCE_THRESHOLD;
            if (isCloseToDestination)
            {
                NavigationLine.enabled = false;
            }
        }
    }
    
    void UpdateNavigationLinePath()
    {
        if (NavigationLine.enabled && !NavigationAgent.pathPending)
        {
            DrawPath();
        }
    }

    public void NavigateTo(Transform destinationTransform)
    {
        // position which is from the Moving AreaTarget space has to be transformed into the Static Navmesh space
        var localPositionInAreaTarget = AreaTargetTransform.InverseTransformPoint(destinationTransform.position);
        mCurrentDestination = localPositionInAreaTarget + mAreaTargetOriginalPosition;
        NavigationAgent.SetDestination(mCurrentDestination.Value);
    }

    void DrawPath()
    {
        NavigationLine.positionCount = NavigationAgent.path.corners.Length;
        // we have to transform the positions from the Static Navmesh space back to the Moving AreaTarget space
        var transformedNavigationCorners = new Vector3[NavigationLine.positionCount];
        for (int i = 0; i < transformedNavigationCorners.Length; i++)
            transformedNavigationCorners[i] = AreaTargetTransform.TransformPoint(NavigationAgent.path.corners[i] - mAreaTargetOriginalPosition);
        NavigationLine.SetPositions(transformedNavigationCorners);
    }
    
    public void OnAreaTargetFound()
    {
        mHasFoundAreaTarget = true; 
    }

    public void OnAreaTargetLost()
    {
        mHasFoundAreaTarget = false;
    }
}
