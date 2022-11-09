/*========================================================================
Copyright (c) 2019-2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Transparent/Phantom" {

    Properties {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGBA)", 2D) = "white" {}
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
        [Toggle] _Blink("Blink", Float) = 0.0
        _BlinkFrequency("Blink Frequency", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Geometry+100" }

        // First pass: render depth mask
        // disable color, but enable ZWrite

        Pass {
            Cull Back
            Lighting Off
            ZTest LEqual
            ZWrite On
            ColorMask 0
        }


        // Second pass: render phantom geometry
        // NOTE: we only render fragments that match
        // the depth mask from the first pass (ZTest = Equal)

        Pass {
            Tags { "LightMode" = "ForwardBase" }

            Cull Back
            ZTest Equal
            ZWrite Off
            Lighting On
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma vertex vert  
            #pragma fragment frag 
            #include "UnityCG.cginc"

            float4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
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
            float _Blink;
            float _BlinkFrequency;

            struct v2f {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            void axial_delta(float3 p, float3 center, float3 axis, float scale, float min, float max, out float deltaMin, out float deltaMax)
            {
                float3 centerToPoint = p - center;
                float proj = dot(centerToPoint, normalize(axis)) / scale;
                deltaMin = proj - min;
                deltaMax = proj - max;
            }

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                float3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                o.normal = worldNormal;
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float4 frag(v2f i) : COLOR
            {
                float3 normal = normalize(i.normal);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 viewDir = normalize(i.pos.xyz - _WorldSpaceCameraPos);

                const float amb = 0.2;
                float diffuse = abs(dot(normal, lightDir));
                float ambDiffuse = clamp(amb + diffuse, 0.0, 1.0);
                
                float edge = 1.0 - abs(dot(viewDir, normal));
                edge = pow(edge, 4.0);
                float edgeLum = pow(1.0 + edge, 2.0);
                
                float4 color = _Color * ambDiffuse * tex2D(_MainTex, i.uv);
                color *= edgeLum;
                
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

                if (_Blink > 0.5)
                {
                    half blinkWave = 0.67 + 0.33 * sin(_BlinkFrequency * 6.28 * _Time.y);
                    color *= blinkWave;
                }

                return color;
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
