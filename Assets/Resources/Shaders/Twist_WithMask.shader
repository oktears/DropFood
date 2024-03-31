// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Particle/Twist_WithMask" {
	Properties {
		_MainTex("Wave Texture", 2D) = "black" {}
		_TwistedTex("Twisted Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
		_MainColor("Wave Color", Color) = (1,1,1,1)
		_TwistingSpeed("Twisting Speed", Float) = 1.0
		_TwistingIntensity("Twisting Intensity", Float) = 0.2
		
		_LightDirection("Light Direction", Vector) = (0.577, 0.577, 0.577, 0.0)
	}
	SubShader {
		Tags { "QUEUE"="Transparent+1" "RenderType"="Transparent" "IGNOREPROJECTOR" = "true" } 
		
		Pass {  
			Blend SrcColor One
			
			CGPROGRAM
			#pragma vertex vert  
	        #pragma fragment frag 

			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform sampler2D _TwistedTex;
			uniform sampler2D _MaskTex;
			uniform float4 _MainColor;
			uniform float _TwistingSpeed;
			uniform float _TwistingIntensity;
			uniform float4 _LightDirection;
			
			 struct v2f {
	            float4 pos : SV_POSITION;
	            float2 uv : TEXCOORD0;
	            
	         };
			
			v2f vert(appdata_base v) 
	         {
	            v2f o;
	 
	            o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = v.texcoord;
				
	            return o;
	         }

//	         float4 frag(v2f i) : COLOR
//	         {
//	         	float twisted = tex2D(_TwistedTex, i.uv + (_Time * _TwistingSpeed).xx).x * _TwistingIntensity;
//	         	
//	         	float nn = tex2D(_MainTex, i.uv + twisted.xx).x;
//	            
//	            float3 ll = normalize(_LightDirection.xyz);
//	            
//	            float dd = max(dot(float3(nn, nn, nn), ll), 0.0);
//	            
//	            float mask = tex2D(_MaskTex, i.uv).x;
//	            
//	            float4 color = _MainColor * dd * mask;
//	            color.a = nn;
//	            return color;
//	         }


	         float4 frag(v2f i) : COLOR
	         {
	         	float twisted = tex2D(_TwistedTex, i.uv + (_Time * _TwistingSpeed).xx).x * _TwistingIntensity;
	         	
	         	float4 nn = tex2D(_MainTex, i.uv + twisted.xx);
	            
	            float3 ll = normalize(_LightDirection.xyz);
	            
	            float dd = max(dot(float3(nn.x, nn.x, nn.x), ll), 0.0);
	            
	            float4 mask = tex2D(_MaskTex, i.uv);
	            
	            float4 color = nn * _MainColor * dd * mask;
	            color.a = nn.a;
	            return color;
	         }
         
			ENDCG
		}
	} 
}
