// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Particle/Additve_UVScroll_AlphaGradientByModel" {

		Properties{
			_MainTex("Base (RGB)", 2D) = "white" {}
			_TintColor("Tint Color",Color) = (0.5, 0.5, 0.5, 0.5)
			_XSpeed("XSpeed", Range(-5, 5)) = 0
			_ScrollController("ScrollController", Range(0, 1)) = 0.5
			_LightAmount("LightAmount", Range(0, 2)) = 1 
		}

		SubShader {

		Tags{ "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
		LOD 100

		Pass {

			Cull Off Lighting Off ZWrite Off Fog{ Color(0,0,0,0) }
			Blend SrcAlpha One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _TintColor;
			fixed _XSpeed;
			fixed _ScrollController;
			fixed _LightAmount;

			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;

				if (o.texcoord.y > _ScrollController) {
					fixed xScrollValue = _XSpeed * _Time.y;
					o.texcoord.x += xScrollValue;
				}
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);

				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord)* _LightAmount * i.color;
				col.a = i.color.a * _TintColor.a * col.a * 2;

				return col;
			}

		
			ENDCG
		}
	}
}
