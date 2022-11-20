/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;

namespace VFX
{
    /// <summary>
    /// Utility class to set custom materials.
    /// </summary>
    public static class MaterialUtil
    {
        /// <summary>
        /// Apply a given material to a GameObject and (optionally) to all of the children in its scene hierarchy.
        /// </summary>
        /// <param name="gameObject">GameObject to apply the Material to</param>
        /// <param name="material">Applicable Material</param>
        /// <param name="applyToChildren">Recursively apply the material or not</param>
        public static void ApplyMaterial(GameObject gameObject, Material material, bool applyToChildren)
        {
            if (!gameObject || !material)
                return;

            var aRenderer = gameObject.GetComponent<Renderer>();
            if (aRenderer)
                ApplyMaterial(aRenderer, material);

            if (applyToChildren)
            {
                var renderers = gameObject.GetComponentsInChildren<Renderer>();
                foreach (var childRenderer in renderers)
                    ApplyMaterial(childRenderer, material);
            }
        }

        /// <summary>
        /// Applies a material to a Renderer.
        /// </summary>
        /// <param name="aRenderer">The Renderer to apply the Material to</param>
        /// <param name="material">Applicable Material</param>
        public static void ApplyMaterial(Renderer aRenderer, Material material)
        {
            if (!aRenderer || !material)
                return;

            aRenderer.sharedMaterial = material;

            // Also handle the case of multiple sub-meshes
            // (multiple materials per mesh)
            if (aRenderer.sharedMaterials != null && aRenderer.sharedMaterials.Length > 0)
            {
                var materials = new Material[aRenderer.sharedMaterials.Length];
                for (int i = 0; i < materials.Length; i++)
                    materials[i] = material;

                aRenderer.sharedMaterials = materials;
            }
        }

        /// <summary>
        /// Apply a given opacity (alpha) to a GameObject and
        /// (optionally) to all of the children in its scene hierarchy.
        /// </summary>
        /// <param name="gameObject">GameObject to apply the given Alpha value to</param>
        /// <param name="alpha">Alpha value</param>
        /// <param name="applyToChildren">Recursively apply the Alpha value or not</param>
        public static void ApplyAlpha(GameObject gameObject, float alpha, bool applyToChildren)
        {
            if (!gameObject)
                return;

            var aRenderer = gameObject.GetComponent<Renderer>();
            if (aRenderer)
                ApplyAlpha(aRenderer, alpha);
            
            if (applyToChildren)
            {
                var renderers = gameObject.GetComponentsInChildren<Renderer>();
                foreach (var childRenderer in renderers)
                    ApplyAlpha(childRenderer, alpha);
            }
        }

        /// <summary>
        /// Apply custom opacity (alpha) to a Renderer.
        /// </summary>
        /// <param name="aRenderer">The Renderer to apply the given Alpha value to</param>
        /// <param name="alpha">Alpha value</param>
        public static void ApplyAlpha(Renderer aRenderer, float alpha)
        {
            if (aRenderer.sharedMaterial && aRenderer.sharedMaterial.HasProperty("_Color"))
            {
                var color = aRenderer.sharedMaterial.color;
                aRenderer.sharedMaterial.color = new Color(color.r, color.g, color.b, alpha);
            }

            // Also handle the case of multiple sub-meshes
            // (multiple materials per mesh)
            var materials = aRenderer.sharedMaterials;
            if (materials != null && materials.Length > 0)
            {
                foreach (var material in materials)
                {
                    if (material.HasProperty("_Color"))
                    {
                        var color = material.color;
                        material.color = new Color(color.r, color.g, color.b, alpha);
                    }
                }
            }
        }
        
        public static void TrySetVector4Property(Material material, string propertyName, Vector4 value)
        {
            if (material != null && material.HasProperty(propertyName))
                material.SetVector(propertyName, value);
        }

        public static void TrySetFloatProperty(Material material, string propertyName, float value)
        {
            if (material != null && material.HasProperty(propertyName))
                material.SetFloat(propertyName, value);
        }
        
        public static void TrySetColorProperty(Material material, string propertyName, Color color)
        {
            if (material && material.HasProperty(propertyName))
                material.SetColor(propertyName, color);
        }
    }
}
