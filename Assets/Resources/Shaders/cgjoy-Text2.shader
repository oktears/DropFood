// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33727,y:32317,varname:node_4013,prsc:2|emission-3683-OUT,clip-5361-OUT;n:type:ShaderForge.SFN_TexCoord,id:1464,x:32186,y:32546,varname:node_1464,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Subtract,id:1687,x:32401,y:32472,varname:node_1687,prsc:2|A-1464-U,B-3892-OUT;n:type:ShaderForge.SFN_Vector1,id:3892,x:32186,y:32503,varname:node_3892,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Subtract,id:7191,x:32401,y:32596,varname:node_7191,prsc:2|A-1464-V,B-3892-OUT;n:type:ShaderForge.SFN_Multiply,id:9380,x:32771,y:32472,varname:node_9380,prsc:2|A-894-OUT,B-894-OUT;n:type:ShaderForge.SFN_Multiply,id:6927,x:32771,y:32608,varname:node_6927,prsc:2|A-9561-OUT,B-9561-OUT;n:type:ShaderForge.SFN_Multiply,id:894,x:32579,y:32472,varname:node_894,prsc:2|A-1555-OUT,B-1687-OUT;n:type:ShaderForge.SFN_Multiply,id:9561,x:32579,y:32608,varname:node_9561,prsc:2|A-7191-OUT,B-1555-OUT;n:type:ShaderForge.SFN_Add,id:1761,x:32948,y:32533,varname:node_1761,prsc:2|A-9380-OUT,B-6927-OUT;n:type:ShaderForge.SFN_OneMinus,id:8180,x:33104,y:32533,varname:node_8180,prsc:2|IN-1761-OUT;n:type:ShaderForge.SFN_Tex2d,id:9427,x:33279,y:32471,ptovrint:False,ptlb:node_9427,ptin:_node_9427,varname:node_9427,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:6dca28c27ae88c247870251101958465,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:3177,x:33279,y:32333,varname:node_3177,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3683,x:33530,y:32422,varname:node_3683,prsc:2|A-3177-RGB,B-9427-R;n:type:ShaderForge.SFN_Multiply,id:5361,x:33530,y:32586,varname:node_5361,prsc:2|A-9427-A,B-1597-OUT;n:type:ShaderForge.SFN_Tex2d,id:4095,x:32765,y:32943,ptovrint:False,ptlb:node_4095,ptin:_node_4095,varname:node_4095,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fef11ba851372714f8adf0d7c4beae9b,ntxv:0,isnm:False|UVIN-1721-OUT;n:type:ShaderForge.SFN_RemapRange,id:1555,x:32401,y:32723,varname:node_1555,prsc:2,frmn:0,frmx:10,tomn:5,tomx:0|IN-2484-OUT;n:type:ShaderForge.SFN_Multiply,id:9393,x:32981,y:32718,varname:node_9393,prsc:2|A-9861-OUT,B-4095-R;n:type:ShaderForge.SFN_TexCoord,id:9770,x:32427,y:32943,varname:node_9770,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:1721,x:32595,y:32943,varname:node_1721,prsc:2|A-9770-UVOUT,B-9065-OUT;n:type:ShaderForge.SFN_Vector2,id:9065,x:32428,y:33066,varname:node_9065,prsc:2,v1:4,v2:4;n:type:ShaderForge.SFN_Multiply,id:1597,x:33279,y:32627,varname:node_1597,prsc:2|A-8180-OUT,B-9393-OUT;n:type:ShaderForge.SFN_Multiply,id:2484,x:32186,y:32776,varname:node_2484,prsc:2|A-1360-A,B-9502-OUT;n:type:ShaderForge.SFN_Vector1,id:9502,x:31998,y:32804,varname:node_9502,prsc:2,v1:10;n:type:ShaderForge.SFN_VertexColor,id:1360,x:31998,y:32689,varname:node_1360,prsc:2;n:type:ShaderForge.SFN_Relay,id:9861,x:32430,y:32887,varname:node_9861,prsc:2|IN-2484-OUT;proporder:9427-4095;pass:END;sub:END;*/

Shader "Shader Forge/Text2" {
    Properties {
        _node_9427 ("node_9427", 2D) = "white" {}
        _node_4095 ("node_4095", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_9427; uniform float4 _node_9427_ST;
            uniform sampler2D _node_4095; uniform float4 _node_4095_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _node_9427_var = tex2D(_node_9427,TRANSFORM_TEX(i.uv0, _node_9427));
                float node_2484 = (i.vertexColor.a*10.0);
                float node_1555 = (node_2484*-0.5+5.0);
                float node_3892 = 0.5;
                float node_894 = (node_1555*(i.uv0.r-node_3892));
                float node_9561 = ((i.uv0.g-node_3892)*node_1555);
                float node_8180 = (1.0 - ((node_894*node_894)+(node_9561*node_9561)));
                float2 node_1721 = (i.uv0*float2(4,4));
                float4 _node_4095_var = tex2D(_node_4095,TRANSFORM_TEX(node_1721, _node_4095));
                float node_9393 = (node_2484*_node_4095_var.r);
                clip((_node_9427_var.a*(node_8180*node_9393)) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (i.vertexColor.rgb*_node_9427_var.r);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_9427; uniform float4 _node_9427_ST;
            uniform sampler2D _node_4095; uniform float4 _node_4095_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _node_9427_var = tex2D(_node_9427,TRANSFORM_TEX(i.uv0, _node_9427));
                float node_2484 = (i.vertexColor.a*10.0);
                float node_1555 = (node_2484*-0.5+5.0);
                float node_3892 = 0.5;
                float node_894 = (node_1555*(i.uv0.r-node_3892));
                float node_9561 = ((i.uv0.g-node_3892)*node_1555);
                float node_8180 = (1.0 - ((node_894*node_894)+(node_9561*node_9561)));
                float2 node_1721 = (i.uv0*float2(4,4));
                float4 _node_4095_var = tex2D(_node_4095,TRANSFORM_TEX(node_1721, _node_4095));
                float node_9393 = (node_2484*_node_4095_var.r);
                clip((_node_9427_var.a*(node_8180*node_9393)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
