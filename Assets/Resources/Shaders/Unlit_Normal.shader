// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Unlit_Normal" {

	Properties {
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Shininess("Shininess", Range(0.01, 10)) = 1
	}

	SubShader {

		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" "IGNOREPROJECTOR" = "true" }

		LOD 100

		Pass {

			CGPROGRAM

			#pragma vertex vert  
			#pragma fragment frag 

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half _Shininess;

			#include "UnityCG.cginc"

			struct appdataInput {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata_base v)
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				float4 col = tex2D(_MainTex, i.uv);
				return col * _Shininess;
			}

			ENDCG
		}
	}
}
