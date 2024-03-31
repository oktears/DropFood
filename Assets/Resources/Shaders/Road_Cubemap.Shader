// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Road_Cubemap" {

	Properties{
		_TintColor("TintColor", Color) = (0.5, 0.5, 0.5, 0.5)
		_MainTex("DiffuseTexture(RGBA)", 2D) = "white" {}
		_Cube("Cube", CUBE) = "Black" { TexGen CubeReflect }
		_ReflAmount("ReflectionAmount", Range(0,1)) = 0.5
		_AlphaAmount("AlphaAmount", Range(0,1)) = 0.5
		_AlphatTest("AlphaTest", Range(0, 0.9)) = 0.5
	}

	SubShader {

		Tags { "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }

		LOD 100

		Pass {

			Cull Off Lighting Off ZWrite Off Fog{ Color(0,0,0,0) }

			Blend SrcAlpha One

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 I : TEXCOORD1;
			};

			float4 _MainTex_ST;

			sampler2D _MainTex;
			samplerCUBE _Cube;
			fixed _ReflAmount;
			fixed _AlphaAmount;
			fixed _AlphatTest;
			fixed4 _TintColor;


			v2f vert(appdata_tan v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				//计算反射

				//坐标
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				//视角方向
				float3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
				//法线
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				//用于采样环境cubemap的纹理坐标
				o.I = reflect(-worldViewDir, worldNormal);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{

				//主纹理颜色
				fixed4 mainTexColor = tex2D(_MainTex, i.uv);
				clip(mainTexColor.a - _AlphatTest);

				//环境cube贴图颜色
				fixed4 cubeColor = texCUBE(_Cube, i.I);
				fixed4 finalColor = (mainTexColor * _TintColor + cubeColor * _ReflAmount) * _AlphaAmount;


				return finalColor;
			}

			ENDCG
		}
	}
}