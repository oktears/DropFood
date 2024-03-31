// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Particle/Particle_Add_UVScroll_Mask" {	
		
	Properties {
		_MainTex("_MainTex", 2D) = "black" {}
		_MaskText("_MaskText", 2D) = "black" {}
		_TintColor("_TintColor", Color) = (1,1,1,0.5)
		_XSpeedMain ("XSpeedMain", Range (-5, 5)) = 0
		_YSpeedMain ("YSpeedMain", Range (-5, 5)) = 0
		_XSpeedMask ("XSpeedMask", Range (-5, 5)) = 0
		_YSpeedMask ("YSpeedMask", Range (-5, 5)) = 0
	}
	SubShader {
	
		Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
		LOD 100
		
		Pass {
		
			Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
    		Blend SrcAlpha One	
    		//Blend SrcColor One
    		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _MainTex_ST;
			sampler2D _MaskText;
			fixed4 _MaskText_ST;
			fixed4 _TintColor;
			fixed _XSpeedMain;
			fixed _YSpeedMain;
			fixed _XSpeedMask;
			fixed _YSpeedMask;			
					
			struct appdata_t {
				fixed4 vertex : POSITION;
				fixed4 color : COLOR;
				fixed2 texcoord : TEXCOORD0;
			};

			struct v2f {
				fixed4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				fixed2 texcoordMain : TEXCOORD0;
				fixed2 texcoordMask : TEXCOORD1;
			};
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				
				fixed2 uv = v.texcoord;
				uv.x += _XSpeedMain * _Time.y;  
            	uv.y += _YSpeedMain * _Time.y;  
				o.texcoordMain = TRANSFORM_TEX(uv, _MainTex);
				
				uv = v.texcoord;
				uv.x += _XSpeedMask * _Time.y;  
            	uv.y += _YSpeedMask * _Time.y; 
				o.texcoordMask = TRANSFORM_TEX(uv, _MaskText);
				

				return o;
			}

			fixed4 frag (v2f i) : COLOR
			{
				fixed4 colMain = tex2D(_MainTex, i.texcoordMain);
				fixed4 colMask = tex2D(_MaskText, i.texcoordMask);
				fixed4 col = i.color * colMain * colMask * _TintColor;
				col *= 2.0;
				
				return col;
			}
			ENDCG 
		}
	}	
	

}
