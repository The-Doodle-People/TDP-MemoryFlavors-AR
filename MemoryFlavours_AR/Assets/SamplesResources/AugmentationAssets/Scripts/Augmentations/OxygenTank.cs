/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

public class OxygenTank : Augmentation
{
    const string ANIMATION_IS_DETAIL_ON = "IsDetailOn";
    const string ANIMATION_DO_ENTER = "DoEnter";
    
    bool IsDetailOn
    {
        get => mAnimator.GetBool(ANIMATION_IS_DETAIL_ON);
        set => mAnimator.SetBool(ANIMATION_IS_DETAIL_ON, value);
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        IsDetailOn = false;
        OnEnterEvent.Invoke();
    }

    public void ShowDetail()
    {
        IsDetailOn = true;
    }

    public void HideDetail()
    {
        IsDetailOn = false;
    }

    public void HandleVirtualButtonPressed()
    {
        ShowDetail();
    }

    public void HandleVirtualButtonReleased()
    {
        HideDetail();
    }
    
    void DoEnter()
    {
        mAnimator.SetTrigger(ANIMATION_DO_ENTER);
    }
}

