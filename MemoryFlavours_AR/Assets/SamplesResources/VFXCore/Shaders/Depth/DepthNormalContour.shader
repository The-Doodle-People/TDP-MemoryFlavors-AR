/*========================================================================
Copyright (c) 2017-2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Depth/DepthNormalContour" {

    Properties {
        _ContourColor("Contour Color", Color) = (1,1,1,1)
        _SurfaceColor("Surface Color", Color) = (0.5,0.5,0.5,1)
        _DepthThreshold("Depth Threshold", Float) = 0.005
        _NormalAngleThreshold("Normal Angle Threshold", Range(15, 90)) = 45        
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
        [Toggle] _Toon("Toon", Float) = 0
        [Toggle] _Blink("Blink", Float) = 0
        _BlinkFrequency("Blink Frequency", Float) = 1
    }

    SubShader {
        Tags { "Queue" = "Geometry-10" "RenderType" = "Opaque" }

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
            float _NormalAngleThreshold;
            float _PixelRadius;

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
            float _Toon;
            float _Blink;
            float _BlinkFrequency;

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

            UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);

            half4 frag(v2f i) : COLOR 
            {
                half4 surfColor = _SurfaceColor;

                float2 uv = i.screenPos.xy / i.screenPos.w;
                float du = 1.0 / _ScreenParams.x;
                float dv = 1.0 / _ScreenParams.y;

                float farDist = _ProjectionParams.z;
                float refDepthStep = _DepthThreshold / farDist;

                float maxDepthStep = 0.0;
                float maxAngle = 0.0;
               
                half3 normal = half3(0.0,0.0,1.0);
                float depth = 0.0;
                DecodeDepthNormal(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv), depth, normal);
                
                half3 x1_normal = half3(0.0,0.0,1.0);
                float x1_depth = 0.0;
                DecodeDepthNormal(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv + float2(-du, 0.0)), x1_depth, x1_normal);

                half3 x2_normal = half3(0.0,0.0,1.0);
                float x2_depth = 0.0;
                DecodeDepthNormal(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv + float2(du, 0.0)), x2_depth, x2_normal);

                half3 y1_normal = half3(0.0,0.0,1.0);
                float y1_depth = 0.0;
                DecodeDepthNormal(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv + float2(0.0, -dv)), y1_depth, y1_normal);

                half3 y2_normal = half3(0.0,0.0,1.0);
                float y2_depth = 0.0;
                DecodeDepthNormal(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv + float2(0.0, dv)), y2_depth, y2_normal);
                
                float depthStepX = max(abs(depth - x1_depth), abs(depth - x2_depth));
                float depthStepY = max(abs(depth - y1_depth), abs(depth - y2_depth));
                float depthStep = max(depthStepX, depthStepY);
                maxDepthStep = max(depthStep, maxDepthStep);

                float angleX = max(acos(dot(normal, x1_normal)), acos(dot(normal, x2_normal)));
                float angleY = max(acos(dot(normal, y1_normal)), acos(dot(normal, y2_normal)));
                float angle = max(angleX, angleY);
                maxAngle = max(angle, maxAngle);
                
                const float angleThresholdRad = _NormalAngleThreshold * (3.14159 / 180.0);

                float depthContour = (maxDepthStep > refDepthStep) ? 1.0 : 0.0; 
                float normalContour = (maxAngle > angleThresholdRad) ? 1.0 : 0.0;
                float contour = max(normalContour, depthContour);
                
                if (_Toon > 0.5)
                {
                    float diffuse = abs(dot(normal, _WorldSpaceLightPos0.xyz));
                    float toon = 0.0;
                    if (diffuse < 0.25)
                        toon = 0.25;
                    else if (diffuse < 0.5)
                        toon = 0.5;
                    else if (diffuse < 0.75)
                        toon = 0.75;
                    else 
                        toon = 1.0;

                    surfColor.rgb *= toon;
                }
                
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
