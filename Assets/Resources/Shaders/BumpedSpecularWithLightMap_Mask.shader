// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Bumped_Specular_LightMap_Mask"
{
	Properties
	{
		_Shininess("Shininess", Range(0.03, 10)) = 0.078125
		_MainTex("Base (RGB) Gloss (A)", 2D) = "white" { }
		_BumpMap("Normalmap", 2D) = "bump" { }
		_LightDir("LightDir", Vector) = (1, 0, 0, 0)
		_LightColor("LightColor", Color) = (1, 1, 1, 1)
	
		_LightMap("Lightmap (RGB)", 2D) = "white" {}
		_LightMapFactor("LightMapFactor", Range(0, 5)) = 1

		_MaskTex ("Mask (RGB)", 2D) = "black" {}
		_MaskFactor("Mask Factor", Range (0, 5)) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" "IGNOREPROJECTOR" = "true"  }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag


			#include "UnityCG.cginc"
			
			struct appdata_input {
				float4 vertex : POSITION;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 uv : TEXCOORD0;
				float4 TtoW0 : TEXCOORD1;
				float4 TtoW1 : TEXCOORD2;
				float4 TtoW2 : TEXCOORD3;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _BumpMap;
			float _Shininess;

			float4 _LightDir;
			fixed3 _LightColor;
			
			sampler2D _LightMap;
			float4 _LightMap_ST;
			float _LightMapFactor;

			sampler2D _MaskTex;
			float _MaskFactor;

			v2f vert(appdata_input v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv.zw = TRANSFORM_TEX(v.texcoord1, _LightMap);

				//世界坐标
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				//世界坐标下的视角方向
				float3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
				//世界坐标下的法线方向
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				//世界坐标下的切线
				float3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				//世界坐标下的副切线
				float tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				float3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;

				//go 光栅化
				o.TtoW0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.TtoW1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.TtoW2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{

				fixed4 mainColor = tex2D(_MainTex, i.uv.xy);
				float3 normal = UnpackNormal(tex2D(_BumpMap, i.uv.xy));
				fixed4 lightmapColor = tex2D(_LightMap, i.uv.zw);

				float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);

				float3 lightDir = normalize(_LightDir.xyz);

				float3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
				worldViewDir = normalize(worldViewDir);

				float3 worldN;
				worldN.x = dot(i.TtoW0.xyz, normal);
				worldN.y = dot(i.TtoW1.xyz, normal);
				worldN.z = dot(i.TtoW2.xyz, normal);

				// diffuse intensity
				float dotWL = dot(worldN, lightDir);
				float diff = max(0.0, dotWL);  // light dir for diffuse

				// specular intensity
				float spec = 0.0;
				if (dotWL >= 0.0)
				{
					spec = pow(max(0.0, dot(reflect(-lightDir, worldN), worldViewDir)), _Shininess);
				}

				fixed4 mask = tex2D(_MaskTex, i.uv.xy);

				fixed3 final = diff * mainColor.rgb + spec * _LightColor.rgb;
				
				final = final * _LightMapFactor * lightmapColor.rgb + mask * _MaskFactor;

				return fixed4(final, 1);
			}
			ENDCG
		}
	}

	
	SubShader {

		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" "IGNOREPROJECTOR" = "true" }

		LOD 99

		Pass {

			CGPROGRAM

			#pragma vertex vert  
			#pragma fragment frag 

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half _Shininess;

			#include "UnityCG.cginc"

			struct appdataInput {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata_base v)
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				float4 col = tex2D(_MainTex, i.uv);
				return col * _Shininess;
			}

			ENDCG
		}
	}
}
