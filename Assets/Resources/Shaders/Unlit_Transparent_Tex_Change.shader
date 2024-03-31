// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Unlit_Transparent" {

	Properties {	
		_MainTex ("MainTexture", 2D) = "white" {}
		_Alpha ("Alpha", Range (0, 5)) = 1
		_MainTex2 ("Sprite Texture2", 2D) = "white" {}
		_XSpeed ("XSpeed", Range (-5, 5)) = 0
		_YSpeed ("YSpeed", Range (-5, 5)) = 0
		_MixValue("MixValue", Range(0, 1)) = 0
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
			sampler2D _MainTex2;
			float4 _MainTex_ST;
			float4 _MainTex2_ST;

			half _Alpha;
			fixed _XSpeed;
			fixed _YSpeed;
			
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
				//float alpha = lerp(col.a, _Alpha, _Alpha);
				col.a = col.a * _Alpha * i.color.a; 
				return col;
			}

			ENDCG
		}
	}

}
