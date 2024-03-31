// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Car/Car_Paint_Body2(Garage)" {

	Properties{
		//主贴图
		[NoScaleOffset] 
		_MainTex("主贴图(jpg)", 2D) = "white" {}
		//喷漆颜色
		_TintColor("喷漆颜色", Color) = (1,1,1,1)
		//非喷漆强度
		_UnTintAmount("非喷漆强度", Range(0, 3)) = 1
		//喷漆强度
		_TintAmount("喷漆强度(只控制喷漆)", Range(0, 3)) = 1
		//喷漆遮罩
		[NoScaleOffset] 
		_TintMaskTex("喷漆遮罩图", 2D) = "white" {}

		//反射遮罩贴图，控制反射强度
		[NoScaleOffset]
		_ReflMaskTex("反射强度图", 2D) = "white" {}
		
		//反射Cubemap
		[NoScaleOffset]
		_CloseCube("近景天空盒", CUBE) = "Black" { TexGen CubeReflect }
		_CloseFactor("近景强度", Range(0, 3)) = 1
		//喷漆颜色
		_NearColor("近景颜色", Color) = (1, 1, 1, 1)
		[NoScaleOffset] _FarCube("远景天空盒", CUBE) = "Black" { TexGen CubeReflect }
		_FarFactor("远景强度", Range(0, 3)) = 1

		_ReflectRate("不受喷漆区域的反射强度系数[近景CubeMap]", Range(0, 3)) = 1
		_ReflectRate2("不受喷漆区域的反射强度系数[远景CubeMap]", Range(0, 3)) = 1

		_FresnelBias("衰减基数", Float) = 0
		_FresnelPower("衰减强度", Float) = 0.8

		_RimColor("边缘颜色", Color) = (0,0,0,0)
		_RimPower("边缘强度", Range(0, 20)) = 10

		_Alpha("总体透明度", Range(0,1)) = 1

		_Switch("调试开关", Float) = 0
	}

	SubShader {

		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" "IGNOREPROJECTOR" = "true" }

		LOD 100

		Pass {

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _ReflMaskTex;
			sampler2D _TintMaskTex;

			fixed4 _TintColor;
			fixed _UnTintAmount;
			fixed _TintAmount;

			samplerCUBE _CloseCube;
			samplerCUBE _FarCube;

			fixed4 _NearColor;

			fixed _CloseFactor;
			fixed _FarFactor;

			fixed _FresnelBias ;
			fixed _FresnelPower ;

			fixed4 _RimColor;
			float _RimPower;

			fixed _Switch;
			fixed _Alpha;

			fixed _ReflectRate;
			fixed _ReflectRate2;

			//uniform float4x4 _Rotation;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
				float3 worldViewDir : TEXCOORD2;
				float3 TanViewDir : TEXCOORD3;
				float3 I : TEXCOORD4;
				//float4 color : COLOR;
			};

			v2f vert(appdata_tan v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				//将主贴图和反射遮罩贴图的uv放到一个通道里
				o.uv = v.texcoord.xy;

				//世界坐标
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				//世界坐标下的视角方向
				float3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
				//世界坐标下的法线方向
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				//世界坐标下的切线
				float3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				//世界坐标下的副切线
				float tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				float3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign ;
				
				float3 TtoW0 = float3(worldTangent.x, worldBinormal.x, worldNormal.x);
				float3 TtoW1 = float3(worldTangent.y, worldBinormal.y, worldNormal.y);
				float3 TtoW2 = float3(worldTangent.z, worldBinormal.z, worldNormal.z);

				//go 光栅化
				o.worldNormal = worldNormal;
				o.worldViewDir = worldViewDir;
				o.TanViewDir =TtoW0 * worldViewDir.x + TtoW1 * worldViewDir.y + TtoW2 * worldViewDir.z;
				o.I = reflect(-worldViewDir, worldNormal);
				
				//o.color = v.vertex * 0.8 + 0.5;  

				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				float3 worldNormal = normalize(i.worldNormal);
				float3 worldViewDir = normalize(i.worldViewDir);

				float3 TanViewDir = normalize(i.TanViewDir);
				float3 I = normalize(i.I);

				//主纹理颜色
				fixed4 mainTexColor = tex2D(_MainTex, i.uv);
				//遮罩纹理颜色
				fixed4 reflMaskColor = tex2D(_ReflMaskTex, i.uv);
				//喷漆遮罩纹理颜色
				fixed4 tintMaskColor = tex2D(_TintMaskTex, i.uv);

				//计算衰减系数
				float Fresnel = 1.0 - saturate(TanViewDir.z);
				//  Fresnel => [0,1]
				float FresnelRefl = min(1.0, _FresnelBias + pow(Fresnel, _FresnelPower));
				
				bool isUnTint = false;
				float reflectRate = 1;
				float reflectRate2 = 1;
				if (tintMaskColor.r == 0
					&& tintMaskColor.g == 0
					&& tintMaskColor.b == 0)  {
					reflectRate *= _ReflectRate;
					reflectRate2 *= _ReflectRate2;
					isUnTint = true;
				}

				//近景cube取色
				fixed4 closeCubeColor = fixed4(0, 0, 0, 0);
				if (_CloseFactor > 0) {
					closeCubeColor = texCUBE(_CloseCube, I);
					closeCubeColor = closeCubeColor * reflMaskColor * FresnelRefl * _CloseFactor * _NearColor * reflectRate;
				}
				
				float nh = 0;
				float nhCalc = 0;

				//远景cube取色
				fixed4 farCubeColor = fixed4(0, 0, 0, 0);
				if (_FarFactor > 0) {
					farCubeColor = texCUBE(_FarCube, I);
					nh = saturate(dot(worldNormal, worldViewDir));
					nhCalc = 1;
					//nh => [0,1]
					float specFar = min(pow(nh, 51.2f), 1.0f);
					farCubeColor = (farCubeColor + specFar) * reflMaskColor * _FarFactor * reflectRate2;
				}

				//边缘色
				fixed4 rimColor = fixed4(0, 0, 0, 0);
				if (_RimPower > 0) {
					if (nhCalc == 0)
						nh = saturate(dot(worldNormal, worldViewDir));
					float rim = 1 - nh;
					// rim => [0,1]
					rimColor = _RimColor*pow(rim, _RimPower);
					rimColor *= FresnelRefl; 
				}

				fixed4 tintColor;
				//tintColor.rgb = _TintColor.rgb;
				if (isUnTint) {
					tintColor.rgb = mainTexColor.rgb * (1 - FresnelRefl) * _UnTintAmount;
					//tintColor.rgb = mainTexColor.rgb * (1 - FresnelRefl);
				} else {
					tintColor.rgb = lerp(fixed3(1, 1, 1), _TintColor.rgb * _TintAmount, tintMaskColor.rgb);
					tintColor.rgb = mainTexColor.rgb * tintColor.rgb * (1 - FresnelRefl);
				}

				fixed4 finalColor;

				finalColor.rgb = tintColor.rgb + closeCubeColor.rgb + farCubeColor.rgb;


				//if (_Switch == 0)
				//	finalColor.rgb = tintColor.rgb + closeCubeColor.rgb + farCubeColor.rgb;
				//else if (_Switch == 1)
				//	finalColor.rgb = reflMaskColor.rgb;
				//else if (_Switch == 2)
				//	finalColor.rgb = tintColor.rgb;
				//else if (_Switch == 3)
				//	finalColor.rgb = closeCubeColor.rgb;
				//else if (_Switch == 4)
				//	finalColor.rgb = farCubeColor.rgb;
				//else if (_Switch == 5)
				//	finalColor.rgb = tintColor.rgb + closeCubeColor.rgb;
				//else
				//	finalColor.rgb = tintColor.rgb + farCubeColor.rgb;

				finalColor.rgb += rimColor.rgb;

				finalColor.a = _Alpha;
				
				//finalColor.rbg = i.color;
				
				return finalColor;
			}

			ENDCG
		}
	
	}
}