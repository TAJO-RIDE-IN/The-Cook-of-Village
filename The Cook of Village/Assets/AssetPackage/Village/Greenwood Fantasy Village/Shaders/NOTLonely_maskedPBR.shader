// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NOT_Lonely/NOT_MaskedPBR"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_ColorMask("Color Mask", 2D) = "white" {}
		_Color01("Color01", Color) = (1,1,1,0)
		_Color02("Color02", Color) = (1,1,1,0)
		_Color03("Color03", Color) = (1,1,1,0)
		_Color04("Color04", Color) = (1,1,1,0)
		_BumpMap("Normal Map", 2D) = "bump" {}
		_Specular("Specular", 2D) = "black" {}
		_Occlusion("Occlusion", 2D) = "white" {}
		_EmissionMap("Emission", 2D) = "white" {}
		[HDR]_EmissionColor("Emission Color", Color) = (0,0,0,0)
		_Dirt("Dirt", Range( 0 , 5)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _BumpMap;
		uniform float4 _BumpMap_ST;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float _Dirt;
		uniform float4 _Color04;
		uniform float4 _Color01;
		uniform sampler2D _ColorMask;
		uniform float4 _ColorMask_ST;
		uniform float4 _Color02;
		uniform float4 _Color03;
		uniform sampler2D _EmissionMap;
		uniform float4 _EmissionMap_ST;
		uniform float4 _EmissionColor;
		uniform sampler2D _Specular;
		uniform float4 _Specular_ST;
		uniform sampler2D _Occlusion;
		uniform float4 _Occlusion_ST;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 uv_BumpMap = i.uv_texcoord * _BumpMap_ST.xy + _BumpMap_ST.zw;
			o.Normal = UnpackNormal( tex2D( _BumpMap, uv_BumpMap ) );
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 temp_cast_0 = (_Dirt).xxxx;
			float4 temp_output_18_0 = pow( abs( tex2D( _MainTex, uv_MainTex ) ) , temp_cast_0 );
			float2 uv_ColorMask = i.uv_texcoord * _ColorMask_ST.xy + _ColorMask_ST.zw;
			float4 tex2DNode12 = tex2D( _ColorMask, uv_ColorMask );
			float4 lerpResult11 = lerp( ( temp_output_18_0 * _Color04 ) , ( temp_output_18_0 * _Color01 ) , tex2DNode12.r);
			float4 lerpResult13 = lerp( lerpResult11 , ( temp_output_18_0 * _Color02 ) , tex2DNode12.g);
			float4 lerpResult14 = lerp( lerpResult13 , ( temp_output_18_0 * _Color03 ) , tex2DNode12.b);
			o.Albedo = lerpResult14.rgb;
			float2 uv_EmissionMap = i.uv_texcoord * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
			o.Emission = ( tex2D( _EmissionMap, uv_EmissionMap ) * _EmissionColor ).rgb;
			float2 uv_Specular = i.uv_texcoord * _Specular_ST.xy + _Specular_ST.zw;
			float4 tex2DNode17 = tex2D( _Specular, uv_Specular );
			o.Specular = tex2DNode17.rgb;
			o.Smoothness = ( tex2DNode17.a - (0.0 + (_Dirt - 0.0) * (0.1 - 0.0) / (5.0 - 0.0)) );
			float2 uv_Occlusion = i.uv_texcoord * _Occlusion_ST.xy + _Occlusion_ST.zw;
			o.Occlusion = tex2D( _Occlusion, uv_Occlusion ).g;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=18933
0;73;2560;1287;2060.161;683.4197;1.50998;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-1658.422,-75.58686;Inherit;True;Property;_MainTex;Albedo (RGB);0;0;Create;False;0;0;0;False;0;False;-1;None;bfddd97f0842428408b6a6ab16def9b6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;19;-1605.786,-208.7353;Float;False;Property;_Dirt;Dirt;11;0;Create;True;0;0;0;False;0;False;1;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;29;-1324.758,-62.11731;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;18;-1174.741,-56.12704;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;6;-1203.199,92.40023;Float;False;Property;_Color01;Color01;2;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.875,0.2573529,0.2573529,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-1190.2,638.4007;Float;False;Property;_Color04;Color04;5;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;12;-885.696,-485.5997;Inherit;True;Property;_ColorMask;Color Mask;1;0;Create;True;0;0;0;False;0;False;-1;None;ae28892126aeef943b7807dfa4b77316;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-734.6006,-83.59991;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;8;-1199.299,273.1006;Float;False;Property;_Color02;Color02;3;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.7699828,0.9485294,0.7462695,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-732.6006,265.4001;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;9;-1191.499,457.7005;Float;False;Property;_Color03;Color03;4;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.9338235,0.5322603,0.3845155,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;11;-430.5979,-76.79963;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-733.6006,32.40009;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-732.6006,144.4001;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;13;-232.9965,52.40039;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;20;-1178.04,981.8576;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;5;False;3;FLOAT;0;False;4;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;17;-911.561,741.6403;Inherit;True;Property;_Specular;Specular;7;0;Create;False;0;0;0;False;0;False;-1;None;30aa10e3626aaa94a89dfda130abc0c7;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;30;-177.7191,-291.293;Inherit;True;Property;_EmissionMap;Emission;9;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;33;-85.28963,-91.36452;Inherit;False;Property;_EmissionColor;Emission Color;10;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;16;-314.8951,294.0011;Inherit;True;Property;_BumpMap;Normal Map;6;0;Create;False;0;0;0;False;0;False;-1;None;d54d1b6ca8356d2449ea68671c532b9d;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;229.3103,-57.5645;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;21;-535.864,898.6777;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;15;-225.6667,933.0629;Inherit;True;Property;_Occlusion;Occlusion;8;0;Create;False;0;0;0;False;0;False;-1;None;91141010aea127842a1cf7348becb18e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;14;-32.7967,154.6003;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;34;357.3999,175;Float;False;True;-1;2;;0;0;StandardSpecular;NOT_Lonely/NOT_MaskedPBR;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;16;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;29;0;1;0
WireConnection;18;0;29;0
WireConnection;18;1;19;0
WireConnection;2;0;18;0
WireConnection;2;1;6;0
WireConnection;5;0;18;0
WireConnection;5;1;10;0
WireConnection;11;0;5;0
WireConnection;11;1;2;0
WireConnection;11;2;12;1
WireConnection;3;0;18;0
WireConnection;3;1;8;0
WireConnection;4;0;18;0
WireConnection;4;1;9;0
WireConnection;13;0;11;0
WireConnection;13;1;3;0
WireConnection;13;2;12;2
WireConnection;20;0;19;0
WireConnection;31;0;30;0
WireConnection;31;1;33;0
WireConnection;21;0;17;4
WireConnection;21;1;20;0
WireConnection;14;0;13;0
WireConnection;14;1;4;0
WireConnection;14;2;12;3
WireConnection;34;0;14;0
WireConnection;34;1;16;0
WireConnection;34;2;31;0
WireConnection;34;3;17;0
WireConnection;34;4;21;0
WireConnection;34;5;15;2
ASEEND*/
//CHKSM=EF3F83A7EA9266C9155138698444A0A1FCC3B12B