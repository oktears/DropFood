// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Particle/Additve_UVScroll_AlphaGradient" {

	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_TintColor("Tint Color",Color) = (0.5, 0.5, 0.5, 0.5)
		_XSpeed("XSpeed", Range(-5, 5)) = 0
		_TransparentBegin("TransparentBegin", Range(-10, 10)) = 0
		_TransparentEnd("TransparentEnd", Range(-10, 10)) = 0
		_ScrollController("ScrollController", Range(0, 1)) = 0.5
	}

	SubShader {

		Tags { "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
		LOD 100

		Pass{

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
			fixed _TransparentEnd;
			fixed _TransparentBegin;
			fixed _ScrollController;

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

				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);

				if (v.vertex.x > _TransparentEnd) {
					o.color.a = 0;
				}
				else if (v.vertex.x < _TransparentBegin) {
					o.color.a = 0;
				}

				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{

				if (i.texcoord.y > _ScrollController) {
					fixed xScrollValue = _XSpeed * _Time.y;
					i.texcoord.x += xScrollValue;
				}
			
				fixed4 col = 2.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord);
				//fixed4 col = 2.0f *  _TintColor * tex2D(_MainTex, i.texcoord);
				return col;
			}

			ENDCG
		}
	}


}
