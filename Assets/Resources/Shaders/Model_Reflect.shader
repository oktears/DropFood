// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Model/Reflect_Simple" {

	Properties{
		//主贴图
		[NoScaleOffset] 
		_MainTex("主贴图(jpg)", 2D) = "white" {}
		[NoScaleOffset] 
		_FarCube("反射天空盒", CUBE) = "Black" { TexGen CubeReflect }
		_FarFactor("反射强度", Range(0, 3)) = 1
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
			samplerCUBE _FarCube;
			fixed _FarFactor;

			struct v2f
			{
				fixed4 pos : SV_POSITION;
				fixed2 uv : TEXCOORD0;
				float3 I : TEXCOORD1;
			};

			v2f vert(appdata_tan v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				//将主贴图和反射遮罩贴图的uv放到一个通道里
				o.uv = v.texcoord.xy;

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

			fixed4 frag(v2f i) : COLOR
			{
				//主纹理颜色
				fixed4 mainTexColor = tex2D(_MainTex, i.uv);
				//远景cube取色
				fixed4 farCubeColor = texCUBE(_FarCube, i.I);
				farCubeColor = farCubeColor * _FarFactor ;

				fixed4 finalColor = mainTexColor + farCubeColor;
				
				return finalColor;
			}

			ENDCG
		}
	
	}
}