// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Chengzi/Sprite2D/Sprite_UV_Scroll_Tex_Gradual"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0

		_MainTex2 ("Sprite Texture2", 2D) = "white" {}
		_XSpeed ("XSpeed", Range (-5, 5)) = 0
		_YSpeed ("YSpeed", Range (-5, 5)) = 0
		_MixValue("MixValue", Range(0, 1)) = 0
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


			sampler2D _MainTex;
			sampler2D _AlphaTex;
			sampler2D _MainTex2;

			float _MixValue;
			fixed _XSpeed;
			fixed _YSpeed;

			float4 _MainTex_ST;
			float4 _MainTex2_ST;

			fixed4 _Color;

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
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

				fixed xScrollValue = _XSpeed * _Time.y;  
            	fixed yScrollValue = _YSpeed * _Time.y;  
            	IN.texcoord.x += xScrollValue;
            	IN.texcoord.y += yScrollValue;
				IN.texcoord1.x += xScrollValue;
				IN.texcoord1.y += yScrollValue;

				OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);
				OUT.texcoord1 = TRANSFORM_TEX(IN.texcoord1, _MainTex2);

				OUT.color = IN.color * _Color * _RendererColor;

				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
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

				//fixed xScrollValue = _XSpeed * _Time.y;
				//fixed yScrollValue = _YSpeed * _Time.y;
				//IN.texcoord.x += xScrollValue;
				//IN.texcoord.y += yScrollValue;

				//if (IN.texcoord.x > 1) {
				//	IN.texcoord.x -= 1;
				//} else if (IN.texcoord.x < 0) {
				//	IN.texcoord.x += 1;
				//}

				//if (IN.texcoord.y > 1) {
				//	IN.texcoord.y -= 1;
				//} else if (IN.texcoord.y < 0) {
				//	IN.texcoord.y += 1;
				//}

				//if (IN.texcoord1.x > 1) {
				//	IN.texcoord1.x -= 1;
				//} else if (IN.texcoord1.x < 0) {
				//	IN.texcoord1.x += 1;
				//}

				//if (IN.texcoord1.y > 1) {
				//	IN.texcoord1.y -= 1;
				//} else if (IN.texcoord1.y < 0) {
				//	IN.texcoord1.y += 1;
				//}

				fixed4 mainTexCol = tex2D (_MainTex, IN.texcoord);
				fixed4 mainTexCol2 = tex2D(_MainTex2, IN.texcoord1);
				mainTexCol = mainTexCol *  (1 - _MixValue);
				mainTexCol2 = mainTexCol2 * _MixValue;
				fixed4 c = (mainTexCol + mainTexCol2) * IN.color;
				c.rgb *= c.a;
				return c;
			}

        ENDCG

        }
    }
}
