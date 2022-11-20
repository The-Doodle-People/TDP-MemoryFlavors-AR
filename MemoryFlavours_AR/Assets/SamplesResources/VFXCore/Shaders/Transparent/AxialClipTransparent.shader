/*========================================================================
Copyright (c) 2017-2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Transparent/AxialClip" {
    Properties {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0, 1)) = 0.5
        _Metallic("Metallic", Range(0, 1)) = 0.0
        _Center("Center", Vector) = (0,0,0,1)
        _AxisX("Axis X", Vector) = (1,0,0,0)
        _AxisY("Axis Y", Vector) = (0,1,0,0)
        _AxisZ("Axis Z", Vector) = (0,0,1,0)
        _Scale("Scale", Vector) = (1,1,1,0)
        _Min("Min", Vector) = (-1,-1,-1, 0)
        _Max("Max", Vector) = ( 1, 1, 1, 0)
        _ClipLineColor("Clip Line Color", Color) = (1,1,1,1)
        _ClipLineWidth("Clip Line Width", Float) = 0.01
    }

    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Cull Off // No face culling when clipping
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade
        #include "../VuforiaVFX.cginc"

        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
        };

        sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float3 _Center;
        float3 _AxisX;
        float3 _AxisY;
        float3 _AxisZ;
        float3 _Scale;
        float3 _Min;
        float3 _Max;
        half4 _ClipLineColor;
        float _ClipLineWidth;  

        
        void surf(Input IN, inout SurfaceOutputStandard o) 
        {
            float dx1, dx2;
            axial_delta(IN.worldPos, _Center, _AxisX, _Scale.x, _Min.x, _Max.x, dx1, dx2); 
            clip(dx1);
            clip(-dx2);

            float dy1, dy2;
            axial_delta(IN.worldPos, _Center, _AxisY, _Scale.y, _Min.y, _Max.y, dy1, dy2); 
            clip(dy1);
            clip(-dy2);

            float dz1, dz2;
            axial_delta(IN.worldPos, _Center, _AxisZ, _Scale.z, _Min.z, _Max.z, dz1, dz2); 
            clip(dz1);
            clip(-dz2);

            float dsX = min(abs(dx1), abs(dx2));
            float dsY = min(abs(dy1), abs(dy2));
            float dsZ = min(abs(dz1), abs(dz2));
            float ds = min(dsX, min(dsY, dsZ));
            float clipLineLum = smoothstep(_ClipLineWidth, 0.0, ds);

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            fixed4 color = _Color * tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = color.rgb + 2.0 * clipLineLum * _ClipLineColor.rgb;
            o.Alpha = color.a + clipLineLum;
        }

        ENDCG
    }
    Fallback "Diffuse"
}