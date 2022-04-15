Shader "NOT_Lonely/NOTLonely_Specular2sided"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_BaseTexture("BaseTexture", 2D) = "white" {}
		_Cutout( "Alpha Cutout", Float ) = 0.5
		_Specular("Specular", 2D) = "black" {}
		_Normal("Normal", 2D) = "bump" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest" "IgnoreProjector" = "True" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _BaseTexture;
		uniform float4 _BaseTexture_ST;
		uniform sampler2D _Specular;
		uniform float4 _Specular_ST;
		uniform float _Cutout = 0.5;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float2 uv_BaseTexture = i.uv_texcoord * _BaseTexture_ST.xy + _BaseTexture_ST.zw;
			float4 albedo = tex2D( _BaseTexture, uv_BaseTexture );
			o.Albedo = albedo.rgb;
			float2 uv_Specular = i.uv_texcoord * _Specular_ST.xy + _Specular_ST.zw;
			float4 spec = tex2D( _Specular, uv_Specular );
			o.Specular = spec.rgb;
			o.Smoothness = spec.a;
			o.Alpha = 1;
			clip( albedo.a - _Cutout );
		}

		ENDCG
	}
	Fallback "Standard (Specular setup)"
	Fallback "Diffuse"
}
