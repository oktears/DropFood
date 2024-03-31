// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Building/Lightmap_Reflective" {	
	
	Properties {
		_MainTex ("主贴图(jpg)", 2D) = "white" {}
		_LightMap ("光照贴图(jpg)", 2D) = "white" {}
		_Shiness ("光照强度", Range (0, 5)) = 1
		
		[NoScaleOffset] _Cube("反射天空盒", CUBE) = "Black" { TexGen CubeReflect }
		[NoScaleOffset] _CubMask("反射遮罩图", 2D) = "Black" {}
		_ReflAmount ("反射强度", Range (0.01, 1)) = 1
		
		AddColor("附加颜色", Color) = (0, 0, 0, 0)
	}
	SubShader {
	
		Tags { "RenderType"="Opaque" "Queue"="Geometry" "IGNOREPROJECTOR"="true" }
		
		LOD 100
		
        Pass {
        
			//Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
			Lighting Off
	
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         sampler2D _MainTex;	
         float4 _MainTex_ST;
         sampler2D _LightMap;	
         float4 _LightMap_ST;
         float _Shiness;
		 
		fixed4 _AddColor;
		samplerCUBE _Cube;
		sampler2D _CubMask;
		float _ReflAmount;

		 #include "UnityCG.cginc"

		struct appdata_input {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 texcoord : TEXCOORD0;
			float4 texcoord1 : TEXCOORD1;
		};
		
         struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
            float2 uv2 : TEXCOORD1;
            float3 I : TEXCOORD2;
         };
 
         v2f vert(appdata_input v) 
         {
            v2f o;
 
            o.pos = UnityObjectToClipPos (v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.uv2 = TRANSFORM_TEX(v.texcoord1, _LightMap);
			
			//世界坐标
			float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			//世界坐标下的视角方向
			float3 worldViewDir = UnityWorldSpaceViewDir(worldPos);
			//世界坐标下的法线方向
			fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);

			o.I = reflect(-worldViewDir, worldNormal);
			
            return o;
         }

         float4 frag(v2f i) : COLOR
         {
			float4 col = tex2D(_MainTex, i.uv);	
			float4 col2 = tex2D(_LightMap, i.uv2);	
			
			float4 maskcol = tex2D (_CubMask, i.uv);
			float4 reflcol = texCUBE( _Cube, i.I );
			
            return col*col2*_Shiness + _ReflAmount *reflcol * maskcol + _AddColor;
         }
 
         ENDCG
      }
   }

}
