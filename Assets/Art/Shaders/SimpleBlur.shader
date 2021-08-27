// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:Standard,iptp:0,cusa:False,bamd:0,cgin:,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33672,y:33426,varname:node_9361,prsc:2|custl-8352-OUT;n:type:ShaderForge.SFN_ScreenPos,id:4200,x:33397,y:33024,varname:node_4200,prsc:2,sctp:2;n:type:ShaderForge.SFN_Set,id:4870,x:33561,y:33024,varname:__screenPos,prsc:2|IN-4200-UVOUT;n:type:ShaderForge.SFN_Slider,id:6196,x:33318,y:33209,ptovrint:False,ptlb:Offset,ptin:_Offset,varname:node_6196,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.01880342,max:0.05;n:type:ShaderForge.SFN_Get,id:1547,x:31932,y:33978,varname:node_1547,prsc:2|IN-6644-OUT;n:type:ShaderForge.SFN_Set,id:9867,x:33640,y:33209,varname:_offset,prsc:2|IN-6196-OUT;n:type:ShaderForge.SFN_Get,id:3153,x:31932,y:34036,varname:node_3153,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_Add,id:5210,x:32187,y:34000,varname:node_5210,prsc:2|A-1547-OUT,B-3153-OUT;n:type:ShaderForge.SFN_Set,id:6644,x:33561,y:33070,varname:__screenPosU,prsc:2|IN-4200-U;n:type:ShaderForge.SFN_Set,id:6101,x:33561,y:33117,varname:__screenPosV,prsc:2|IN-4200-V;n:type:ShaderForge.SFN_Get,id:7678,x:31955,y:33017,varname:node_7678,prsc:2|IN-6644-OUT;n:type:ShaderForge.SFN_Get,id:1367,x:31955,y:33075,varname:node_1367,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_Add,id:7084,x:32210,y:33039,cmnt:X axis positive,varname:node_7084,prsc:2|A-7678-OUT,B-1367-OUT;n:type:ShaderForge.SFN_Append,id:8766,x:32403,y:33081,varname:node_8766,prsc:2|A-7084-OUT,B-5216-OUT;n:type:ShaderForge.SFN_Get,id:5216,x:32210,y:33175,varname:node_5216,prsc:2|IN-6101-OUT;n:type:ShaderForge.SFN_Get,id:9707,x:31955,y:33258,varname:node_9707,prsc:2|IN-6101-OUT;n:type:ShaderForge.SFN_Get,id:9084,x:31955,y:33316,varname:node_9084,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_Add,id:2750,x:32210,y:33280,cmnt:Y axis positive,varname:node_2750,prsc:2|A-9707-OUT,B-9084-OUT;n:type:ShaderForge.SFN_Append,id:6353,x:32401,y:33318,varname:node_6353,prsc:2|A-4-OUT,B-2750-OUT;n:type:ShaderForge.SFN_Get,id:4,x:32210,y:33416,varname:node_4,prsc:2|IN-6644-OUT;n:type:ShaderForge.SFN_SceneColor,id:6449,x:32609,y:33081,varname:node_6449,prsc:2|UVIN-8766-OUT;n:type:ShaderForge.SFN_SceneColor,id:9349,x:32609,y:33318,varname:node_9349,prsc:2|UVIN-6353-OUT;n:type:ShaderForge.SFN_Add,id:1142,x:32848,y:33277,varname:node_1142,prsc:2|A-2714-OUT,B-6449-RGB,C-9349-RGB,D-1375-RGB,E-4541-RGB;n:type:ShaderForge.SFN_Divide,id:8352,x:33328,y:33635,varname:node_8352,prsc:2|A-4451-OUT,B-3233-OUT;n:type:ShaderForge.SFN_Vector1,id:3233,x:33079,y:33705,varname:node_3233,prsc:2,v1:9;n:type:ShaderForge.SFN_SceneColor,id:75,x:32421,y:32894,varname:node_75,prsc:2|UVIN-8511-OUT;n:type:ShaderForge.SFN_Get,id:8511,x:32215,y:32894,varname:node_8511,prsc:2|IN-4870-OUT;n:type:ShaderForge.SFN_Get,id:8739,x:31955,y:33478,varname:node_8739,prsc:2|IN-6644-OUT;n:type:ShaderForge.SFN_Get,id:189,x:31955,y:33532,varname:node_189,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_Append,id:3842,x:32403,y:33538,varname:node_3842,prsc:2|A-6943-OUT,B-2761-OUT;n:type:ShaderForge.SFN_Get,id:2761,x:32210,y:33632,varname:node_2761,prsc:2|IN-6101-OUT;n:type:ShaderForge.SFN_Get,id:1285,x:31955,y:33715,varname:node_1285,prsc:2|IN-6101-OUT;n:type:ShaderForge.SFN_Get,id:6259,x:31955,y:33773,varname:node_6259,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_Append,id:1,x:32401,y:33775,varname:node_1,prsc:2|A-8053-OUT,B-5321-OUT;n:type:ShaderForge.SFN_Get,id:8053,x:32210,y:33873,varname:node_8053,prsc:2|IN-6644-OUT;n:type:ShaderForge.SFN_SceneColor,id:1375,x:32609,y:33538,varname:node_1375,prsc:2|UVIN-3842-OUT;n:type:ShaderForge.SFN_SceneColor,id:4541,x:32609,y:33775,varname:node_4541,prsc:2|UVIN-1-OUT;n:type:ShaderForge.SFN_Subtract,id:6943,x:32210,y:33496,cmnt:X axis negative,varname:node_6943,prsc:2|A-8739-OUT,B-189-OUT;n:type:ShaderForge.SFN_Subtract,id:5321,x:32210,y:33737,cmnt:Y axis negative,varname:node_5321,prsc:2|A-1285-OUT,B-6259-OUT;n:type:ShaderForge.SFN_Color,id:5799,x:32421,y:32734,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_5799,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.4852941,c3:0.4852941,c4:1;n:type:ShaderForge.SFN_Add,id:2714,x:32609,y:32840,varname:node_2714,prsc:2|A-5799-RGB,B-75-RGB;n:type:ShaderForge.SFN_Append,id:4736,x:32410,y:34084,cmnt:pos x pos y,varname:node_4736,prsc:2|A-5210-OUT,B-7821-OUT;n:type:ShaderForge.SFN_Get,id:6317,x:31932,y:34134,varname:node_6317,prsc:2|IN-6101-OUT;n:type:ShaderForge.SFN_Get,id:7346,x:31932,y:34192,varname:node_7346,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_Add,id:7821,x:32187,y:34156,varname:node_7821,prsc:2|A-6317-OUT,B-7346-OUT;n:type:ShaderForge.SFN_SceneColor,id:1466,x:32599,y:34084,varname:node_1466,prsc:2|UVIN-4736-OUT;n:type:ShaderForge.SFN_Get,id:2411,x:31932,y:34317,varname:node_2411,prsc:2|IN-6644-OUT;n:type:ShaderForge.SFN_Get,id:2962,x:31932,y:34375,varname:node_2962,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_Add,id:4661,x:32187,y:34339,varname:node_4661,prsc:2|A-2411-OUT,B-2962-OUT;n:type:ShaderForge.SFN_Append,id:5918,x:32410,y:34423,cmnt:pos x neg y,varname:node_5918,prsc:2|A-4661-OUT,B-6652-OUT;n:type:ShaderForge.SFN_Get,id:7300,x:31932,y:34473,varname:node_7300,prsc:2|IN-6101-OUT;n:type:ShaderForge.SFN_Get,id:3476,x:31932,y:34531,varname:node_3476,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_SceneColor,id:8634,x:32600,y:34423,varname:node_8634,prsc:2|UVIN-5918-OUT;n:type:ShaderForge.SFN_Get,id:368,x:31932,y:34661,varname:node_368,prsc:2|IN-6644-OUT;n:type:ShaderForge.SFN_Get,id:9428,x:31932,y:34719,varname:node_9428,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_Append,id:2985,x:32410,y:34767,cmnt:neg x pos y,varname:node_2985,prsc:2|A-7942-OUT,B-8555-OUT;n:type:ShaderForge.SFN_Get,id:9270,x:31932,y:34817,varname:node_9270,prsc:2|IN-6101-OUT;n:type:ShaderForge.SFN_Get,id:2361,x:31932,y:34875,varname:node_2361,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_Add,id:8555,x:32187,y:34839,varname:node_8555,prsc:2|A-9270-OUT,B-2361-OUT;n:type:ShaderForge.SFN_SceneColor,id:8804,x:32604,y:34767,varname:node_8804,prsc:2|UVIN-2985-OUT;n:type:ShaderForge.SFN_Get,id:4367,x:31932,y:34993,varname:node_4367,prsc:2|IN-6644-OUT;n:type:ShaderForge.SFN_Get,id:3843,x:31932,y:35051,varname:node_3843,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_Append,id:8185,x:32410,y:35099,cmnt:neg x neg y,varname:node_8185,prsc:2|A-5390-OUT,B-183-OUT;n:type:ShaderForge.SFN_Get,id:1137,x:31932,y:35149,varname:node_1137,prsc:2|IN-6101-OUT;n:type:ShaderForge.SFN_Get,id:5719,x:31932,y:35207,varname:node_5719,prsc:2|IN-9867-OUT;n:type:ShaderForge.SFN_SceneColor,id:8028,x:32603,y:35099,varname:node_8028,prsc:2|UVIN-8185-OUT;n:type:ShaderForge.SFN_Subtract,id:5390,x:32175,y:35015,varname:node_5390,prsc:2|A-4367-OUT,B-3843-OUT;n:type:ShaderForge.SFN_Subtract,id:183,x:32175,y:35149,varname:node_183,prsc:2|A-1137-OUT,B-5719-OUT;n:type:ShaderForge.SFN_Subtract,id:6652,x:32187,y:34495,varname:node_6652,prsc:2|A-7300-OUT,B-3476-OUT;n:type:ShaderForge.SFN_Subtract,id:7942,x:32187,y:34683,varname:node_7942,prsc:2|A-368-OUT,B-9428-OUT;n:type:ShaderForge.SFN_Add,id:4451,x:32901,y:34070,varname:node_4451,prsc:2|A-1466-RGB,B-8634-RGB,C-8804-RGB,D-8028-RGB;n:type:ShaderForge.SFN_Add,id:2192,x:33079,y:33580,varname:node_2192,prsc:2|A-1142-OUT,B-4451-OUT;proporder:6196-5799;pass:END;sub:END;*/

Shader "Cameron/SimpleBlur" {
    Properties {
        _Offset ("Offset", Range(0, 0.05)) = 0.01880342
        _Tint ("Tint", Color) = (1,0.4852941,0.4852941,1)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ "Refraction" }
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
            uniform sampler2D Refraction;
            uniform float _Offset;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 projPos : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeGrabScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(Refraction, sceneUVs);
////// Lighting:
                float __screenPosU = sceneUVs.r;
                float _offset = _Offset;
                float __screenPosV = sceneUVs.g;
                float3 node_4451 = (tex2D( Refraction, float2((__screenPosU+_offset),(__screenPosV+_offset))).rgb+tex2D( Refraction, float2((__screenPosU+_offset),(__screenPosV-_offset))).rgb+tex2D( Refraction, float2((__screenPosU-_offset),(__screenPosV+_offset))).rgb+tex2D( Refraction, float2((__screenPosU-_offset),(__screenPosV-_offset))).rgb);
                float3 finalColor = (node_4451/9.0);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Standard"
    CustomEditor "ShaderForgeMaterialInspector"
}
