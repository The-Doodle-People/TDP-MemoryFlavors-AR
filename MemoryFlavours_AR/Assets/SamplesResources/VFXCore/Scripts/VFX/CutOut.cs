/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;

namespace VFX
{
    /// <summary>
    /// A script to handle Cut Out visual FX on mesh surfaces.
    /// This component requires a Cut Out Shader (material) set on the targetScene meshes. 
    /// </summary>
    public class CutOut : MonoBehaviour
    {
        [Tooltip("The target scene to which we apply this cutout effect.")]
        public Transform TargetScene;

        [Tooltip("The radius of the cutout.")]
        public float Radius = 0.5f;

        const string PROPERTY_CENTER = "_Center";
        const string PROPERTY_CLIP_CENTER = "_ClipCenter";
        const string PROPERTY_CUT_CENTER = "_CutCenter";
        const string PROPERTY_CUTOUT_CENTER = "_CutOutCenter";
        const string PROPERTY_RADIUS = "_Radius";
        const string PROPERTY_CLIP_RADIUS = "_ClipRadius";
        const string PROPERTY_CUT_RADIUS = "_CutRadius";
        const string PROPERTY_CUTOUT_RADIUS = "_CutOutRadius";
        
        void Update()
        {
            if (!TargetScene)
                return;
            
            // Get the cut-out center (in world CS)
            var center = transform.position;

            // Get the renderers and update shader properties
            var renderers = TargetScene.GetComponentsInChildren<Renderer>(true);
            foreach (var aRenderer in renderers)
            {
                var rendererMaterial = aRenderer.material;
                // Set the cut-out center in the shader
                MaterialUtil.TrySetVector4Property(rendererMaterial, PROPERTY_CENTER, center);
                MaterialUtil.TrySetVector4Property(rendererMaterial, PROPERTY_CLIP_CENTER, center);
                MaterialUtil.TrySetVector4Property(rendererMaterial, PROPERTY_CUT_CENTER, center);
                MaterialUtil.TrySetVector4Property(rendererMaterial, PROPERTY_CUTOUT_CENTER, center);

                // Set the cut-out radius in the shader
                MaterialUtil.TrySetFloatProperty(rendererMaterial, PROPERTY_RADIUS, Radius);
                MaterialUtil.TrySetFloatProperty(rendererMaterial, PROPERTY_CLIP_RADIUS, Radius);
                MaterialUtil.TrySetFloatProperty(rendererMaterial, PROPERTY_CUT_RADIUS, Radius);
                MaterialUtil.TrySetFloatProperty(rendererMaterial, PROPERTY_CUTOUT_RADIUS, Radius);
            }
        }
    }
}