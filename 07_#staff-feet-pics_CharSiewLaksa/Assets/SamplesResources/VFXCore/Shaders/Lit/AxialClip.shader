/*========================================================================
Copyright (c) 2018-2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Lit/AxialClip" {
    Properties {
        _Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _MainTex("Texture", 2D) = "white" {}
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
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        Cull Back

        CGPROGRAM
        
        #pragma surface surf Lambert
        #include "../VuforiaVFX.cginc"

        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
        };

        sampler2D _MainTex;
        half4 _Color;
        float3 _Center;
        float3 _AxisX;
        float3 _AxisY;
        float3 _AxisZ;
        float3 _Scale;
        float3 _Min;
        float3 _Max;
        half4 _ClipLineColor;
        float _ClipLineWidth;

        
        void surf(Input IN, inout SurfaceOutput o) 
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
            half3 origColor = _Color.rgb * tex2D(_MainTex, IN.uv_MainTex).rgb;
            o.Albedo = origColor + 2.0 * clipLineLum * _ClipLineColor.rgb;
            o.Alpha = _Color.a;
        }
        
        ENDCG
    }

    Fallback "Diffuse"
}