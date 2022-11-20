Shader "Hidden/Lean/Texture/Blit"
{
	Properties
	{
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#include "Blit.cginc"
			#pragma vertex   Vert
			#pragma fragment Frag
			ENDCG
		}

		Pass
		{
			CGPROGRAM
			#include "Blit.cginc"
			#pragma vertex   Vert
			#pragma fragment Frag_LinearToGamma
			ENDCG
		}
	}
}