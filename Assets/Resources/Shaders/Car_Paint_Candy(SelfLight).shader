// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Car/Car_Paint_Candy(SelfLight)" {

	Properties{
		//主贴图
		[NoScaleOffset] _MainTex("主贴图(jpg)", 2D) = "white" {}
		//喷漆颜色
		_TintColor("喷漆颜色", Color) = (1,1,1,1)
		//糖果色
		_AmbientColor2("糖果色", Color) = (1, 1, 1, 1)
		//喷漆颜色
		_NearColor("近景颜色", Color) = (1, 1, 1, 1)
		//喷漆强度
		_TintAmount("喷漆强度", Range(0, 5)) = 1
		//非喷漆强度
		_UnTintAmount("非喷漆强度", Range(0, 3)) = 1
		//喷漆遮罩
		[NoScaleOffset] 
		_TintMaskTex("喷漆遮罩图", 2D) = "white" {}

		//反射遮罩贴图，控制反射强度
		[NoScaleOffset] 
		_ReflMaskTex("反射强度图", 2D) = "white" {}

		_ReflectRate("不受喷漆区域的反射强度系数[近景CubeMap]", Range(0, 3)) = 1
		_ReflectRate2("不受喷漆区域的反射强度系数[远景CubeMap]", Range(0, 3)) = 1

		//反射Cubemap
		[NoScaleOffset] _CloseCube("近景天空盒", CUBE) = "Black" { TexGen CubeReflect }

		[NoScaleOffset] _FarCube("远景天空盒", CUBE) = "Black" { TexGen CubeReflect }

		_CloseFactor("近景强度", Range(0, 3)) = 1
		_FarFactor("远景强度", Range(0, 3)) = 1

		//糖果色范围
		_CandyScale ("糖果色强度", Range(0.0,4.0)) = 0
		//糖果色强度
		_CandyPower ("糖果色范围", Range(0.0,20.0)) = 0
	}

	SubShader {

		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" "IGNOREPROJECTOR" = "false" }

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
			fixed _UnTintAmount;

			samplerCUBE _CloseCube;
			samplerCUBE _FarCube;

			fixed4 _NearColor;

			fixed _CloseFactor;
			fixed _FarFactor;

			fixed _ReflectRate;
			fixed _ReflectRate2;

			uniform float4x4 _CloseRotation;
			uniform float4x4 _FarRotation;

			fixed _CandyScale;		
			fixed _CandyPower;
			fixed4 _AmbientColor2;

			struct v2f
			{
				fixed4 pos : SV_POSITION;
				fixed2 uv : TEXCOORD0;
				fixed3 I_close : TEXCOORD1;
				fixed3 I_far : TEXCOORD4;
				fixed3 viewDir : TEXCOORD2;
				fixed3 normal : TEXCOORD3;
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
				o.I_close = mul(_CloseRotation, fixed4(dir, 0));
				o.I_far = mul(_FarRotation, fixed4(dir, 0));

				o.viewDir = worldViewDir;
				o.normal = v.normal;
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
				

				bool isUnTint = false;
				fixed reflectRate = 1;
				fixed reflectRate2 = 1;
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
					closeCubeColor = texCUBE(_CloseCube, i.I_close);
					closeCubeColor = closeCubeColor * reflMaskColor * _CloseFactor * _NearColor * reflectRate;
				}
				
				//远景cube取色
				fixed4 farCubeColor = fixed4(0, 0, 0, 0);
				if (_FarFactor > 0) {
					farCubeColor = texCUBE(_FarCube, i.I_far);
					farCubeColor = farCubeColor  * reflMaskColor * _FarFactor * reflectRate2;
				}

				fixed4 tintColor;
				if (isUnTint) {
					tintColor.rgb = mainTexColor.rgb * _UnTintAmount;
				} else {
					tintColor.rgb = lerp(fixed3(1, 1, 1), _TintColor.rgb * _TintAmount, tintMaskColor.rgb);
					tintColor.rgb = mainTexColor.rgb * tintColor.rgb;
				}

				fixed3 normEyeVec = normalize (i.viewDir);
				//float dotEyeVecNormal = abs(dot(normEyeVec, i.normal));	
				fixed dotEyeVecNormal = abs(dot(normEyeVec, i.normal));	

				fixed candy = _CandyScale * pow(dotEyeVecNormal, _CandyPower);

				fixed4 finalColor;
				finalColor.rgb = tintColor.rgb + closeCubeColor.rgb + farCubeColor.rgb + (candy * _AmbientColor2.rgb * tintMaskColor.rgb);

				finalColor.a = 1;
				
				return finalColor;
			}

			ENDCG
		}
	
	}
}