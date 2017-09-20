Shader "Unlit/White"
{
	Properties
	{
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		zwrite off
//		ztest lequal

		Pass
		{
			CGPROGRAM

			#pragma vertex vert             
			#pragma fragment frag

			struct vertInput
			{
				float4 pos : POSITION;
			};  

			struct vertOutput
			{
				float4 pos : SV_POSITION;
			};

			vertOutput vert(vertInput input)
			{
				vertOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, input.pos);
				return o;
			}

			half4 frag(vertOutput output) : COLOR
			{
//				return half4(0.0, 0.0, 0.0, 1.0);
				return half4(1.0, 1.0, 1.0, 1.0);
			}
			ENDCG
		}
	}
}
