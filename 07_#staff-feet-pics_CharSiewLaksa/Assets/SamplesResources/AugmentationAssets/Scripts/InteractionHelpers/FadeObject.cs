/*========================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.
 
Confidential and Proprietary - Protected under copyright and other laws.
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/

using UnityEngine;

/// <summary>
/// Controls the opacity of an object with renderers that use the standard shader
/// </summary>
public class FadeObject : MonoBehaviour
{
    public bool IsVisible;
    public float FadeDuration = 1f;
    public Renderer[] RenderersToFade;

    float mFadeRatio;
    bool mIsInitialOpacitySet;

    const string SHADER_COLOR_PROPERTY = "_Color";
    const string SHADER_FLAG_SOURCE_BLEND = "_SrcBlend";
    const string SHADER_FLAG_DESTINATION_BLEND = "_DstBlend";
    const string SHADER_FLAG_ZWRITE = "_ZWrite";
    const string SHADER_KEYWORD_ALPHA_TEST = "_ALPHATEST_ON";
    const string SHADER_KEYWORD_ALPHA_BLEND = "_ALPHABLEND_ON";
    const string SHADER_KEYWORD_ALPHA_MULTIPLY = "_ALPHAPREMULTIPLY_ON";
    
    void Awake()
    {
        foreach (var renderer in RenderersToFade)
            renderer.sharedMaterial = Instantiate(renderer.sharedMaterial);

        // If we haven't already set the initial opacity, set it based on IsVisible bool
        if (!mIsInitialOpacitySet)
        {
            var opacity = IsVisible ? 1.0f : 0.0f;
            SetInitialOpacity(opacity);
        }
    }

    void Update()
    {
        if (IsVisible && mFadeRatio < 1.0f)
        {
            mFadeRatio += Time.deltaTime / FadeDuration;
            if (mFadeRatio > 1.0f)
            {
                mFadeRatio = 1.0f;
                SetOpaque();
            }
            SetOpacity(mFadeRatio);
        }
        else if (!IsVisible && mFadeRatio > 0.0f)
        {
            mFadeRatio -= Time.deltaTime / FadeDuration;
            if (mFadeRatio < 0.0f)
                mFadeRatio = 0.0f;
            SetOpacity(mFadeRatio);
            SetTransparent();
        }
    }
    
    /// <summary>
    /// Immediately changes the opacity and prevents this script from setting its on opacity on start
    /// </summary>
    public void SetInitialOpacity(float opacity)
    {
        mFadeRatio = opacity;
        SetOpacity(opacity);
        if (opacity >= 1.0f)
            SetOpaque();
        else
            SetTransparent();
        mIsInitialOpacitySet = true;
    }
    
    void SetOpacity(float opacity)
    {
        foreach (var renderer in RenderersToFade)
        {
            if(renderer.sharedMaterial.HasProperty(SHADER_COLOR_PROPERTY))
            {
                var fadedColor = renderer.sharedMaterial.GetColor(SHADER_COLOR_PROPERTY);
                fadedColor.a = opacity;
                renderer.sharedMaterial.SetColor(SHADER_COLOR_PROPERTY, fadedColor);
            }
        }
    }

    void SetOpaque()
    {
        foreach (var fadeRenderer in RenderersToFade)
        {
            var mat = fadeRenderer.sharedMaterial;
            mat.SetInt(SHADER_FLAG_SOURCE_BLEND, (int) UnityEngine.Rendering.BlendMode.One);
            mat.SetInt(SHADER_FLAG_DESTINATION_BLEND, (int) UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt(SHADER_FLAG_ZWRITE, 1);
            mat.DisableKeyword(SHADER_KEYWORD_ALPHA_TEST);
            mat.DisableKeyword(SHADER_KEYWORD_ALPHA_BLEND);
            mat.DisableKeyword(SHADER_KEYWORD_ALPHA_MULTIPLY);
            mat.renderQueue = -1;
        }
    }

    void SetTransparent()
    {
        foreach (var fadeRenderer in RenderersToFade)
        {
            var mat = fadeRenderer.sharedMaterial;
            mat.SetInt(SHADER_FLAG_SOURCE_BLEND, (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt(SHADER_FLAG_DESTINATION_BLEND, (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt(SHADER_FLAG_ZWRITE, 0);
            mat.DisableKeyword(SHADER_KEYWORD_ALPHA_TEST);
            mat.EnableKeyword(SHADER_KEYWORD_ALPHA_BLEND);
            mat.DisableKeyword(SHADER_KEYWORD_ALPHA_MULTIPLY);
            mat.renderQueue = 3000;
        }
    }
}

