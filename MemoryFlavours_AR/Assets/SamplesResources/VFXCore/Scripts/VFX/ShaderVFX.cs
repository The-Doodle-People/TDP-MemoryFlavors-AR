/*===============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/

using UnityEngine;
using UnityEngine.Events;

namespace VFX
{
    /// <summary>
    /// A script that controls and animates a set of shader properties.
    /// Properties can be either 'static' or 'dynamic':
    /// - dynamic properties are animated by interpolating between start and end values;
    /// - static properties are set to a constant value.
    /// Properties can be of type:
    /// - 'scalar' (float value)
    /// - 'vector'
    /// - 'color'
    /// The shader properties should be a subset of
    /// those exposed by the shader of the specified material.
    /// </summary>
    public class ShaderVFX : MonoBehaviour
    {
        [Tooltip("The object to which the effect applies.")]
        public GameObject TargetObject;

        [Tooltip("The effect material.")]
        public Material EffectMaterial;

        [Tooltip("Whether the property animation should run be started automatically.")]
        public bool PlayOnAwake = true;

        [Tooltip("Whether the animation should loop.")]
        public bool Loop = false;

        [Tooltip("Should the effect animation auto-rewind at every play.")]
        public bool AnimationAutoRewind = true;

        [Tooltip("Animation transition mode (linear vs cubic.")]
        public TransitionMode TransitionMode = TransitionMode.LINEAR;

        [Header("Shader Properties")]
        [Tooltip("The shader scalar (float) static properties.")]
        public StaticScalarProperty[] StaticScalarProperties;

        [Tooltip("The shader vector static properties.")]
        public StaticVectorProperty[] StaticVectorProperties;

        [Tooltip("The shader static colors.")]
        public StaticColor[] StaticColors;

        [Tooltip("The shader scalar (float) animated properties.")]
        public DynamicScalarProperty[] DynamicScalarProperties;

        [Tooltip("The shader scalar (float) animated properties.")]
        public DynamicVectorProperty[] DynamicVectorProperties;

        [Tooltip("The shader animated color properties.")]
        public DynamicColor[] DynamicColors;


        [Header("Events")]
        public UnityEvent OnEffectStarted;
        public UnityEvent OnReachedEnd;
        
        protected Material mSharedMaterial;
        protected float mAnimationTime;
        protected bool mIsPlaying;

        protected virtual void Awake()
        {
            mAnimationTime = 0;
            mIsPlaying = false;
            
            if (PlayOnAwake)
                Play();
        }

        protected virtual void Update()
        {
            if (!mIsPlaying)
                return;

            mAnimationTime += Time.deltaTime;
            var maxTime = 0f;

            UpdateDynamicScalarProperties(ref maxTime);
            UpdateDynamicVectorProperties(ref maxTime);
            UpdateDynamicColors(ref maxTime);

            if (mAnimationTime > maxTime)
            {
                OnReachedEnd?.Invoke();

                if (Loop)
                    Rewind();
                else
                    Pause();
            }
        }

        public void Play()
        {
            if (mIsPlaying)
                return;

            InitMaterial();
            InitStaticProperties();
            InitDynamicProperties();

            if (AnimationAutoRewind)
                Rewind();

            mIsPlaying = true;
            OnEffectStarted?.Invoke();
        }

        public void Pause()
        {
            mIsPlaying = false;
        }

        public void Rewind()
        {
            mAnimationTime = 0;
        }

        public void RewindAndPlay()
        {
            Rewind();
            Play();
        }

        public void Stop()
        {
            mIsPlaying = false;
            mAnimationTime = 0;
        }

        void UpdateDynamicColors(ref float maxTime)
        {
            foreach (var property in DynamicColors)
            {
                var currentTime = SetMaxTimeAndGetLerpValue(ref maxTime, property.delay, property.duration);
                var lerpPropColor = Color.Lerp(property.startColor, property.endColor, currentTime);
                MaterialUtil.TrySetColorProperty(mSharedMaterial, property.name, lerpPropColor);
            }
        }

        void UpdateDynamicVectorProperties(ref float maxTime)
        {
            foreach (var property in DynamicVectorProperties)
            {
                var currentTime = SetMaxTimeAndGetLerpValue(ref maxTime, property.delay, property.duration);
                var lerpValue = Vector3.Lerp(property.startValue, property.endValue, currentTime);
                MaterialUtil.TrySetVector4Property(mSharedMaterial, property.name, lerpValue);
            }
        }

        void UpdateDynamicScalarProperties(ref float maxTime)
        {
            foreach (var property in DynamicScalarProperties)
            {
                var currentTime = SetMaxTimeAndGetLerpValue(ref maxTime, property.delay, property.duration);
                var lerpValue = Mathf.Lerp(property.startValue, property.endValue, currentTime);
                MaterialUtil.TrySetFloatProperty(mSharedMaterial, property.name, lerpValue);
            }
        }

        float SetMaxTimeAndGetLerpValue(ref float maxTime, float delay, float duration)
        {
            if (delay + duration > maxTime)
                maxTime = delay + duration;

            var propAnimTime = Mathf.Max(0, mAnimationTime - delay);
            var currentTime = duration == 0 ? 1 : Mathf.Clamp01(propAnimTime / duration);
                
            if (TransitionMode == TransitionMode.CUBIC)
                currentTime = Mathf.SmoothStep(0, 1, currentTime);
            
            return currentTime;
        }

        void InitMaterial()
        {
            if (!TargetObject)
            {
                Debug.LogError("Failed to init renderers material, target object not set!");
                return;
            }

            if (!mSharedMaterial && EffectMaterial)
                mSharedMaterial = new Material(EffectMaterial);

            if (mSharedMaterial)
            {
                var renderers = TargetObject.GetComponentsInChildren<Renderer>(includeInactive: true);
                foreach (var aRenderer in renderers)
                    aRenderer.sharedMaterial = mSharedMaterial;
            }
        }

        void InitStaticProperties()
        {
            foreach (var property in StaticScalarProperties)
                MaterialUtil.TrySetFloatProperty(mSharedMaterial, property.name, property.value);

            foreach (var property in StaticVectorProperties)
                MaterialUtil.TrySetVector4Property(mSharedMaterial, property.name, property.value);

            foreach (var color in StaticColors)
                MaterialUtil.TrySetColorProperty(mSharedMaterial, color.name, color.color);
        }

        void InitDynamicProperties()
        {
            foreach (var property in DynamicScalarProperties)
                MaterialUtil.TrySetFloatProperty(mSharedMaterial, property.name, property.startValue);

            foreach (var property in DynamicVectorProperties)
                MaterialUtil.TrySetVector4Property(mSharedMaterial, property.name, property.startValue);

            foreach (var color in DynamicColors)
                MaterialUtil.TrySetColorProperty(mSharedMaterial, color.name, color.startColor);
        }
    }
}