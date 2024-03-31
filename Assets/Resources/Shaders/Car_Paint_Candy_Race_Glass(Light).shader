

//�ǹ�ɫ����, ��Ҫ�ܹ�
Shader "Chengzi/Car/Car_Paint_Candy_Race_Glass" {

	Properties {
		//������ɫ
		_ReflectionColor("Reflection Color", Color) = (1,1,1, 1)		
		//����ͼ		
		_MainTex("Diffuse", 2D) = "white" {}
		//��������
		_RefMaskTex("RefMaskTex", 2D) = "white" {}
		//������պ�(��)
		_CloseCube("CloseCube", Cube) = "" { TexGen CubeReflect }
		//������պ�(Զ)
		_FarCube("FarCube", Cube) = "" { TexGen CubeReflect }
		//����ǿ��
		_CloseFactor("CloseFactor", Range(0, 3)) = 1
		//Զ��ǿ��
		_FarFactor("FarFactor", Range(0, 3)) = 1
		//���������䷶Χ		
		_FresnelScale ("Fresnel Intensity", Range(0,2) ) = 0
		//����������ǿ��
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
			
			//0.���㷨��
			o.Normal = normalize(float3(0.0,0.0,1.0));
			
			//1.�����ӽǺ͹��շ���
			float3 worldRefl = WorldReflectionVector (IN, o.Normal);
			float3 normEyeVec = normalize (IN.viewDir);
			
			float3 closeRefl = mul(_CloseRotation, fixed4(worldRefl, 0));
			float3 farRefl = mul(_FarRotation, fixed4(worldRefl, 0));
					
			//2.��������ͼ + ������ɫ
			float4 Tex1 = tex2D( _MainTex, IN.uv_MainTex);			
			float specularmask = Tex1.a ;
			float4 Diffuse = Tex1;				
			
			//4.������ƶ�����	
			float4 closeCube = texCUBE(_CloseCube, closeRefl) * _CloseFactor;
			float4 farCube = texCUBE(_FarCube, farRefl) * _FarFactor;
			float4 refMask = tex2D(_RefMaskTex, IN.uv_MainTex);
			float4 fresnel = (1.0 - dot( normEyeVec, o.Normal));
			float4 emmission = _FresnelScale * (closeCube + farCube) * pow(fresnel, _FresnelPower) * refMask * _ReflectionColor;			
			
			//5.�������ɫ���ǹ�ɫ
						
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
