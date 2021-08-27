Shader "Cameron/BeatGlow" 
{
	Properties 
	{
		_EmissionColor("Emission Color", Color) = (0,0,0)
		_EmissionAmount("Emission Amount", Range(0,10)) = 0
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		#pragma shader_feature _EMISSION

		float3 _EmissionColor;
		float _EmissionAmount;	

		struct Input 
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = fixed4(_EmissionColor * _EmissionAmount, 1);
			o.Albedo = c.rgb;
			o.Emission = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
