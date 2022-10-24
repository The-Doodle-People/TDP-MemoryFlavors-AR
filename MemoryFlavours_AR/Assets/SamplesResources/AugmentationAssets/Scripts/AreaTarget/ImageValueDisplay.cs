/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;
using UnityEngine.UI;

public class ImageValueDisplay : MonoBehaviour
{
    public Image ValueImage;
    public float BaseValue = 0.5f;
    public float Range = 0.05f;
    
    float mCurrentTime;

    public void OnValueChanged(float sliderValue)
    {
        var roundedValue = Mathf.RoundToInt(sliderValue);
        switch (roundedValue)
        {
            case 2:
                BaseValue = 0.95f;
                break;
            case 1:
                BaseValue = 0.75f;
                break;
            case 0:
            default:
                BaseValue = 0.45f;
                break;
        }
    }

    void Update()
    {
        var newValue = BaseValue + Random.Range(-Range, Range);
        ValueImage.fillAmount = newValue;
    }
}