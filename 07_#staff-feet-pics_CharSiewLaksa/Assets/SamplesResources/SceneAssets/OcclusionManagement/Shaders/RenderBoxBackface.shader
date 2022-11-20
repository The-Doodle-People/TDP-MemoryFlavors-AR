//==============================================================================
//Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc.
//All Rights Reserved.
//==============================================================================

Shader "Custom/RenderBoxBackface" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Tags {"Queue"="overlay+2" "RenderType"="overlay" }
        Pass {
            // Front culling
            Cull Front

            // Combine the main texture 
            SetTexture [_MainTex] {
                combine texture 
            }
        }
    }
}
