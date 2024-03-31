// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Water/Island_Simple_1" {
	Properties{
		[NoScaleOffset] _ControlTex("控制图", 2D) = "white" {}
		_Texture1("材质贴图1（R）", 2D) = "black"
		_Texture2("材质贴图2（G）,水深", 2D) = "black"
		_Texture3("材质贴图3（B）", 2D) = "black"
		_Texture4("材质贴图4（A）", 2D) = "black"

		[NoScaleOffset] _Foam("泡沫贴图", 2D) = "white" {}
		[NoScaleOffset] _FoamGradient("泡沫梯度图 ", 2D) = "white" {}

}


	Subshader{
		Tags{ "RenderType" = "Opaque" "IGNOREPROJECTOR" = "true" }
		Pass{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"



			sampler2D _ControlTex;

			sampler2D _Texture1;
			float4 _Texture1_ST;

			sampler2D _Texture2;
			float4 _Texture2_ST;

			sampler2D _Texture3;
			float4 _Texture3_ST;

			sampler2D _Texture4;
			float4 _Texture4_ST;

			sampler2D _Foam;
			sampler2D _FoamGradient;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float4 mainuv : TEXCOORD0;
				float4 uv12 : TEXCOORD1;
				float4 uv34 : TEXCOORD2;

			};

			v2f vert(appdata_tan v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);

				// scroll bump waves
				float4 temp;
				float4 wpos = mul(unity_ObjectToWorld, v.vertex);

				o.mainuv.xy = v.texcoord;
				o.mainuv.zw = 8*wpos.xz + 5 * float2(_SinTime.w, _SinTime.w);
				o.mainuv.zw *= 0.01;

				o.uv12.xy = TRANSFORM_TEX(v.texcoord, _Texture1);
				o.uv12.zw = TRANSFORM_TEX(v.texcoord, _Texture2);
				o.uv34.xy = TRANSFORM_TEX(v.texcoord, _Texture3);
				o.uv34.zw = TRANSFORM_TEX(v.texcoord, _Texture4);

				return o;
			}


			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 color;
				fixed4 control = tex2D(_ControlTex, i.mainuv.xy);
				fixed4 color1 = tex2D(_Texture1, i.uv12.xy);
				fixed4 color2 = tex2D(_Texture2, i.uv12.zw);
				fixed4 color3 = tex2D(_Texture3, i.uv34.xy);
				fixed4 color4 = tex2D(_Texture4, i.uv34.zw);
				color.rgb = color1.rgb*control.r + color2.rgb*control.g + color3.rgb*control.b + color4.rgb*control.a;
				color.a = 1;

				//if (control.g > 0.1 + _SinTime.w / 50) {
				if (control.g > 0.1) {
					fixed intensityFactor = 1 - control.g; //saturate(1 - control.g);
					fixed x = intensityFactor - _Time.y*0.2 ;
					fixed3 foamGradient = 1 - tex2D(_FoamGradient, float2(x, 0));
					fixed3 foamColor = tex2D(_Foam, i.mainuv.zw).rgb;
					color.rgb = lerp(color.rgb, color.rgb + foamGradient * intensityFactor * foamColor, control.g);
				}


				return color;
			}
			ENDCG

		}
	}

}
