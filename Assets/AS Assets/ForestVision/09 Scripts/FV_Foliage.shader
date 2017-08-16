// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'


// Thanks to Gambinolnd for the base of this shader which I modified for ForestVision
// http://forum.unity3d.com/threads/shader-moving-trees-grass-in-wind-outside-of-terrain.230911/


Shader "ForestVision/FV_Foliage" {
    Properties {
    [Header(Typical Controls)]_Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}

    _Normal ("Normal", 2D) = "bump" {}
    _BumpPower ("Normal Power", Range (0.01, 2)) = 1 //added slider control for Normal strength

    [Header(Animation Controls)] _VertexWeight ("Vertex Weight", 2D) = "weight" {} //added new weight map slot to drive vertex animation
	[HideInInspector]_OcclusionMap("Occlusion", 2D) = "white" {}
    [HideInInspector]_OcclusionStrength ("Occlusion Strength", Range(0.01,1)) = 1
    _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    //_ShakeDisplacement ("Displacement", Range (0, 1.0)) = 1.0
    _ShakeTime ("Animation Time", Range (0, 1.0)) = .25
    _ShakeWindspeed ("Wind Speed", Range (0, 1.0)) = .3
    _ShakeBending ("Displacement", Range (0, 1.0)) = 0.2
	[Space(10)]
        
    [Header(Additional Coloring)] _TopDirection ("Coloring Direction", Vector) = (0,1.5,0)
    _TopLevel ("Coloring Level", Range(0,1) ) = 0.2
    _TopDepth ("Coloring Depth", Range(0,1)) = 0.7
    _TopColor ("Coloring Color", Color) = (1,0.894,0.710,1.0) //orange
        
    

}
 
SubShader {
    Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
    LOD 400
    Cull [_Cull]

   

CGPROGRAM
#pragma target 3.0
#pragma surface surf Lambert alphatest:_Cutoff vertex:vert addshadow
 
sampler2D _MainTex;
sampler2D _Normal;
sampler2D _VertexWeight;
sampler2D _OcclusionMap;

fixed4 _Color;
fixed _BumpPower;
fixed _OcclusionStrength;

float _ShakeDisplacement;
float _ShakeTime = 0.1;
float _ShakeWindspeed;
float _ShakeBending;

float _TopLevel;
float4 _TopColor;
float4 _TopDirection;
float4 _TopDepth;



 
struct Input {
    float2 uv_MainTex;
    float2 uv_Normal;
    float2 uv_Weight;
    float2 uv_OcculsionMap;
     float3 worldNormal;
     INTERNAL_DATA
};
 
void FastSinCos (float4 val, out float4 s, out float4 c) {
    val = val * 6.408849 - 3.1415927;
    float4 r5 = val * val;
    float4 r6 = r5 * r5;
    float4 r7 = r6 * r5;
    float4 r8 = r6 * r5;
    float4 r1 = r5 * val;
    float4 r2 = r1 * r5;
    float4 r3 = r2 * r5;
    float4 sin7 = {1, -0.16161616, 0.0083333, -0.00019841} ;
    float4 cos8  = {-0.5, 0.041666666, -0.0013888889, 0.000024801587} ;
    s =  val + r1 * sin7.y + r2 * sin7.z + r3 * sin7.w;
    c = 1 + r5 * cos8.x + r6 * cos8.y + r7 * cos8.z + r8 * cos8.w;
}
 
void vert (inout appdata_full v) {
	//get coloring direction
	float4 top = mul(UNITY_MATRIX_IT_MV, _TopDirection);


	float4 WeightMap = tex2Dlod (_VertexWeight, float4(v.texcoord.xy,0,0));

    float factor = (1 - _ShakeDisplacement -  v.color.r) * 0.5;
       
    const float _WindSpeed  = (_ShakeWindspeed  +  v.color.g );    
    const float _WaveScale = _ShakeDisplacement;
   
    const float4 _waveXSize = float4(0.048, 0.06, 0.24, 0.096);
    const float4 _waveZSize = float4 (0.024, .08, 0.08, 0.2);
    const float4 waveSpeed = float4 (1.2, 2, 1.6, 4.8);
 
    float4 _waveXmove = float4(0.024, 0.04, -0.12, 0.096);
    float4 _waveZmove = float4 (0.006, .02, -0.02, 0.1);
   
    float4 waves;
    waves = v.vertex.x * _waveXSize;
    waves += v.vertex.z * _waveZSize;
 
    waves += _Time.x * (1 - _ShakeTime * 2 - v.color.b ) * waveSpeed *_WindSpeed;
 
    float4 s, c;
    waves = frac (waves);
    FastSinCos (waves, s,c);
 
    float waveAmount = WeightMap.r  * (v.color.a + _ShakeBending);
    s *= waveAmount;
 
    s *= normalize (waveSpeed);
 
    s = s * s;
    float fade = dot (s, 1.3);
    s = s * s;
    float3 waveMove = float3 (0,0,0);
    waveMove.x = dot (s, _waveXmove);
    waveMove.z = dot (s, _waveZmove);
    v.vertex.xz -= mul ((float3x3)unity_WorldToObject, waveMove).xz;
   
}
 
void surf (Input IN, inout SurfaceOutput o) {
    fixed4 baseColor = tex2D(_MainTex, IN.uv_MainTex) * _Color;
    fixed3 occlusion = tex2D(_OcclusionMap, IN.uv_OcculsionMap);
    //c.rgb =
    fixed3 normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal)); 
    normal.z = normal.z / _BumpPower; 
    o.Normal = normalize(normal);
    
    half difference2 = dot(WorldNormalVector(IN, o.Normal), _TopDirection.xyz) - lerp(1,-1,_TopLevel);
    difference2 = saturate(difference2 / _TopDepth);
    
    
    o.Albedo = (difference2 * (_TopColor.rgb *2)  + (1-difference2)) * baseColor.rgb * _Color.rgb;
    
    o.Alpha = baseColor.a;
    
}
ENDCG


} //end subshader

 
Fallback "Transparent/Cutout/VertexLit"
}
 
 

