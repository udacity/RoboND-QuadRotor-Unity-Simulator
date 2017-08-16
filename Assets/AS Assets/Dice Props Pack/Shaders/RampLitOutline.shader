Shader "Ramp/Lit Outline" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Ramp (RGB)", 2D) = "gray" {} 
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		UsePass "Ramp/Lit/FORWARD"
		UsePass "Ramp/Basic Outline/OUTLINE"
	} 
	
	Fallback "Ramp/Lit"
}
