Shader "Chengzi/Car/Unlit_Reflective_Fix_Main_Decal" {
 Properties
    {
        _MainTint ("DiffuseColor", Color) = (1,1,1,1)
        _MainTex ("DiffuseTexture (RGB)", 2D) = "white" {}
		_DecalTex ("DecalTexture (RGBA)", 2D) = "black" {}
        _Cube ("Reflective Cubemap", CUBE) = ""{}
		_ReflMask ("Reflective Mask (RGB)", 2D) = "white" {}
        _ReflAmount ("Reflection Amount", Range(0,1)) = 0.5
    }
    
    SubShader {
        Tags {"Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Opaque"}
		LOD 100			
        
        CGPROGRAM
        #pragma surface surf Unlit

		fixed4 LightingUnlit(SurfaceOutput s, fixed3 lightDir, fixed atten) {
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;   
			return c;
	    }

        samplerCUBE _Cube;
        sampler2D _MainTex;
	    sampler2D _DecalTex;
		sampler2D _ReflMask;
        float4 _MainTint;
        float _ReflAmount;
    
        struct Input
        {
            float2 uv_MainTex;
		    float2 uv_DecalTex;
            float3 worldNormal;
            float3 worldRefl;
            float3 viewDir;
            INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutput o) {
			//主纹理颜色
            half4 mainTexColor = tex2D (_MainTex, IN.uv_MainTex); 
			//贴花纹理颜色
			half4 decalTexColor = tex2D(_DecalTex, IN.uv_DecalTex);
			//遮罩纹理反射颜色        
			half4 maskColor = tex2D (_ReflMask, IN.uv_MainTex);    
			//反射颜色        
            half3 emission = texCUBE (_Cube, WorldReflectionVector (IN, IN.worldNormal)).rgb * _ReflAmount * maskColor.rgb;
			o.Emission = emission;
			half4 finalColor;
			if (decalTexColor.a > 0) {
				finalColor.rgb = lerp(mainTexColor.rgb * _MainTint, decalTexColor.rgb, decalTexColor.a);
			} else {
				finalColor.rgb = mainTexColor.rgb * _MainTint;
			}
            o.Albedo = finalColor;
        }

        ENDCG
    }
    FallBack "Diffuse"
} 