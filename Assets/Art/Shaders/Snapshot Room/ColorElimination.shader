// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33676,y:32755,varname:node_3138,prsc:2|emission-8022-OUT;n:type:ShaderForge.SFN_Multiply,id:8022,x:33457,y:32844,varname:node_8022,prsc:2|A-2723-RGB,B-1860-OUT;n:type:ShaderForge.SFN_SceneColor,id:2723,x:33231,y:32717,varname:node_2723,prsc:2|UVIN-1807-UVOUT;n:type:ShaderForge.SFN_Power,id:1860,x:33231,y:32844,varname:node_1860,prsc:2|VAL-4368-OUT,EXP-9031-OUT;n:type:ShaderForge.SFN_SceneDepth,id:4653,x:32190,y:32594,varname:node_4653,prsc:2;n:type:ShaderForge.SFN_Depth,id:506,x:32190,y:32725,varname:node_506,prsc:2;n:type:ShaderForge.SFN_Subtract,id:6111,x:32383,y:32662,varname:node_6111,prsc:2|A-4653-OUT,B-506-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8961,x:32383,y:32604,ptovrint:False,ptlb:Density,ptin:_Density,varname:node_8961,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:9551,x:32571,y:32625,varname:node_9551,prsc:2|A-8961-OUT,B-6111-OUT;n:type:ShaderForge.SFN_Vector4Property,id:4243,x:32383,y:32828,ptovrint:False,ptlb:node_4243,ptin:_node_4243,varname:node_4243,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:-0.1;n:type:ShaderForge.SFN_Add,id:7797,x:32586,y:32828,varname:node_7797,prsc:2|A-4243-XYZ,B-4243-W;n:type:ShaderForge.SFN_Add,id:8338,x:32586,y:33010,varname:node_8338,prsc:2|A-1773-RGB,B-1773-A;n:type:ShaderForge.SFN_Vector1,id:3529,x:32586,y:33162,varname:node_3529,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:2271,x:32586,y:33230,varname:node_2271,prsc:2,v1:0;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:1302,x:32852,y:32841,varname:node_1302,prsc:2|IN-9551-OUT,IMIN-7797-OUT,IMAX-8338-OUT,OMIN-3529-OUT,OMAX-2271-OUT;n:type:ShaderForge.SFN_Clamp01,id:4368,x:33024,y:32841,varname:node_4368,prsc:2|IN-1302-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9031,x:33024,y:32992,ptovrint:False,ptlb:Fade Power,ptin:_FadePower,varname:node_9031,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Color,id:1773,x:32111,y:33016,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1773,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ScreenPos,id:1807,x:33007,y:32671,varname:node_1807,prsc:2,sctp:0;proporder:8961-4243-9031-1773;pass:END;sub:END;*/

Shader "Cameron/ColorElimination" {
    Properties {
        _Density ("Density", Float ) = 1
        _node_4243 ("node_4243", Vector) = (0,0,0,-0.1)
        _FadePower ("Fade Power", Float ) = 3
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _CameraDepthTexture;
            uniform float _Density;
            uniform float4 _node_4243;
            uniform float _FadePower;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float4 projPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float3 node_7797 = (_node_4243.rgb+_node_4243.a);
                float node_3529 = 1.0;
                float3 emissive = (tex2D( _GrabTexture, (sceneUVs * 2 - 1).rg).rgb*pow(saturate((node_3529 + ( ((_Density*(max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sceneUVs)) - _ProjectionParams.g)-partZ)) - node_7797) * (0.0 - node_3529) ) / ((_Color.rgb+_Color.a) - node_7797))),_FadePower));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
