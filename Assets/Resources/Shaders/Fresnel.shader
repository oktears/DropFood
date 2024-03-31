// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.16 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.16;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:33186,y:32447,varname:node_3138,prsc:2|emission-7812-OUT,alpha-903-OUT;n:type:ShaderForge.SFN_Fresnel,id:8644,x:31677,y:32239,varname:node_8644,prsc:2;n:type:ShaderForge.SFN_Slider,id:640,x:31756,y:32137,ptovrint:False,ptlb:Power,ptin:_Power,varname:node_640,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.682651,max:10;n:type:ShaderForge.SFN_Multiply,id:5001,x:32112,y:32237,varname:node_5001,prsc:2|A-640-OUT,B-8512-OUT;n:type:ShaderForge.SFN_Slider,id:8521,x:31560,y:32432,ptovrint:False,ptlb:fresnel,ptin:_fresnel,varname:_node_640_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4748535,max:10;n:type:ShaderForge.SFN_Power,id:8512,x:31913,y:32304,varname:node_8512,prsc:2|VAL-8644-OUT,EXP-8521-OUT;n:type:ShaderForge.SFN_Tex2d,id:9685,x:31882,y:32758,ptovrint:False,ptlb:Tex_Main,ptin:_Tex_Main,varname:node_9685,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7b673f9d2b13481489e604133af7f36a,ntxv:0,isnm:False|UVIN-817-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2189,x:32647,y:32404,varname:node_2189,prsc:2|A-5633-OUT,B-1236-OUT;n:type:ShaderForge.SFN_Power,id:8498,x:32487,y:32989,varname:node_8498,prsc:2|VAL-6689-OUT,EXP-6689-OUT;n:type:ShaderForge.SFN_Multiply,id:1236,x:32522,y:32614,varname:node_1236,prsc:2|A-9685-RGB,B-8498-OUT;n:type:ShaderForge.SFN_Vector1,id:6689,x:32214,y:32991,varname:node_6689,prsc:2,v1:2;n:type:ShaderForge.SFN_Panner,id:3472,x:31307,y:32390,varname:node_3472,prsc:2,spu:0.3,spv:0.3;n:type:ShaderForge.SFN_Panner,id:817,x:31679,y:32683,varname:node_817,prsc:2,spu:0.3,spv:0.3|UVIN-3472-UVOUT,DIST-8041-R;n:type:ShaderForge.SFN_Tex2d,id:8041,x:31430,y:32922,ptovrint:False,ptlb:Tex_Noise,ptin:_Tex_Noise,varname:_node_9685_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7b673f9d2b13481489e604133af7f36a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5528,x:32840,y:32438,varname:node_5528,prsc:2|A-2189-OUT,B-5635-RGB;n:type:ShaderForge.SFN_Color,id:5635,x:32691,y:32638,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5635,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5574,x:32890,y:32705,varname:node_5574,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7812,x:33013,y:32478,varname:node_7812,prsc:2|A-5528-OUT,B-5574-RGB;n:type:ShaderForge.SFN_Multiply,id:903,x:32941,y:32957,varname:node_903,prsc:2|A-5574-A,B-9685-B;n:type:ShaderForge.SFN_SwitchProperty,id:5633,x:32457,y:32356,ptovrint:False,ptlb:FresnelSwitch,ptin:_FresnelSwitch,varname:node_5633,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-5489-OUT,B-5001-OUT;n:type:ShaderForge.SFN_Vector1,id:5489,x:32308,y:32193,varname:node_5489,prsc:2,v1:1;proporder:5633-640-8521-9685-8041-5635;pass:END;sub:END;*/

Shader "Chengzi/Fresnel" {
    Properties {
        [MaterialToggle] _FresnelSwitch ("FresnelSwitch", Float ) = 1
        _Power ("Power", Range(0, 10)) = 1.682651
        _fresnel ("fresnel", Range(0, 10)) = 0.4748535
        _Tex_Main ("Tex_Main", 2D) = "white" {}
        _Tex_Noise ("Tex_Noise", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _Power;
            uniform float _fresnel;
            uniform sampler2D _Tex_Main; uniform float4 _Tex_Main_ST;
            uniform sampler2D _Tex_Noise; uniform float4 _Tex_Noise_ST;
            uniform float4 _Color;
            uniform fixed _FresnelSwitch;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _Tex_Noise_var = tex2D(_Tex_Noise,TRANSFORM_TEX(i.uv0, _Tex_Noise));
                float4 node_9140 = _Time + _TimeEditor;
                float2 node_817 = ((i.uv0+node_9140.g*float2(0.3,0.3))+_Tex_Noise_var.r*float2(0.3,0.3));
                float4 _Tex_Main_var = tex2D(_Tex_Main,TRANSFORM_TEX(node_817, _Tex_Main));
                float node_6689 = 2.0;
                float3 emissive = (((lerp( 1.0, (_Power*pow((1.0-max(0,dot(normalDirection, viewDirection))),_fresnel)), _FresnelSwitch )*(_Tex_Main_var.rgb*pow(node_6689,node_6689)))*_Color.rgb)*i.vertexColor.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,(i.vertexColor.a*_Tex_Main_var.b));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
