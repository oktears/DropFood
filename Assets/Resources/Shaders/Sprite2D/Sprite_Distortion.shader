// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Chengzi/Sprite2D/Sprite_Distortion"
{
 Properties {
		_MainTex ("Grass Texture", 2D) = "white" {}

        _Refraction ("Refraction", float) = 1.0        //’€…‰÷µ
        _DistortionMap ("Distortion Map", 2D) = "" {}    //≈§«˙

        _DistortionScrollX ("DistortionX Offset", float) = 0    
        _DistortionScrollY ("DistortionY Offset", float) = 0    
        _DistortionScaleX ("DistortionX Scale", float) = 1.0
        _DistortionScaleY ("DistortionY Scale", float) = 1.0
        _DistortionPower ("Distortion Power", float) = 0.08	
	
		_SpeedX("OffsetXSpeed", float) = 0.2  
		_SpeedY("OffsetYSpeed", float) = 0.2  
	}
 
	SubShader{

		Tags{"Queue"="Transparent" "RenderType"="Opaque" "IgnoreProject"="True"}

		Pass{
			Tags{"LightMode"="ForwardBase"}
 
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc" 
 
			sampler2D _MainTex;
			fixed _TimeScale;
			fixed _OffsetX;
            uniform sampler2D _DistortionMap;
 
            uniform float _DistortionScrollX;
            uniform float _DistortionScrollY; 
            uniform float _DistortionPower;
            uniform float _DistortionScaleX;
            uniform float _DistortionScaleY;
            uniform float _Refraction;
			uniform float _SpeedX;   
			uniform float _SpeedY;

			struct a2v {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			
			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
 
			v2f vert(a2v v){
				v2f o;
				float4 offset = float4(0,0,0,0);
				o.pos = UnityObjectToClipPos(v.vertex + offset);
				o.uv = v.texcoord.xy;
				return o;
			}
 
			fixed4 frag(v2f i) : SV_Target{
                float2 disOffset = float2(_DistortionScrollX,_DistortionScrollY);
                float2 disScale = float2(_DistortionScaleX,_DistortionScaleY);
                   
				fixed xScrollValue = _SpeedX * _Time.y;  
            	fixed yScrollValue = _SpeedY * _Time.y;  
            	disOffset.x += xScrollValue;
            	disOffset.y += yScrollValue;

                float4 disTex = tex2D(_DistortionMap, disScale * i.uv+disOffset);
                   
                float2 offsetUV = (-_Refraction*(disTex * _DistortionPower - (_DistortionPower * 0.5)));
				float4 retColor = tex2D(_MainTex, i.uv + offsetUV);   


				return retColor;
			}
 
			ENDCG
		}
	}
	FallBack Off
}
