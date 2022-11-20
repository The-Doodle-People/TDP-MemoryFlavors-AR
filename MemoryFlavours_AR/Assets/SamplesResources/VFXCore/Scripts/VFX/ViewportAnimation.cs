/*========================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;
using UnityEngine.Events;

namespace VFX
{
    /// <summary>
    /// A script that controls a simple viewport rect animation,
    /// where the rect position and size is interpolated between start and end values.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class ViewportAnimation : MonoBehaviour
    {
        public enum TransitionMode
        {
            LINEAR,
            CUBIC
        }

        [Tooltip("Whether the animation should run be started automatically.")]
        public bool PlayOnAwake = true;

        [Tooltip("Start delay (in seconds) of the animation.")]
        public float Delay = 0;

        [Tooltip("Duration (in seconds) of the animation.")]
        public float Duration = 1;

        [Tooltip("Whether the animation should loop.")]
        public bool Loop = false;

        [Tooltip("Viewport rect at animation start.")]
        public Rect StartRect = new Rect(Vector2.zero, Vector2.zero);

        [Tooltip("Viewport rect at animation end.")]
        public Rect EndRect = new Rect(Vector2.zero, Vector2.one);

        [Tooltip("Animation transition mode (linear vs cubic).")]
        public TransitionMode Transition = TransitionMode.LINEAR;

        [Tooltip("Whether the start rect should be set on awake.")]
        public bool SetStartRectOnAwake = true;
        
        [Header("Events")]
        public UnityEvent OnReachedEnd;
        
        float mAnimationTime;
        bool mIsPlaying;
        Camera mCamera;

        void Awake()
        {
            mCamera = GetComponent<Camera>();
            mAnimationTime = 0;
            mIsPlaying = false;

            if (SetStartRectOnAwake)
                mCamera.rect = StartRect;
            
            if (PlayOnAwake)
                Play();
        }

        void Update()
        {
            if (mIsPlaying)
            {
                mAnimationTime += Time.deltaTime;

                var animTime = Mathf.Max(0.0f, mAnimationTime - Delay);
                if (animTime > Duration)
                {
                    OnReachedEnd?.Invoke();

                    if (Loop)
                        Rewind();
                    else
                        Pause();
                }

                var u = Mathf.Clamp01(animTime / Duration);
                if (Transition == TransitionMode.CUBIC)
                    u = Mathf.SmoothStep(0, 1, u);
                
                var rectPos = Vector2.Lerp(StartRect.min, EndRect.min, u);
                var rectSize = Vector2.Lerp(StartRect.size, EndRect.size, u);
                mCamera.rect = new Rect(rectPos, rectSize);
            }
        }


        public void Play()
        {
            mIsPlaying = true;
        }

        public void Pause()
        {
            mIsPlaying = false;
        }

        public void Rewind()
        {
            mAnimationTime = 0;
        }

        public void Stop()
        {
            mIsPlaying = false;
            mAnimationTime = 0;
        }
    }
}