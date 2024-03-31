// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/UI/UGUI/TextureAtlas_Additive【图集_高亮叠加】"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		//以texutre在atlas上的左下角点的rect的UV区间来处理
		_TextureCoordU("TextureCoordU", range(0, 1)) = 0
		_TextureCoordV("TextureCoordV", range(0, 1)) = 0
		_TextureWidth("TextureWidth", range(0, 1)) = 0
		_TextureHeight("TextureHeight", range(0, 1)) = 0
	}

	SubShader
	{
		Tags {
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha One

		Pass{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;
			float _TextureCoordU;
			float _TextureCoordV;
			float _TextureWidth;
			float _TextureHeight;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
			#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
			#endif
				OUT.color = IN.color * _Color;
				 
				return OUT;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 texCol = tex2D(_MainTex, IN.texcoord);
				half4 color = texCol * IN.color;
				clip(color.a - 0.5);

				 //在textureRect之外的像素丢弃
				if (IN.texcoord.x > (_TextureCoordU + _TextureWidth)) {
					discard;
				}
				else if (IN.texcoord.y > (_TextureCoordV + _TextureHeight)) 
				{
					discard;
				}
				else if (IN.texcoord.x < _TextureCoordU) {
					discard;
				}
				else if (IN.texcoord.y < _TextureCoordV) {
					discard;
				}

				return color;
			}

			ENDCG

		}
	}
}
