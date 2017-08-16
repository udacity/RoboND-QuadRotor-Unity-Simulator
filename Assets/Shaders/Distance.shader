// Upgrade NOTE: commented out 'float3 _WorldSpaceCameraPos', a built-in variable

Shader "Custom/Distance"
{
	Properties
	{
//		_MainTex ("Base (RGB)", 2D) = "white" {}
//		_ChangePoint ("Change at this distance", Float) = 3
//		_OuterTex ("Base (RGB)", 2D) = "black" {}
//		_CentrePoint ("Centre", Vector) = (0, 0, 0, 0)
//		_BlendThreshold ("Blend Distance", Float) = 0.5
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
 
//		sampler2D _MainTex;
//		float _ChangePoint;
//		float4 _CentrePoint;
//		sampler2D _OuterTex;
//		float _BlendThreshold;
 
		struct Input
		{
			float2 uv_MainTex;
			float3 worldPos;
			// float3 _WorldSpaceCameraPos;
		};
 
		void surf (Input IN, inout SurfaceOutput o)
		{
			float pos = length(_WorldSpaceCameraPos - IN.worldPos);
//			o.Albedo = c.rgb;
//			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
 }