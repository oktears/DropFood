﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Car/Car_Paint_Body(Garage)" {

	Properties{
		//主贴图
		[NoScaleOffset] _MainTex("主贴图(jpg)", 2D) = "white" {}
		//喷漆颜色
		_TintColor("喷漆颜色", Color) = (1,1,1,1)
		//喷漆强度
		_TintAmount("喷漆强度", Range(0, 5)) = 1
		//喷漆遮罩
		[NoScaleOffset] _TintMaskTex("喷漆遮罩图", 2D) = "white" {}

		//反射遮罩贴图，控制反射强度
		[NoScaleOffset] _ReflMaskTex("反射强度图", 2D) = "white" {}
		
		//反射Cubemap
		[NoScaleOffset] _CloseCube("近景天空盒", CUBE) = "Black" { TexGen CubeReflect }
		_CloseFactor("近景强度", Range(0, 3)) = 1
	}

	SubShader {

		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" "IGNOREPROJECTOR" = "true" }

		LOD 100

		Pass {

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"


			sampler2D _MainTex;
			sampler2D _ReflMaskTex;
			sampler2D _TintMaskTex;

			fixed4 _TintColor;
			fixed _TintAmount;

			samplerCUBE _CloseCube;
			fixed _CloseFactor;

			uniform float4x4 _FarRotation;

			struct v2f
			{
				fixed4 pos : SV_POSITION;
				fixed2 uv : TEXCOORD0;
				fixed3 I : TEXCOORD1;
			};

			v2f vert(appdata_tan v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				//将主贴图和反射遮罩贴图的uv放到一个通道里
				o.uv = v.texcoord.xy;

				//世界坐标
				fixed3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				//世界坐标下的视角方向
				fixed3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
				//世界坐标下的法线方向
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);

				fixed3 dir = reflect(-worldViewDir, worldNormal);
				dir = mul(_FarRotation, float4(dir, 0));
				o.I = dir;
				
//				o.I = reflect(-worldViewDir, worldNormal);

				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{

				//主纹理颜色
				fixed4 mainTexColor = tex2D(_MainTex, i.uv);
				//遮罩纹理颜色
				fixed4 reflMaskColor = tex2D(_ReflMaskTex, i.uv);
				//喷漆遮罩纹理颜色
				fixed4 tintMaskColor = tex2D(_TintMaskTex, i.uv);
				
				//近景cube取色
				fixed4 closeCubeColor = fixed4(0, 0, 0, 0);
				if (_CloseFactor > 0) {
					closeCubeColor = texCUBE(_CloseCube, i.I);
					closeCubeColor = closeCubeColor * reflMaskColor  * _CloseFactor;
				}
				

				fixed3 tintColor;
				tintColor = lerp(fixed3(1, 1, 1), _TintColor.rgb, tintMaskColor.rgb);
				tintColor = mainTexColor.rgb * tintColor.rgb * _TintAmount;

				fixed4 finalColor;
				finalColor.rgb = tintColor + closeCubeColor.rgb;
				finalColor.a = 1;
				
				return finalColor;
			}

			ENDCG
		}
	
	}
}