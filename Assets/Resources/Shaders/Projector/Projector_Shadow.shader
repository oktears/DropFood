// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Projector/Projector_Shadow" {
	Properties{
		_ShadowTex("ShadowTex", 2D) = "gray" {}
		_LightAmount("LightAmount", Range(0, 5)) = 1
	}

	Subshader{
		Tags{ "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }

		Pass{
		Offset -1,-1
		ZWrite Off		
		ColorMask RGB
		//Blend DstColor Zero		
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_fog
		#include "UnityCG.cginc"

		struct v2f {
			float4 uvShadow : TEXCOORD0;
			float4 uvFalloff : TEXCOORD1;
			float4 pos : SV_POSITION;
		};

		float4x4 unity_Projector;
		fixed _LightAmount;

		v2f vert(float4 vertex : POSITION)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(vertex);
			o.uvShadow = mul(unity_Projector, vertex);
			return o;
		}

		sampler2D _ShadowTex;

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 texS = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD(i.uvShadow)) * _LightAmount;
			return texS;
		}
			ENDCG
		}
	}
}
