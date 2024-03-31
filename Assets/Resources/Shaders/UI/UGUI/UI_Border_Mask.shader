// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/UI/UGUI/BorderMask【边缘虚化】"
{
	Properties
	{
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1 , 1, 1, 1)
		_AlphaTex("Alpha Mask", 2D) = "white" {}
		_OffsetX("OffsetX", Float) = 1
		_OffsetY("OffsetY", Float) = 1
		_Scale("Scale", Float) = 1
		//_CenterVertex("CenterVertex", Vector) = (1, 1, 1, 1)
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha
		
		Pass
		{

		CGPROGRAM
			#include "UnityCG.cginc"
			
			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			sampler2D _AlphaTex;

			struct appdata_t
			{
				fixed4 vertex : POSITION;
				fixed4 color : COLOR;
				fixed2 texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				fixed4 pos : SV_POSITION;
				fixed4 color : COLOR;
				fixed2 uvMain : TEXCOORD1;
				fixed2 uvAlpha : TEXCOORD2;
			};
			
			fixed4 _Color;
			fixed _OffsetX;
			fixed _OffsetY;
			fixed _Scale;
			fixed4 _MainTex_ST;
			fixed4 _CenterVertex;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uvMain = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color * _Color;
				
				o.uvAlpha = mul(unity_ObjectToWorld, v.vertex).xy;
				//o.uvAlpha = mul(_Object2World, _CenterVertex).xy;

				o.uvAlpha *= _Scale;
	 			o.uvAlpha.x += _OffsetX;
				o.uvAlpha.y += _OffsetY;
				

				return o;
			}

			half4 frag (v2f i) : COLOR
			{
				half4 texcol = tex2D(_MainTex, i.uvMain) * i.color;
				texcol.a *= tex2D(_AlphaTex, i.uvAlpha).rgb;
				texcol.rgb *= texcol.a;
				return texcol;
			}
			
		ENDCG
		}
	}
	
	Fallback "Unlit/Texture"
}
