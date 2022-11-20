/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

public class Drone : Augmentation
{
    const string ANIMATION_IS_FACING_OBJECT = "IsFacingObject";
    const string ANIMATION_IS_SCANNING = "IsScanning";
    const string ANIMATION_IS_SHOWING_LASER = "IsShowingLaser";
    
    bool IsFacingObject
    {
        get => mAnimator.GetBool(ANIMATION_IS_FACING_OBJECT);
        set => mAnimator.SetBool(ANIMATION_IS_FACING_OBJECT, value);
    }

    bool IsScanning
    {
        get => mAnimator.GetBool(ANIMATION_IS_SCANNING);
        set => mAnimator.SetBool(ANIMATION_IS_SCANNING, value);
    }

    bool IsShowingLaser
    {
        get => mAnimator.GetBool(ANIMATION_IS_SHOWING_LASER);
        set => mAnimator.SetBool(ANIMATION_IS_SHOWING_LASER, value);
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        OnEnterEvent.Invoke();
    }

    public override void OnExit()
    {
        base.OnExit();
        IsScanning = false;
        IsFacingObject = false;
        IsShowingLaser = false;
    }

    public void SetAnimationScanning(bool value)
    {
        IsScanning = value;
        IsShowingLaser = value;
        if(value)
            IsFacingObject = true;
    }
    
    public void HandleVirtualButtonPressed()
    {
        SetAnimationScanning(true);
    }

    public void HandleVirtualButtonReleased()
    {
        SetAnimationScanning(false);
    }
}

