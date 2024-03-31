// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Car/Car_Paint_Body_Simple" {

	Properties{
		//主贴图
		[NoScaleOffset] _MainTex("主贴图(jpg)", 2D) = "white" {}
		//喷漆颜色
		_TintColor("喷漆颜色", Color) = (1,1,1,1)
		//喷漆强度
		_TintAmount("喷漆强度", Range(0, 5)) = 1
		//喷漆遮罩
		[NoScaleOffset] _TintMaskTex("喷漆遮罩图", 2D) = "white" {}
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
			sampler2D _TintMaskTex;

			fixed4 _TintColor;
			fixed _TintAmount;


			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata_tan v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				//将主贴图和反射遮罩贴图的uv放到一个通道里
				o.uv = v.texcoord.xy;

				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{

				//主纹理颜色
				fixed4 mainTexColor = tex2D(_MainTex, i.uv);
				//喷漆遮罩纹理颜色
				fixed4 tintMaskColor = tex2D(_TintMaskTex, i.uv);
				

				fixed4 tintColor;
				tintColor.rgb = lerp(fixed3(1, 1, 1), _TintColor.rgb, tintMaskColor.rgb);
				tintColor.rgb = mainTexColor.rgb * tintColor.rgb * _TintAmount;
				tintColor.a = 1;

				return tintColor;
			}

			ENDCG
		}
	
	}
}