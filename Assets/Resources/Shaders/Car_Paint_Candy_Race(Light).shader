

//�ǹ�ɫ����, ��Ҫ�ܹ�
Shader "Chengzi/Car/Car_Paint_Candy_Race" {

	Properties {
		//������ɫ
		_Color("Diffuse Color", Color) = (1, 1, 1,1)
		//�߹���ɫ
		_SpecColor("Specular Color", Color) = (1,1,1,1)
		//����ɫ		
		_AmbientColor("Metalic Color", Color) = (1,1,1, 1)
		//�ǹ�ɫ
		_AmbientColor2("Candy Color", Color) = (1,1,1, 1)
		//������ɫ
		_ReflectionColor("Reflection Color", Color) = (1,1,1, 1)		
		//�����
		_Shininess("Glossiness", Range(0.1,2) ) = 0.5
		//����ͼ		
		_MainTex("Diffuse", 2D) = "white" {}
		//��������
		_PaintMaskTex("PaintMaskTex", 2D) = "white" {}
		//��������
		_RefMaskTex("RefMaskTex", 2D) = "white" {}
		////ϸ����ͼ
		//_FlakeTex("Flakes", 2D) = "white" {}
		////ϸ���ܶ�
		//_FlakeDens("Flakes Tile", Range(1,40) ) = 2
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
		//��������Χ		
		_MetalicScale ("Metalic Intensity", Range(0.0,4.0)) = 0
		//��������ǿ��
		_MetalicPower ("Metalic Power", Range(0.0,20.0)) = 0		
		//�ǹ�ɫ��Χ
		_CandyScale ("Candy Intensity", Range(0.0,4.0)) = 0
		//�ǹ�ɫǿ��
		_CandyPower ("Candy Power", Range(0.0,20.0)) = 0
	} 

	SubShader {
			
		Tags { "RenderType"="Opaque" "IGNOREPROJECTOR" = "false" }
		LOD 100			
		Cull Back
		ZWrite On
		ZTest Lequal
		ColorMask RGBA
		CGPROGRAM		

		#pragma surface surf BlinnPhong
		#pragma target 3.0		

		struct Input
		{
			float2 uv_MainTex;	
			//float2 uv_FlakeTex;
			float3 worldRefl;
			float3 viewDir;
			float3 lightDir;
			float3 normal;
			INTERNAL_DATA
		};		

		sampler2D _MainTex;
		sampler2D _PaintMaskTex;
		sampler2D _RefMaskTex;
		//sampler2D _FlakeTex;
		samplerCUBE _CloseCube;		
		samplerCUBE _FarCube;		
		float4 _Color;		
	  	float4 _AmbientColor;
	  	float4 _AmbientColor2;
	  	float4 _ReflectionColor;	  	
		float _Shininess;
		//float _FlakeDens;
		float _FresnelScale;
		float _FresnelPower;
		float _MetalicScale;		
		float _MetalicPower;
		float _CandyScale;		
		float _CandyPower;
		float _CloseFactor;
		float _FarFactor;

		uniform float4x4 _CloseRotation;
		uniform float4x4 _FarRotation;

		void surf (Input IN, inout SurfaceOutput o){		
			
			//0.���㷨��
			o.Normal = normalize(float3(0.0,0.0,1.0));
			
			//1.�����ӽǺ͹��շ���
			float3 worldRefl = WorldReflectionVector (IN, o.Normal);

			float3 closeRefl = mul(_CloseRotation, fixed4(worldRefl, 0));
			float3 farRefl = mul(_FarRotation, fixed4(worldRefl, 0));

			float3 normEyeVec = normalize (IN.viewDir);
			float3 worldLight = normalize (IN.lightDir);
			float dotEyeVecNormal = abs(dot(normEyeVec, o.Normal));				
			
			//2.��������ͼ + ������ɫ
			float4 Tex1 = tex2D( _MainTex, IN.uv_MainTex);	
			float4 tintMaskColor = tex2D(_PaintMaskTex, IN.uv_MainTex);
		
			float specularmask = Tex1.a ;
			_Color.rgb = lerp(fixed3(1, 1, 1), _Color.rgb, tintMaskColor.rgb);
			//float4 Diffuse = ((_Color* (specularmask) )* Tex1);
			//float4 Diffuse = ((_Color* (specularmask) )* Tex1) + (Tex1*(1-specularmask));	
			float4 Diffuse = _Color* Tex1;				

			//3.����ϸ�ڿ������߹�ɫ
			//float4 Tex2 = tex2D( _FlakeTex, IN.uv_FlakeTex * _FlakeDens );	
			//Tex2.rgb = lerp(fixed3(1, 1, 1), Tex2.rgb, tintMaskColor.rgb);
			float4 Specular = _Shininess;
			
			//4.������ƶ�����	
			float4 closeCube = texCUBE(_CloseCube, closeRefl) * _CloseFactor;
			float4 farCube = texCUBE(_FarCube, farRefl) * _FarFactor;
			float4 refMask = tex2D(_RefMaskTex, IN.uv_MainTex);
			float4 fresnel = (1.0 - dot( normEyeVec, o.Normal));
			float4 emmission = _FresnelScale * (closeCube + farCube) * pow(fresnel, _FresnelPower) * refMask * _ReflectionColor;			
			
			//5.�������ɫ���ǹ�ɫ
			float metalic = (specularmask * _MetalicScale) * pow(dotEyeVecNormal, _MetalicPower);
			float candy = _CandyScale * pow(dotEyeVecNormal, _CandyPower);
			
			o.Albedo = Diffuse * ((metalic + (1 - specularmask) + (specularmask)) 
			+ (_AmbientColor.rgb * tintMaskColor.rgb)  ) 
			+ (candy * _AmbientColor2.rgb * tintMaskColor.rgb);
			//o.Albedo = Diffuse;
			o.Specular = Specular;						
			o.Gloss = _SpecColor * specularmask;				
			o.Emission = emmission * emmission * specularmask;
		}		
		ENDCG
	} 
	FallBack "Reflective/Diffuse"
}
