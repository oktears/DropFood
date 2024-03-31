// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Unlit_Transparent_Light" {

	Properties {	
		_MainTex ("MainTexture", 2D) = "white" {}
		_LightAmount ("Alpha", Range (0, 5)) = 1
	}

	SubShader {

		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" "IGNOREPROJECTOR" = "true" }
		
		Cull Off Lighting Off ZWrite Off Fog{ Color(0,0,0,0) }
		Blend SrcAlpha OneMinusSrcAlpha
		//Blend SrcAlpha One

		LOD 100

		Pass {  

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdataInput {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half _LightAmount;
			
			v2f vert(appdataInput v)
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color;

				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				float4 col = tex2D(_MainTex, i.uv);
				//col.a = col.a * _LightAmount; 
				col *= _LightAmount;
				//col.rgb = (0.5, 0.56, 0.31);
				col.a = 1;
				return col;
			}

			ENDCG
		}
	}

}
