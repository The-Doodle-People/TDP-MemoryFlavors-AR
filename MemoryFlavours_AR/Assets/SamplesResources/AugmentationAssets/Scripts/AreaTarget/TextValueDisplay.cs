/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;
using UnityEngine.UI;

public class TextValueDisplay : MonoBehaviour
{
    public Text TextValue;
    public float BaseValue = 0.5f;
    public float Range = 0.1f;
    
    float mCurrentTime = 0.5f;
    float mUpdateFrequency = 0.5f;

    void Update()
    {
        mCurrentTime += Time.deltaTime;
        if (mCurrentTime >= mUpdateFrequency)
        {
            mCurrentTime = 0;
            var newValue = BaseValue + Random.Range(-Range, Range);
            TextValue.text = newValue.ToString("F2");
        }
    }
}