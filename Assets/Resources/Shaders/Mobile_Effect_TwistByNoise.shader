// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Particle/Mobile_Effect_TwistByNoise"
{
	Properties
	{
		_TintColor("主色调", Color) = (1, 1, 1, 0.5)
		_TintAmount("颜色强度", range(0,5)) = 1
		_NoiseTex("噪音图 (灰度)", 2D) = "white" {}
		_MainTex("主贴图", 2D) = "white" {}
		_MaskTex("遮罩贴图", 2D) = "white" {}
		_HeatTime("时间因子", range(-1, 1)) = 0
		_ForceX("X因子", range(0, 2)) = 0.1
		_ForceY("Y因子", range(0, 2)) = 0.1
	}
	SubShader
	{
		Tags{ "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
		LOD 100

		Pass
		{

			Cull Off Lighting Off ZWrite Off Fog{ Color(0, 0, 0, 0) } 
			Blend SrcAlpha One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			fixed4 _TintColor;
			float _TintAmount;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MaskTex;
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			float _HeatTime;
			float _ForceX;
			float _ForceY;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv.zw = TRANSFORM_TEX(v.uv, _NoiseTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 maskColor = tex2D(_MaskTex, i.uv.xy);
				fixed4 offsetColor1 = tex2D(_NoiseTex, i.uv.zw + _Time.xz * _HeatTime);
				fixed4 offsetColor2 = tex2D(_NoiseTex, i.uv.zw + _Time.yx * _HeatTime);
				float factor = (offsetColor1.r + offsetColor2.r) - 1;
				i.uv.x += factor * _ForceX;
				i.uv.y += factor * _ForceY;
				return  tex2D(_MainTex, i.uv.xy) * maskColor * _TintColor * _TintAmount;
			}
			ENDCG
		}
	}
}
