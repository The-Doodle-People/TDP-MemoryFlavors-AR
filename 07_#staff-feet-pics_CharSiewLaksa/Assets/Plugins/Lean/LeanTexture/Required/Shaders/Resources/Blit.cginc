#include "UnityCG.cginc"

float2          _NewSize;
sampler2D_float _NewTexture;

struct appdata
{
	float4 vertex : POSITION;
	float2 uv     : TEXCOORD0;
};

struct v2f
{
	float4 vertex   : SV_POSITION;
	float2 texcoord : TEXCOORD0;
};

float2 SnapToPixel(float2 coord, float2 size)
{
	float2 pixel = floor(coord * size);
#ifndef UNITY_HALF_TEXEL_OFFSET
	pixel += 0.5f;
#endif
	return pixel / size;
}

float4 SampleMip0(sampler2D_float s, float2 coord)
{
	return tex2Dbias(s, float4(coord.x, coord.y, 0, -15.0));
}

void Vert(appdata v, out v2f o)
{
	o.vertex   = float4(v.uv * 2.0f - 1.0f, 0.5f, 1.0f);
	o.texcoord = v.uv;
#if UNITY_UV_STARTS_AT_TOP
	o.vertex.y = -o.vertex.y;
#endif
}

float4 Frag(v2f i) : SV_Target
{
	return SampleMip0(_NewTexture, SnapToPixel(i.texcoord, _NewSize));
}

float4 Frag_LinearToGamma(v2f i) : SV_Target
{
	float4 color = SampleMip0(_NewTexture, SnapToPixel(i.texcoord, _NewSize));

	color.xyz = LinearToGammaSpace(color);

	return color;
}