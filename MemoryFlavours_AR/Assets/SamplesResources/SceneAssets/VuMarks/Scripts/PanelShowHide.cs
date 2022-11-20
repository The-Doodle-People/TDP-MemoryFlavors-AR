/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.UI;

public class PanelShowHide : MonoBehaviour
{
    public Animator PanelAnimator;
    public Image PanelImage;
    public Text PanelInfo;

    const string ANIMATION_HIDE_PANEL = "HidePanel";
    const string ANIMATION_SHOW_PANEL = "ShowPanel";
    const string ANIMATION_SHOW_ANIM_NAME = "ShowAnim";

    public void Hide()
    {
        PanelAnimator.SetTrigger(ANIMATION_HIDE_PANEL);
    }

    public void Show(string vuMarkId, string vuMarkDataType, string vuMarkDesc, Sprite vuMarkImage)
    {
        PanelAnimator.ResetTrigger(ANIMATION_HIDE_PANEL);

        PanelInfo.text =
            "<color=yellow>VuMark Instance Id: </color>" +
            "\n" + vuMarkId + " - " + vuMarkDesc +
            "\n\n<color=yellow>VuMark Type: </color>" +
            "\n" + vuMarkDataType;

        PanelImage.sprite = vuMarkImage;

        if (!PanelAnimator.GetCurrentAnimatorStateInfo(0).IsName(ANIMATION_SHOW_ANIM_NAME))
            PanelAnimator.SetTrigger(ANIMATION_SHOW_PANEL);
    }

    public void ResetShowTrigger()
    {
        PanelAnimator.ResetTrigger(ANIMATION_SHOW_PANEL);
    }
}
