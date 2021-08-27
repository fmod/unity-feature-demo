// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:Standard,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:0,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.02,fgrn:36.45,fgrf:49,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32748,y:32685,varname:node_2865,prsc:2|diff-171-OUT,spec-358-OUT,emission-6685-OUT,alpha-9011-OUT,refract-4714-OUT,voffset-9622-OUT;n:type:ShaderForge.SFN_Color,id:6665,x:30921,y:32099,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.01016437,c2:0.3811986,c3:0.6911765,c4:0.5;n:type:ShaderForge.SFN_Tex2d,id:5964,x:30554,y:32707,varname:_BumpMap,prsc:2,tex:077a10adfe7833347b250d448bf41899,ntxv:3,isnm:True|UVIN-4188-OUT,TEX-4128-TEX;n:type:ShaderForge.SFN_Slider,id:358,x:32374,y:32469,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:31366,y:32497,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3279174,max:1;n:type:ShaderForge.SFN_Tex2d,id:2180,x:30668,y:33347,varname:node_2180,prsc:2,tex:b9b0a7505464056409ca5ec7a10c29bc,ntxv:0,isnm:False|UVIN-817-OUT,TEX-9467-TEX;n:type:ShaderForge.SFN_Multiply,id:9622,x:32426,y:33732,varname:node_9622,prsc:2|A-5971-OUT,B-7972-OUT;n:type:ShaderForge.SFN_NormalVector,id:5971,x:32210,y:33581,prsc:2,pt:False;n:type:ShaderForge.SFN_Slider,id:3769,x:31539,y:33689,ptovrint:False,ptlb:Frequency,ptin:_Frequency,varname:node_3769,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.2;n:type:ShaderForge.SFN_TexCoord,id:3932,x:30007,y:32627,varname:node_3932,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Rotator,id:9408,x:30187,y:32707,varname:node_9408,prsc:2|UVIN-3932-UVOUT,SPD-8227-OUT;n:type:ShaderForge.SFN_Rotator,id:807,x:30187,y:32459,varname:node_807,prsc:2|UVIN-3932-UVOUT,SPD-7777-OUT;n:type:ShaderForge.SFN_Multiply,id:1646,x:31336,y:32607,varname:node_1646,prsc:2|A-5643-RGB,B-5964-RGB;n:type:ShaderForge.SFN_Tex2d,id:5643,x:30554,y:32372,varname:node_5643,prsc:2,tex:90ff426733b42774fa4fb464cc68b2c9,ntxv:3,isnm:True|UVIN-2904-OUT,TEX-6220-TEX;n:type:ShaderForge.SFN_Tex2d,id:5100,x:30680,y:33748,varname:node_5100,prsc:2,tex:111d6c7bba262844b9d4f718e8d0198f,ntxv:0,isnm:False|UVIN-9193-OUT,TEX-4943-TEX;n:type:ShaderForge.SFN_Fresnel,id:4329,x:31727,y:32478,varname:node_4329,prsc:2|NRM-3096-OUT,EXP-1813-OUT;n:type:ShaderForge.SFN_NormalVector,id:122,x:33052,y:32561,prsc:2,pt:False;n:type:ShaderForge.SFN_ComponentMask,id:7377,x:33235,y:32561,varname:node_7377,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-3096-OUT;n:type:ShaderForge.SFN_Slider,id:7148,x:33088,y:32760,ptovrint:False,ptlb:Refraction Intensity,ptin:_RefractionIntensity,varname:node_7148,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.33,max:2;n:type:ShaderForge.SFN_Multiply,id:4176,x:33461,y:32605,varname:node_4176,prsc:2|A-7377-OUT,B-7148-OUT;n:type:ShaderForge.SFN_Slider,id:9011,x:32399,y:32947,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_9011,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6373062,max:1;n:type:ShaderForge.SFN_Multiply,id:2904,x:30370,y:32372,varname:node_2904,prsc:2|A-807-UVOUT,B-1373-OUT;n:type:ShaderForge.SFN_Vector1,id:6432,x:30187,y:32602,varname:node_6432,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:4188,x:30367,y:32707,varname:node_4188,prsc:2|A-9408-UVOUT,B-1373-OUT;n:type:ShaderForge.SFN_Normalize,id:3096,x:31518,y:32607,varname:node_3096,prsc:2|IN-1646-OUT;n:type:ShaderForge.SFN_Sin,id:7809,x:32033,y:33750,varname:node_7809,prsc:2|IN-8919-OUT;n:type:ShaderForge.SFN_Multiply,id:1240,x:31393,y:33619,varname:node_1240,prsc:2|A-2180-RGB,B-5100-RGB;n:type:ShaderForge.SFN_Multiply,id:8919,x:31863,y:33750,varname:node_8919,prsc:2|A-3769-OUT,B-1240-OUT;n:type:ShaderForge.SFN_Multiply,id:7972,x:32210,y:33750,varname:node_7972,prsc:2|A-7809-OUT,B-5197-OUT;n:type:ShaderForge.SFN_Set,id:4091,x:33647,y:32605,varname:__Refraction,prsc:2|IN-4176-OUT;n:type:ShaderForge.SFN_Get,id:4714,x:32535,y:33031,varname:node_4714,prsc:2|IN-4091-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5197,x:32033,y:33899,ptovrint:False,ptlb:_scale,ptin:__scale,varname:node_5197,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Negate,id:7777,x:30007,y:32459,varname:node_7777,prsc:2|IN-8227-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8227,x:29770,y:32695,ptovrint:False,ptlb:_normalSpeed,ptin:__normalSpeed,varname:node_8227,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.01;n:type:ShaderForge.SFN_TexCoord,id:4655,x:30047,y:33751,varname:node_4655,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Rotator,id:5788,x:30234,y:33751,varname:node_5788,prsc:2|UVIN-4655-UVOUT,SPD-5938-OUT;n:type:ShaderForge.SFN_Rotator,id:9980,x:30225,y:33437,varname:node_9980,prsc:2|UVIN-4655-UVOUT,SPD-6638-OUT;n:type:ShaderForge.SFN_Multiply,id:817,x:30496,y:33347,varname:node_817,prsc:2|A-9980-UVOUT,B-8682-OUT;n:type:ShaderForge.SFN_Vector1,id:8682,x:30225,y:33645,varname:node_8682,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:9193,x:30508,y:33748,varname:node_9193,prsc:2|A-5788-UVOUT,B-8682-OUT;n:type:ShaderForge.SFN_Negate,id:6638,x:30056,y:33437,varname:node_6638,prsc:2|IN-5938-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5938,x:29862,y:33649,ptovrint:False,ptlb:_heightSpeed,ptin:__heightSpeed,varname:__normalSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.01;n:type:ShaderForge.SFN_RemapRange,id:2857,x:31581,y:33498,varname:node_2857,prsc:2,frmn:0.3,frmx:1,tomn:0,tomx:1|IN-1240-OUT;n:type:ShaderForge.SFN_Multiply,id:2310,x:31853,y:33501,varname:node_2310,prsc:2|A-2857-OUT,B-6628-OUT;n:type:ShaderForge.SFN_Vector1,id:6628,x:31694,y:33588,varname:node_6628,prsc:2,v1:5;n:type:ShaderForge.SFN_Lerp,id:171,x:31109,y:32272,varname:node_171,prsc:2|A-6665-RGB,B-1486-RGB,T-2310-OUT;n:type:ShaderForge.SFN_Color,id:1486,x:30921,y:32272,ptovrint:False,ptlb:Color_copy,ptin:_Color_copy,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3795415,c2:0.7412449,c3:0.9558824,c4:0.5;n:type:ShaderForge.SFN_Slider,id:4227,x:31082,y:32149,ptovrint:False,ptlb:Emission Intensity,ptin:_EmissionIntensity,varname:node_4227,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.5;n:type:ShaderForge.SFN_Multiply,id:6685,x:31487,y:32150,varname:node_6685,prsc:2|A-4227-OUT,B-171-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9467,x:30496,y:33499,ptovrint:False,ptlb:Height,ptin:_Height,varname:node_9467,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b9b0a7505464056409ca5ec7a10c29bc,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:4943,x:30508,y:33908,ptovrint:False,ptlb:Height2,ptin:_Height2,varname:node_4943,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:111d6c7bba262844b9d4f718e8d0198f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:4128,x:30367,y:32860,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_4128,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:077a10adfe7833347b250d448bf41899,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2dAsset,id:6220,x:30367,y:32521,ptovrint:False,ptlb:Normal Map2,ptin:_NormalMap2,varname:node_6220,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:90ff426733b42774fa4fb464cc68b2c9,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:1373,x:29948,y:32983,ptovrint:False,ptlb:node_1373,ptin:_node_1373,varname:node_1373,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:10,max:50;proporder:6665-358-1813-3769-7148-9011-5197-8227-5938-1486-4227-9467-4943-4128-6220-1373;pass:END;sub:END;*/

