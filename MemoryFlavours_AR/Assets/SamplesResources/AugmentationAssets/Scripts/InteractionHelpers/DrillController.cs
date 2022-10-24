/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;

public class DrillController : MonoBehaviour
{
    public bool IsDrilling;
    
    float mDrillLerpPercentage = 0.0f;
    
    void Update()
    {
        if (IsDrilling && mDrillLerpPercentage < 1.0f)
        {
            mDrillLerpPercentage += Time.deltaTime * 3.0f;

            if (mDrillLerpPercentage > 1.0f)
                mDrillLerpPercentage = 1.0f;

            transform.localScale = Vector3.one * mDrillLerpPercentage;
        }
        else if (!IsDrilling && mDrillLerpPercentage > 0.0f)
        {
            mDrillLerpPercentage -= Time.deltaTime * 3.0f;

            if (mDrillLerpPercentage < 0.0f)
                mDrillLerpPercentage = 0.0f;

            transform.localScale = Vector3.one * mDrillLerpPercentage;
        }
    }
}
