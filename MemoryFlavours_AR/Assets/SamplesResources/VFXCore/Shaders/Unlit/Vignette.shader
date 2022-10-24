/*========================================================================
Copyright (c) 2021-2022 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
=========================================================================*/
Shader "Vuforia/VFX/Unlit/Vignette"
{
    Properties
    {
        _MainTex("Base (RGBA)", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _BorderColor("Border Color", Color) = (0.5,0.5,0.5,0.5)
        _BorderWidth("Border Width", Range(0, 0.5)) = 0.25
    }
        
    SubShader
    {
        Pass
        {
            Lighting Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            half4 _MainTex_TexelSize;
            half4 _Color;
            half4 _BorderColor;
            float _BorderWidth;

            struct v2f
            {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
                
            #if UNITY_UV_STARTS_AT_TOP
                if (_MainTex_TexelSize.y < 0.0)
                    o.uv.y = 1 - o.uv.y;
            #endif

               return o;
            }

            half4 frag(v2f i) : COLOR
            {
                half4 fgColor = _Color * tex2D(_MainTex, i.uv);
                float x = 2.0 * abs(i.uv.x - 0.5);
                float y = 2.0 * abs(i.uv.y - 0.5);
                float ux = 1.0 - smoothstep(1.0 - _BorderWidth, 1.0, x);
                float uy = 1.0 - smoothstep(1.0 - _BorderWidth, 1.0, y);

                float u = ux * uy;
                return fgColor * u + (1.0 - u) * _BorderColor;
            }

            ENDCG
        }
    }
}