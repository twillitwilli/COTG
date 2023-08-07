// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34020,y:32577,varname:node_3138,prsc:2|custl-2300-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32967,y:32431,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:1689,x:32630,y:32302,ptovrint:False,ptlb:Equirectangular Projection Texture (RGB),ptin:_EquirectangularProjectionTextureRGB,varname:node_1689,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1431-OUT;n:type:ShaderForge.SFN_Tex2d,id:9151,x:32405,y:32600,ptovrint:False,ptlb:Main texture (RGB),ptin:_MainTex,varname:node_9151,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:1700,x:33700,y:33232,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_1700,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Multiply,id:7815,x:32967,y:32593,varname:node_7815,prsc:2|A-2845-OUT,B-7241-RGB,C-4517-OUT;n:type:ShaderForge.SFN_NormalVector,id:8679,x:31864,y:33235,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:2544,x:32057,y:33395,varname:node_2544,prsc:2;n:type:ShaderForge.SFN_Dot,id:8445,x:32057,y:33235,varname:node_8445,prsc:2,dt:0|A-8679-OUT,B-2544-OUT;n:type:ShaderForge.SFN_Append,id:5668,x:32372,y:33235,varname:node_5668,prsc:2|A-6756-OUT,B-9311-OUT;n:type:ShaderForge.SFN_Vector1,id:9311,x:32216,y:33384,varname:node_9311,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Tex2d,id:6405,x:32535,y:33235,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_6405,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dda255c6e7ca25c4da940fccb27f832d,ntxv:0,isnm:False|UVIN-5668-OUT;n:type:ShaderForge.SFN_Desaturate,id:6833,x:32701,y:33235,varname:node_6833,prsc:2|COL-6405-RGB;n:type:ShaderForge.SFN_Add,id:6343,x:32866,y:33235,varname:node_6343,prsc:2|A-6833-OUT,B-6991-OUT;n:type:ShaderForge.SFN_Vector1,id:6991,x:32701,y:33354,varname:node_6991,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Posterize,id:2233,x:33209,y:33235,varname:node_2233,prsc:2|IN-9048-OUT,STPS-4857-OUT;n:type:ShaderForge.SFN_Vector1,id:4857,x:33032,y:33366,varname:node_4857,prsc:2,v1:3;n:type:ShaderForge.SFN_Clamp01,id:9048,x:33032,y:33235,varname:node_9048,prsc:2|IN-6343-OUT;n:type:ShaderForge.SFN_Vector1,id:4517,x:32967,y:32725,varname:node_4517,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp01,id:6756,x:32216,y:33235,varname:node_6756,prsc:2|IN-8445-OUT;n:type:ShaderForge.SFN_Desaturate,id:5160,x:33201,y:32431,varname:node_5160,prsc:2|COL-7815-OUT,DES-3971-OUT;n:type:ShaderForge.SFN_Vector1,id:3971,x:33201,y:32382,varname:node_3971,prsc:2,v1:-0.5;n:type:ShaderForge.SFN_Desaturate,id:9411,x:33404,y:32431,varname:node_9411,prsc:2|COL-5160-OUT,DES-8933-OUT;n:type:ShaderForge.SFN_OneMinus,id:8933,x:33201,y:32593,varname:node_8933,prsc:2|IN-7241-A;n:type:ShaderForge.SFN_LightColor,id:951,x:32686,y:32888,varname:node_951,prsc:2;n:type:ShaderForge.SFN_Code,id:6511,x:32358,y:32839,varname:node_6511,prsc:2,code:cgBlAHQAdQByAG4AIABTAGgAYQBkAGUAUwBIADkAKABoAGEAbABmADQAKABuAG8AcgBtAGEAbAAsACAAMQAuADAAKQApADsACgA=,output:2,fname:Function_node_3693,width:292,height:112,input:2,input_1_label:normal|A-8487-OUT;n:type:ShaderForge.SFN_Multiply,id:9979,x:32860,y:32888,varname:node_9979,prsc:2|A-951-RGB,B-2967-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:2967,x:32535,y:33045,varname:node_2967,prsc:2;n:type:ShaderForge.SFN_Clamp01,id:6092,x:33201,y:32838,varname:node_6092,prsc:2|IN-7052-OUT;n:type:ShaderForge.SFN_Vector3,id:8487,x:32181,y:32839,varname:node_8487,prsc:2,v1:0,v2:1,v3:0;n:type:ShaderForge.SFN_Add,id:7052,x:33029,y:32838,varname:node_7052,prsc:2|A-6511-OUT,B-9979-OUT;n:type:ShaderForge.SFN_ViewVector,id:2089,x:31650,y:32154,varname:node_2089,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:4810,x:31884,y:32154,varname:node_4810,prsc:2,cc1:2,cc2:0,cc3:1,cc4:-1|IN-2089-OUT;n:type:ShaderForge.SFN_Pi,id:2632,x:31917,y:32292,varname:node_2632,prsc:2;n:type:ShaderForge.SFN_Negate,id:3403,x:32055,y:32292,varname:node_3403,prsc:2|IN-2632-OUT;n:type:ShaderForge.SFN_ArcCos,id:1044,x:32055,y:32164,varname:node_1044,prsc:2|IN-4810-B;n:type:ShaderForge.SFN_Divide,id:1808,x:32231,y:32302,varname:node_1808,prsc:2|A-1044-OUT,B-3403-OUT;n:type:ShaderForge.SFN_ArcTan2,id:5315,x:32055,y:32016,varname:node_5315,prsc:2,attp:2|A-4810-R,B-4810-G;n:type:ShaderForge.SFN_Append,id:6711,x:32231,y:32164,varname:node_6711,prsc:2|A-5315-OUT,B-1808-OUT;n:type:ShaderForge.SFN_OneMinus,id:9020,x:32405,y:32164,varname:node_9020,prsc:2|IN-6711-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4903,x:32405,y:32302,varname:node_4903,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9020-OUT;n:type:ShaderForge.SFN_Add,id:2922,x:32701,y:33045,varname:node_2922,prsc:2|A-2967-OUT,B-1700-OUT;n:type:ShaderForge.SFN_Subtract,id:6537,x:32860,y:33045,varname:node_6537,prsc:2|A-2967-OUT,B-3093-OUT;n:type:ShaderForge.SFN_Vector1,id:3093,x:32701,y:33168,varname:node_3093,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:2300,x:33769,y:32834,varname:node_2300,prsc:2|A-9411-OUT,B-1700-OUT,C-6169-OUT;n:type:ShaderForge.SFN_Clamp01,id:6169,x:33536,y:32838,varname:node_6169,prsc:2|IN-5562-OUT;n:type:ShaderForge.SFN_Multiply,id:5562,x:33374,y:32838,varname:node_5562,prsc:2|A-6092-OUT,B-2233-OUT;n:type:ShaderForge.SFN_Multiply,id:2845,x:32796,y:32593,varname:node_2845,prsc:2|A-3107-OUT,B-6612-OUT;n:type:ShaderForge.SFN_Lerp,id:3107,x:32630,y:32469,varname:node_3107,prsc:2|A-7920-OUT,B-1689-RGB,T-8759-OUT;n:type:ShaderForge.SFN_Vector1,id:7920,x:32079,y:32739,varname:node_7920,prsc:2,v1:1;n:type:ShaderForge.SFN_Lerp,id:6612,x:32630,y:32593,varname:node_6612,prsc:2|A-7920-OUT,B-9151-RGB,T-6835-OUT;n:type:ShaderForge.SFN_Clamp01,id:8759,x:32405,y:32459,varname:node_8759,prsc:2|IN-6611-OUT;n:type:ShaderForge.SFN_OneMinus,id:9601,x:31916,y:32596,varname:node_9601,prsc:2|IN-6611-OUT;n:type:ShaderForge.SFN_Add,id:748,x:32079,y:32596,varname:node_748,prsc:2|A-7920-OUT,B-9601-OUT;n:type:ShaderForge.SFN_Clamp01,id:6835,x:32242,y:32596,varname:node_6835,prsc:2|IN-748-OUT;n:type:ShaderForge.SFN_Slider,id:6611,x:32085,y:32526,ptovrint:False,ptlb:Crossfade Overlay,ptin:_CrossfadeOverlay,varname:node_6611,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Append,id:1986,x:32636,y:32015,varname:node_1986,prsc:2|A-4097-OUT,B-3842-OUT;n:type:ShaderForge.SFN_Time,id:47,x:32636,y:32164,varname:node_47,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7401,x:32817,y:32025,varname:node_7401,prsc:2|A-1986-OUT,B-47-T;n:type:ShaderForge.SFN_Add,id:1431,x:32817,y:32164,varname:node_1431,prsc:2|A-7401-OUT,B-4903-OUT;n:type:ShaderForge.SFN_Slider,id:2514,x:32479,y:31799,ptovrint:False,ptlb:Speed X,ptin:_SpeedX,varname:node_2514,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:4097,x:32817,y:31735,varname:node_4097,prsc:2|A-9795-OUT,B-2514-OUT;n:type:ShaderForge.SFN_Vector1,id:9795,x:32636,y:31735,varname:node_9795,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:3842,x:32817,y:31875,varname:node_3842,prsc:2|A-9795-OUT,B-5322-OUT;n:type:ShaderForge.SFN_Slider,id:5322,x:32479,y:31880,ptovrint:False,ptlb:Speed Y,ptin:_SpeedY,varname:node_5322,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;proporder:7241-9151-6611-1689-2514-5322-6405-1700;pass:END;sub:END;*/

