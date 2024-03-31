﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Effect/Dissolve"
{
	Properties
	{
        _MainTex ("主贴图", 2D) = "white" {}
        _DissolorTex ("溶解贴图", 2D) = "white" {}
        _RAmount ("溶解程度", Range (0, 1)) = 0.5
        
        _DissolorWith("溶解过度宽度", float) = 0.1
        _DissColor ("溶解颜色", Color) = (1,1,1,1)
        _Illuminate ("亮度", Range (0, 4)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "IGNOREPROJECTOR" = "true" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			
			#include "UnityCG.cginc"

			struct appdata
			{
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
			};

			struct v2f
			{
                float4 pos : POSITION;
                half2 texcoord : TEXCOORD0;
                half2 texcoord1 : TEXCOORD1;
			};

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _DissolorTex;
            float4 _DissolorTex_ST;
            half _RAmount;
            
            half _DissolorWith;
            half4 _DissColor;
            half _Illuminate;
			
			v2f vert (appdata v)
			{
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.texcoord1 = TRANSFORM_TEX(v.texcoord1, _DissolorTex);
                return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                half4 mainCol = tex2D(_MainTex,i.texcoord);
                half4 DissolorTexCol = tex2D(_DissolorTex,i.texcoord1);
                half clipVauleR = DissolorTexCol.r - _RAmount;
                if(clipVauleR <= 0)
                {
                    if(clipVauleR > -_DissolorWith)
                    {
                        if(_RAmount != 1)
                        {
                            //插值颜色过度
                            float t = clipVauleR / -_DissolorWith;
                            mainCol = lerp(mainCol, _DissColor, t);
                        }
                        else
                        {
                            discard;
                        }
                    }
                    else
                    {
                        discard;
                    }
                    
                }
                
                return mainCol * _Illuminate;
			}
			ENDCG
		}
	}
}
