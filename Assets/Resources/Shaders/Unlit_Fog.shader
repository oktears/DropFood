// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Unlit_Fog" {	
	
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
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
         fixed _Shiness;
         		 
		 fixed4 _FogColor;
		 fixed _FogStart;
		 fixed _FogEnd;

		 #include "UnityCG.cginc"

		struct appdata_input {
			fixed4 vertex : POSITION;
			fixed4 texcoord : TEXCOORD0;
		};
		
         struct v2f {
            fixed4 pos : SV_POSITION;
            fixed2 uv : TEXCOORD0;
			fixed4 viewSpacePos : TEXCOORD1;
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
			 fixed dist = length(pos);
			 fixed fogFactor = (_FogEnd - abs(dist)) / (_FogEnd - _FogStart);

			 fogFactor = clamp(fogFactor, 0.0, 1.0);
			 fixed3 afterFog = _FogColor.rgb *(1.0 - fogFactor) + col.rgb *fogFactor;

			 return fixed4(afterFog, col.a);
		 }

         fixed4 frag(v2f i) : COLOR
         {
			fixed4 col = tex2D(_MainTex, i.uv);	
			return SimulateFog(i.viewSpacePos, col * _Shiness);
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
         fixed _Shiness;
         		 
		 #include "UnityCG.cginc"

		struct appdata_input {
			fixed4 vertex : POSITION;
			fixed2 texcoord : TEXCOORD0;
		};
		
         struct v2f {
            fixed4 pos : SV_POSITION;
            fixed2 uv : TEXCOORD0;
         };
 
         v2f vert(appdata_input v) 
         {
            v2f o;
 
            o.pos = UnityObjectToClipPos (v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			
            return o;
         }

         fixed4 frag(v2f i) : COLOR
         {
			fixed4 col = tex2D(_MainTex, i.uv.xy);	
			return col * _Shiness;
         }
 
         ENDCG
      }
   }

}
