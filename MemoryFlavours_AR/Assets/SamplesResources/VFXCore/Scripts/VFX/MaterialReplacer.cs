/*===============================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using System.Collections;
using UnityEngine;


namespace VFX
{
    /// <summary>
    /// Component that enables replacing the materials (shaders) used in a game-object hierarchy,
    /// while preserving (optionally) the colors and textures of the original materials.
    /// </summary>
    public class MaterialReplacer : MonoBehaviour
    {
        public Material ReplacementMaterial;
        public bool KeepOriginalColor = true;
        public bool KeepOriginalTexture = true;
        public bool ReplaceOnAwake = true;

        /// <summary>
        /// How many materials we can change in a single frame
        /// while ensuring a smooth rendering.
        /// </summary>
        const int MAX_MATERIALS_CHANGED_PER_FRAME = 50;

        void Awake()
        {
            if (ReplaceOnAwake)
                Replace();
        }

        public void Replace()
        {
            if (!ReplacementMaterial)
            {
                Debug.LogError("Replacement Material not set!");
                return;
            }

            StartCoroutine(ReplaceMaterials());   
        }

        IEnumerator ReplaceMaterials()
        {
            var renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
            for (int i = 0; i < renderers.Length; i++)
            {
                var rnd = renderers[i];
                var origMat = rnd.material;
                var newMat = new Material(ReplacementMaterial);

                if (KeepOriginalColor && origMat.HasProperty("_Color") && newMat.HasProperty("_Color"))
                    newMat.color = origMat.color;

                if (KeepOriginalTexture && origMat.HasProperty("_MainTex") && newMat.HasProperty("_MainTex"))
                    newMat.mainTexture = origMat.mainTexture;

                rnd.material = newMat;
                
                if (i % MAX_MATERIALS_CHANGED_PER_FRAME == 0)
                    yield return null;
            }
        }
    }
}
