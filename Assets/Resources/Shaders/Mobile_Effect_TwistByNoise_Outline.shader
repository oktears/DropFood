// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Particle/Mobile_Effect_TwistByNosie Outline"
{
	Properties
	{
		_TintColor("主色调", Color) = (1, 1, 1, 1)
		_TintAmount("颜色强度", range(0, 100)) = 1
		_NoiseTex("噪音图 (灰度)", 2D) = "white" {}
		_MainTex("主贴图", 2D) = "white" {}
		_HeatTime("时间因子", range(-1, 1)) = 0
		_ForceX("X因子", range(0, 2)) = 0.1
		_ForceY("Y因子", range(0, 2)) = 0.1

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

			fixed4 _TintColor;
			float _TintAmount;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			float _HeatTime;
			float _ForceX;
			float _ForceY;

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
				float4 uv : TEXCOORD0;
				float3 worldViewDir : TEXCOORD1;
				float3 worldNormal : TEXCOORD2;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv.zw = TRANSFORM_TEX(v.uv, _NoiseTex);

				//世界坐标
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.worldViewDir = UnityWorldSpaceViewDir(worldPos);
				//世界坐标下的法线方向
				o.worldNormal = UnityObjectToWorldNormal(v.normal);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 offsetColor1 = tex2D(_NoiseTex, i.uv.zw + _Time.xz * _HeatTime);
				fixed4 offsetColor2 = tex2D(_NoiseTex, i.uv.zw + _Time.yx * _HeatTime);
				float factor = (offsetColor1.r + offsetColor2.r) - 1;
				i.uv.x += factor * _ForceX;
				i.uv.y += factor * _ForceY;
				fixed4 color = tex2D(_MainTex, i.uv.xy) * _TintColor * _TintAmount;

				//float rim = 1 - saturate(dot(i.worldNormal, i.worldViewDir));
				float rim = 1 - saturate(dot(i.worldNormal, normalize(i.worldViewDir))); //这里只要normalize worldViewDir就有差不多的效果了


				fixed4 col;
				col.rgb = color + _RimColor.rgb * pow(rim, _RimPower) * _AllPower;
				col.a = pow(rim, _AlphaPower) * _AllPower;

				return col;
			}
			ENDCG
		}
	}
}
