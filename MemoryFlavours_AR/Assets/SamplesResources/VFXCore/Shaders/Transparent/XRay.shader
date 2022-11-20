/*========================================================================
Copyright (c) 2017-2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Transparent/XRay" {
    Properties{
        _Color("Color", Color) = (1,1,1,0.5)
        _Center("Center", Vector) = (0,0,0,1)
        _AxisX("Axis X", Vector) = (1,0,0,0)
        _AxisY("Axis Y", Vector) = (0,1,0,0)
        _AxisZ("Axis Z", Vector) = (0,0,1,0)
        _Scale("Scale", Vector) = (1,1,1,0)
        [Toggle] _AxialClip("Axial Clip", Float) = 0
        _Min("Min", Vector) = (-1,-1,-1, 0)
        _Max("Max", Vector) = ( 1, 1, 1, 0)
        [Toggle] _RadialClip("Radial Clip", Float) = 0
        _MinRadius("Min Radius", float) = 0
        _MaxRadius("Max Radius", float) = 1
        _ClipLineColor("Clip Line Color", Color) = (1,1,1,1)
        _ClipLineWidth("Clip Line Width", Float) = 0.01
        _EdgeSharpness("Edge Sharpness", Range(1.0, 32.0)) = 4.0
        _EdgeBrightness("Edge Brightness", Range(0.0, 1.0)) = 0.5
        _EdgeOpacity("Edge Opacity", Range(0.0, 1.0)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }

        Pass {
            Tags { "LightMode" = "ForwardBase" }

            Lighting On
            Cull Off
            ZWrite Off
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert  
            #pragma fragment frag 
            #include "UnityCG.cginc"
            #include "../VuforiaVFX.cginc"

            float4 _Color;
            float3 _Center;
            float3 _AxisX;
            float3 _AxisY;
            float3 _AxisZ;
            float3 _Scale;
            float _AxialClip; 
            float3 _Min;
            float3 _Max;
            float _RadialClip;
            float _MinRadius;
            float _MaxRadius;
            half4 _ClipLineColor;
            float _ClipLineWidth;
            float _EdgeSharpness;
            float _EdgeBrightness;
            float _EdgeOpacity;

            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                float3 lightDir : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };


            v2f vert (appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.pos = UnityObjectToClipPos(v.vertex);

                float3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                o.normal = worldNormal;
                o.lightDir = _WorldSpaceLightPos0.xyz;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float4 frag(v2f i) : COLOR
            {
                float3 normal = normalize(i.normal);
                float3 lightDir = normalize(i.lightDir);
                float3 viewDir = normalize(i.pos.xyz - _WorldSpaceCameraPos);

                float diffuse = abs(dot(normal, lightDir));
                const float amb = 0.2;
                float ambDiffuse = clamp(amb + diffuse, 0.0, 1.0);
                
                float edge = 1.0 - abs(dot(viewDir, normal));
                edge = pow(edge, _EdgeSharpness);
                
                float4 color = _Color;
                color.rgb *= ambDiffuse;
                color.rgb += edge * _EdgeBrightness;
                color.a += edge * _EdgeOpacity;
                
                if (_AxialClip > 0.5)
                {
                    float dx1, dx2;
                    axial_delta(i.worldPos, _Center, _AxisX, _Scale.x, _Min.x, _Max.x, dx1, dx2); 
                    clip(dx1);
                    clip(-dx2);

                    float dy1, dy2;
                    axial_delta(i.worldPos, _Center, _AxisY, _Scale.y, _Min.y, _Max.y, dy1, dy2); 
                    clip(dy1);
                    clip(-dy2);

                    float dz1, dz2;
                    axial_delta(i.worldPos, _Center, _AxisZ, _Scale.z, _Min.z, _Max.z, dz1, dz2); 
                    clip(dz1);
                    clip(-dz2);

                    float dsX = min(abs(dx1), abs(dx2));
                    float dsY = min(abs(dy1), abs(dy2));
                    float dsZ = min(abs(dz1), abs(dz2));
                    float ds = min(dsX, min(dsY, dsZ));
                    float clipLineLum = smoothstep(_ClipLineWidth, 0.0, ds);
                    color += half4(clipLineLum, clipLineLum, clipLineLum, clipLineLum);
                }
                else if (_RadialClip > 0.5)
                {
                    float radius = length(i.worldPos - _Center) / length(_Scale);
                    float dr1 = radius - _MinRadius;
                    float dr2 = radius - _MaxRadius;
                    clip(dr1);
                    clip(-dr2);

                    float dr = min(abs(dr1), abs(dr2));
                    float clipLineLum = smoothstep(_ClipLineWidth, 0.0, dr);
                    color += half4(clipLineLum, clipLineLum, clipLineLum, clipLineLum);
                }

                return color;
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
