// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Chengzi/UI/Transparent_With_Mask_VertexColor" {
	Properties {
	 _MainTex ("Base (RGB)", 2D) = "White" {}
	 _MaskTex ("Base (RGB)", 2D) = "White" {}
	}
	
	SubShader {

		Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
		
		LOD 100
        Pass {
			Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
					
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

			
			sampler2D _MainTex;
			sampler2D _MaskTex;

			struct appdata_input {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float4 texcoord : TEXCOORD0;
			};
					
			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};
			
			uniform float4 _MainTex_ST;
			
			v2f vert(appdata_input v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color;
				return o; 
			}

            fixed4 frag(v2f i) : COLOR {
                fixed4 texcol = tex2D (_MainTex, i.uv);
                fixed4 maskcol = tex2D (_MaskTex, i.uv);
				fixed4 col = fixed4(1-maskcol.r, 1-maskcol.g, 1-maskcol.b, maskcol.a);
				col *= texcol * i.color;
				return col;
            }

            ENDCG
        }
    }
}
