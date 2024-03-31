// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/UI/UGUI/AlphaGradient_Portrait_Overlay【纵向透明度渐变_颜色覆盖】"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		//渐变强度
		_GradientAmount("GradientAmount", range(1, 2)) = 1.5
		//渐变结束的百分比，纵向
		_GradientEndPercent("GradientEndPrecent", range(0, 1)) = 0.5
		_LightAmount("LightAmount", range(1, 2)) = 1

	}

		SubShader
		{
			Tags
			{
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
			Blend DstColor One

			Pass {

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
				float _GradientAmount;
				float _GradientEndPercent;
				float _LightAmount;

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
					half4 color = tex2D(_MainTex, IN.texcoord) * IN.color * _LightAmount;
					clip(color.a - 0.01);

					if (IN.texcoord.y > _GradientEndPercent) {
						color.a = 0;
					}
					else {
						color.a = lerp(color.a, 0, IN.texcoord.y * _GradientAmount);
					}

					return color;
				}

				ENDCG
			}
		}
}
