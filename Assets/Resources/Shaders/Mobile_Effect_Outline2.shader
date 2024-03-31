// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Particle/Mobile_Effect_Outline2"
{
	Properties
	{
		_TintColor("主颜色", Color) = (1, 1, 1, 1)
		_TintAmount("颜色强度", range(0, 100)) = 1
		_MainTex("主贴图", 2D) = "white" {}

		_RimColor("边缘颜色", Color) = (0, 0, 0, 1)
		_AlphaPower("边缘透明度衰减", Range(0, 50)) = 1
		_RimPower("边缘颜色衰减", Range(0, 50)) = 1
		_AllPower("边缘整体衰减", Range(0, 50)) = 1

		_ReflCube("反射天空盒", CUBE) = "Black" { TexGen CubeReflect }
		_ReflAmount ("反射强度", Range (0.01, 5)) = 1

	}
	SubShader
	{
		Tags{ "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
		LOD 100

		Pass
		{

			Cull Off Lighting Off ZWrite Off Fog{ Color(0,0,0,0) }

			Blend SrcAlpha One
			//Blend One OneMinusSrcAlpha
        	//Blend One SrcAlpha
        	Blend SrcAlpha OneMinusSrcAlpha
        	//Blend One Zero
        	//Blend SrcAlpha One	


			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			fixed4 _TintColor;
			float _TintAmount;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			fixed4 _RimColor;
			float _AlphaPower;
			float _RimPower;
			float _AllPower;
			samplerCUBE _ReflCube;
			float _ReflAmount;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldViewDir : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				float3 I : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);


				//世界坐标
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				//世界坐标下的视角方向
				o.worldViewDir = UnityWorldSpaceViewDir(worldPos);
				//世界坐标下的法线方向
				o.worldNormal = UnityObjectToWorldNormal(v.normal);

				o.I = reflect(-o.worldViewDir, o.worldNormal);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 texColor = tex2D(_MainTex, i.uv);
				float4 color = texColor * _TintColor * _TintAmount;

				//float rim = 1 - saturate(dot(i.worldNormal, i.worldViewDir));
				float rim = 1 - saturate(dot(i.worldNormal, normalize(i.worldViewDir)));
				float4 reflcol = texCUBE( _ReflCube, i.I );

				fixed4 col;
				col.rgb = color + reflcol.rgb * _ReflAmount + _RimColor.rgb * pow(rim, _RimPower) * _AllPower ;
				col.a = pow(rim, _AlphaPower) * _AllPower * _TintColor.a * texColor.a;

				return col;
			}
			ENDCG
		}
	}
}
