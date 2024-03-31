// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Water/Island" {
Properties {
	[NoScaleOffset] _ControlTex ("控制图", 2D) = "white" {}
	_Texture1("材质贴图1（R）", 2D) = "black"
	_Texture2("材质贴图2（G）,水深", 2D) = "black"
	_Texture3("材质贴图3（B）", 2D) = "black"
	_Texture4("材质贴图4（A）", 2D) = "black"

	//_Shininess("光照衰减", Float) = 5
	//_BumpMap("材质1法线贴图", 2D) = "bump" { }
	//_LightDir("光照方向", Vector) = (1, 0, 0, 0)
	//_LightColor("光照颜色", Color) = (1, 1, 1, 1)

	[NoScaleOffset] _Foam ("泡沫贴图", 2D) = "white" {}
	[NoScaleOffset] _FoamGradient ("泡沫梯度图 ", 2D) = "white" {}
	_FoamStrength ("泡沫强度", Range (0, 100.0)) = 1.0
	
		 
	_UVDistort("UV扭曲系数，锯齿度", Range(0, 5)) = 0.05
	_FoamSpeed("岸边浪速度", Range(0, 5)) = 0.2
	_FoamFactor("岸边浪数量系数", Range(0, 5)) = 1

	_FoamScale("泡沫尺寸", Range(0, 20)) = 8
	_FoamPeriod("泡沫周期", Range(0, 20)) = 5
	_FoamScale2("泡沫整体缩放", Float) = 0.01

	//_FogColor("Fog Color", Color) = (1, 1, 1, 1)
	//_FogStart("Fog Start", float) = 0
	//_FogEnd("Fog End", float) = 300

	//_Switch("开关", Range(0,5)) = 0



}


// -----------------------------------------------------------
// Fragment program cards


