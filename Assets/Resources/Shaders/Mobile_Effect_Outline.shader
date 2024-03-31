// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Particle/Mobile_Effect_Outline"
{
	Properties
	{
		_RimColor("边缘颜色", Color) = (0, 0, 0, 1)
		_AlphaPower("边缘透明度衰减", Range(0, 50)) = 1
		_RimPower("边缘颜色衰减", Range(0, 50)) = 1
		_AllPower("边缘整体衰减", Range(0, 50)) = 1
	}
	SubShader
	{
		Tags{ "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
		LOD 100

		Pass
		{
			Blend SrcAlpha One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			fixed4 _RimColor;
			float _AlphaPower;
			float _RimPower;
			float _AllPower;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 worldViewDir : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				//世界坐标
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.worldViewDir = UnityWorldSpaceViewDir(worldPos);
				//世界坐标下的法线方向
				o.worldNormal = UnityObjectToWorldNormal(v.normal);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				//float rim = 1 - saturate(dot(i.worldNormal, i.worldViewDir));
				float rim = 1 - saturate(dot(i.worldNormal, normalize(i.worldViewDir))); //这里只要normalize worldViewDir就有差不多的效果了


				fixed4 col;
				col.rgb = _RimColor.rgb * pow(rim, _RimPower) * _AllPower;
				col.a = pow(rim, _AlphaPower) * _AllPower;

				return col;
			}
			ENDCG
		}
	}
}
