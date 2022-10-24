/*========================================================================
Copyright (c) 2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Transparent/LightWave" {

    Properties {
        _Color("Color", Color) = (1,1,1,1)
        _Center("Center", Vector) = (0,0,0,1)
        _Scale("Scale", Vector) = (1,1,1,0)
        _Radius("_Radius", Float) = 0.5
        _WaveLength("Wave Length", Float) = 0.05
        [Toggle] _WaveNoise("Wave Noise", Float) = 0
        _NoiseTex ("Noise (RGBA)", 2D) = "white" {}
        _NoiseAmp("Noise Amplitude", Float) = 0.5
        _NoiseFreq("Noise Frequency", Float) = 0.5
        [Toggle] _MosaicEffect("Mosaic Effect", Float) = 0
        _MosaicBlockSize("Mosaic Block Size", Float) = 0.1
    }

    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Geometry" }

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
            Cull Back
            Lighting Off
            ZTest Equal
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _Color;
            float3 _Center;
            float3 _Scale;
            float _Radius;
            float _WaveLength;

            float _WaveNoise;
            sampler2D _NoiseTex;
            float _NoiseAmp;
            float _NoiseFreq;

            float _MosaicEffect;
            float _MosaicBlockSize;

            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert(appdata v) 
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.pos = UnityObjectToClipPos(v.vertex);
                float3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal)); 
                o.normal = worldNormal;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            half4 frag(v2f i) : COLOR 
            {
                float3 normal = normalize(i.normal);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 viewDir = normalize(i.pos.xyz - _WorldSpaceCameraPos);

                const float amb = 0.2;
                float diffuse = abs(dot(normal, lightDir));
                float ambDiffuse = clamp(amb + diffuse, 0.0, 1.0);
                half4 baseColor = _Color * ambDiffuse;

                float3 pos = i.worldPos;
                if (_MosaicEffect > 0.5) {
                    pos = floor(pos / _MosaicBlockSize) * _MosaicBlockSize;
                }
    
                if (_WaveNoise > 0.5)
                {
                    float4 noiseColorXZ  = tex2D(_NoiseTex, _NoiseFreq * pos.xz);
                    float4 noiseColorXY = tex2D(_NoiseTex, _NoiseFreq * pos.xy);
                    float noise = noiseColorXZ.r * noiseColorXY.r;
                    noise = _NoiseAmp * (-0.5 + 0.5 * noise);
                    pos += float3(noise, noise, noise);
                }
                 
                float radius = length(pos - _Center) / length(_Scale);
                float innerRadius = _Radius - 0.5f * _WaveLength;
                float outerRadius = _Radius + 0.5f * _WaveLength;
                clip(radius - innerRadius);
                clip(outerRadius - radius);

                float dr = abs(radius - _Radius);
                half4 color = baseColor * smoothstep(0.5 * _WaveLength, 0.0, dr);
                float waveLum = smoothstep(0.25 * _WaveLength, 0.0, dr);
                color += half4(waveLum, waveLum, waveLum, waveLum);
                return color;
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
