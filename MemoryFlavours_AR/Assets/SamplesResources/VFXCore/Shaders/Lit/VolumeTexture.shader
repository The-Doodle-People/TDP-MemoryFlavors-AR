/*========================================================================
Copyright (c) 2020-2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Lit/VolumeTexture" {
    Properties {
        _Color("Color", Color) = (1,1,1,1)
        _AmbientFactor("Ambient Factor", Float) = 0.67
        _MainTex("Texture 3D (RGBA)", 3D) = "white" {}  
        _Center("Center", Vector) = (0,0,0,1)
        _AxisX("Axis X", Vector) = (1,0,0,0)
        _AxisY("Axis Y", Vector) = (0,1,0,0)
        _AxisZ("Axis Z", Vector) = (0,0,1,0)
        _Scale("Scale", Vector) = (1,1,1,0)
    }

    SubShader {

        Tags  { "Queue"="Geometry" "RenderType"="Opaque"}
        
        Pass {
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler3D _MainTex;
            half4 _Color;
            half _AmbientFactor;
            float3 _Center;
            float3 _AxisX;
            float3 _AxisY;
            float3 _AxisZ;
            float3 _Scale;

            struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4  pos : SV_POSITION;
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
                o.normal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                float3 centerToPoint = i.worldPos - _Center;
                float u = 0.5 + dot(centerToPoint, normalize(_AxisX)) / _Scale.x;
                float v = 0.5 + dot(centerToPoint, normalize(_AxisY)) / _Scale.y;
                float w = 0.5 + dot(centerToPoint, normalize(_AxisZ)) / _Scale.z;
                half4 color = _Color * tex3D(_MainTex, float4(u, v, w, 0.0));

                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 normal = normalize(i.normal);
                float diffuse = abs(dot(normal, lightDir));

                color.rgb *= _AmbientFactor + (1.0 - _AmbientFactor) * diffuse;
                return color;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
