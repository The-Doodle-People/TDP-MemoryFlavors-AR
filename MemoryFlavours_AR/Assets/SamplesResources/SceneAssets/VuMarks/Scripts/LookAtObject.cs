/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform LookAtTargetTransform;

    void Update()
    {
        if (LookAtTargetTransform != null)
        {
            var targetDirection = transform.position - LookAtTargetTransform.position;
            transform.forward = targetDirection;
        }
    }
}
