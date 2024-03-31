// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Lightmap_Fog" {	
	
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_LightMap ("Lightmap (RGB)", 2D) = "white" {}
		_Shiness ("Shiness", Range (0, 5)) = 1

		_FogColor("Fog Color", Color) = (1, 1, 1, 1)
		_FogStart("Fog Start", float) = 0
		_FogEnd("Fog End", float) = 300
	}

	//高端机，开启雾
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
         fixed4 _MainTex_ST;
         sampler2D _LightMap;	
         fixed4 _LightMap_ST;
         fixed _Shiness;
         
         sampler2D _MaskTex;
         fixed _MaskFactor;
		 
		 fixed4 _FogColor;
		 fixed _FogStart;
		 fixed _FogEnd;

		 #include "UnityCG.cginc"

		struct appdata_input {
			fixed4 vertex : POSITION;
			fixed4 texcoord : TEXCOORD0;
			fixed4 texcoord1 : TEXCOORD1;
		};
		
         struct v2f {
            fixed4 pos : SV_POSITION;
            fixed4 uv : TEXCOORD0;
			fixed4 viewSpacePos : TEXCOORD1;
         };
 
         v2f vert(appdata_input v) 
         {
            v2f o;
 
            o.pos = UnityObjectToClipPos (v.vertex);
			o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.uv.zw = TRANSFORM_TEX(v.texcoord1, _LightMap);
			o.viewSpacePos = mul(UNITY_MATRIX_MV, v.vertex);
			
            return o;
         }

		 inline fixed4 SimulateFog(float4 pos, fixed4 col)
		 {
			 pos.w = 0.0;
			 fixed dist = length(pos);
			 fixed fogFactor = (_FogEnd - abs(dist)) / (_FogEnd - _FogStart);

			 fogFactor = clamp(fogFactor, 0.0, 1.0);
			 fixed3 afterFog = _FogColor.rgb *(1.0 - fogFactor) + col.rgb *fogFactor;

			 return fixed4(afterFog, col.a);
		 }

         fixed4 frag(v2f i) : COLOR
         {
			fixed4 col = tex2D(_MainTex, i.uv.xy);	
			fixed4 col2 = tex2D(_LightMap, i.uv.zw);	
			return SimulateFog(i.viewSpacePos, col*col2*_Shiness);
         }
 
         ENDCG
      }
   }

   	//低端机，关闭雾
	SubShader {
	
		Tags { "RenderType"="Opaque" "Queue"="Geometry" "IGNOREPROJECTOR"="true" }
		 
		LOD 99
		
        Pass {
        
		//Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
		Lighting Off
	
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         sampler2D _MainTex;	
         fixed4 _MainTex_ST;
         sampler2D _LightMap;	
         fixed4 _LightMap_ST;
         fixed _Shiness;
         
         sampler2D _MaskTex;
         fixed _MaskFactor;
		 
		 #include "UnityCG.cginc"

		struct appdata_input {
			fixed4 vertex : POSITION;
			fixed4 texcoord : TEXCOORD0;
			fixed4 texcoord1 : TEXCOORD1;
		};
		
         struct v2f {
            fixed4 pos : SV_POSITION;
            fixed4 uv : TEXCOORD0;
         };
 
         v2f vert(appdata_input v) 
         {
            v2f o;
 
            o.pos = UnityObjectToClipPos (v.vertex);
			o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.uv.zw = TRANSFORM_TEX(v.texcoord1, _LightMap);
			
            return o;
         }

         fixed4 frag(v2f i) : COLOR
         {
			fixed4 col = tex2D(_MainTex, i.uv.xy);	
			fixed4 col2 = tex2D(_LightMap, i.uv.zw);	
			return col * col2 * _Shiness;
         }
 
         ENDCG
      }
   }

}
