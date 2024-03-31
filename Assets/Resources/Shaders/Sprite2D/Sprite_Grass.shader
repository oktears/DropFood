// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Chengzi/Sprite2D/Sprite_Grass"
{
 Properties {
		_MainTex ("Grass Texture", 2D) = "white" {}
		_TimeScale ("Time Scale", float) = 1
		_OffsetX("OffsetX", float) = 0
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
				offset.x = sin(3.14 * _Time.y * clamp(v.texcoord.y + _OffsetX - 0.5, 0, 1)) * _TimeScale ;
				//offset.x = sin(3.14 * _Time.y * clamp(v.texcoord.y - 0.5, 0, 1)) * _TimeScale ;

				o.pos = UnityObjectToClipPos(v.vertex + offset);
				o.uv = v.texcoord.xy;
				return o;
			}
 
			fixed4 frag(v2f i) : SV_Target{
				return tex2D(_MainTex, i.uv);
			}
 
			ENDCG
		}
	}
	FallBack Off
}
