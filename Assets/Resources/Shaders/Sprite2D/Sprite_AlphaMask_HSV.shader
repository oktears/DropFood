// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Sprite2D/Sprite_AlphaMask_HSV"
{
	Properties
	{
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[Toggle] _ClampHoriz ("Clamp Alpha Horizontally", Float) = 0
		[Toggle] _ClampVert ("Clamp Alpha Vertically", Float) = 0
		[Toggle] _UseAlphaChannel ("Use Mask Alpha Channel (not RGB)", Float) = 0
        _MaskRotation ("Mask Rotation in Radians", Float) = 0
		_AlphaTex ("Alpha Mask", 2D) = "white" {}
		_ClampBorder ("Clamping Border", Float) = 0.01
		[KeywordEnum(X, Y, Z)] _Axis ("Alpha Mapping Axis", Float) = 0
		
		//Hue的值范围为0-359. 其他两个为0-1 ,这里我们设置到3，因为乘以3后 都不一定能到超过.
        _Hue ("Hue", Range(0, 359)) = 0
        _Saturation ("Saturation", Range(0, 3.0)) = 1.0
        _Value ("Value", Range(0, 3.0)) = 1.0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha
		
		Pass
		{
		CGPROGRAM
			#include "UnityCG.cginc"
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#pragma multi_compile DUMMY _CLAMPHORIZ_ON
			#pragma multi_compile DUMMY _CLAMPVERT_ON
			#pragma multi_compile DUMMY _USEALPHACHANNEL_ON
			#pragma multi_compile DUMMY _AXIS_X
			#pragma multi_compile DUMMY _AXIS_Y
			#pragma multi_compile DUMMY _AXIS_Z
			#pragma multi_compile DUMMY _SCREEN_SPACE_UI

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _ClampBorder;
			float _MaskRotation;
			
			float _Blend;

			struct appdata_t
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 pos : SV_POSITION;
				fixed4 color : COLOR;
				float2 uvMain : TEXCOORD1;
				float2 uvAlpha : TEXCOORD2;
			};
			
			fixed4 _Color;
			float4 _MainTex_ST;
			float4 _AlphaTex_ST;
			
			half _Hue;
			half _Saturation;
			half _Value;
			
			
			//RGB to HSV
			float3 RGBConvertToHSV(float3 rgb)
			{
				float R = rgb.x,G = rgb.y,B = rgb.z;
				float3 hsv;
				float max1=max(R,max(G,B));
				float min1=min(R,min(G,B));
				if (R == max1) 
				{
					hsv.x = (G-B)/(max1-min1);
				}
				if (G == max1) 
				{
					hsv.x = 2 + (B-R)/(max1-min1);
				}
				if (B == max1) 
				{
					hsv.x = 4 + (R-G)/(max1-min1);
				}
				hsv.x = hsv.x * 60.0;   
				if (hsv.x < 0) 
					hsv.x = hsv.x + 360;
				hsv.z=max1;
				hsv.y=(max1-min1)/max1;
				return hsv;
			}

			//HSV to RGB 
			float3 HSVConvertToRGB(float3 hsv)
			{
				float R,G,B;
				//float3 rgb;
				if( hsv.y == 0 )
				{
					R=G=B=hsv.z;
				}
				else
				{
					hsv.x = hsv.x/60.0; 
					int i = (int)hsv.x;
					float f = hsv.x - (float)i;
					float a = hsv.z * ( 1 - hsv.y );
					float b = hsv.z * ( 1 - hsv.y * f );
					float c = hsv.z * ( 1 - hsv.y * (1 - f ));
					if (i == 0) {
						R = hsv.z; G = c; B = a;
					} else if (i == 1) {
						R = b; G = hsv.z; B = a; 
					} else if (i == 2) {
						R = a; G = hsv.z; B = c; 
					} else if (i == 3) {
						R = a; G = b; B = hsv.z; 
					} else if (i == 4) {
						R = c; G = a; B = hsv.z; 
					} else {
						R = hsv.z; G = a; B = b; 
					}
				}
				return float3(R,G,B);
			}    

			v2f vert (appdata_t v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uvMain = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color * _Color;
				
				o.uvAlpha = float2(0, 0); 
				o.uvAlpha = TRANSFORM_TEX(v.texcoord, _AlphaTex);

				float s = sin(_MaskRotation);
				float c = cos(_MaskRotation);
				float2x2 rotationMatrix = float2x2(c, -s, s, c);
				o.uvAlpha = mul(o.uvAlpha, rotationMatrix);

				o.uvAlpha = o.uvAlpha * _AlphaTex_ST.xy + _AlphaTex_ST.zw;

				#ifdef PIXELSNAP_ON
				o.pos = UnityPixelSnap(o.pos);
				#endif
				
				return o;
			}

			half4 frag (v2f i) : COLOR
			{
				float2 alphaCoords = i.uvAlpha;
				
				#ifdef _CLAMPHORIZ_ON
				alphaCoords.x = clamp(alphaCoords.x, _ClampBorder, 1.0 - _ClampBorder);
				#endif
				
				#ifdef _CLAMPVERT_ON
				alphaCoords.y = clamp(alphaCoords.y, _ClampBorder, 1.0 - _ClampBorder);
				#endif
		
				half4 texcol = tex2D(_MainTex, i.uvMain);
				texcol.a *= i.color.a;
				texcol.rgb = clamp(texcol.rgb, 0, 1) * i.color.rgb;
				
				//#ifdef _USEALPHACHANNEL_ON
				//texcol.a *= tex2D(_AlphaTex, alphaCoords).a;
				//#endif
				
				//#ifndef _USEALPHACHANNEL_ON
				//#endif

				//texcol.rgb *= texcol.a;
				
				float3 colorHSV;    
				colorHSV.rgb = RGBConvertToHSV(texcol.rgb);		//转换为HSV
				colorHSV.r += _Hue;								//调整偏移Hue值
				colorHSV.r = colorHSV.r % 360;					//超过360的值从0开始
				colorHSV.g *= _Saturation;						//调整饱和度
				colorHSV.b *= _Value;                           

				texcol.rgb = HSVConvertToRGB(colorHSV.rgb);		 //将调整后的HSV，转换为RGB颜色

				texcol.a *= tex2D(_AlphaTex, alphaCoords).rgb;
				texcol.rgb *= texcol.a;

				return texcol;
			}
			
		ENDCG
		}
	}
	
	Fallback "Unlit/Texture"
}
