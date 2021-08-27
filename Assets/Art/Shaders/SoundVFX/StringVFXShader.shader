//	https://www.shadertoy.com/view/ls3Sz4

Shader "Custom/StringVFXShader" 
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

		float hash(int x) { return frac(sin(float(x))*7.847); } 

		float dSegment(float2 a, float2 b, float2 c)
		{
			float2 ab = b-a;
			float2 ac = c-a;

			float h = clamp(dot(ab, ac)/dot(ab, ab), 0., 1.);
			float2 point0 = a+ab*h;
			return length(c-point0);
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
/*
		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}	
*/	

		void surf ( Input IN, inout SurfaceOutputStandard o )
		{
			float2 uv = (IN.uv_MainTex * 2.0 - float2(iResolutionX, iResolutionY)) / float2(iResolutionY, iResolutionY);
			
			float3 color = float3( 0, 0, 0);
			color = lerp(float3(0.325, 0.431, 0.364), color, abs(uv.x) * 0.25);
			
			for(int i = 0; i < 10; ++i)
			{
				float2 a = float2(hash(i) * 2.0 - 1.0, hash(i + 1) * 2.0 - 1.0);
				float2 b = float2(hash(10 * i + 1) * 2.0 - 1.0, hash(11 * i + 2) * 2.0 - 1.0);
				float3 lineColor = float3(hash(10 + i), hash(18 + i * 3), hash(5 + i * 10));
				float speed = b.y * 0.15;
				float size = (0.005 + 0.3 * hash(5 + i * i * 2)) + (0.5 + 0.5 * sin(a.y * 5.0 + _Time.y * speed)) * 0.1;
				
				a += float2(sin(a.x * 20.0 + _Time.y * speed), sin(a.y * 15.0 + _Time.y * 0.4 * speed) * 0.5);
				b += float2(b.x * 5.0 + cos(_Time.y * speed), cos(b.y * 10.0 + _Time.y * 2.0 * speed) * 0.5);
				float dist = dSegment(a, b, uv);
				
				float soundWave = 1.5 * tex2D(_SoundImage, float2(0.10, 0.2)).x;
				color += lerp(lineColor, float3(0,0,0), smoothstep(0., 1.0, pow(dist/size, soundWave*(0.5+0.5*sin(_Time.y * 2.+size+lineColor.x*140.))*0.20) ));
			}
			
			o.Albedo = float4(color,1.0);
		}

		ENDCG
	}
	FallBack "Diffuse"
}