Shader "NoeNoe/Projection Overlay Texture (2D Tiled)" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Main texture (RGB)", 2D) = "white" {}
        _CrossfadeOverlay ("Crossfade Overlay", Range(0, 2)) = 1
        _EquirectangularProjectionTextureRGB ("Equirectangular Projection Texture (RGB)", 2D) = "white" {}
        _SpeedX ("Speed X", Range(-1, 1)) = 0
        _SpeedY ("Speed Y", Range(-1, 1)) = 0
        _Ramp ("Ramp", 2D) = "white" {}
        _Intensity ("Intensity", Range(0, 10)) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _EquirectangularProjectionTextureRGB; uniform float4 _EquirectangularProjectionTextureRGB_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Intensity;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            float3 Function_node_3693( float3 normal ){
            return ShadeSH9(half4(normal, 1.0));
            
            }
            
            uniform float _CrossfadeOverlay;
            uniform float _SpeedX;
            uniform float _SpeedY;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_7920 = 1.0;
                float node_9795 = 0.1;
                float4 node_47 = _Time + _TimeEditor;
                float3 node_4810 = viewDirection.brg;
                float2 node_1431 = ((float2((node_9795*_SpeedX),(node_9795*_SpeedY))*node_47.g)+(1.0 - float2(((atan2(node_4810.r,node_4810.g)/6.28318530718)+0.5),(acos(node_4810.b)/(-1*3.141592654)))).rg);
                float4 _EquirectangularProjectionTextureRGB_var = tex2D(_EquirectangularProjectionTextureRGB,TRANSFORM_TEX(node_1431, _EquirectangularProjectionTextureRGB));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_5668 = float2(saturate(dot(i.normalDir,lightDirection)),0.2);
                float4 _Ramp_var = tex2D(_Ramp,TRANSFORM_TEX(node_5668, _Ramp));
                float node_4857 = 3.0;
                float3 finalColor = (lerp(lerp(((lerp(float3(node_7920,node_7920,node_7920),_EquirectangularProjectionTextureRGB_var.rgb,saturate(_CrossfadeOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),_EquirectangularProjectionTextureRGB_var.rgb,saturate(_CrossfadeOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),dot(lerp(((lerp(float3(node_7920,node_7920,node_7920),_EquirectangularProjectionTextureRGB_var.rgb,saturate(_CrossfadeOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),_EquirectangularProjectionTextureRGB_var.rgb,saturate(_CrossfadeOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),float3(0.3,0.59,0.11)),(1.0 - _Color.a))*_Intensity*saturate((saturate((Function_node_3693( float3(0,1,0) )+(_LightColor0.rgb*attenuation)))*floor(saturate((dot(_Ramp_var.rgb,float3(0.3,0.59,0.11))+0.8)) * node_4857) / (node_4857 - 1))));
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _EquirectangularProjectionTextureRGB; uniform float4 _EquirectangularProjectionTextureRGB_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Intensity;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            float3 Function_node_3693( float3 normal ){
            return ShadeSH9(half4(normal, 1.0));
            
            }
            
            uniform float _CrossfadeOverlay;
            uniform float _SpeedX;
            uniform float _SpeedY;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_7920 = 1.0;
                float node_9795 = 0.1;
                float4 node_47 = _Time + _TimeEditor;
                float3 node_4810 = viewDirection.brg;
                float2 node_1431 = ((float2((node_9795*_SpeedX),(node_9795*_SpeedY))*node_47.g)+(1.0 - float2(((atan2(node_4810.r,node_4810.g)/6.28318530718)+0.5),(acos(node_4810.b)/(-1*3.141592654)))).rg);
                float4 _EquirectangularProjectionTextureRGB_var = tex2D(_EquirectangularProjectionTextureRGB,TRANSFORM_TEX(node_1431, _EquirectangularProjectionTextureRGB));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_5668 = float2(saturate(dot(i.normalDir,lightDirection)),0.2);
                float4 _Ramp_var = tex2D(_Ramp,TRANSFORM_TEX(node_5668, _Ramp));
                float node_4857 = 3.0;
                float3 finalColor = (lerp(lerp(((lerp(float3(node_7920,node_7920,node_7920),_EquirectangularProjectionTextureRGB_var.rgb,saturate(_CrossfadeOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),_EquirectangularProjectionTextureRGB_var.rgb,saturate(_CrossfadeOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),dot(lerp(((lerp(float3(node_7920,node_7920,node_7920),_EquirectangularProjectionTextureRGB_var.rgb,saturate(_CrossfadeOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),_EquirectangularProjectionTextureRGB_var.rgb,saturate(_CrossfadeOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),float3(0.3,0.59,0.11)),(1.0 - _Color.a))*_Intensity*saturate((saturate((Function_node_3693( float3(0,1,0) )+(_LightColor0.rgb*attenuation)))*floor(saturate((dot(_Ramp_var.rgb,float3(0.3,0.59,0.11))+0.8)) * node_4857) / (node_4857 - 1))));
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