Shader "Cameron/WaterEffectThunderDome" {
    Properties {
        _Color ("Color", Color) = (0.01016437,0.3811986,0.6911765,0.5)
        _Metallic ("Metallic", Range(0, 1)) = 0.8
        _Gloss ("Gloss", Range(0, 1)) = 0.3279174
        _Frequency ("Frequency", Range(0, 0.2)) = 0
        _RefractionIntensity ("Refraction Intensity", Range(0, 2)) = 1.33
        _Opacity ("Opacity", Range(0, 1)) = 0.6373062
        __scale ("_scale", Float ) = 1
        __normalSpeed ("_normalSpeed", Float ) = 0.01
        __heightSpeed ("_heightSpeed", Float ) = 0.01
        _Color_copy ("Color_copy", Color) = (0.3795415,0.7412449,0.9558824,0.5)
        _EmissionIntensity ("Emission Intensity", Range(0, 0.5)) = 0
        _Height ("Height", 2D) = "white" {}
        _Height2 ("Height2", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalMap2 ("Normal Map2", 2D) = "bump" {}
        _node_1373 ("node_1373", Range(0, 50)) = 10
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend One OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform float _Metallic;
            uniform float _Frequency;
            uniform float _RefractionIntensity;
            uniform float _Opacity;
            uniform float __scale;
            uniform float __normalSpeed;
            uniform float __heightSpeed;
            uniform float4 _Color_copy;
            uniform float _EmissionIntensity;
            uniform sampler2D _Height; uniform float4 _Height_ST;
            uniform sampler2D _Height2; uniform float4 _Height2_ST;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _NormalMap2; uniform float4 _NormalMap2_ST;
            uniform float _node_1373;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 projPos : TEXCOORD7;
                UNITY_FOG_COORDS(8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_5440 = _Time;
                float node_9980_ang = node_5440.g;
                float node_9980_spd = (-1*__heightSpeed);
                float node_9980_cos = cos(node_9980_spd*node_9980_ang);
                float node_9980_sin = sin(node_9980_spd*node_9980_ang);
                float2 node_9980_piv = float2(0.5,0.5);
                float2 node_9980 = (mul(o.uv0-node_9980_piv,float2x2( node_9980_cos, -node_9980_sin, node_9980_sin, node_9980_cos))+node_9980_piv);
                float node_8682 = 1.0;
                float2 node_817 = (node_9980*node_8682);
                float4 node_2180 = tex2Dlod(_Height,float4(TRANSFORM_TEX(node_817, _Height),0.0,0));
                float node_5788_ang = node_5440.g;
                float node_5788_spd = __heightSpeed;
                float node_5788_cos = cos(node_5788_spd*node_5788_ang);
                float node_5788_sin = sin(node_5788_spd*node_5788_ang);
                float2 node_5788_piv = float2(0.5,0.5);
                float2 node_5788 = (mul(o.uv0-node_5788_piv,float2x2( node_5788_cos, -node_5788_sin, node_5788_sin, node_5788_cos))+node_5788_piv);
                float2 node_9193 = (node_5788*node_8682);
                float4 node_5100 = tex2Dlod(_Height2,float4(TRANSFORM_TEX(node_9193, _Height2),0.0,0));
                float3 node_1240 = (node_2180.rgb*node_5100.rgb);
                v.vertex.xyz += (v.normal*(sin((_Frequency*node_1240))*__scale));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 node_5440 = _Time;
                float node_807_ang = node_5440.g;
                float node_807_spd = (-1*__normalSpeed);
                float node_807_cos = cos(node_807_spd*node_807_ang);
                float node_807_sin = sin(node_807_spd*node_807_ang);
                float2 node_807_piv = float2(0.5,0.5);
                float2 node_807 = (mul(i.uv0-node_807_piv,float2x2( node_807_cos, -node_807_sin, node_807_sin, node_807_cos))+node_807_piv);
                float2 node_2904 = (node_807*_node_1373);
                float3 node_5643 = UnpackNormal(tex2D(_NormalMap2,TRANSFORM_TEX(node_2904, _NormalMap2)));
                float node_9408_ang = node_5440.g;
                float node_9408_spd = __normalSpeed;
                float node_9408_cos = cos(node_9408_spd*node_9408_ang);
                float node_9408_sin = sin(node_9408_spd*node_9408_ang);
                float2 node_9408_piv = float2(0.5,0.5);
                float2 node_9408 = (mul(i.uv0-node_9408_piv,float2x2( node_9408_cos, -node_9408_sin, node_9408_sin, node_9408_cos))+node_9408_piv);
                float2 node_4188 = (node_9408*_node_1373);
                float3 _BumpMap = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_4188, _NormalMap)));
                float3 node_3096 = normalize((node_5643.rgb*_BumpMap.rgb));
                float __Refraction = (node_3096.g*_RefractionIntensity);
                float node_4714 = __Refraction;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + float2(node_4714,node_4714);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Metallic,_Metallic,_Metallic);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 indirectSpecular = (gi.indirect.specular)*specularColor;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float node_9980_ang = node_5440.g;
                float node_9980_spd = (-1*__heightSpeed);
                float node_9980_cos = cos(node_9980_spd*node_9980_ang);
                float node_9980_sin = sin(node_9980_spd*node_9980_ang);
                float2 node_9980_piv = float2(0.5,0.5);
                float2 node_9980 = (mul(i.uv0-node_9980_piv,float2x2( node_9980_cos, -node_9980_sin, node_9980_sin, node_9980_cos))+node_9980_piv);
                float node_8682 = 1.0;
                float2 node_817 = (node_9980*node_8682);
                float4 node_2180 = tex2D(_Height,TRANSFORM_TEX(node_817, _Height));
                float node_5788_ang = node_5440.g;
                float node_5788_spd = __heightSpeed;
                float node_5788_cos = cos(node_5788_spd*node_5788_ang);
                float node_5788_sin = sin(node_5788_spd*node_5788_ang);
                float2 node_5788_piv = float2(0.5,0.5);
                float2 node_5788 = (mul(i.uv0-node_5788_piv,float2x2( node_5788_cos, -node_5788_sin, node_5788_sin, node_5788_cos))+node_5788_piv);
                float2 node_9193 = (node_5788*node_8682);
                float4 node_5100 = tex2D(_Height2,TRANSFORM_TEX(node_9193, _Height2));
                float3 node_1240 = (node_2180.rgb*node_5100.rgb);
                float3 node_171 = lerp(_Color.rgb,_Color_copy.rgb,((node_1240*1.428571+-0.4285715)*5.0));
                float3 diffuseColor = node_171;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (_EmissionIntensity*node_171);
