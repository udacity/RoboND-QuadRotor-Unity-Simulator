// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_LightmapInd', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D
// Upgrade NOTE: replaced tex2D unity_LightmapInd with UNITY_SAMPLE_TEX2D_SAMPLER

// Shader created with Shader Forge Beta 0.31 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.31;sub:START;pass:START;ps:flbk:Transparent/Diffuse,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:True,lprd:False,enco:False,frtr:True,vitr:True,dbil:True,rmgx:True,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.07,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|diff-2-RGB,emission-356-OUT,alpha-317-OUT,clip-2-A;n:type:ShaderForge.SFN_Tex2d,id:2,x:33218,y:32528,ptlb:Diffuse,ptin:_Diffuse,tex:f36bb6e6929b4cf4cbafe6d9aea66673,ntxv:0,isnm:False|UVIN-416-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:11,x:33412,y:32878,ptlb:HologramSlider,ptin:_HologramSlider,tex:8f127a8df03754bb9bc30c6c1cb78d31,ntxv:0,isnm:False|UVIN-14-UVOUT;n:type:ShaderForge.SFN_Panner,id:14,x:33598,y:32878,spu:0,spv:1|DIST-1331-OUT;n:type:ShaderForge.SFN_Multiply,id:127,x:33224,y:32895|A-2-A,B-11-G;n:type:ShaderForge.SFN_Subtract,id:317,x:33043,y:32811|A-344-OUT,B-127-OUT;n:type:ShaderForge.SFN_Slider,id:344,x:33224,y:32782,ptlb:Flicker Alpha,ptin:_FlickerAlpha,min:0,cur:1,max:1;n:type:ShaderForge.SFN_ValueProperty,id:355,x:33204,y:33195,ptlb:Emission,ptin:_Emission,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:356,x:33027,y:33047|A-2-RGB,B-355-OUT;n:type:ShaderForge.SFN_Panner,id:416,x:33433,y:32526,spu:0,spv:0.01;n:type:ShaderForge.SFN_Time,id:1329,x:34016,y:32962;n:type:ShaderForge.SFN_Multiply,id:1331,x:33782,y:32901|A-1332-OUT,B-1329-TSL;n:type:ShaderForge.SFN_ValueProperty,id:1332,x:33979,y:32881,ptlb:Flicker Speed,ptin:_FlickerSpeed,glob:False,v1:0.3;proporder:2-11-344-355-1332;pass:END;sub:END;*/

Shader "Hologram/Diffuse Illumin DPan" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _HologramSlider ("HologramSlider", 2D) = "white" {}
        _FlickerAlpha ("Flicker Alpha", Range(0, 1)) = 1
        _Emission ("Emission", Float ) = 1
        _FlickerSpeed ("Flicker Speed", Float ) = 0.3
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            #ifndef LIGHTMAP_OFF
                // sampler2D unity_Lightmap;
                // float4 unity_LightmapST;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _HologramSlider; uniform float4 _HologramSlider_ST;
            uniform float _FlickerAlpha;
            uniform float _Emission;
            uniform float _FlickerSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                #ifndef LIGHTMAP_OFF
                    float2 uvLM : TEXCOORD5;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                #ifndef LIGHTMAP_OFF
                    o.uvLM = v.uv1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                #endif
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float4 node_2100 = _Time + _TimeEditor;
                float2 node_2099 = i.uv0;
                float2 node_416 = (node_2099.rg+node_2100.g*float2(0,0.01));
                float4 node_2 = tex2D(_Diffuse,TRANSFORM_TEX(node_416, _Diffuse));
                clip(node_2.a - 0.5);
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                #ifndef LIGHTMAP_OFF
                    float4 lmtex = UNITY_SAMPLE_TEX2D(unity_Lightmap,i.uvLM);
                    #ifndef DIRLIGHTMAP_OFF
                        float3 lightmap = DecodeLightmap(lmtex);
                        float3 scalePerBasisVector = DecodeLightmap(UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd,unity_Lightmap,i.uvLM));
                        UNITY_DIRBASIS
                        half3 normalInRnmBasis = saturate (mul (unity_DirBasis, float3(0,0,1)));
                        lightmap *= dot (normalInRnmBasis, scalePerBasisVector);
                    #else
                        float3 lightmap = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap,i.uvLM));
                    #endif
                #endif
                #ifndef LIGHTMAP_OFF
                    #ifdef DIRLIGHTMAP_OFF
                        float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                    #else
                        float3 lightDirection = normalize (scalePerBasisVector.x * unity_DirBasis[0] + scalePerBasisVector.y * unity_DirBasis[1] + scalePerBasisVector.z * unity_DirBasis[2]);
                        lightDirection = mul(lightDirection,tangentTransform); // Tangent to world
                    #endif
                #else
                    float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                #endif
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                #ifndef LIGHTMAP_OFF
                    float3 diffuse = lightmap;
                #else
                    float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.xyz*2;
                #endif
////// Emissive:
                float3 emissive = (node_2.rgb*_Emission);
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * node_2.rgb;
                finalColor += emissive;
                float4 node_1329 = _Time + _TimeEditor;
                float2 node_14 = (node_2099.rg+(_FlickerSpeed*node_1329.r)*float2(0,1));
/// Final Color:
                return fixed4(finalColor,(_FlickerAlpha-(node_2.a*tex2D(_HologramSlider,TRANSFORM_TEX(node_14, _HologramSlider)).g)));
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            #ifndef LIGHTMAP_OFF
                // sampler2D unity_Lightmap;
                // float4 unity_LightmapST;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _HologramSlider; uniform float4 _HologramSlider_ST;
            uniform float _FlickerAlpha;
            uniform float _Emission;
            uniform float _FlickerSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normalDir = mul(float4(v.normal,0), unity_WorldToObject).xyz;
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float4 node_2102 = _Time + _TimeEditor;
                float2 node_2101 = i.uv0;
                float2 node_416 = (node_2101.rg+node_2102.g*float2(0,0.01));
                float4 node_2 = tex2D(_Diffuse,TRANSFORM_TEX(node_416, _Diffuse));
                clip(node_2.a - 0.5);
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i)*2;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * node_2.rgb;
                float4 node_1329 = _Time + _TimeEditor;
                float2 node_14 = (node_2101.rg+(_FlickerSpeed*node_1329.r)*float2(0,1));
/// Final Color:
                return fixed4(finalColor * (_FlickerAlpha-(node_2.a*tex2D(_HologramSlider,TRANSFORM_TEX(node_14, _HologramSlider)).g)),0);
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
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            #ifndef LIGHTMAP_OFF
                // sampler2D unity_Lightmap;
                // float4 unity_LightmapST;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
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
                float4 node_2104 = _Time + _TimeEditor;
                float2 node_416 = (i.uv0.rg+node_2104.g*float2(0,0.01));
                float4 node_2 = tex2D(_Diffuse,TRANSFORM_TEX(node_416, _Diffuse));
                clip(node_2.a - 0.5);
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
            #pragma exclude_renderers xbox360 ps3 flash 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            #ifndef LIGHTMAP_OFF
                // sampler2D unity_Lightmap;
                // float4 unity_LightmapST;
                #ifndef DIRLIGHTMAP_OFF
                    // sampler2D unity_LightmapInd;
                #endif
            #endif
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
                float4 node_2106 = _Time + _TimeEditor;
                float2 node_416 = (i.uv0.rg+node_2106.g*float2(0,0.01));
                float4 node_2 = tex2D(_Diffuse,TRANSFORM_TEX(node_416, _Diffuse));
                clip(node_2.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
