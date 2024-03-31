// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/MirrorReflection"
{
	Properties
	{
		[NoScaleOffset] _MainTex("主贴图", 2D) = "white" {}
		_TintColor("染色", Color) = (0.5, 0.5, 0.5, 0.5)
		_TintAmount("染色强度", Range(0, 5)) = 1
		[NoScaleOffset] _MaskTex("反射遮罩图", 2D) = "white" {}
		_ReflAmount("反射强度", Range(0, 5)) = 1
		_BlurSize("模糊因子", Float) = 0.02
		[HideInInspector] _ReflectionTex("反射贴图", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" "IGNOREPROJECTOR" = "true"  }
		LOD 100

		Pass{

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata {
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};

			struct v2f
			{
				float4 uv : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
				float4 pos : SV_POSITION;
			};

			//float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.pos);
				o.uv.xy = v.uv; // TRANSFORM_TEX(uv, _MainTex);
				o.uv.zw = v.uv2;
				o.screenPos = ComputeScreenPos(o.pos);
				return o;
			}

			sampler2D _MainTex;
			sampler2D _MaskTex;
			sampler2D _ReflectionTex;
			fixed4 _TintColor;
			fixed _TintAmount;
			fixed _ReflAmount;
			float _BlurSize;

			//uniform float4 _MainTex_TexelSize;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 mainColor = tex2D(_MainTex, i.uv.xy);
				fixed4 maskColor = tex2D(_MaskTex, i.uv.zw);

				float4 uv0 = UNITY_PROJ_COORD(i.screenPos);
				
				fixed4 color0 = tex2Dproj(_ReflectionTex, uv0);
				fixed4 color1 = tex2Dproj(_ReflectionTex, uv0 + float4(_BlurSize, _BlurSize, 0, 0) );
				fixed4 color2 = tex2Dproj(_ReflectionTex, uv0 + float4(-_BlurSize, -_BlurSize, 0, 0));
				fixed4 color3 = tex2Dproj(_ReflectionTex, uv0 + float4(_BlurSize, -_BlurSize, 0, 0));
				fixed4 color4 = tex2Dproj(_ReflectionTex, uv0 + float4(-_BlurSize, _BlurSize, 0, 0));

				fixed4 color5 = tex2Dproj(_ReflectionTex, uv0 + float4(_BlurSize, 0, 0, 0));
				fixed4 color6 = tex2Dproj(_ReflectionTex, uv0 + float4(0, _BlurSize, 0, 0));

				fixed4 color7 = tex2Dproj(_ReflectionTex, uv0 + float4(-_BlurSize, 0, 0, 0));
				fixed4 color8 = tex2Dproj(_ReflectionTex, uv0 + float4(0, -_BlurSize, 0, 0));

				fixed4 reflColor = (color0 + color1 + color2 + color3 + color4 + color5 + color6 + color7 + color8) / 9;

				return mainColor  * _TintColor * _TintAmount + maskColor * reflColor * _ReflAmount;
			}

			ENDCG
		}
	}
}