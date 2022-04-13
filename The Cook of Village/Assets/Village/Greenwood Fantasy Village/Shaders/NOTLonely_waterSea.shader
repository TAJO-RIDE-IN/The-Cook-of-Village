// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NOT_Lonely/NOTLonely_waterSea"
{
	Properties
	{
		_Color("Color", Color) = (0,0.2,0.2,1)
		_BumpMap("Normal Map", 2D) = "bump" {}
		_NormalIntensity("Normal Intensity", Range( 0 , 2)) = 1
		_Gloss("Gloss", Range( 0 , 1)) = 0.8
		_Speed("Speed", Float) = 1
		_FoamTexture("Foam Texture", 2D) = "white" {}
		_FoamColor("Foam Color", Color) = (1,1,1,1)
		_FoamIntensity("Foam Intensity", Range( 0 , 10)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _BumpMap;
		uniform float _Speed;
		uniform float4 _BumpMap_ST;
		uniform float _NormalIntensity;
		uniform float4 _Color;
		uniform float4 _FoamColor;
		uniform sampler2D _FoamTexture;
		uniform float4 _FoamTexture_ST;
		uniform float _FoamIntensity;
		uniform float _Gloss;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 appendResult12 = (float2(_Speed , 0.0));
			float2 temp_output_14_0 = ( appendResult12 * float2( 0.5,0 ) );
			float2 uv_BumpMap = i.uv_texcoord * _BumpMap_ST.xy + _BumpMap_ST.zw;
			float2 panner9 = ( _Time.x * temp_output_14_0 + ( uv_BumpMap * 2.0 ));
			float2 panner13 = ( _Time.x * appendResult12 + uv_BumpMap);
			o.Normal = BlendNormals( UnpackScaleNormal( tex2D( _BumpMap, panner9 ), _NormalIntensity ) , UnpackScaleNormal( tex2D( _BumpMap, panner13 ), _NormalIntensity ) );
			float2 uv_FoamTexture = i.uv_texcoord * _FoamTexture_ST.xy + _FoamTexture_ST.zw;
			float2 panner50 = ( _Time.x * temp_output_14_0 + uv_FoamTexture);
			float4 tex2DNode22 = tex2D( _FoamTexture, panner50 );
			float lerpResult27 = lerp( tex2DNode22.a , 0.0 , pow( abs( 0.97 ) , _FoamIntensity ));
			float4 lerpResult25 = lerp( _Color , ( _FoamColor * tex2DNode22 ) , lerpResult27);
			o.Albedo = lerpResult25.rgb;
			o.Smoothness = _Gloss;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=18933
0;73;2560;1287;834.1694;1188.067;1.634893;True;False
Node;AmplifyShaderEditor.RangedFloatNode;11;-1650.755,110.0217;Inherit;False;Property;_Speed;Speed;4;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;12;-1478.755,117.0217;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1557.755,-16.97827;Inherit;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-1640.405,-153.2776;Inherit;False;0;15;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-926.795,-523.6682;Inherit;False;0;22;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-1336.155,-156.5783;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1175.226,21.44909;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;33;323.9037,-347.3645;Inherit;False;Constant;_Float2;Float 2;7;0;Create;True;0;0;0;False;0;False;0.97;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;10;-1334.089,295.3922;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;50;-601.9908,-483.67;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-10.09647,-241.3643;Inherit;False;Property;_FoamIntensity;Foam Intensity;7;0;Create;True;0;0;0;False;0;False;1;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;31;497.9036,-296.3643;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;15;-1047.527,-284.4356;Inherit;True;Property;_BumpMap;Normal Map;1;0;Create;False;0;0;0;False;0;False;None;None;False;bump;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.PannerNode;13;-981.5964,159.597;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;9;-952.7556,-31.37833;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;16;-732.9308,-102.5693;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-735.3907,167.1626;Inherit;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-717.4712,431.755;Inherit;False;Property;_NormalIntensity;Normal Intensity;2;0;Create;True;0;0;0;False;0;False;1;1;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;29;644.9037,-297.3643;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;23;26.90352,-965.3647;Inherit;False;Property;_FoamColor;Foam Color;6;0;Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;640.9036,-496.3643;Inherit;False;Constant;_Float1;Float 1;6;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;22;-59.11732,-670.348;Inherit;True;Property;_FoamTexture;Foam Texture;5;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.UnpackScaleNormalNode;17;-382.5818,-90.93079;Inherit;False;Tangent;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;358.9036,-692.3643;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;26;23.90352,-444.3643;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;0,0.2,0.2,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;27;843.9036,-512.3643;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.UnpackScaleNormalNode;19;-375.4712,170.755;Inherit;False;Tangent;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;37;1222.278,-14.01007;Inherit;False;Property;_Gloss;Gloss;3;0;Create;True;0;0;0;False;0;False;0.8;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;21;-113.4712,52.755;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;25;1026.904,-707.3644;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;52;1676.366,-185.2747;Float;False;True;-1;2;;0;0;Standard;NOT_Lonely/NOTLonely_waterSea;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;16;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;0;11;0
WireConnection;7;0;6;0
WireConnection;7;1;8;0
WireConnection;14;0;12;0
WireConnection;50;0;51;0
WireConnection;50;2;14;0
WireConnection;50;1;10;1
WireConnection;31;0;33;0
WireConnection;13;0;6;0
WireConnection;13;2;12;0
WireConnection;13;1;10;1
WireConnection;9;0;7;0
WireConnection;9;2;14;0
WireConnection;9;1;10;1
WireConnection;16;0;15;0
WireConnection;16;1;9;0
WireConnection;18;0;15;0
WireConnection;18;1;13;0
WireConnection;29;0;31;0
WireConnection;29;1;30;0
WireConnection;22;1;50;0
WireConnection;17;0;16;0
WireConnection;17;1;20;0
WireConnection;24;0;23;0
WireConnection;24;1;22;0
WireConnection;27;0;22;4
WireConnection;27;1;28;0
WireConnection;27;2;29;0
WireConnection;19;0;18;0
WireConnection;19;1;20;0
WireConnection;21;0;17;0
WireConnection;21;1;19;0
WireConnection;25;0;26;0
WireConnection;25;1;24;0
WireConnection;25;2;27;0
WireConnection;52;0;25;0
WireConnection;52;1;21;0
WireConnection;52;4;37;0
ASEEND*/
//CHKSM=9DC1AE4FD8FDE3557378C3935369DDE5C190886D