// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Car/Car_Paint_Old(Garage)" {

	Properties{
		//喷漆颜色
		_TintColor("TintColor", Color) = (0.5, 0.5, 0.5, 0.5)
		//喷漆强度
		_TintAmount("MainColor", Range(0, 3)) = 1
		//主贴图
		_MainTex("DiffuseTexture(RGBA)", 2D) = "white" {}
		//反射Cubemap
		_CloseCube("CloseCube", CUBE) = "Black" { TexGen CubeReflect }
		//加强近景cubemap的黑白色
		_CubemapWhite("ReflWhiteAmount", Range(1, 5)) = 1
		_CubemapBlack("ReflBlackAmount", Range(1, 5)) = 1
		//反射遮罩贴图，控制反射强度
		_ReflMaskTex("ReflectiveMask (RGB)", 2D) = "white" {}
		//反射强度
		_ReflAmount("ReflectionAmount", Range(0,1)) = 0.5
	}

	SubShader {

		Tags { "Queue" = "Geometry" "RenderType" = "Opaque" "IGNOREPROJECTOR" = "true" }//"IgnoreProjector" = "True" 

		LOD 100			

		Pass {

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 detail : TEXCOORD02;
				float3 I : TEXCOORD1;
			};

			fixed4 _MainTex_ST;
			fixed4 _DetailTex_ST;

			sampler2D _MainTex;
			sampler2D _ReflectionTex;
			sampler2D _ReflMaskTex;
			samplerCUBE _CloseCube;
			fixed _ReflAmount;
			fixed _LightAmount;
			fixed4 _TintColor;
			fixed _CubemapWhite;
			fixed _CubemapBlack;
			fixed _TintAmount;
			float _Cutoff;

			uniform float4x4 _Rotation;

			v2f vert(appdata_tan v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.detail = TRANSFORM_TEX(v.texcoord, _DetailTex);

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
				fixed4 maskColor = tex2D(_ReflMaskTex, i.uv);
				//cube贴图颜色
				fixed4 closeCubeColor = texCUBE(_CloseCube, i.I);

				//环境反射白色部分更亮，黑色部分更黑
				if (closeCubeColor.r > 0.95
					&& closeCubeColor.g > 0.95
					&& closeCubeColor.b > 0.95)
				{
					closeCubeColor.rgb = closeCubeColor.rgb * _CubemapWhite;
				}
				else if (closeCubeColor.r < 0.1
					&& closeCubeColor.g < 0.1
					&& closeCubeColor.b < 0.1)
				{
					closeCubeColor.rgb = closeCubeColor.rgb / _CubemapBlack;
				}

				fixed4 tintColor = lerp(mainTexColor, mainTexColor * _TintColor, mainTexColor.a);
				fixed4 finalColor;
				finalColor = (tintColor * _TintAmount) + closeCubeColor * fixed4(maskColor.rgb, 1) * _ReflAmount;

				return finalColor;
			}

			ENDCG
		}
	
	}
}