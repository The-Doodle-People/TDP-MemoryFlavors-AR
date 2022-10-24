/*========================================================================
Copyright (c) 2020-2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Unlit/Texture/HeightClip" {
    Properties {
        _MainTex ("Base (RGBA)", 2D) = "white" {}
        _MinHeight("Min Height", Float) = -10
        _MaxHeight("Max Height", Float) = 10
    }

    SubShader {
        Tags  { "Queue"="Geometry" "RenderType"="Opaque" }

        Pass {
            ZWrite On
            Cull Back
            Lighting Off
            
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _MinHeight;
            float _MaxHeight;

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

            v2f vert (appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.pos = UnityObjectToClipPos (v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }
            
            half4 frag(v2f i) : COLOR
            {
                clip(i.worldPos.y - _MinHeight);
                clip(_MaxHeight - i.worldPos.y);
                
                half4 color = tex2D(_MainTex, i.uv);
                return color;
            }

            ENDCG
        }
    }
     
    FallBack "Diffuse"
}