Subshader {
	Tags { "RenderType"="Opaque" "IGNOREPROJECTOR" = "true" }
	Pass {
//Blend SrcAlpha OneMinusSrcAlpha
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"



sampler2D _ControlTex;

sampler2D _Texture1;
float4 _Texture1_ST;

sampler2D _Texture2;
float4 _Texture2_ST;

sampler2D _Texture3;
float4 _Texture3_ST;

sampler2D _Texture4;
float4 _Texture4_ST;

//float _Shininess;
//sampler2D _BumpMap;
//float4 _LightDir;
//fixed4 _LightColor;

sampler2D _Foam;
sampler2D _FoamGradient;
float _FoamStrength;

float _UVDistort;
float _FoamSpeed;
float _FoamFactor;

float _FoamScale;
float _FoamPeriod;
float _FoamScale2;

//fixed4 _FogColor;
//float _FogStart;
//float _FogEnd;

//float _Switch;

struct appdata {
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
};

struct v2f {
	float4 pos : SV_POSITION;
	float4 mainuv : TEXCOORD0;
	float4 uv12 : TEXCOORD1;
	float4 uv34 : TEXCOORD2;

	//float4 TtoW0 : TEXCOORD3;
	//float4 TtoW1 : TEXCOORD4;
	//float4 TtoW2 : TEXCOORD5;

	//float4 viewSpacePos : TEXCOORD6;
};

v2f vert(appdata_tan v)
{
	v2f o;
	o.pos = UnityObjectToClipPos (v.vertex);
	
	// scroll bump waves
	float4 temp;
	float4 wpos = mul (unity_ObjectToWorld, v.vertex);
	
	o.mainuv.xy = v.texcoord;
	o.mainuv.zw = _FoamScale*wpos.xz + _FoamPeriod * float2(_SinTime.w, _SinTime.w);
	o.mainuv.zw *= _FoamScale2;
	
	o.uv12.xy = TRANSFORM_TEX(v.texcoord, _Texture1);
	o.uv12.zw = TRANSFORM_TEX(v.texcoord, _Texture2);
	o.uv34.xy = TRANSFORM_TEX(v.texcoord, _Texture3);
	o.uv34.zw = TRANSFORM_TEX(v.texcoord, _Texture4);

	////世界坐标
	//float3 worldPos = mul(_Object2World, v.vertex).xyz;
	////世界坐标下的视角方向
	//float3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
	////世界坐标下的法线方向
	//fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
	////世界坐标下的切线
	//float3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
	////世界坐标下的副切线
	//float tangentSign = v.tangent.w * unity_WorldTransformParams.w;
	//float3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;

	////go 光栅化
	//o.TtoW0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
	//o.TtoW1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
	//o.TtoW2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

	//o.viewSpacePos = mul(UNITY_MATRIX_MV, v.vertex);

	return o;
}

//inline float4 SimulateFog(float4 pos, float4 col)
//{
//	pos.w = 0.0;
//	float dist = length(pos);
//	float fogFactor = (_FogEnd - abs(dist)) / (_FogEnd - _FogStart);
//
//	fogFactor = clamp(fogFactor, 0.0, 1.0);
//	float3 afterFog = _FogColor.rgb *(1.0 - fogFactor) + col.rgb *fogFactor;
//
//	return float4(afterFog, col.a);
//}


half4 frag( v2f i ) : SV_Target
{
	fixed4 color;
	fixed4 control = tex2D(_ControlTex, i.mainuv.xy);
	fixed4 color1 = tex2D(_Texture1, i.uv12.xy);
	fixed4 color2 = tex2D(_Texture2, i.uv12.zw);
	//fixed4 color2 = tex2D(_Texture2, i.mainuv.zw);
	fixed4 color3 = tex2D(_Texture3, i.uv34.xy);
	fixed4 color4 = tex2D(_Texture4, i.uv34.zw);
	color.rgb = color1.rgb*control.r + color2.rgb*control.g + color3.rgb*control.b + color4.rgb*control.a;

	//if (control.b > 0) 
	//{
	//	fixed3 normal = UnpackNormal(tex2D(_BumpMap, i.uv34.xy));
	//	float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
	//	float3 lightDir = normalize(_LightDir);
	//	float3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
	//	worldViewDir = normalize(worldViewDir);

	//	float3 worldN;
	//	worldN.x = dot(i.TtoW0.xyz, normal);
	//	worldN.y = dot(i.TtoW1.xyz, normal);
	//	worldN.z = dot(i.TtoW2.xyz, normal);

	//	// diffuse intensity
	//	float diff = max(0.0, dot(worldN, lightDir));  // light dir for diffuse
	//	// specular intensity
	//	float spec;
	//	float atten = 1;    // for directional light
	//	float dotWL = dot(worldN, lightDir);
	//	if (dotWL < 0.0)
	//	{
	//		spec = 0.0;
	//	}
	//	else
	//	{
	//		spec = atten * pow(max(0.0, dot(reflect(-lightDir, worldN), worldViewDir)), _Shininess);
	//	}
	//	color.rgb = lerp(color.rgb, color.rgb * diff + _LightColor.rgb * spec, control.b);
	//}


	// bump.xy * 0.05 描述了波浪的锯齿程度
	//_Time.y*0.2 描述了波浪的速度
	//intensityFactor - _Time.y*0.2 描述了波浪的个数
	if (control.g > 0.1 + _SinTime.w / 50) {
		float2 uvDistort = float2(_UVDistort, _UVDistort);
		float intensityFactor = saturate((1 - control.g) / _FoamStrength);
		float x = (intensityFactor - _Time.y*_FoamSpeed) * _FoamFactor;
		half3 foamGradient = 1 - tex2D(_FoamGradient, float2(x, 0) + uvDistort);
		half3 foamColor = tex2D(_Foam, i.mainuv.zw).rgb;
		//color.rgb += foamGradient * intensityFactor * foamColor;
		color.rgb = lerp(color.rgb, color.rgb+foamGradient * intensityFactor * foamColor, control.g);

		//color = SimulateFog(i.viewSpacePos, color);
		return color;
	}


	return color;
}
ENDCG

	}
}

}
