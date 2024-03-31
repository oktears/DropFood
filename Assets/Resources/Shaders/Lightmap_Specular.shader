Shader "Chengzi/Building/Lightmap_Specular" {
	Properties {
		
		_MainTex ("Texture (RGB)", 2D) = "white" {}
		_LightMap ("Lightmap (RGB)", 2D) = "white" {}
		
		_LightColor("Light Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_LightIntensity ("Light Intensity", Float) = 1.0
		
		_SpecularColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecularPower("Specular Power", Float) = 8.0
		_SpecularIntensity ("Specular Intensity", Float) = 1.0
		
	}
	SubShader {
		
		CGPROGRAM
		#pragma surface surf Lambert
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _LightMap;
		uniform	float _LightIntensity;
		uniform	float4 _LightColor;
		uniform	float _SpecularPower;
		uniform	float _SpecularIntensity;
		uniform	float4 _SpecularColor;
			
		struct Input {
			float2 uv_MainTex : TEXCOORD0;
			float2 uv2_LightMap : TEXCOORD1;
		};


		void surf (Input IN, inout SurfaceOutput o) {
			float4 c = tex2D (_MainTex, IN.uv_MainTex);
			float4 l = tex2D (_LightMap, IN.uv2_LightMap);
			float4 s = pow(l, _SpecularPower);
			o.Albedo = c * l * _LightColor * _LightIntensity + s * _SpecularColor * _SpecularIntensity;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
