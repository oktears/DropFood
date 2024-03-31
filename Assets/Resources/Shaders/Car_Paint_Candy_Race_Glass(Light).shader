

//糖果色喷漆, 需要受光
Shader "Chengzi/Car/Car_Paint_Candy_Race_Glass" {

	Properties {
		//反射颜色
		_ReflectionColor("Reflection Color", Color) = (1,1,1, 1)		
		//主贴图		
		_MainTex("Diffuse", 2D) = "white" {}
		//反射遮罩
		_RefMaskTex("RefMaskTex", 2D) = "white" {}
		//反射天空盒(近)
		_CloseCube("CloseCube", Cube) = "" { TexGen CubeReflect }
		//反射天空盒(远)
		_FarCube("FarCube", Cube) = "" { TexGen CubeReflect }
		//近景强度
		_CloseFactor("CloseFactor", Range(0, 3)) = 1
		//远景强度
		_FarFactor("FarFactor", Range(0, 3)) = 1
		//菲涅尔反射范围		
		_FresnelScale ("Fresnel Intensity", Range(0,2) ) = 0
		//菲涅尔反射强度
		_FresnelPower ("Fresnel Power", Range(0.1,3) ) = 0
	} 

	SubShader {
			
		Tags { "RenderType"="Opaque" "IGNOREPROJECTOR" = "false" }
		LOD 100			
		Cull Back
		ZWrite On
		ZTest Lequal
		ColorMask RGBA
        Lighting Off  
		CGPROGRAM		

		#pragma surface surf BlinnPhong
		#pragma target 3.0		

		struct Input
		{
			float2 uv_MainTex;	
			float3 worldRefl;
			float3 viewDir;
			float3 lightDir;
			float3 normal;
			INTERNAL_DATA
		};		

		sampler2D _MainTex;
		sampler2D _RefMaskTex;
		//sampler2D _FlakeTex;
		samplerCUBE _CloseCube;		
		samplerCUBE _FarCube;		
	  	float4 _ReflectionColor;	  	
		float _FresnelScale;
		float _FresnelPower;
		float _CloseFactor;
		float _FarFactor;

		uniform float4x4 _CloseRotation;
		uniform float4x4 _FarRotation;

		void surf (Input IN, inout SurfaceOutput o) {		
			
			//0.计算法线
			o.Normal = normalize(float3(0.0,0.0,1.0));
			
			//1.计算视角和光照方向
			float3 worldRefl = WorldReflectionVector (IN, o.Normal);
			float3 normEyeVec = normalize (IN.viewDir);
			
			float3 closeRefl = mul(_CloseRotation, fixed4(worldRefl, 0));
			float3 farRefl = mul(_FarRotation, fixed4(worldRefl, 0));
					
			//2.计算主贴图 + 喷漆颜色
			float4 Tex1 = tex2D( _MainTex, IN.uv_MainTex);			
			float specularmask = Tex1.a ;
			float4 Diffuse = Tex1;				
			
			//4.计算捏菲尔反射	
			float4 closeCube = texCUBE(_CloseCube, closeRefl) * _CloseFactor;
			float4 farCube = texCUBE(_FarCube, farRefl) * _FarFactor;
			float4 refMask = tex2D(_RefMaskTex, IN.uv_MainTex);
			float4 fresnel = (1.0 - dot( normEyeVec, o.Normal));
			float4 emmission = _FresnelScale * (closeCube + farCube) * pow(fresnel, _FresnelPower) * refMask * _ReflectionColor;			
			
			//5.计算金属色和糖果色
						
			o.Albedo = Diffuse * ((1 - specularmask) + (specularmask));
			//o.Albedo = Diffuse;
			//o.Specular = Specular;						
			o.Gloss = _SpecColor * specularmask;				
			o.Emission = emmission * emmission * specularmask;
		}		
		ENDCG
	} 
	FallBack "Reflective/Diffuse"
}
