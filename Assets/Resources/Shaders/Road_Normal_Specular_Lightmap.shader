// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Road_Normal_Specular_LightMap" {
	Properties{
		//主颜色
		_MainColor("MainColor", Color) = (1, 1, 1, 1)
		//高光颜色
		_SpecColor("SpecularColor", Color) = (0.5, 0.5, 0.5, 1)
		//光照颜色
		_LightColor("LightColor", Color) = (0.5, 0.5, 0.5, 1)
		//光照角度
		_LightRotationX("LightRotationX", Range(-2, 2)) = 0
		_LightRotationY("LightRotationY", Range(-2, 2)) = 0
		_LightRotationZ("LightRotationZ", Range(-2, 2)) = 0
		//高光强度
		_SpecularAmount("SpecularAmount", Range(0, 10)) = 1
		//高光范围
		_SpecularScope("SpecularScope", Range(0.01, 1)) = 0.08
		//LightMap强度
		_LightMapAmount("LightMapAmount", Range(0, 10)) = 1
		//主贴图, alpha通道作为高光贴图
		_MainTex("MainTexture", 2D) = "white" {}
		//法线贴图
		_NormalTex("NormalTexture", 2D) = "bump" {}
		//光照贴图
		_LightMap("LightMap", 2D) = "white" {}
	}

	SubShader	{

		Tags { "RenderType" = "Opaque" "IgnoreProjector" = "True"  }

		LOD 100			

		Pass {

			Name "FORWARD"

			Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM
			// compile directives
			#pragma vertex vert_surf
			#pragma fragment frag_surf
			#pragma target 3.0
			#include "HLSLSupport.cginc"
			#include "UnityShaderVariables.cginc"

			#define UNITY_PASS_FORWARDBASE
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

	#define INTERNAL_DATA 
			half3 internalSurfaceTtoW0; 
			half3 internalSurfaceTtoW1;
			half3 internalSurfaceTtoW2;
	#define WorldReflectionVector(data, normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
	#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))


			//#pragma surface surf BlinnPhong
			//#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _NormalTex;
			sampler2D _LightMap;
			fixed4 _MainColor;
			fixed4 _LightColor;

			half _SpecularAmount;
			half _SpecularScope;
			fixed _LightRotationX;
			fixed _LightRotationY;
			fixed _LightRotationZ;
			fixed _LightMapAmount;

			struct Input {
				float2 uv_MainTex;
				float2 uv_NormalTex;
				float2 uv_LightMap;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
				fixed4 lightMap = tex2D(_LightMap, IN.uv_LightMap);
				fixed4 c = tex * _MainColor;
				o.Albedo = c.rgb;
				//高光的计算用主贴图的alpha通道
				o.Emission = c.rgb * tex.a * _SpecularAmount + c.rgb * lightMap.rgb * _LightMapAmount;
				o.Gloss = tex.a;
				o.Alpha = c.a * lightMap.a;
				o.Specular = _SpecularScope;
				o.Normal = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
			}

			struct v2f_surf {
				float4 pos : SV_POSITION;
				//主贴图，高光贴图，用xy来表示
				float4 main : TEXCOORD0;
				//第二个通道作为nromal贴图的通道
				float4 normal : TEXCOORD1;
				//TBN坐标系
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
			};

			float4 _MainTex_ST;
			float4 _LightMap_ST;
			float4 _NormalTex_ST;

			v2f_surf vert_surf(appdata_full v) {

				v2f_surf o;
				UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
				//顶点坐标
				o.pos = UnityObjectToClipPos(v.vertex);
				//UV1
				o.main.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				//UV2
				o.main.zw = TRANSFORM_TEX(v.texcoord1, _LightMap);
				//法线贴图使用第一套UV，单独的通道处理
				o.normal.xy = TRANSFORM_TEX(v.texcoord, _NormalTex);

				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
				o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

				return o;
			}

			fixed4 frag_surf(v2f_surf IN) : SV_Target {

				//surace shader输入参数
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT(Input,surfIN);

				//主贴图使用通道1的xy，使用模型的uv1
				surfIN.uv_MainTex = IN.main.xy;
				//光照贴图使用通道1的zw，使用模型的uv2
				surfIN.uv_LightMap = IN.main.zw;
				//法线贴图使用通道2来处理，tiling单独处理
				surfIN.uv_NormalTex = IN.normal.xy;

				float3 worldPos = float3(IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w);

				fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				//fixed3 lightDir = worldViewDir;
				//fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
				//fixed3 lightDir = normalize(_LightRotation - _WorldSpaceLightPos0);
				fixed3 _LightRotation;
				_LightRotation.x = _LightRotationX;
				_LightRotation.y = _LightRotationY; 
				_LightRotation.z = _LightRotationZ;
				fixed3 lightDir = normalize(_LightRotation - _WorldSpaceLightPos0);
			//surface shader输出参数
			#ifdef UNITY_COMPILER_HLSL
				SurfaceOutput o = (SurfaceOutput)0;
			#else
				SurfaceOutput o;
			#endif
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Specular = 0.0;
				o.Alpha = 0.0;
				o.Gloss = 0.0;
				fixed3 normalWorldVertex = fixed3(0, 0, 1);

				//调用surface shader来计算光材质参数
				surf(surfIN, o);

				// compute lighting & shadowing factor
				UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
				fixed4 c = 0;
				fixed3 worldN;
				worldN.x = dot(IN.tSpace0.xyz, o.Normal);
				worldN.y = dot(IN.tSpace1.xyz, o.Normal);
				worldN.z = dot(IN.tSpace2.xyz, o.Normal);
				o.Normal = worldN;

				// Setup lighting environment
				UnityGI gi;
				UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
				gi.indirect.diffuse = 0;
				gi.indirect.specular = 0;
				gi.light.color = _LightColor.rgb;
				gi.light.dir = lightDir;
				gi.light.ndotl = LambertTerm(o.Normal, gi.light.dir);
				//// Call GI (lightmaps/SH/reflections) lighting function
				//UnityGIInput giInput;
				//UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
				//giInput.light = gi.light;
				//giInput.worldPos = worldPos;
				//giInput.worldViewDir = worldViewDir;
				//giInput.atten = atten;

				//LightingBlinnPhong_GI(o, giInput, gi);

				c += LightingBlinnPhong(o, worldViewDir, gi);
				c.rgb += o.Emission;
				c.a = 1;
				return c;
			}

		ENDCG

		}

	}
	FallBack "Mobile/Diffuse"
}
