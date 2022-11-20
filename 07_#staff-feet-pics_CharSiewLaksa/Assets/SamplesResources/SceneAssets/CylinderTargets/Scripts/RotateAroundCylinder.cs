/*============================================================================== 
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/
using UnityEngine;

public class RotateAroundCylinder : MonoBehaviour
{
    Animator mDroneAnimator;
    const float ROTATION_SPEED = -60;
    
    void Start()
    {
        mDroneAnimator = GetComponent<Animator>();

        if (mDroneAnimator)
        {
            mDroneAnimator.SetBool("IsFacingObject", true);
            mDroneAnimator.SetBool("IsShowingLaser", true);
            mDroneAnimator.SetBool("IsScanning", true);
        }
    }

    void Update()
    {
        var parentTransform = transform.parent;
        transform.RotateAround(parentTransform.position, parentTransform.up, ROTATION_SPEED * Time.deltaTime);
    }
}
