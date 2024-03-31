// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Unlit_Transparent_Lightmap_Single" {

	Properties {	
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		_LightMap ("Lightmap (RGB)", 2D) = "white" {}
		_Cutoff ("Base Alpha cutoff", Range (0,.9)) = .5
		_Shininess ("Shininess", Range (0.01, 10)) = 1
	}

	SubShader {

		Tags{ "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
		LOD 100

		Pass {  

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;				
				float2 texcoord : TEXCOORD0;
				float2 texcoord2 : TEXCOORD1;
			};

			struct v2f {
				float4 vertex : SV_POSITION;			
				float2 texcoord : TEXCOORD0;
				float2 texcoord2 : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Cutoff;
			sampler2D _LightMap;
			half _Shininess;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);			
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.texcoord2 = TRANSFORM_TEX(v.texcoord2, _MainTex);
				return o;
			}
			
			fixed4 _Color;
			fixed4 frag (v2f i) : SV_Target
			{
				half4 col = tex2D(_MainTex, i.texcoord);
				half4 l = tex2D(_LightMap, i.texcoord2);
				clip(col.a - _Cutoff);
				return col*l*_Shininess;
			}

			ENDCG
		}
	}

}
