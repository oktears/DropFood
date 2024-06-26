﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Effect/Mobile_Color_Add" {	
	
	Properties {
		_TintColor ("Tint Color",Color) = (1,1,1,1)
	}
	SubShader {
	
		Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
		LOD 100
		
        Pass {
        
		  	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
        	Blend SrcAlpha One	
        	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _TintColor;

			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				return i.color * _TintColor;
			}

			 ENDCG
      }
   }

}
