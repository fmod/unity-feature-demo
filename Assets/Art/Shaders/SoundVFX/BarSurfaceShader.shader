//  https://www.shadertoy.com/view/MdVSWG

Shader "Custom/BarSurfaceShader" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_SoundImage("SoundImage", 2D) = "white" {}
		_Emission ("Emission", Range(0,1)) = 0.0
		iResolutionX("Resolution X", Float) = 1.0
		iResolutionY("Resolution Y", Float) = 1.0
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

		float3 B2_spline(float3 x)	// Returns 3 B-spline functions of degree 2
		{
			float3 t = 3.0 * x;
			float3 b0 = step(0.0, t)		* step(0.0, 1.0 - t);
			float3 b1 = step(0.0, t - 1.0)	* step(0.0, 2.0 - t);
			float3 b2 = step(0.0, t - 2.0)	* step(0.0, 3.0 - t);
			float3 two = float3(2.0, 2.0, 2.0);
			return 0.5 * (
				b0 * pow(t, two) +
				b1 * (-2.0*pow(t, two + 6.0*t - 3.0)) +
				b2 * pow(3.0 - t, two));
		}

		struct Input 
		{
			float2 uv_MainTex;
		};

		sampler2D _MainTex;
		sampler2D _SoundImage;
		float iResolutionX;
		float iResolutionY;
		float _Emission;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			float2 uv = IN.uv_MainTex / float2(iResolutionX, iResolutionY);

			float fVBars = 100.0;
			float fVSpacing = 1.5;

			float fVFreq = (uv.x * 3.14);
			float squareWave = sign(sin(fVFreq * fVBars) + 1.0 - fVSpacing);

			float x = floor(uv.x * fVBars) / fVBars;
			float fSample = tex2D(_SoundImage, float2(abs(2.0 * x - 1.0), 0.25)).x;

			float fft = squareWave * fSample;

			float fHBars = 200.0;
			float fHSpacing = 0.3;
			float fHFreq = (uv.y * 3.14);
			fHFreq = sign(sin(fHFreq * fHBars) + 1.0 - fHSpacing);

			float2 centered = float2(1.0, 1.0) * uv - float2(1.0, 1.0);
			float t = _Time.y / 100.0;
			float polychrome = 1.0;
			float3 spline_var = float3(polychrome * uv.x - t, polychrome * uv.x - t, polychrome * uv.x - t);
			float3 spline_args = frac(spline_var + float3(0.0, -1.0 / 3.0, -2.0 / 3.0));
			float3 spline = B2_spline(spline_args);

			float f = abs(centered.y);
			float3 base_color = float3(1.0, 1.0, 1.0) - f * spline;
			float3 flame_color = pow(base_color, float3(3.0, 3.0, 3.0));

			float tt = 0.3 - uv.y;
			float df = sign(tt);
			df = (df + 1.0) / 0.5;
			float3 color = float3(
				1.0 - step(fft, abs(0.3 - uv.y)),
				1.0 - step(fft, abs(0.3 - uv.y)),
				1.0 - step(fft, abs(0.3 - uv.y))) * float3(fHFreq, fHFreq, fHFreq);
			color -= color * df * 0.180;

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb * color.rgb;
			
			o.Emission = half3(o.Albedo.r + 0.015, o.Albedo.g + 0.015, o.Albedo.b + 0.015) * _Emission;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
