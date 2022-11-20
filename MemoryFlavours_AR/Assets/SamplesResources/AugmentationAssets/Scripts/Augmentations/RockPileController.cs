/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;

public class RockPileController : MonoBehaviour
{
    public Transform GrabbableRock;
    FadeObject mFadeController;
    
    void Awake()
    {
        mFadeController = GetComponent<FadeObject>();
    }
    
    public void FadeOut()
    {
        mFadeController.IsVisible = false;

        if (GrabbableRock != null)
        {
            var rockFader = GrabbableRock.GetComponent<FadeObject>();
            rockFader.IsVisible = false;
        }
    }

    public void FadeIn()
    {
        mFadeController.IsVisible = true;

        if (GrabbableRock != null)
        {
            var rockFader = GrabbableRock.GetComponent<FadeObject>();
            rockFader.IsVisible = true;
        }
    }
}

