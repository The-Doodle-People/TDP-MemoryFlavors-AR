/*========================================================================
Copyright (c) 2017-2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Depth/DepthMapContour" {

    Properties {
        _ContourColor("Contour Color", Color) = (1,1,1,1)
        _SurfaceColor("Surface Color", Color) = (0.5,0.5,0.5,1)
        _DepthThreshold("Depth Threshold", Float) = 0.01
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

        [Toggle] _DistanceFade("Distance Fade", Float) = 0
        _StartFadeDistance("Start Fade Distance", Float) = 10
        _EndFadeDistance("End Fade Distance", Float) = 20
    }

    SubShader {
        Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }

        // First pass: render depth mask
        // disable color, but enable ZWrite

        Pass {
            Cull Back
            Lighting Off
            ZTest LEqual
            ZWrite On
            ColorMask 0
        }


        // Second pass:
        // Render geometry with colors
        // NOTE: we only render fragments that match
        // the depth mask from the first pass (ZTest = Equal)

        Pass {
            Cull Back
            Lighting Off
            ZTest Equal
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "../VuforiaVFX.cginc"
            
            float4 _ContourColor;
            float4 _SurfaceColor;
            float _DepthThreshold;
            
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
            float _DistanceFade;
            float _StartFadeDistance;
            float _EndFadeDistance;

            struct appdata {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };


            v2f vert(appdata v) 
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }


            UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthTexture);

            half4 frag(v2f i) : COLOR 
            {
                half4 surfColor = _SurfaceColor;

                float2 uv = i.screenPos.xy / i.screenPos.w;
                float du = 1.0 / _ScreenParams.x;
                float dv = 1.0 / _ScreenParams.y;

                float2 uv1 = uv + float2(du, 0.0);
                float2 uv2 = uv + float2(0.0, dv);
                float2 uv3 = uv + float2(-du, 0.0);
                float2 uv4 = uv + float2(0.0, -dv);

                float depth = LinearEyeDepth(UNITY_SAMPLE_DEPTH(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthTexture, uv)));
                float depth1 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthTexture, uv1)));
                float depth2 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthTexture, uv2)));
                float depth3 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthTexture, uv3)));
                float depth4 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthTexture, uv4)));

                float depthStepX = max(abs(depth - depth1), abs(depth - depth3));
                float depthStepY = max(abs(depth - depth2), abs(depth - depth4));
                float depthStep = length(float2(depthStepX, depthStepY));

                // Make depthThreshold increase with distance for best visual quality:
                // - near objects need smaller depth thresholds to reveal small details
                // - far objects need larger depth thresholds to avoid visual clutter

                float depthThreshold = _DepthThreshold * depth;
                half contour = step(depthThreshold, depthStep);
                half4 color = surfColor * (1.0 - contour) + _ContourColor * contour;

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

                if (_DistanceFade > 0.5)
                {
                    float fadeRange = _EndFadeDistance - _StartFadeDistance;
                    float u = clamp((depth - _StartFadeDistance ) / fadeRange, 0.0, 1.0);
                    float distanceOpacity = lerp(1.0, 0.0, u);
                    color.a *= distanceOpacity;
                }

                return color;
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
