// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Unlit_Transparent_Lightmap" {
		Properties {	
	_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
	_LightMap ("Lightmap (RGB)", 2D) = "white" {}
	_Cutoff ("Base Alpha cutoff", Range (0,.9)) = .5
	_Shininess ("Shininess", Range (0.01, 10)) = 1
}

SubShader {
	Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout" }
	Lighting off
	
	// Render both front and back facing polygons.
	Cull Off
	
	// first pass:
	//   render any pixels that are more than [_Cutoff] opaque
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			//#pragma multi_compile_fog
			
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
				UNITY_FOG_COORDS(1)
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
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 _Color;
			fixed4 frag (v2f i) : SV_Target
			{
				half4 col = tex2D(_MainTex, i.texcoord);
				half4 l = tex2D(_LightMap, i.texcoord2);
				clip(col.a - _Cutoff);
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col*l*_Shininess;
			}
		ENDCG
	}

	// Second pass:
	//   render the semitransparent details.
	Pass {
		Tags { "RequireOption" = "SoftVegetation" }
		
		// Dont write to the depth buffer
		ZWrite off
		
		// Set up alpha blending
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			//#pragma multi_compile_fog
			
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
				UNITY_FOG_COORDS(1)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Cutoff;
			sampler2D _LightMap;
			half _Shininess;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);			
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.texcoord2 = TRANSFORM_TEX(v.texcoord2, _MainTex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 _Color;
			fixed4 frag (v2f i) : SV_Target
			{
				half4 col = tex2D(_MainTex, i.texcoord);
				half4 l = tex2D(_LightMap, i.texcoord2);
				clip(-(col.a - _Cutoff));
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col*l*_Shininess;
			}
		ENDCG
	}
}

}
