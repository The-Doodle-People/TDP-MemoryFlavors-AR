/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererPatternShifter : MonoBehaviour
{
    public FlowDirection Direction = FlowDirection.POSITIVE;
    Material mMaterial;
    
    float mTime;
    
    void Awake()
    {
        mMaterial = GetComponent<LineRenderer>().material;
    }

    void Update()
    {
        mMaterial.mainTextureOffset = new Vector2(mTime * ((int)Direction), 0);
        mTime += Time.deltaTime;
        if (mTime >= 1)
            mTime = 0;
    }

    public enum FlowDirection
    {
        POSITIVE = 1,
        NEGATIVE = -1
    }
}