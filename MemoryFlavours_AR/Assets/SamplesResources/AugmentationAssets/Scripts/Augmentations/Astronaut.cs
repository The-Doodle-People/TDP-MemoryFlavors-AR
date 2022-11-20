/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;

public class Astronaut : Augmentation
{
    public DrillController Drill;
    public ParticleSystem DrillEffectSmoke;
    public ParticleSystem DrillEffectRocks;
    public RockPileController RockPile;

    const string ANIMATION_IS_DRILLING = "IsDrilling";
    const string ANIMATION_IS_WAVING = "IsWaving";
    
    bool IsDrilling
    {
        get => mAnimator.GetBool(ANIMATION_IS_DRILLING);
        set => mAnimator.SetBool(ANIMATION_IS_DRILLING, value);
    }

    bool IsWaving
    {
        get => mAnimator.GetBool(ANIMATION_IS_WAVING);
        set => mAnimator.SetBool(ANIMATION_IS_WAVING, value);
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        OnEnterEvent.Invoke();
    }

    public override void OnExit()
    {
        base.OnExit();
        Drill.IsDrilling = false;
        IsWaving = false;
    }

    public void StartDrilling()
    {
        IsDrilling = true;
    }

    public void SetAnimationIsDrillingToTrue()
    {
        Drill.IsDrilling = true;
    }
    
    public void SetAnimationIsDrillingToFalse()
    {
        Drill.IsDrilling = false;
    }

    public void SetAnimationDrillEffectOn()
    {
        SetDrillEffect(true);
    }
    
    public void SetAnimationDrillEffectOff()
    {
        SetDrillEffect(false);
    }
    
    void SetDrillEffect(bool value)
    {
        var emissionSmoke = DrillEffectSmoke.emission;
        emissionSmoke.enabled = value;

        var emissionRocks = DrillEffectRocks.emission;
        emissionRocks.enabled = value;

        if (value)
        {
            DrillEffectSmoke.Play();
            DrillEffectRocks.Play();
            RockPile.FadeIn();
        }
        else
            RockPile.FadeOut();
    }

    public void SetAnimationWavingOff()
    {
        IsWaving = false;
    }

    public void HandleVirtualButtonPressed()
    {
        IsWaving = true;
    }

    public void HandleVirtualButtonReleased()
    {

    }
}

