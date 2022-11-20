/*========================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEditor;
using UnityEngine;

namespace VFX
{
    /// <summary>
    /// An Editor Wizard that allows selecting a material
    /// and applying to one of more selected gamed objects.
    /// </summary>
    public class MaterialSelectionWizard : ScriptableWizard
    {
        [Tooltip("The material that will be applied to the selected objects.")]
        public Material Material;

        [Tooltip("The target objects the material will be applied to.")]
        public GameObject[] SelectedObjects;


        void OnWizardCreate()
        {
            ApplyMaterial(SelectedObjects, Material);
        }

        void OnWizardUpdate()
        {
            helpString = "Please choose the material to apply.";
        }

        static void ApplyMaterial(GameObject[] objects, Material aMaterial)
        {
            if (objects == null || objects.Length == 0)
            {
                var errorMsg = "No objects selected.";
                Debug.LogWarning(errorMsg);
                EditorUtility.DisplayDialog("Material not applied", errorMsg, "OK");
                return;
            }

            if (aMaterial == null)
            {
                var errorMsg = "No material selected.";
                Debug.LogWarning(errorMsg);
                EditorUtility.DisplayDialog("Material not applied", errorMsg, "OK");
                return;
            }

            foreach (var obj in objects)
                MaterialUtil.ApplyMaterial(obj, aMaterial, applyToChildren: true);
        }
    }
}