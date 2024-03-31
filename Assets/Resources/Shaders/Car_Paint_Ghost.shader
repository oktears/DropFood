// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chengzi/Car/Car_Paint_Body_Ghost" {
    Properties {
        //描边颜色
        _OutlineColor ("OutlineColor", Color) = (0, 0, 0, 1) 
		//描边强度
		_OutlineAmount("OutlineAmount", float) = 0.007
		//远离开始变粗的距离
		_FarDistance("FarDistance", float) = 1
		//摄像机远离之后描边强度系数
		_FarAmount("FarAmount", float) = 0.1
		//摄像机远离之后描边强度系数
		_DiscardX("DiscardX", float) = 0.1
		_DiscardY("DiscardY", float) = 0.1

	}
 
	SubShader {
        Tags {
            "Queue" = "Transparent"  "IGNOREPROJECTOR" = "true" 
		}  

		LOD 100			

		Pass {
            Name "OUTLINE"

			Tags {
                "LightMode" = "Always" 
			}

			Cull Front
			ZWrite Off
			ZTEST Less
			ColorMask RGB

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            }


;
            struct v2f {
                float4 pos : POSITION;
                float4 color : COLOR;
            }

			 
;
            uniform float _OutlineAmount;
            uniform float4 _OutlineColor;
            uniform float _FarAmount;
            uniform float _FarDistance;
            uniform float _DiscardX;
            uniform float _DiscardY;
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float3 dir = normalize(v.vertex.xyz);
                float3 dir2 = v.normal;
                float D = dot(dir, dir2);
                D = (D / 1 + 1) / (1 + 1 / 1);
                dir = lerp(dir2, dir, D);
                dir = mul ((float3x3)UNITY_MATRIX_IT_MV, dir);
                float2 offset = TransformViewToProjection(dir.xy);
                offset = normalize(offset);
                //z距离摄像机越远，描边越粗，防止车跑远了看不清楚的情况
				float dis = o.pos.z;
                if (o.pos.z > _FarDistance) {
                    dis = o.pos.z * _FarAmount;
                } else {
                    dis = o.pos.z;
                }

				o.pos.xy += offset * dis * _OutlineAmount;
                o.color = _OutlineColor;
                return o;
            }

			half4 frag(v2f i) :COLOR {

                return i.color;
            }

			ENDCG
		}
	} 
  
	Fallback "Diffuse"
}