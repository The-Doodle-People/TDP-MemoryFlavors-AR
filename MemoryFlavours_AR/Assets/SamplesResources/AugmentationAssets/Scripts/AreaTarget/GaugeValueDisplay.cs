/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;

public class GaugeValueDisplay : MonoBehaviour
{
    public RectTransform Pointer;
    public float BaseValue = 0.5f;
    public float Range = 0.05f;
    
    float mCurrentTime;

    const float GAUGE_VALUE_RANGE = 65f;
    
    void Update()
    {
        var newValue = BaseValue + Random.Range(-Range, Range);
        SetPointerToValue(newValue);
    }

    void SetPointerToValue(float percentage)
    {
        Pointer.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(GAUGE_VALUE_RANGE, -GAUGE_VALUE_RANGE,percentage));
    }
}