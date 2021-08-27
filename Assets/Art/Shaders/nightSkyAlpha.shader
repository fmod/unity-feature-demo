// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:Unlit/Transparent,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:1,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-5432-OUT,alpha-2064-OUT,clip-9177-OUT;n:type:ShaderForge.SFN_OneMinus,id:2596,x:31974,y:33022,varname:node_2596,prsc:2|IN-9336-RGB;n:type:ShaderForge.SFN_ComponentMask,id:9177,x:32165,y:33022,varname:node_9177,prsc:2,cc1:2,cc2:-1,cc3:-1,cc4:-1|IN-2596-OUT;n:type:ShaderForge.SFN_Slider,id:2064,x:32008,y:32905,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_2064,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Tex2d,id:9336,x:31734,y:32804,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_9336,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1d89fe34026dfba45b2064cdbe5548bf,ntxv:0,isnm:False|UVIN-6340-OUT;n:type:ShaderForge.SFN_Slider,id:6203,x:31809,y:32690,ptovrint:False,ptlb:Emission Intensity,ptin:_EmissionIntensity,varname:node_6203,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:0.1,max:5;n:type:ShaderForge.SFN_Multiply,id:5432,x:32212,y:32705,varname:node_5432,prsc:2|A-6203-OUT,B-9336-RGB;n:type:ShaderForge.SFN_TexCoord,id:4348,x:31203,y:32712,varname:node_4348,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6340,x:31496,y:32804,varname:node_6340,prsc:2|A-4348-UVOUT,B-9236-OUT;n:type:ShaderForge.SFN_Slider,id:9236,x:31046,y:32907,ptovrint:False,ptlb:Tiling,ptin:_Tiling,varname:node_9236,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;proporder:9336-2064-6203-9236;pass:END;sub:END;*/

Shader "Cameron/nightSkyAlpha" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 0.5
        _EmissionIntensity ("Emission Intensity", Range(0.1, 5)) = 0.1
        _Tiling ("Tiling", Range(0, 10)) = 1
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
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Front
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float _Opacity;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _EmissionIntensity;
            uniform float _Tiling;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 node_6340 = (i.uv0*_Tiling);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_6340, _MainTex));
                clip((1.0 - _MainTex_var.rgb).b - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (_EmissionIntensity*_MainTex_var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,_Opacity);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Tiling;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 node_6340 = (i.uv0*_Tiling);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_6340, _MainTex));
                clip((1.0 - _MainTex_var.rgb).b - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Unlit/Transparent"
    CustomEditor "ShaderForgeMaterialInspector"
}
