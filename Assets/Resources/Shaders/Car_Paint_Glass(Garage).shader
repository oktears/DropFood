// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Car/Car_Paint_Glass(Garage)" {

	Properties{
		//主贴图
		[NoScaleOffset] _MainTex("主贴图(jpg)", 2D) = "white" {}
		//喷漆颜色
		_TintColor("喷漆颜色", Color) = (1, 1, 1, 1)
		//喷漆强度
		_TintAmount("喷漆强度", Range(0, 5)) = 1
		//反射遮罩贴图，控制反射强度
		[NoScaleOffset] _ReflMaskTex("反射强度图(jpg)", 2D) = "white" {}

		//反射Cubemap
		[NoScaleOffset] _CloseCube("近景天空盒", CUBE) = "Black" { TexGen CubeReflect }
		_CloseFactor("近景强度", Range(0, 3)) = 1
		[NoScaleOffset] _FarCube("远景天空盒", CUBE) = "Black" { TexGen CubeReflect }
		_FarFactor("远景强度", Range(0, 3)) = 1

		_FresnelBias("衰减基数", Float) = 0
		_FresnelPower("衰减强度", Float) = 0.8

		_RimColor("边缘颜色", Color) = (0, 0, 0, 0)
		_RimPower("边缘强度", Range(0, 20)) = 10

		_Alpha("透明度", Range(0,1)) = 0.5

		//_Switch("调试开关", Float) = 0
}

	SubShader{

		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" "IGNOREPROJECTOR" = "true" }

		Blend SrcAlpha OneMinusSrcAlpha

		LOD 100			

			Pass{

			CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"


			sampler2D _MainTex;
			sampler2D _ReflMaskTex;

			fixed4 _TintColor;
			fixed _TintAmount;

			samplerCUBE _CloseCube;
			samplerCUBE _FarCube;

			fixed _CloseFactor;
			fixed _FarFactor;

			fixed _FresnelBias;
			fixed _FresnelPower;

			fixed4 _RimColor;
			float _RimPower;

			//fixed _Switch;

			float _Alpha;

			uniform float4x4 _Rotation;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 TtoW0 : TEXCOORD1;
				float4 TtoW1 : TEXCOORD2;
				float4 TtoW2 : TEXCOORD3;
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
				float3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;

				//go 光栅化
				o.TtoW0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.TtoW1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.TtoW2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);


				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{

				float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
				float3 worldNormal = normalize(float3(i.TtoW0.z, i.TtoW1.z, i.TtoW2.z));
				float3 worldViewDir = normalize(_WorldSpaceCameraPos - worldPos);

				float3 TanViewDir = i.TtoW0.xyz * worldViewDir.x + i.TtoW1.xyz * worldViewDir.y + i.TtoW2.xyz * worldViewDir.z;
				float3 I = reflect(-worldViewDir, worldNormal);

				//主纹理颜色  
				fixed4 mainTexColor = tex2D(_MainTex, i.uv);
				//遮罩纹理颜色
				fixed4 reflMaskColor = tex2D(_ReflMaskTex, i.uv);

				//计算衰减系数
				float Fresnel = 1.0 - saturate(normalize(TanViewDir).z);
				float FresnelRefl = min(1.0, _FresnelBias + pow(Fresnel, _FresnelPower));

				//近景cube取色
				fixed4 closeCubeColor = fixed4(0, 0, 0, 0);
				if (_CloseFactor > 0) {
					closeCubeColor = texCUBE(_CloseCube, I);
					closeCubeColor = closeCubeColor * reflMaskColor * FresnelRefl * _CloseFactor;
				}

				float nh = 0;
				float nhCalc = 0;

				//远景cube取色
				fixed4 farCubeColor = fixed4(0, 0, 0, 0);
				if (_FarFactor > 0) {
					farCubeColor = texCUBE(_FarCube, I);
					nh = saturate(dot(worldNormal, worldViewDir));
					nhCalc = 1;
					float specFar = min(pow(nh, 51.2f), 1.0f);
					farCubeColor = (farCubeColor + specFar) * reflMaskColor * _FarFactor;
				}

				//边缘色
				fixed4 rimColor = fixed4(0, 0, 0, 0);
				if (_RimPower > 0) {
					if (nhCalc == 0)
						nh = saturate(dot(worldNormal, worldViewDir));
					float rim = 1 - nh;
					rimColor = _RimColor*pow(rim, _RimPower);
				}

				fixed4 tintColor = mainTexColor* _TintColor * (1 - FresnelRefl) * _TintAmount;
				fixed4 finalColor;

				//if (_Switch == 0)
					finalColor.rgb = tintColor.rgb + closeCubeColor.rgb + farCubeColor.rgb;
				/* else if (_Switch == 1)
					finalColor.rgb = reflMaskColor.rgb;
				else if (_Switch == 2)
					finalColor.rgb = tintColor.rgb;
				else if (_Switch == 3)
					finalColor.rgb = closeCubeColor.rgb;
				else if (_Switch == 4)
					finalColor.rgb = farCubeColor.rgb;
				*/

				finalColor.rgb += rimColor.rgb;

				finalColor.a = _Alpha;



				return finalColor;
			}

			ENDCG
		}

	}
}