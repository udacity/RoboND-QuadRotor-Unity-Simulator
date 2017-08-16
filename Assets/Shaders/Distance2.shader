Shader "Custom/Distance2"
{
	Properties
	{
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM

			#pragma vertex vert             
			#pragma fragment frag

			struct vertInput
			{
				float4 pos : POSITION;
//				float3 worldPos;
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
				half4 result = (1, 1, 1, 1);
				return result;
			}
			ENDCG
		}
	}
}
