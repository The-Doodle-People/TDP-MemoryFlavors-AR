/*===============================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using UnityEditor;

namespace VFX
{
    public class VFXContextMenu
    {
        [MenuItem("GameObject/Vuforia Engine/VFX/Apply Material")]
        static void ApplyMaterial(MenuCommand command)
        {
            var go = command.context as GameObject;
            var wizard = ScriptableWizard.DisplayWizard<MaterialSelectionWizard>("Apply Material", "Apply");
            wizard.SelectedObjects = new [] { go };
        }
    }
}