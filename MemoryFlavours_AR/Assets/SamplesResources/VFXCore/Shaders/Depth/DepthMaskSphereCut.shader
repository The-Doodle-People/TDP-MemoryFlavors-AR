/*========================================================================
Copyright (c) 2019-2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Depth/DepthMaskSphereCut" {
    Properties{
        _Color("Color", Color) = (1.0,1.0,1.0,1.0)
        _BackgroundColor("Background Color", Color) = (0.2,0.2,0.2,1.0)
        _MainTex ("Base (RGBA)", 2D) = "white" {}
        _Center("Center", Vector) = (0.0, 0.0, 0.0, 1.0)
        _Radius("Radius", Float) = 0.5
        _BorderWidth("Border Width", Float) = 0.05
        [Toggle] _OcclusionMode ("Occlusion Mode", Float) = 1.0
        [Toggle] _RadialFade ("Radial Fade", Float) = 0.0
    }

    SubShader {

        Tags { "Queue"="Geometry-10" }

        // First pass: render depth mask

        Pass {
            Cull Off
            ZTest LEqual
            ZWrite On
            Lighting Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            half4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Center;
            float _Radius;
            float _BorderWidth;
            float _OcclusionMode;
            
            v2f vert(appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }
        
            half4 frag(v2f i) : COLOR
            {
                float3 radialVec = i.worldPos - _Center.xyz;
                float3 radialDir = normalize(radialVec);
                float radialDist = length(radialVec);
                
                float angle = asin(radialDir.y);
                float wave = sin(10.0 * angle);
                float radius = _Radius * (1.02 + 0.02 * wave);
                
                float dr = radialDist - radius;
                clip(dr);
                
                float borderWidth = _BorderWidth * radius;
                float alpha = 1.0 - smoothstep(0.0, borderWidth, abs(dr));
                half4 color = _Color * tex2D(_MainTex, i.uv);
                if (_OcclusionMode > 0.5)
                {
                    color.a *= alpha;
                }
                return color;
            }

            ENDCG
        }

        // Second pass: render background geometry

        Pass {
            Cull Front
            ZTest LEqual
            ZWrite Off
            Lighting Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            half4 _BackgroundColor;
            float4 _Center;
            float _Radius;
            float _RadialFade;

            struct appdata {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert(appdata_base v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                if (_RadialFade > 0.5)
                {
                    float3 radialVec = i.worldPos - _Center.xyz;
                    float3 radialDir = normalize(radialVec);
                    float radialDist = length(radialVec);

                    float angle = asin(radialDir.y);
                    float wave = sin(10.0 * angle);
                    float radius = _Radius * (1.02 + 0.02 * wave);

                    float u = smoothstep(0.8 * radius, radius, radialDist);
                    return (1.0 - u) * _BackgroundColor + u * half4(_BackgroundColor.rgb, 0.0);
                }
                else
                {
                    return _BackgroundColor;
                }
            }

            ENDCG
        }
    }
     
    FallBack "Diffuse"
}

