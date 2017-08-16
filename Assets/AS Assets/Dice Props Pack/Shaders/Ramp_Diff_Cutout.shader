// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

#warning Upgrade NOTE: unity_Scale shader variable was removed; replaced 'unity_Scale.w' with '1.0'

// Shader created with Shader Forge Beta 0.22 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.22;sub:START;pass:START;ps:lgpr:1,nrmq:1,limd:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,uamb:True,mssp:True,ufog:False,aust:True,igpj:False,qofs:0,lico:1,qpre:2,flbk:Transparent/Cutout/Diffuse,rntp:3,lmpd:False,lprd:True,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,hqsc:False,hqlp:False,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0;n:type:ShaderForge.SFN_Final,id:0,x:32153,y:31966|custl-64-OUT,clip-82-A;n:type:ShaderForge.SFN_LightAttenuation,id:37,x:32583,y:32026;n:type:ShaderForge.SFN_Dot,id:40,x:33775,y:32289,dt:1|A-42-OUT,B-41-OUT;n:type:ShaderForge.SFN_NormalVector,id:41,x:33975,y:32382,pt:True;n:type:ShaderForge.SFN_LightVector,id:42,x:33984,y:32262;n:type:ShaderForge.SFN_Dot,id:52,x:33766,y:32461,dt:1|A-41-OUT,B-62-OUT;n:type:ShaderForge.SFN_Add,id:55,x:32583,y:32288|A-82-RGB,B-187-RGB,C-220-OUT;n:type:ShaderForge.SFN_Power,id:58,x:33599,y:32585,cmnt:Specular Light|VAL-52-OUT,EXP-244-OUT;n:type:ShaderForge.SFN_HalfVector,id:62,x:33975,y:32521;n:type:ShaderForge.SFN_LightColor,id:63,x:32583,y:32155;n:type:ShaderForge.SFN_Multiply,id:64,x:32399,y:32155|A-37-OUT,B-63-RGB,C-55-OUT;n:type:ShaderForge.SFN_Tex2d,id:82,x:33087,y:32002,ptlb:Diffuse,ntxv:0,isnm:False;n:type:ShaderForge.SFN_AmbientLight,id:187,x:32851,y:32282;n:type:ShaderForge.SFN_Round,id:196,x:33241,y:32281|IN-197-OUT;n:type:ShaderForge.SFN_Multiply,id:197,x:33420,y:32281|A-40-OUT,B-216-OUT;n:type:ShaderForge.SFN_Divide,id:199,x:33087,y:32380|A-196-OUT,B-216-OUT;n:type:ShaderForge.SFN_Multiply,id:215,x:33406,y:32585|A-216-OUT,B-58-OUT;n:type:ShaderForge.SFN_ValueProperty,id:216,x:33566,y:32461,ptlb:Bands,v1:8;n:type:ShaderForge.SFN_Round,id:218,x:33230,y:32585|IN-215-OUT;n:type:ShaderForge.SFN_Divide,id:220,x:33069,y:32686|A-218-OUT,B-216-OUT;n:type:ShaderForge.SFN_Slider,id:239,x:34305,y:32590,ptlb:Gloss,min:0.5,cur:0.5,max:1;n:type:ShaderForge.SFN_Add,id:240,x:33975,y:32678|A-242-OUT,B-241-OUT;n:type:ShaderForge.SFN_Vector1,id:241,x:34143,y:32766,v1:1;n:type:ShaderForge.SFN_Multiply,id:242,x:34143,y:32616|A-239-OUT,B-243-OUT;n:type:ShaderForge.SFN_Vector1,id:243,x:34305,y:32660,v1:10;n:type:ShaderForge.SFN_Exp,id:244,x:33804,y:32678,et:1|IN-240-OUT;proporder:82-216-239;pass:END;sub:END;*/

Shader "Ramp/Diff Cutout" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Bands ("Bands", Float ) = 8
        _Gloss ("Gloss", Range(0.5, 1)) = 0.5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _Bands;
            uniform float _Gloss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                float3 shLight : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.shLight = ShadeSH9(float4(v.normal * 1.0,1)) * 0.5;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_281 = i.uv0;
                float4 node_82 = tex2D(_Diffuse,TRANSFORM_TEX(node_281.rg, _Diffuse));
                clip(node_82.a - 0.5);
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_216 = _Bands;
                float3 node_41 = normalDirection;
                float3 finalColor = (attenuation*_LightColor0.rgb*(node_82.rgb+UNITY_LIGHTMODEL_AMBIENT.rgb+(round((node_216*pow(max(0,dot(node_41,halfDirection)),exp2(((_Gloss*10.0)+1.0)))))/node_216)));
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers gles xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _Bands;
            uniform float _Gloss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_282 = i.uv0;
                float4 node_82 = tex2D(_Diffuse,TRANSFORM_TEX(node_282.rg, _Diffuse));
                clip(node_82.a - 0.5);
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_216 = _Bands;
                float3 node_41 = normalDirection;
                float3 finalColor = (attenuation*_LightColor0.rgb*(node_82.rgb+UNITY_LIGHTMODEL_AMBIENT.rgb+(round((node_216*pow(max(0,dot(node_41,halfDirection)),exp2(((_Gloss*10.0)+1.0)))))/node_216)));
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers gles xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float4 uv0 : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_283 = i.uv0;
                float4 node_82 = tex2D(_Diffuse,TRANSFORM_TEX(node_283.rg, _Diffuse));
                clip(node_82.a - 0.5);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles xbox360 ps3 flash 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_284 = i.uv0;
                float4 node_82 = tex2D(_Diffuse,TRANSFORM_TEX(node_284.rg, _Diffuse));
                clip(node_82.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