/// Final Color:
                float3 finalColor = diffuse * _Opacity + specular + emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,_Opacity),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform float _Metallic;
            uniform float _Frequency;
            uniform float _RefractionIntensity;
            uniform float _Opacity;
            uniform float __scale;
            uniform float __normalSpeed;
            uniform float __heightSpeed;
            uniform float4 _Color_copy;
            uniform float _EmissionIntensity;
            uniform sampler2D _Height; uniform float4 _Height_ST;
            uniform sampler2D _Height2; uniform float4 _Height2_ST;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _NormalMap2; uniform float4 _NormalMap2_ST;
            uniform float _node_1373;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 projPos : TEXCOORD7;
                LIGHTING_COORDS(8,9)
                UNITY_FOG_COORDS(10)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 node_806 = _Time;
                float node_9980_ang = node_806.g;
                float node_9980_spd = (-1*__heightSpeed);
                float node_9980_cos = cos(node_9980_spd*node_9980_ang);
                float node_9980_sin = sin(node_9980_spd*node_9980_ang);
                float2 node_9980_piv = float2(0.5,0.5);
                float2 node_9980 = (mul(o.uv0-node_9980_piv,float2x2( node_9980_cos, -node_9980_sin, node_9980_sin, node_9980_cos))+node_9980_piv);
                float node_8682 = 1.0;
                float2 node_817 = (node_9980*node_8682);
                float4 node_2180 = tex2Dlod(_Height,float4(TRANSFORM_TEX(node_817, _Height),0.0,0));
                float node_5788_ang = node_806.g;
                float node_5788_spd = __heightSpeed;
                float node_5788_cos = cos(node_5788_spd*node_5788_ang);
                float node_5788_sin = sin(node_5788_spd*node_5788_ang);
                float2 node_5788_piv = float2(0.5,0.5);
                float2 node_5788 = (mul(o.uv0-node_5788_piv,float2x2( node_5788_cos, -node_5788_sin, node_5788_sin, node_5788_cos))+node_5788_piv);
                float2 node_9193 = (node_5788*node_8682);
                float4 node_5100 = tex2Dlod(_Height2,float4(TRANSFORM_TEX(node_9193, _Height2),0.0,0));
                float3 node_1240 = (node_2180.rgb*node_5100.rgb);
                v.vertex.xyz += (v.normal*(sin((_Frequency*node_1240))*__scale));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 node_806 = _Time;
                float node_807_ang = node_806.g;
                float node_807_spd = (-1*__normalSpeed);
                float node_807_cos = cos(node_807_spd*node_807_ang);
                float node_807_sin = sin(node_807_spd*node_807_ang);
                float2 node_807_piv = float2(0.5,0.5);
                float2 node_807 = (mul(i.uv0-node_807_piv,float2x2( node_807_cos, -node_807_sin, node_807_sin, node_807_cos))+node_807_piv);
                float2 node_2904 = (node_807*_node_1373);
                float3 node_5643 = UnpackNormal(tex2D(_NormalMap2,TRANSFORM_TEX(node_2904, _NormalMap2)));
                float node_9408_ang = node_806.g;
                float node_9408_spd = __normalSpeed;
                float node_9408_cos = cos(node_9408_spd*node_9408_ang);
                float node_9408_sin = sin(node_9408_spd*node_9408_ang);
                float2 node_9408_piv = float2(0.5,0.5);
                float2 node_9408 = (mul(i.uv0-node_9408_piv,float2x2( node_9408_cos, -node_9408_sin, node_9408_sin, node_9408_cos))+node_9408_piv);
                float2 node_4188 = (node_9408*_node_1373);
                float3 _BumpMap = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_4188, _NormalMap)));
                float3 node_3096 = normalize((node_5643.rgb*_BumpMap.rgb));
                float __Refraction = (node_3096.g*_RefractionIntensity);
                float node_4714 = __Refraction;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + float2(node_4714,node_4714);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Metallic,_Metallic,_Metallic);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float node_9980_ang = node_806.g;
                float node_9980_spd = (-1*__heightSpeed);
                float node_9980_cos = cos(node_9980_spd*node_9980_ang);
                float node_9980_sin = sin(node_9980_spd*node_9980_ang);
                float2 node_9980_piv = float2(0.5,0.5);
                float2 node_9980 = (mul(i.uv0-node_9980_piv,float2x2( node_9980_cos, -node_9980_sin, node_9980_sin, node_9980_cos))+node_9980_piv);
                float node_8682 = 1.0;
                float2 node_817 = (node_9980*node_8682);
                float4 node_2180 = tex2D(_Height,TRANSFORM_TEX(node_817, _Height));
                float node_5788_ang = node_806.g;
                float node_5788_spd = __heightSpeed;
                float node_5788_cos = cos(node_5788_spd*node_5788_ang);
                float node_5788_sin = sin(node_5788_spd*node_5788_ang);
                float2 node_5788_piv = float2(0.5,0.5);
                float2 node_5788 = (mul(i.uv0-node_5788_piv,float2x2( node_5788_cos, -node_5788_sin, node_5788_sin, node_5788_cos))+node_5788_piv);
                float2 node_9193 = (node_5788*node_8682);
                float4 node_5100 = tex2D(_Height2,TRANSFORM_TEX(node_9193, _Height2));
                float3 node_1240 = (node_2180.rgb*node_5100.rgb);
                float3 node_171 = lerp(_Color.rgb,_Color_copy.rgb,((node_1240*1.428571+-0.4285715)*5.0));
                float3 diffuseColor = node_171;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse * _Opacity + specular;
                fixed4 finalRGBA = fixed4(finalColor,0);
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float _Frequency;
            uniform float __scale;
            uniform float __heightSpeed;
            uniform sampler2D _Height; uniform float4 _Height_ST;
            uniform sampler2D _Height2; uniform float4 _Height2_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                float2 uv2 : TEXCOORD3;
                float4 posWorld : TEXCOORD4;
                float3 normalDir : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_1967 = _Time;
                float node_9980_ang = node_1967.g;
                float node_9980_spd = (-1*__heightSpeed);
                float node_9980_cos = cos(node_9980_spd*node_9980_ang);
                float node_9980_sin = sin(node_9980_spd*node_9980_ang);
                float2 node_9980_piv = float2(0.5,0.5);
                float2 node_9980 = (mul(o.uv0-node_9980_piv,float2x2( node_9980_cos, -node_9980_sin, node_9980_sin, node_9980_cos))+node_9980_piv);
                float node_8682 = 1.0;
                float2 node_817 = (node_9980*node_8682);
                float4 node_2180 = tex2Dlod(_Height,float4(TRANSFORM_TEX(node_817, _Height),0.0,0));
                float node_5788_ang = node_1967.g;
                float node_5788_spd = __heightSpeed;
                float node_5788_cos = cos(node_5788_spd*node_5788_ang);
                float node_5788_sin = sin(node_5788_spd*node_5788_ang);
                float2 node_5788_piv = float2(0.5,0.5);
                float2 node_5788 = (mul(o.uv0-node_5788_piv,float2x2( node_5788_cos, -node_5788_sin, node_5788_sin, node_5788_cos))+node_5788_piv);
                float2 node_9193 = (node_5788*node_8682);
                float4 node_5100 = tex2Dlod(_Height2,float4(TRANSFORM_TEX(node_9193, _Height2),0.0,0));
                float3 node_1240 = (node_2180.rgb*node_5100.rgb);
                v.vertex.xyz += (v.normal*(sin((_Frequency*node_1240))*__scale));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _Metallic;
            uniform float _Frequency;
            uniform float __scale;
            uniform float __heightSpeed;
            uniform float4 _Color_copy;
            uniform float _EmissionIntensity;
            uniform sampler2D _Height; uniform float4 _Height_ST;
            uniform sampler2D _Height2; uniform float4 _Height2_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_8079 = _Time;
                float node_9980_ang = node_8079.g;
                float node_9980_spd = (-1*__heightSpeed);
                float node_9980_cos = cos(node_9980_spd*node_9980_ang);
                float node_9980_sin = sin(node_9980_spd*node_9980_ang);
                float2 node_9980_piv = float2(0.5,0.5);
                float2 node_9980 = (mul(o.uv0-node_9980_piv,float2x2( node_9980_cos, -node_9980_sin, node_9980_sin, node_9980_cos))+node_9980_piv);
                float node_8682 = 1.0;
                float2 node_817 = (node_9980*node_8682);
                float4 node_2180 = tex2Dlod(_Height,float4(TRANSFORM_TEX(node_817, _Height),0.0,0));
                float node_5788_ang = node_8079.g;
                float node_5788_spd = __heightSpeed;
                float node_5788_cos = cos(node_5788_spd*node_5788_ang);
                float node_5788_sin = sin(node_5788_spd*node_5788_ang);
                float2 node_5788_piv = float2(0.5,0.5);
                float2 node_5788 = (mul(o.uv0-node_5788_piv,float2x2( node_5788_cos, -node_5788_sin, node_5788_sin, node_5788_cos))+node_5788_piv);
                float2 node_9193 = (node_5788*node_8682);
                float4 node_5100 = tex2Dlod(_Height2,float4(TRANSFORM_TEX(node_9193, _Height2),0.0,0));
                float3 node_1240 = (node_2180.rgb*node_5100.rgb);
                v.vertex.xyz += (v.normal*(sin((_Frequency*node_1240))*__scale));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_8079 = _Time;
                float node_9980_ang = node_8079.g;
                float node_9980_spd = (-1*__heightSpeed);
                float node_9980_cos = cos(node_9980_spd*node_9980_ang);
                float node_9980_sin = sin(node_9980_spd*node_9980_ang);
                float2 node_9980_piv = float2(0.5,0.5);
                float2 node_9980 = (mul(i.uv0-node_9980_piv,float2x2( node_9980_cos, -node_9980_sin, node_9980_sin, node_9980_cos))+node_9980_piv);
                float node_8682 = 1.0;
                float2 node_817 = (node_9980*node_8682);
                float4 node_2180 = tex2D(_Height,TRANSFORM_TEX(node_817, _Height));
                float node_5788_ang = node_8079.g;
                float node_5788_spd = __heightSpeed;
                float node_5788_cos = cos(node_5788_spd*node_5788_ang);
                float node_5788_sin = sin(node_5788_spd*node_5788_ang);
                float2 node_5788_piv = float2(0.5,0.5);
                float2 node_5788 = (mul(i.uv0-node_5788_piv,float2x2( node_5788_cos, -node_5788_sin, node_5788_sin, node_5788_cos))+node_5788_piv);
                float2 node_9193 = (node_5788*node_8682);
                float4 node_5100 = tex2D(_Height2,TRANSFORM_TEX(node_9193, _Height2));
                float3 node_1240 = (node_2180.rgb*node_5100.rgb);
                float3 node_171 = lerp(_Color.rgb,_Color_copy.rgb,((node_1240*1.428571+-0.4285715)*5.0));
                o.Emission = (_EmissionIntensity*node_171);
                
                float3 diffColor = node_171;
                float3 specColor = float3(_Metallic,_Metallic,_Metallic);
                o.Albedo = diffColor + specColor * 0.125; // No gloss connected. Assume it's 0.5
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Standard"
    CustomEditor "ShaderForgeMaterialInspector"
}
