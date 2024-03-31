// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Chengzi/Sprite2D/Sprite_HSV"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0

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
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"

			//以下内容修改引用UnitySprites.gc里的内容
			#ifdef UNITY_INSTANCING_ENABLED

				UNITY_INSTANCING_BUFFER_START(PerDrawSprite)
					// SpriteRenderer.Color while Non-Batched/Instanced.
					UNITY_DEFINE_INSTANCED_PROP(fixed4, unity_SpriteRendererColorArray)
					// this could be smaller but that's how bit each entry is regardless of type
					UNITY_DEFINE_INSTANCED_PROP(fixed2, unity_SpriteFlipArray)
				UNITY_INSTANCING_BUFFER_END(PerDrawSprite)

				#define _RendererColor  UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteRendererColorArray)
				#define _Flip           UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteFlipArray)

			#endif // instancing

			CBUFFER_START(UnityPerDrawSprite)
			#ifndef UNITY_INSTANCING_ENABLED
				fixed4 _RendererColor;
				fixed2 _Flip;
			#endif
				float _EnableExternalAlpha;
			CBUFFER_END

			// Material Color.
			fixed4 _Color;
			half _Hue;
			half _Saturation;
			half _Value;		

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			v2f SpriteVert(appdata_t IN)
			{
				v2f OUT;

				UNITY_SETUP_INSTANCE_ID (IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

			#ifdef UNITY_INSTANCING_ENABLED
				IN.vertex.xy *= _Flip;
			#endif

				OUT.vertex = UnityObjectToClipPos(IN.vertex);

				OUT.texcoord = IN.texcoord;

				OUT.color = IN.color * _Color * _RendererColor;

				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;


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


			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

			#if ETC1_EXTERNAL_ALPHA
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
			#endif
 
				return color;
			}

			fixed4 SpriteFrag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				c.rgb *= c.a;
				float3 colorHSV;    
				colorHSV.rgb = RGBConvertToHSV(c.rgb);  //转换为HSV
				colorHSV.r += _Hue;						//调整偏移Hue值
				colorHSV.r = colorHSV.r % 360;			//超过360的值从0开始

				colorHSV.g *= _Saturation;			    //调整饱和度
				colorHSV.b *= _Value;                           

				c.rgb = HSVConvertToRGB(colorHSV.rgb);   //将调整后的HSV，转换为RGB颜色

				return c;
			}

        ENDCG

        }
    }
}
