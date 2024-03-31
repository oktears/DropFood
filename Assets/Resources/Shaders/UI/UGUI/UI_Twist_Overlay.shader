// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/UI/UGUI/Twist_Overlay【热扭曲_颜色覆盖】"
{
	Properties
	{
	    _MainTex ("Sprite Texture", 2D) = "white" {}
	    _OperateTex ("Operate Texture", 2D) = "white" {}
		_TwistedTex("Twisted Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_MainColor("Wave Color", Color) = (1,1,1,1)
		_TwistingSpeed("Twisting Speed", Float) = 1.0
		_TwistingIntensity("Twisting Intensity", Float) = 0.2
		_LightDirection("Light Direction", Vector) = (0.577, 0.577, 0.577, 0.0)
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15
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
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend DstColor One

		ColorMask [_ColorMask]

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			uniform sampler2D _OperateTex;
			uniform sampler2D _TwistedTex;
			uniform sampler2D _MainTex;
			uniform float4 _MainColor;
			uniform float _TwistingSpeed;
			uniform float _TwistingIntensity;
			uniform float4 _LightDirection;
			
			fixed4 _OperateTex_ST;
			fixed4 _TwistedTex_ST;

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;

			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _OperateTex);
				OUT.texcoord1 = TRANSFORM_TEX(IN.texcoord, _TwistedTex);

#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
#endif
				OUT.color = IN.color * _Color;

				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				
	         	float twisted = tex2D(_TwistedTex, IN.texcoord + (_Time * _TwistingSpeed).xx).x * _TwistingIntensity;
	         	
	         	float4 nn = tex2D(_OperateTex, IN.texcoord1 + twisted.xx);
	            
	            float3 ll = normalize(_LightDirection.xyz);
	            
	            float dd = max(dot(float3(nn.x, nn.x, nn.x), ll), 0.0);
	            
	            float4 mask = tex2D(_MainTex, IN.texcoord);
	            
	            float4 color = nn * _MainColor * dd * mask;
	            color.a = nn.a;
	            
				//clip (color.a - 0.01);

				return color;
			}
		ENDCG
		}
	}
}
