Shader "RaceOL/OutLine" {

	Properties {
		//外描边色
		_RimColor ("Rim Color", Color) = (0.5,0.5,0.5,0.5)
		//外描边强度  
		_RimPower ("Rim Power", Range(0.0,5.0)) = 2.5
		//透明区域描边强度
		_AlphaPower ("Alpha Rim Power", Range(0.0, 8.0)) = 4.0
		//整体描边强度 
		_AllPower ("All Power", Range(0.0, 10.0)) = 1.0
	}

	SubShader {
		Tags { "Queue" = "Transparent" "IGNOREPROJECTOR" = "true"  }

		CGPROGRAM
		#pragma surface surf Lambert alpha
		struct Input {
			float3 viewDir;
			INTERNAL_DATA
		};

		float4 _RimColor;
		float _RimPower;
		float _AlphaPower;
		float _AlphaMin;
		float _InnerColorPower;
		float _AllPower;
		float4 _InnerColor;

		void surf (Input IN, inout SurfaceOutput o) {
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow (rim, _RimPower) * _AllPower;
			o.Alpha = (pow (rim, _AlphaPower)) * _AllPower;
		}

		ENDCG
	}

	Fallback "VertexLit"
} 