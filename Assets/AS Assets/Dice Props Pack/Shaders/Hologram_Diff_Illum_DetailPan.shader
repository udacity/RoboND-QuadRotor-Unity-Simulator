// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_LightmapInd', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D
// Upgrade NOTE: replaced tex2D unity_LightmapInd with UNITY_SAMPLE_TEX2D_SAMPLER

// Shader created with Shader Forge Beta 0.32 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.32;sub:START;pass:START;ps:flbk:Transparent/Diffuse,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:True,lprd:False,enco:False,frtr:True,vitr:True,dbil:True,rmgx:True,hqsc:True,hqlp:False,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.07,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|diff-1741-OUT,emission-1799-OUT,amdfl-1471-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33379,y:32516,ptlb:Diffuse,ptin:_Diffuse,tex:f36bb6e6929b4cf4cbafe6d9aea66673,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:11,x:33412,y:32878,ptlb:HologramStripes,ptin:_HologramStripes,tex:b370a1b908278ac45868c01d8d2c7e08,ntxv:0,isnm:False|UVIN-14-UVOUT;n:type:ShaderForge.SFN_Panner,id:14,x:33601,y:32881,spu:0,spv:5|DIST-1331-OUT;n:type:ShaderForge.SFN_ValueProperty,id:355,x:33434,y:32768,ptlb:Emission,ptin:_Emission,glob:False,v1:1;n:type:ShaderForge.SFN_Time,id:1329,x:34016,y:32962;n:type:ShaderForge.SFN_Multiply,id:1331,x:33783,y:32881|A-1332-OUT,B-1329-TSL;n:type:ShaderForge.SFN_ValueProperty,id:1332,x:33979,y:32881,ptlb:Flicker Speed,ptin:_FlickerSpeed,glob:False,v1:0.3;n:type:ShaderForge.SFN_Multiply,id:1471,x:33056,y:32775|A-11-RGB,B-355-OUT,C-1565-RGB,D-1832-OUT;n:type:ShaderForge.SFN_Color,id:1565,x:33337,y:32360,ptlb:Color,ptin:_Color,glob:False,c1:0,c2:0.7517242,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:1675,x:33428,y:33110,ptlb:HologramVerical,ptin:_HologramVerical,tex:1d13dccd315d3b3409181cd2092f1edd,ntxv:2,isnm:False|UVIN-1677-UVOUT;n:type:ShaderForge.SFN_Panner,id:1677,x:33601,y:33100,spu:0,spv:10|DIST-1331-OUT;n:type:ShaderForge.SFN_Multiply,id:1741,x:33036,y:32622|A-1565-RGB,B-2-RGB;n:type:ShaderForge.SFN_Multiply,id:1799,x:33056,y:32990|A-1675-RGB,B-1565-RGB,C-355-OUT;n:type:ShaderForge.SFN_Vector1,id:1832,x:33296,y:32733,v1:0.5;proporder:1565-2-355-11-1332-1675;pass:END;sub:END;*/

Shader "Hologram/Diffuse Illumin Detail Pan" {
    Properties {
        _Color ("Color", Color) = (0,0.7517242,1,1)
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Emission ("Emission", Float ) = 1
        _HologramStripes ("HologramStripes", 2D) = "white" {}
        _FlickerSpeed ("Flicker Speed", Float ) = 0.3
        _HologramVerical ("HologramVerical", 2D) = "black" {}
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
            uniform sampler2D _HologramStripes; uniform float4 _HologramStripes_ST;
            uniform float _Emission;
            uniform float _FlickerSpeed;
            uniform float4 _Color;
            uniform sampler2D _HologramVerical; uniform float4 _HologramVerical_ST;
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
                float4 node_1329 = _Time + _TimeEditor;
                float node_1331 = (_FlickerSpeed*node_1329.r);
                float2 node_1928 = i.uv0;
                float2 node_1677 = (node_1928.rg+node_1331*float2(0,10));
                float3 emissive = (tex2D(_HologramVerical,TRANSFORM_TEX(node_1677, _HologramVerical)).rgb*_Color.rgb*_Emission);
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float2 node_14 = (node_1928.rg+node_1331*float2(0,5));
                diffuseLight += (tex2D(_HologramStripes,TRANSFORM_TEX(node_14, _HologramStripes)).rgb*_Emission*_Color.rgb*0.5); // Diffuse Ambient Light
                finalColor += diffuseLight * (_Color.rgb*tex2D(_Diffuse,TRANSFORM_TEX(node_1928.rg, _Diffuse)).rgb);
                finalColor += emissive;
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
            uniform float _Emission;
            uniform float _FlickerSpeed;
            uniform float4 _Color;
            uniform sampler2D _HologramVerical; uniform float4 _HologramVerical_ST;
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
                float2 node_1929 = i.uv0;
                finalColor += diffuseLight * (_Color.rgb*tex2D(_Diffuse,TRANSFORM_TEX(node_1929.rg, _Diffuse)).rgb);
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
