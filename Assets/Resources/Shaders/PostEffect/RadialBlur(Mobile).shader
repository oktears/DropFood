﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Chengzi/PostEffect/RadialBlur(Mobile)" {
	
	
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}


	CGINCLUDE

		#include "UnityCG.cginc"

		struct appdata_t {
			float4 vertex : POSITION;
			half2 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 vertex : SV_POSITION;
			half2 texcoord : TEXCOORD0;
		};

		sampler2D _MainTex;
		sampler2D _BlurTex;
		uniform float _SampleDist;
		uniform float _SampleStrength;
		
		v2f vert (appdata_t v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.texcoord = v.texcoord;
			return o;
		}
		
		fixed4 fragRadialBlur (v2f i) : COLOR
		{
			fixed2 dir = 0.5-i.texcoord;
			fixed dist = length(dir);
			dir /= dist;
			dir *= _SampleDist;

			fixed4 sum = tex2D(_MainTex, i.texcoord - dir*0.01);
			sum += tex2D(_MainTex, i.texcoord - dir*0.02);
			sum += tex2D(_MainTex, i.texcoord - dir*0.03);
			sum += tex2D(_MainTex, i.texcoord - dir*0.05);
			sum += tex2D(_MainTex, i.texcoord - dir*0.08);
			sum += tex2D(_MainTex, i.texcoord + dir*0.01);
			sum += tex2D(_MainTex, i.texcoord + dir*0.02);
			sum += tex2D(_MainTex, i.texcoord + dir*0.03);
			sum += tex2D(_MainTex, i.texcoord + dir*0.05);
			sum += tex2D(_MainTex, i.texcoord + dir*0.08);
			sum *= 0.1;
			
			return sum;
		}

		fixed4 fragCombine (v2f i) : COLOR
		{
			// fixed2 dir = 0.5-i.texcoord;
			fixed dist = length(0.5-i.texcoord);
			fixed4  col = tex2D(_MainTex, i.texcoord);
			fixed4  blur = tex2D(_BlurTex, i.texcoord);
			col=lerp(col, blur,saturate(_SampleStrength*dist));
			return col;
		}
	ENDCG

	SubShader {
		ZTest Always  ZWrite Off Cull Off Blend Off

		Fog { Mode off } 

		Pass { 

			CGPROGRAM
		
			#pragma vertex vert
			#pragma fragment fragRadialBlur
			#pragma fragmentoption ARB_precision_hint_fastest 
		
			ENDCG	 
		}	

		Pass { 
			CGPROGRAM
		
			#pragma vertex vert
			#pragma fragment fragCombine
			#pragma fragmentoption ARB_precision_hint_fastest 
		
			ENDCG	 
		}				
	}	

	        // Properties {
         //        _MainTex ("Base (RGB)", 2D) = "white" {}
         //        _fSampleDist("SampleDist", Float) = 1 //采样距离
         //        _fSampleStrength("SampleStrength", Float) = 2.2 //采样力度
         //}
         //SubShader {
         //        Pass {               
         //                ZTest Always Cull Off ZWrite Off
         //                Fog { Mode off }  
         //                CGPROGRAM
         //                #pragma vertex vert
         //                #pragma fragment frag
         
         //                #include "UnityCG.cginc"
         
         //                struct appdata_t {
         //                        float4 vertex : POSITION;
         //                        float2 texcoord : TEXCOORD;
         //                };
         
         //                struct v2f {
         //                        float4 vertex : POSITION;
         //                        float2 texcoord : TEXCOORD;
         //                };
                         
         //                float4 _MainTex_ST;
                         
         //                v2f vert (appdata_t v)
         //                {
         //                        v2f o;
         //                        o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
         //                        o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
         //                        return o;
         //                }
         
         //                sampler2D _MainTex;
         //                float _fSampleDist;
         //                float _fSampleStrength;

         //                // some sample positions  
         //                static const float samples[6] =   
         //                {   
         //                   -0.05,  
         //                   -0.03,    
         //                   -0.01,  
         //                   0.01,    
         //                   0.03,  
         //                   0.05,  
         //                }; 
                         
         //                half4 frag (v2f i) : SV_Target
         //                {
                                
         //                   //0.5,0.5屏幕中心
         //                   float2 dir = float2(0.5, 0.5) - i.texcoord;//从采样中心到uv的方向向量
         //                   float2 texcoord = i.texcoord;
         //                      float dist = length(dir);  
         //                   dir = normalize(dir); 
         //                   float4 color = tex2D(_MainTex, texcoord);  

         //                   float4 sum = color;
         //                      //    6次采样
         //                   for (int i = 0; i < 6; ++i)  
         //                   {  
                                                            
         //                          sum += tex2D(_MainTex, texcoord + dir * samples[i] * _fSampleDist);    
         //                   }  

         //                   //求均值
         //                   sum /= 7.0f;  

                           
         //                   //越离采样中心近的地方，越不模糊
         //                   float t = saturate(dist * _fSampleStrength);  

         //                   //插值
         //                   return lerp(color, sum, t);
                            
         //                }
         //                ENDCG 
         //        }
         //} 
         //Fallback off
}
