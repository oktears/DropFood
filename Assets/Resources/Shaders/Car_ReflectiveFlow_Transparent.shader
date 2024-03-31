// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Car/Car_ReflectiveFlow" {

	Properties {
		_TintColor("TintColor", Color) = (0.5, 0.5, 0.5, 0.5)
		_TintAmount("MainColor", Range(0, 3)) = 1
		_MainTex("DiffuseTexture(RGBA)", 2D) = "white" {}
		_Cube("Cube", CUBE) = "Black" { TexGen CubeReflect }
		//加强cubemap的黑白色
		_CubemapWhite("ReflWhiteAmount", Range(1, 5)) = 1
		_CubemapBlack("ReflBlackAmount", Range(1, 5)) = 1
		_ReflMask("ReflectiveMask (RGB)", 2D) = "white" {}
		_ReflAmount("ReflectionAmount", Range(0,1)) = 0.5
	}

	SubShader {

		Tags{ "Queue" = "Geometry" "RenderType" = "Opaque" "IGNOREPROJECTOR" = "true"  }//"IgnoreProjector" = "True" 

		LOD 100			

		Pass{

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
			sampler2D _ReflectionTex;
			sampler2D _ReflMask;
			samplerCUBE _Cube;
			float _ReflAmount;
			float _LightAmount;
			fixed4 _TintColor;
			float _CubemapWhite;
			float _CubemapBlack;
			fixed _TintAmount;

			uniform float4x4 _Rotation;

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
				//遮罩纹理颜色
				fixed4 maskColor = tex2D(_ReflMask, i.uv);
				//环境cube贴图颜色
				fixed4 cubeColor = texCUBE(_Cube, mul(_Rotation, float4(i.I, 0)));

				//环境反射白色部分更亮，黑色部分更黑
				if (cubeColor.r > 0.95
					&& cubeColor.g > 0.95
					&& cubeColor.b > 0.95)
				{
					cubeColor.rgb = cubeColor.rgb * _CubemapWhite;
				}
				else if (cubeColor.r < 0.1
					&& cubeColor.g < 0.1
					&& cubeColor.b < 0.1)
				{
					cubeColor.rgb = cubeColor.rgb / _CubemapBlack;
				}

	/*			fixed4 finalColor;
				if (mainTexColor.a == 0) {
					finalColor.rgb = (mainTexColor * _TintAmount) + cubeColor * maskColor * _ReflAmount;
				}
				else {
					finalColor = (mainTexColor * _TintColor * _TintAmount) + cubeColor * maskColor * _ReflAmount;
				}*/

				fixed4 tintColor = lerp(mainTexColor, mainTexColor * _TintColor, mainTexColor.a);
				fixed4 finalColor;
				finalColor = (tintColor * _TintAmount) + cubeColor * maskColor * _ReflAmount;
				return finalColor;
			}

			ENDCG
		}
	}
}