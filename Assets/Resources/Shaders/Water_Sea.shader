// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Water/Water_Sea" {
Properties {
	[NoScaleOffset] _MainTex ("水深贴图", 2D) = "white" {}
	_WaveScale ("波浪尺寸", Range (0.01,1)) = 0.01

	[NoScaleOffset] _BumpMap ("法线贴图 ", 2D) = "bump" {}
	[NoScaleOffset] _Foam ("泡沫贴图", 2D) = "white" {}
	[NoScaleOffset] _FoamGradient ("泡沫梯度图 ", 2D) = "white" {}
	_FoamStrength ("泡沫强度", Range (0, 100.0)) = 1.0
	WaveSpeed ("浪速 (map1 x,y; map2 x,y)", Vector) = (20,1,-20,-1)
	[NoScaleOffset] _ReflectiveColor ("水纹颜色 (RGB) 水纹变化 (A) ", 2D) = "" {}
	_TargetColor ("目标颜色", COLOR)  = ( .172, .463, .435, 1)
	_ReflectionTex ("反射贴图", 2D) = "white" {}
	_ReflectionPower("反射强度", Range(0, 3)) = 0.3
	_UVDistort("UV扭曲系数，锯齿度", Range(0, 5)) = 0.05
	_FoamSpeed("岸边浪速度", Range(0, 5)) = 0.2
	_FoamFactor("岸边浪数量系数", Range(0, 5)) = 1

	_FoamScale("泡沫尺寸", Range(0, 20)) = 8
	_FoamPeriod("泡沫周期", Range(0, 20)) = 5
	_FoamScale2("泡沫整体缩放", Float) = 0.01

	_Switch("开关", Range(0,5)) = 0



}


// -----------------------------------------------------------
// Fragment program cards


Subshader {
	Tags { "RenderType"="Transparent" "QUEUE"="Transparent" }
	Pass {
Blend SrcAlpha OneMinusSrcAlpha
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

uniform float4 _WaveScale4;
uniform float4 _WaveOffset;

uniform sampler2D _MainTex;
uniform float _FoamStrength;

float _FoamScale;
float _FoamPeriod;
float _FoamScale2;


//uniform float _ReflDistort;

float _Switch;

struct appdata {
	float4 vertex : POSITION;
	float4 color : COLOR;
	float2 uv : TEXCOORD0;
};

struct v2f {
	float4 pos : SV_POSITION;
	float4 mainuv : TEXCOORD0;
	float4 bumpuv : TEXCOORD1;
	float3 viewDir : TEXCOORD2;
	//float4 ref : TEXCOORD3;
	float4 vertexCol : COLOR;
};

v2f vert(appdata v)
{
	v2f o;
	o.pos = UnityObjectToClipPos (v.vertex);
	
	// scroll bump waves
	float4 temp;
	float4 wpos = mul (unity_ObjectToWorld, v.vertex);
	temp.xyzw = wpos.xzxz * _WaveScale4 + _WaveOffset;
	o.bumpuv.xy = temp.xy;
	o.bumpuv.zw = temp.wz;
	
	o.mainuv.xy = v.uv;
	o.mainuv.zw = _FoamScale*wpos.xz + _FoamPeriod * float2(_SinTime.w, _SinTime.w);
	o.mainuv.zw *= _FoamScale2;
	
	// object space view direction (will normalize per pixel)
	o.viewDir.xzy = WorldSpaceViewDir(v.vertex); 

	//o.ref = ComputeScreenPos(o.pos);

	o.vertexCol = v.color;

	return o;
}


sampler2D _ReflectionTex;
sampler2D _ReflectiveColor;

float _ReflectionPower;

uniform float4 _TargetColor;
sampler2D _BumpMap;

sampler2D _Foam;
sampler2D _FoamGradient;

float _UVDistort;
float _FoamSpeed;
float _FoamFactor;

half4 frag( v2f i ) : SV_Target
{
	i.viewDir = normalize(i.viewDir);
	
	// combine two scrolling bumpmaps into one
	half3 bump1 = UnpackNormal(tex2D( _BumpMap, i.bumpuv.xy )).rgb;
	half3 bump2 = UnpackNormal(tex2D( _BumpMap, i.bumpuv.zw )).rgb;
	half3 bump = (bump1 + bump2) * 0.5;
	
	// fresnel factor
	half fresnelFac = dot( i.viewDir, bump );
	
	float uvDistort = bump.xy * _UVDistort;
	float2 reflUV = float2((i.viewDir.x + 1)*0.5, (i.viewDir.z + 1)*0.5);
	half4 refl = tex2D(_ReflectionTex, reflUV + uvDistort);
	
	// final color is between refracted and reflected based on fresnel
	half4 color;

	half4 water = tex2D( _ReflectiveColor, float2(fresnelFac,fresnelFac) );
	color.a = water.a;

	if (_Switch == 0) {
		color.rgb = water.rgb*_TargetColor.rgb + _ReflectionPower*refl.rgb;
	}
	else if (_Switch == 1) {
		color.rgb = water.rgb*_TargetColor.rgb;
	}
	else if (_Switch == 2) {
		color.rgb = refl.rgb;
	}
	else if (_Switch == 3) {
		color.rgb = water.rgb*_TargetColor.rgb;
	}
	else if (_Switch == 4) {
		color.rgb = _ReflectionPower*refl.rgb;
	}
	else if (_Switch == 5) {
		color.rgb = water.rgb*_TargetColor.rgb *_ReflectionPower*refl.rgb;
	}
	else {
		color.rgb = water.rgb*_TargetColor.rgb + _ReflectionPower*refl.rgb;
	}
	
	// bump.xy * 0.05 描述了波浪的锯齿程度
	//_Time.y*0.2 描述了波浪的速度
	//intensityFactor - _Time.y*0.2 描述了波浪的个数

	float intensityFactor = saturate(tex2D(_MainTex, i.mainuv.xy).r / _FoamStrength);
	float x = (intensityFactor - _Time.y*_FoamSpeed) * _FoamFactor;
	half3 foamGradient = 1 - tex2D(_FoamGradient, float2(x, 0) + uvDistort);
	half3 foamColor = tex2D(_Foam, i.mainuv.zw ).rgb;
	color.rgb += foamGradient * intensityFactor * foamColor;


	color.a = i.vertexCol.a;

	return color;
}
ENDCG

	}
}

}
