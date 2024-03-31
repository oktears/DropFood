// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Unlit_Transparent_Tree_Fog" {
	Properties {	
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}	
		_Cutoff ("Base Alpha cutoff", Range (0,.9)) = .5
		_Shininess ("Shininess", Range (0.01, 1.5)) = 1

		_FogColor("Fog Color", Color) = (1, 1, 1, 1)
		_FogStart("Fog Start", float) = 0
		_FogEnd("Fog End", float) = 300
	}

	SubShader {

		Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout" }
		Lighting off
		LOD 100
	
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
				};

				struct v2f {
					float4 vertex : SV_POSITION;				
					float2 texcoord : TEXCOORD0;				
					float4 viewSpacePos : TEXCOORD1;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				fixed _Cutoff;		
				half _Shininess;

				fixed4 _FogColor;
				float _FogStart;
				float _FogEnd;
			
				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);				
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);			
					o.viewSpacePos = mul(UNITY_MATRIX_MV, v.vertex);
					return o;
				}

				inline float4 SimulateFog(float4 pos, float4 col)
				{
					pos.w = 0.0;
					float dist = length(pos);
					float fogFactor = (_FogEnd - abs(dist)) / (_FogEnd - _FogStart);

					fogFactor = clamp(fogFactor, 0.0, 1.0);
					float3 afterFog = _FogColor.rgb *(1.0 - fogFactor) + col.rgb *fogFactor;

					return float4(afterFog, col.a);
				}
			
				fixed4 _Color;
				fixed4 frag (v2f i) : SV_Target
				{
					half4 col = tex2D(_MainTex, i.texcoord);				
					clip(col.a - _Cutoff);
					//UNITY_APPLY_FOG(i.fogCoord, col);
					return SimulateFog(i.viewSpacePos, col*_Shininess);
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
					//UNITY_FOG_COORDS(1)
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _Cutoff;			
				half _Shininess;
			
				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);			
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);				
					//UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}
			
				fixed4 _Color;
				fixed4 frag (v2f i) : SV_Target
				{
					half4 col = tex2D(_MainTex, i.texcoord);				
					clip(-(col.a - _Cutoff));
					//UNITY_APPLY_FOG(i.fogCoord, col);
					return col*_Shininess;
				}
			ENDCG
		}
	}

	SubShader {

		Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout" }
		Lighting off
		LOD 99
	
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
				};

				struct v2f {
					float4 vertex : SV_POSITION;				
					float2 texcoord : TEXCOORD0;				
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				fixed _Cutoff;		
				half _Shininess;

			
				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);				
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);			
					return o;
				}
			
				fixed4 _Color;
				fixed4 frag (v2f i) : SV_Target
				{
					half4 col = tex2D(_MainTex, i.texcoord);				
					clip(col.a - _Cutoff);
					//UNITY_APPLY_FOG(i.fogCoord, col);
					return col*_Shininess;
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
					//UNITY_FOG_COORDS(1)
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _Cutoff;			
				half _Shininess;
			
				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);			
					o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);				
					//UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}
			
				fixed4 _Color;
				fixed4 frag (v2f i) : SV_Target
				{
					half4 col = tex2D(_MainTex, i.texcoord);				
					clip(-(col.a - _Cutoff));
					//UNITY_APPLY_FOG(i.fogCoord, col);
					return col*_Shininess;
				}
			ENDCG
		}
	}
}
