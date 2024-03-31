// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/RadialBlur" {
	Properties{
	_MainTex("Base (RGB)", 2D) = "white" {}
}

	SubShader
	{
		Pass
		{
		ZTest Always Cull Off ZWrite Off
		Fog{ Mode off }

		CGPROGRAM
		#pragma vertex vert 
		#pragma fragment frag 
		#pragma fragmentoption ARB_precision_hint_fastest

		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform float fSampleDist;
		uniform float fSampleStrength;
		uniform float4 _MainTex_ST;
		//uniform float4 _MainTex_TexelSize;

		struct v2f {
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
		};

		v2f vert(appdata_img v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = v.texcoord.xy;
			return o;
		}

		fixed4 frag(v2f i) : COLOR
		{


			float2 texCoord = i.uv;
			float2 dir = float2(0.5, 0.5) - texCoord;

			float dist = length(dir);
			dir /= dist;
			fixed4 color = tex2D(_MainTex, texCoord);
			fixed4 sum = color;

			float2 dirDist = dir * fSampleDist;;

			sum += tex2D(_MainTex, texCoord + dirDist * (-0.08));
			sum += tex2D(_MainTex, texCoord + dirDist * (-0.05));
			sum += tex2D(_MainTex, texCoord + dirDist * (-0.03));
			sum += tex2D(_MainTex, texCoord + dirDist * (-0.02));
			sum += tex2D(_MainTex, texCoord + dirDist * (-0.01));
			sum += tex2D(_MainTex, texCoord + dirDist * 0.01);
			sum += tex2D(_MainTex, texCoord + dirDist * 0.02);
			sum += tex2D(_MainTex, texCoord + dirDist * 0.03);
			sum += tex2D(_MainTex, texCoord + dirDist * 0.05);
			sum += tex2D(_MainTex, texCoord + dirDist * 0.08);

			sum *= 0.0909;  // sum /= 11.0;


			float t = saturate(dist * fSampleStrength);
			return lerp(color, sum, t);
		}
		ENDCG

	}
	}

	Fallback off

}