// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "LC/CameraShockWave2" {
	Properties {
		_MainTex ("", 2D) = "white" {}
		_Ratio ("", float) = 1

		_Shock0("", Vector) = (0.5, 0.5, 1, 0)	// x, y, distance, elapsed
		_Shock1("", Vector) = (0.5, 0.5, 1, 0)	// x, y, distance, elapsed
		_Shock2("", Vector) = (0.5, 0.5, 1, 0)	// x, y, distance, elapsed
		_Shock3("", Vector) = (0.5, 0.5, 1, 0)	// x, y, distance, elapsed

		_ShockSpeed("", float ) = 1
		_WaveWidth("", float) = 0.02
		_WaveIntensity("", float) = 0.03
		_DistanceEffect("", float) = 0.2
		_WaveDecayFactor("", float) = 20

		_WaveTintColor("", Color) = (1, 1, 1, 0)
	}
	
		SubShader {
			Pass {
				
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				float _Ratio;
				float4 _Shock0;
				float4 _Shock1;
				float4 _Shock2;
				float4 _Shock3;

				float _ShockSpeed;
				float _WaveWidth;
				float _WaveIntensity;
				float _DistanceEffect;
				float _WaveDecayFactor;
				float4 _WaveTintColor;

				struct v2f {
					float4 pos : POSITION;
					float2 uv : TEXCOORD0;
				};

				v2f vert( appdata_base v ) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);;
					return o;
				}


				float3 shockeffect(float2 iuv, float3 effect, float4 shock) {
					
					float z = _DistanceEffect * shock.z;
					float t = shock.w * _ShockSpeed;

					float decay = 1 / (t * t * _WaveDecayFactor + 1);

					float2 offset = (iuv - shock.xy);
					offset.y /= _Ratio;
					float d = length(offset);
					
					float r = t * z;
					float hw = _WaveWidth * z;

					float3 effectNew = effect;

					if (d > r - hw && d < r + hw) {
						float x = (d - r) / hw * 3.14;
						float y = cos(x) * 0.5 + 0.5;
						y *= _WaveIntensity * decay;
						effectNew.z = y;
						effectNew.xy = lerp(effect.xy, shock.xy, y);
					}

					return effectNew;
				}

				half4 frag(v2f i) : COLOR {
				
					float2 uv = i.uv;
					float3 e0 = float3(i.uv, 0);
					float3 e1 = shockeffect(i.uv, e0, _Shock0);
					float3 e2 = shockeffect(i.uv, e1, _Shock1);
					float3 e3 = shockeffect(i.uv, e2, _Shock2);
					float3 e4 = shockeffect(i.uv, e3, _Shock3);

					half4 col = tex2D(_MainTex, e4.xy);

					col += e4.z * half4(_WaveTintColor.xyz, 0.0);
					
					return col;
				}
				ENDCG
			}
		}
	Fallback Off
}