Shader "Custom/LineSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_SoundImage("SoundImage", 2D) = "white" {}
		_Emission("Emission", Range(0,1)) = 0.0
		iResolutionX("Resolution X", Float) = 1.0
		iResolutionY("Resolution Y", Float) = 1.0

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		// https://www.shadertoy.com/view/lsdGR8#

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
		float audio_freq(in sampler2D channel, in float f) { return tex2D(channel, float2(f, 0.25)).x; }
		float audio_ampl(in sampler2D channel, in float t) { return tex2D(channel, float2(t, 0.75)).x; }

		sampler2D _MainTex;
		sampler2D _SoundImage;
		float iResolutionX;
		float iResolutionY;
		float _Emission;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			float2 uv = IN.uv_MainTex * (iResolutionX / iResolutionY);
			float2 centered = 2.0 * uv - 1.0;
			centered.x *= iResolutionX / iResolutionY;

			float dist2 = dot(centered, centered);
			float clamped_dist = smoothstep(0.0, 1.0, dist2);
			float arcLength = abs(atan2(centered.y, centered.x) / radians(360.0)) + 0.01;

			// Color variation functions
			float t = _Time.y / 100.0;
			float polychrome = (1.0 + sin(t*10.0)) / 2.0;	// 0 --> uniform color, 1 --> full spectrum
			float spline_val1 = float3(polychrome * uv.x - t, polychrome * uv.x - t, polychrome * uv.x - t);
			float3 spline_args = frac(spline_val1 + float3(0.0, -1 / 3.0, -2.0 / 3.0));
			float3 spline = B2_spline(spline_args);

			float f = abs(centered.y);
			float3 base_color = float3 (1.0, 1.0, 1.0) - f * spline;
			float3 flame_color = pow(base_color, float3(3.0, 3.0, 3.0));
			float3 disc_color = 0.20 * base_color;
			float3 wave_color = 0.10 * base_color;
			float3 flash_color = 0.05 * base_color;

			float sample1 = audio_freq(_SoundImage, abs(2.0 * uv.x - 1.0) + 0.01);
			float sample2 = audio_ampl(_SoundImage, clamped_dist);
			float sample3 = audio_ampl(_SoundImage, arcLength);

			float disp_dist = smoothstep(-0.2, -0.1, sample3 - dist2);

			float3 color = float3(0.0, 0.0, 0.0);

			float vert = abs(uv.y - 0.5);
			color += smoothstep(vert, vert * 8.0, sample1);
			//color += smoothstep(0.5, 1.0, sample2) * (1.0 - clamped_dist); // Circles in the centre

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
