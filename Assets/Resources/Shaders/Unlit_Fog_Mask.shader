// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Unlit_Fog_Mask" {	
	
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Shiness ("Shiness", Range (0, 5)) = 1

		_MaskTex ("Mask (RGB)", 2D) = "black" {}
		_MaskFactor("Mask Factor", Range (0, 5)) = 1
		
		_FogColor("Fog Color", Color) = (1, 1, 1, 1)
		_FogStart("Fog Start", float) = 0
		_FogEnd("Fog End", float) = 300
	}

	SubShader {
		
		Tags { "RenderType"="Opaque" "Queue"="Geometry" "IGNOREPROJECTOR"="true" }
		
		LOD 100
		
        Pass {
        
			//Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
			Lighting Off
	
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         sampler2D _MainTex;	
         float4 _MainTex_ST;
         float _Shiness;
         
         sampler2D _MaskTex;
         float _MaskFactor;
		 
		 fixed4 _FogColor;
		 float _FogStart;
		 float _FogEnd;

		 #include "UnityCG.cginc"

		struct appdata_input {
			float4 vertex : POSITION;
			float4 texcoord : TEXCOORD0;
			float4 texcoord1 : TEXCOORD1;
		};
		
         struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
			float4 viewSpacePos : TEXCOORD1;
         };
 
         v2f vert(appdata_input v) 
         {
            v2f o;
 
            o.pos = UnityObjectToClipPos (v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.viewSpacePos = mul(UNITY_MATRIX_MV, v.vertex);
			
            return o;
         }

		 inline fixed4 SimulateFog(float4 pos, fixed4 col)
		 {
			 pos.w = 0.0;
			 float dist = length(pos);
			 float fogFactor = (_FogEnd - abs(dist)) / (_FogEnd - _FogStart);

			 fogFactor = clamp(fogFactor, 0.0, 1.0);
			 fixed3 afterFog = _FogColor.rgb *(1.0 - fogFactor) + col.rgb *fogFactor;

			 return fixed4(afterFog, col.a);
		 }

         fixed4 frag(v2f i) : COLOR
         {
			fixed4 col = tex2D(_MainTex, i.uv.xy);	
			fixed4 col3 = tex2D(_MaskTex, i.uv.xy);
			return SimulateFog(i.viewSpacePos, col*_Shiness + col3 * _MaskFactor );
         }
 
         ENDCG
      }
   }


   SubShader {
		
		Tags { "RenderType"="Opaque" "Queue"="Geometry" "IGNOREPROJECTOR"="true" }
		
		LOD 99
		
        Pass {
        
			Lighting Off
	
			CGPROGRAM
 
			#pragma vertex vert  
			#pragma fragment frag 
 
			sampler2D _MainTex;	
			float4 _MainTex_ST;
			float _Shiness;
         
			sampler2D _MaskTex;
			float _MaskFactor;

			#include "UnityCG.cginc"

			struct appdata_input {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
			};
		
			 struct v2f {
				float4 pos : SV_POSITION;
				float4 uv : TEXCOORD0;
				float4 viewSpacePos : TEXCOORD1;
			 };
 
			 v2f vert(appdata_input v) 
			 {
				v2f o;
 
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
			
				return o;
			 }


			 fixed4 frag(v2f i) : COLOR
			 {
				fixed4 col = tex2D(_MainTex, i.uv);	
				fixed4 col3 = tex2D(_MaskTex, i.uv);
				return col  * _Shiness + col3 * _MaskFactor;
			 }
 
         ENDCG
      }
   }

}
