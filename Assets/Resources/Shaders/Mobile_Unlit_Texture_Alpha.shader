﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Mobile_Unlit_Texture_Alpha" {	
	
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Alpha ("Alpha", Range (0, 1)) = 1
	}
	SubShader {
	
		Tags { "RenderType"="Opaque" "Queue"="Geometry" "IGNOREPROJECTOR"="true" }
		
		LOD 100
		
        Pass {
        
			//Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
        	//Blend One OneMinusSrcAlpha
        	//Blend One SrcAlpha
        	//Blend SrcAlpha OneMinusSrcAlpha
        	//Blend One Zero
        	//Blend SrcAlpha One	
			Blend SrcAlpha OneMinusSrcAlpha
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         sampler2D _MainTex;	
         float4 _MainTex_ST;
         float _Alpha;
		 

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
 
            o.pos = UnityObjectToClipPos (v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			
            return o;
         }

         float4 frag(v2f i) : COLOR
         {
			float4 col = tex2D(_MainTex, i.uv);	
			col.a = _Alpha;
            return col;
         }
 
         ENDCG
      }
   }

}
