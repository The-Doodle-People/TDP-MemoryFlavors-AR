Shader "Custom/NegativeGrayscale"
{
    Properties
    {
        [NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
        [NoScaleOffset] _UVTex("UV Texture", 2D) = "white" {}
        _TouchX ("TouchX", Float) = 0.0
        _TouchY ("TouchY", Float) = 0.0
    }
    SubShader
    {
        Tags {"Queue" = "geometry-11" "RenderType" = "opaque" }
        Pass {
            ZWrite Off
            Cull Off
            Lighting Off

            CGPROGRAM

            #pragma multi_compile VUFORIA_RGB VUFORIA_YUVNV12 VUFORIA_YUVNV21

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _TouchX;
            float _TouchY;
#if (VUFORIA_YUVNV12 || VUFORIA_YUVNV21)
            sampler2D _UVTex;
            float4 _UVTex_ST;
#endif

            struct v2f {
                float4  pos : SV_POSITION;
                float2  uv : TEXCOORD0;
#if (VUFORIA_YUVNV12 || VUFORIA_YUVNV21)
                float2  uv2 : TEXCOORD1;
#endif
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);

                float distance;
                float2 direction;
                float2 _Touch;
                
                // take into account homogeneous coordinate!
                float2 viewPos = (o.pos.xy / o.pos.w);
                float sinDistance;
                
                _Touch.x = _TouchX;
                _Touch.y = _TouchY;
            
                direction = viewPos.xy - _Touch;
                distance = sqrt(direction.x * direction.x + direction.y * direction.y);
                sinDistance = (sin(distance) + 1.0);
                direction = direction / distance;

                if ((sinDistance > 0.0) && (_Touch.x != 2.0))
                {
                    viewPos.xy += (direction * (0.3 / sinDistance));
                    o.pos.xy = (viewPos.xy * o.pos.w);
                }

//Default VideoBackground
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
#if (VUFORIA_YUVNV12 || VUFORIA_YUVNV21)
                o.uv2 = TRANSFORM_TEX(v.texcoord, _UVTex);
#endif
                return o;
            }

#if (VUFORIA_YUVNV12 || VUFORIA_YUVNV21)
            half4 frag(v2f i) : COLOR
            {
                half4 c;
                half2 uv = tex2D(_UVTex, i.uv2).rg;
                float y = tex2D(_MainTex, i.uv).r;

#if VUFORIA_YUVNV12
                half4 v4yuv1 = half4(y, uv, 1.0);

                c.r = dot(half4(1.1640625,  0.000000000,  1.5957031250, -0.87060546875), v4yuv1);
                c.g = dot(half4(1.1640625, -0.390625000, -0.8134765625,  0.52929687500), v4yuv1);
                c.b = dot(half4(1.1640625,  2.017578125,  0.0000000000, -1.08154296875), v4yuv1);
                c.a = 1.0;
#else               
                half4 v4yuv1 = half4(y, uv, 1.0);

                c.r = dot(half4(1.1640625,  1.5957031250,  0.000000000, -0.87060546875), v4yuv1);
                c.g = dot(half4(1.1640625, -0.8134765625, -0.390625000,  0.52929687500), v4yuv1);
                c.b = dot(half4(1.1640625,  0.0000000000,  2.017578125, -1.08154296875), v4yuv1);
                c.a = 1.0;
#endif

                half color = 1.0 - ((c.r + c.g + c.b) / 3);
                c.r = color;
                c.g = color;
                c.b = color;

#ifdef UNITY_COLORSPACE_GAMMA
                return c;
#else
                return fixed4(GammaToLinearSpace(c.rgb), c.a);
#endif	
            }
#else
            half4 frag(v2f i) : COLOR
            {
                half4 c = tex2D(_MainTex, i.uv);

                //invert color value
                half color = 1.0 - ((c.r + c.g + c.b) / 3);
                c.r = color;
                c.g = color;
                c.b = color;
                c.a = 1.0;

//Default VideoBackground shader code
#ifdef UNITY_COLORSPACE_GAMMA
                return c;
#else
                return fixed4(GammaToLinearSpace(c.rgb), c.a);
#endif	
            }
#endif
            ENDCG
        }
    }
        Fallback "Legacy Shaders/Diffuse"
}
