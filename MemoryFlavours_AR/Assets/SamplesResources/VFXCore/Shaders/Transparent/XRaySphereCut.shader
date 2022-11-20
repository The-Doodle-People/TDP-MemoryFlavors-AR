/*========================================================================
Copyright (c) 2017-2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Transparent/XRay/SphereCut" {
    Properties{
        _Color("Color", Color) = (1,1,1,0.5)
        _XRayColor("XRay Color", Color) = (1,1,1,0.5)
        _BackgroundColor("Background Color", Color) = (0.2,0.2,0.2,0.8)
        _MainTex ("Base (RGBA)", 2D) = "white" {}
        _Center("Center", Vector) = (0,0,0,1)
        _Radius("Radius", Float) = 0.5
        [Toggle] _OcclusionMode ("Occlusion Mode", Float) = 1
    }

    SubShader
    {
        Tags { "Queue" = "Geometry-10" }

        // Pass 1: render depth mask
        
        Pass {
            
            ZTest LEqual
            ZWrite On
            Lighting Off
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha
            Offset 0, -1

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 lightDir : TEXCOORD2;
            };

            half4 _Color;
            half4 _XRayColor;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Center;
            float _Radius;
            float _OcclusionMode;
            
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.lightDir = _WorldSpaceLightPos0.xyz;
                return o;
            }
        
            half4 frag(v2f i) : COLOR
            {
                float3 normal = normalize(i.normal);
                float3 lightDir = normalize(i.lightDir);
                float3 viewDir = normalize(i.pos.xyz - _WorldSpaceCameraPos);

                float diffuse = abs(dot(normal, lightDir));
                float amb = 0.2;
                float ambDiffuse = clamp(amb + diffuse, 0.0, 1.0);

                float3 radialVec = i.worldPos - _Center.xyz;
                float3 radialDir = normalize(radialVec);
                float radialDist = length(radialVec);
                
                float angle = asin(radialDir.y);
                float wave = sin(10.0 * angle);
                float radius = _Radius * (1.02 + 0.02 * wave);
                
                float dr = radialDist - radius;
                clip(dr);
                
                float borderWidth = 0.05 * radius;
                float u = smoothstep(0.0, borderWidth, dr);
                float alpha = 1.0 - u;
                half4 outerColor = _Color * tex2D(_MainTex, i.uv);
                outerColor.rgb *= ambDiffuse;
                half4 xrayColor = _XRayColor * (0.9 + 0.1 * tex2D(_MainTex, i.uv));
                if (_OcclusionMode > 0.5)
                {
                    outerColor.a *= alpha;
                }
                half4 color = u * outerColor + (1.0 - u) * xrayColor;
                return color;
            }

            ENDCG
        }

        // Pass 2: render background geometry

        Pass {
            Lighting Off
            ZTest Less
            ZWrite Off
            Cull Front
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            half4 _BackgroundColor;

            struct v2f {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                return _BackgroundColor;
            }

            ENDCG
        }

        // Pass 3: render transparent content with X-Ray color

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

            float4 _XRayColor;

            struct v2f {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                float3 lightDir : TEXCOORD0;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                float3 worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                o.normal = worldNormal;
                o.lightDir = _WorldSpaceLightPos0.xyz;
                return o;
            }

            float4 frag(v2f i) : COLOR
            {
                float3 normal = normalize(i.normal);
                float3 lightDir = normalize(i.lightDir);
                float3 viewDir = normalize(i.pos.xyz - _WorldSpaceCameraPos);

                float diffuse = abs(dot(normal, lightDir));
                const float amb = 0.3;
                float ambDiffuse = clamp(amb + diffuse, 0.0, 1.0);
                float4 color = _XRayColor * ambDiffuse;

                float edge = 1.0 - abs(dot(viewDir, normal));
                edge = pow(edge, 4.0);
                
                float edgeExtraLum = 0.3 * edge;
                float edgeOpacity = max(0.5, color.a);
                color.rgb *= pow(1.0 + edge, 4.0);
                color.a *= pow(1.0 + edge, 4.0);
                return color;
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
