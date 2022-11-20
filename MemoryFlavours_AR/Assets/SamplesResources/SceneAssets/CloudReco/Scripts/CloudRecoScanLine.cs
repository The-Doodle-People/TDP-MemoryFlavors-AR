/*===============================================================================
Copyright (c) 2016-2018 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using Vuforia;

public class CloudRecoScanLine : MonoBehaviour
{
    public CloudRecoBehaviour CloudRecognitionBehaviour;
    public UnityEngine.UI.Image ScanLine;
    
    bool mIsCacheEnabled;
    Renderer mRenderer;
        
    const float SCAN_DURATION_IN_SECONDS = 4;
    float mScanLineYPositionPercentage;
    float mDirection;
    
    bool CloudEnabled
    {
        get { return CloudRecognitionBehaviour && CloudRecognitionBehaviour.RecoStarted; }
    }

    void Start()
    {
        // Cache the Cloud enable state so that we can reset the scanline
        // when the enabled state changes
        mIsCacheEnabled = CloudEnabled;
    }

    void Update()
    {
        if (mIsCacheEnabled != CloudEnabled)
        {
            mIsCacheEnabled = CloudEnabled;
            // Reset the ScanLine position when Cloud enabled state changes
            mScanLineYPositionPercentage = 0;
            mDirection = 1;
        }
        
        ScanLine.enabled = CloudEnabled; // show/hide scanline
        if (CloudEnabled)
        {
            mScanLineYPositionPercentage += Time.deltaTime / SCAN_DURATION_IN_SECONDS * mDirection;
            ScanLine.rectTransform.position = new Vector3(ScanLine.rectTransform.position.x, 
                mScanLineYPositionPercentage * Screen.height, 0);
            if (mScanLineYPositionPercentage > 1)
                mDirection = -1;
            else if (mScanLineYPositionPercentage <= 0)
                mDirection = 1;
        }
    }
}
