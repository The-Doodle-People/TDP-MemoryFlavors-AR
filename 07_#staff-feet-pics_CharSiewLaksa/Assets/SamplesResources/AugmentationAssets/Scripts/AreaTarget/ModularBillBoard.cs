/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;

public class ModularBillBoard : MonoBehaviour
{
    [Header("AR Camera")]
    public Transform ArCamera; 
    [Header("Canvas Content")]
    public GameObject LandmarkIcon;
    public GameObject LandmarkLeft;
    public GameObject LandmarkRight;
    public RectTransform Mask;
    public CanvasGroup ContentCanvasGroup;
    [Header("Other Content")] 
    public GameObject Content;
    [Header("Icon Opening Range")] 
    public float OpeningStarted = 2.5f;
    public float OpeningFinished = 1.5f;

    
    BillBoardState mState = BillBoardState.CLOSED;
    // Animation Values, where the ContentMask would roll down after it is fully opened
    float mCurrentRollOutTime;
    bool mHasBackgroundRolledOut;
    float mMinPositionY = -2.5f;
    float mMaxPositionY;
    float mMinSizeY = 56f;
    float mMaxSizeY;
    const float ROLL_TIME = 0.25f;
    // Animation Values, where the ContentMask's content would be visible
    float mAlpha = -1f;
    // Animation Values, where the LandMarkIcons would split and the ContentMask would spread out between them
    float mOpeningDistance; 
    Vector3 mLandmarkLEnd;
    Vector3 mLandmarkLStart;
    Vector3 mLandmarkREnd;
    Vector3 mLandmarkRStart;
    Vector2 mMaskStartSize;
    Vector2 mMaskEndSize;
    
    void Awake()
    {
        LandmarkIcon.SetActive(true);
        LandmarkLeft.SetActive(false);
        LandmarkRight.SetActive(false);
        Mask.gameObject.SetActive(false);
        if(Content != null)
            Content.SetActive(false);

        CalculateDerivedValues();
    }

    void CalculateDerivedValues()
    {
        mOpeningDistance = OpeningStarted - OpeningFinished;
        CalculateMaskSize();
        CalculateLandmarkPosition();
    }
    
    void CalculateMaskSize()
    {
        mMaxSizeY = Mask.sizeDelta.y;
        mMaxPositionY = Mask.localPosition.y;
        Mask.sizeDelta = new Vector2(Mask.sizeDelta.x, mMinSizeY);
        Mask.localPosition = new Vector3(Mask.localPosition.x, mMinPositionY, Mask.localPosition.z);
    }
    
    void CalculateLandmarkPosition()
    {
        var contentMaskSize = Mask.sizeDelta;
        var leftLandmarkLocalPosition = LandmarkLeft.transform.localPosition;
        mLandmarkLStart = leftLandmarkLocalPosition;
        mLandmarkLEnd = new Vector3(leftLandmarkLocalPosition.x - contentMaskSize.x / 2f,
            leftLandmarkLocalPosition.y, leftLandmarkLocalPosition.z);
        
        var rightLandmarkLocalPosition = LandmarkRight.transform.localPosition;
        mLandmarkRStart = rightLandmarkLocalPosition;
        mLandmarkREnd = new Vector3(rightLandmarkLocalPosition.x + contentMaskSize.x / 2f,
            rightLandmarkLocalPosition.y, rightLandmarkLocalPosition.z);
        
        mMaskStartSize = new Vector2(0, contentMaskSize.y);
        mMaskEndSize = contentMaskSize;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - ArCamera.position);
        
        var currentDistanceFromArCamera = Vector3.Distance(transform.position, ArCamera.position);
        // Billboard's state machine
        if (currentDistanceFromArCamera <= OpeningFinished)
            FullyOpened();
        else if (currentDistanceFromArCamera >= OpeningStarted)
            Closed();
        else
            Opening(currentDistanceFromArCamera);
    }
    
    void Opening(float currentDistanceFromArCamera)
    {
        SwitchtoOpening();
        var currentPercentage = Mathf.Abs(OpeningStarted - currentDistanceFromArCamera) / mOpeningDistance;
        LandmarkLeft.transform.localPosition = Vector3.Lerp(mLandmarkLStart, mLandmarkLEnd, currentPercentage);
        LandmarkRight.transform.localPosition = Vector3.Lerp(mLandmarkRStart, mLandmarkREnd, currentPercentage);
        Mask.sizeDelta = Vector2.Lerp(mMaskStartSize, mMaskEndSize, currentPercentage);
    }

    void Closed()
    {
        SwitchToClosed();
    }

    void FullyOpened()
    {
        SwitchedToOpen();
        if (!mHasBackgroundRolledOut)
            RollOutTheBackgroundForContent();
        else
            TurnContentVisible();
    }

    void TurnContentVisible()
    {
        if (mAlpha > 0)
        {
            mAlpha -= Time.deltaTime;
            ContentCanvasGroup.alpha = 1 - mAlpha;
            if (mAlpha <= 0)
                ContentCanvasGroup.alpha = 1;
        }
    }

    void RollOutTheBackgroundForContent()
    {
        mCurrentRollOutTime += Time.deltaTime / ROLL_TIME;
        
        Mask.sizeDelta = new Vector2(Mask.sizeDelta.x, Mathf.Lerp(mMinSizeY,
            mMaxSizeY, mCurrentRollOutTime));
        Mask.localPosition = Vector3.Lerp(new Vector3(0,mMinPositionY, 0), 
            new Vector3(0, mMaxPositionY, 0), mCurrentRollOutTime);
        
        if (mCurrentRollOutTime >= 1)
        {
            mCurrentRollOutTime = 0;
            mHasBackgroundRolledOut = true;
        }
    }

    void SwitchedToOpen()
    {
        if(mState == BillBoardState.OPENED) 
            return;
        mState = BillBoardState.OPENED;
        LandmarkIcon.SetActive(false);
        LandmarkLeft.SetActive(true);
        LandmarkRight.SetActive(true);
        mAlpha = 1;
        if (Content != null)
            Content.SetActive(true);
    }
    
    void SwitchToClosed()
    {
        if (mState == BillBoardState.CLOSED)
            return;
        mState = BillBoardState.CLOSED;
        LandmarkIcon.SetActive(true);
        LandmarkLeft.SetActive(false);
        LandmarkRight.SetActive(false);
        Mask.gameObject.SetActive(false);
    }
    
    void SwitchtoOpening()
    {
        if (mState == BillBoardState.OPENING)
            return;
        mState = BillBoardState.OPENING;
        LandmarkIcon.SetActive(false);
        LandmarkLeft.SetActive(true);
        LandmarkRight.SetActive(true);
        Mask.gameObject.SetActive(true);
        mCurrentRollOutTime = 0;
        mHasBackgroundRolledOut = false;
        mAlpha = -1;
        ContentCanvasGroup.alpha = 0;
        if (Content != null)
            Content.SetActive(false);
    }

    enum BillBoardState
    {
        OPENED,
        CLOSED,
        OPENING
    }
}