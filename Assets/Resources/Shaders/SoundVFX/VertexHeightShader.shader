Shader "Cameron/VertexHeightShader" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Amount ("Height Adjustment", Float) = 0.0
		_Alpha ("Alpha", Range(0,1)) = 0.0
		_Emission ("Emission", Range(0,1)) = 0.0
	}
	SubShader 
	{
		Tags { "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
			float4 color : COLOR0;
		};

		half _Glossiness;
		half _Metallic;
		float _Emission;
		fixed4 _Color;
		float _Amount;
		float _Alpha;

		void vert (inout appdata_full v)
		{
			if (v.vertex.y < -0.1)
			{
				_Amount *= _Amount * 15;
				if (_Amount > -v.vertex.y)
					v.vertex.y -= _Amount;
			}
			v.color.rgb = -v.vertex.yyy;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			
			if (_Amount < 0.4)
				o.Albedo = float3( 0, 0, 1);
			else if (_Amount < 0.7)
				o.Albedo = lerp(float3(0, 0.75, 1), float3(1, 1, 0), _Amount);
			else
				o.Albedo = lerp(float3(1, 1, 0), float3(1, 1, 1), _Amount);
				
			o.Emission = o.Albedo * _Emission;				

			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = _Alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
