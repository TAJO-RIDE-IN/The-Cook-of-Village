// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NOT_Lonely/Ground Vertex Blend (PBR)"
{
	Properties
	{
		_TriplanarHardness("Triplanar Hardness", Range( 1.1 , 25)) = 5
		[NoScaleOffset][SingleLineTexture]_TextureY01("Grass, Albedo(RGB), Smoothness(A)", 2D) = "white" {}
		[NoScaleOffset][SingleLineTexture]_NormalY01("Grass Normal", 2D) = "bump" {}
		_ScaleandOffsetY01("Grass Scale and Offset", Vector) = (1,1,0,0)
		_MetallicY01("Grass Metallic", Range( 0 , 1)) = 0
		_GlossinessY01("Grass Smoothness", Range( 0 , 1)) = 0.5
		[NoScaleOffset][SingleLineTexture]_BlendY01("Grass Blend Map", 2D) = "white" {}
		_HeightBlendHardness("Height Blend Hardness", Range( 0 , 1)) = 1
		[NoScaleOffset][SingleLineTexture]_TextureY02("Ground_1, Albedo(RGB), Smoothness(A)", 2D) = "white" {}
		[NoScaleOffset][SingleLineTexture]_NormalY02("Ground_1 Normal", 2D) = "bump" {}
		_ScaleandOffsetY02("Ground_1 Scale and Offset", Vector) = (1,1,0,0)
		_MetallicY02("Ground_1 Metallic", Range( 0 , 1)) = 0
		_GlossinessY02("Ground_1 Smoothness", Range( 0 , 1)) = 0.5
		[NoScaleOffset][SingleLineTexture]_TextureY03("Ground_2, Albedo(RGB), Smoothness(A)", 2D) = "white" {}
		[NoScaleOffset][SingleLineTexture]_NormalY03("Ground_2 Normal", 2D) = "bump" {}
		_ScaleandOffsetY03("Ground_2 Scale and Offset", Vector) = (1,1,0,0)
		_MetallicY03("Ground_2 Metallic", Range( 0 , 1)) = 0
		_GlossinessY03("Ground_2 Smoothness", Range( 0 , 1)) = 0.5
		[NoScaleOffset][SingleLineTexture]_TextureXZ("Rock, Albedo(RGB), Smoothness(A)", 2D) = "white" {}
		[NoScaleOffset][SingleLineTexture]_NormalXZ1("Rock Normal", 2D) = "bump" {}
		_ScaleandOffsetXZ("Rock Scale and Offset", Vector) = (1,1,0,0)
		_MetallicXZ("Rock Metallic", Range( 0 , 1)) = 0
		_GlossinessXZ("Rock Smoothness", Range( 0 , 1)) = 0.5
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _NormalXZ1;
		uniform float4 _ScaleandOffsetXZ;
		uniform float _TriplanarHardness;
		uniform sampler2D _BlendY01;
		uniform float4 _ScaleandOffsetY01;
		uniform float _HeightBlendHardness;
		uniform sampler2D _NormalY01;
		uniform sampler2D _NormalY02;
		uniform float4 _ScaleandOffsetY02;
		uniform sampler2D _NormalY03;
		uniform float4 _ScaleandOffsetY03;
		uniform sampler2D _TextureY01;
		uniform sampler2D _TextureY02;
		uniform sampler2D _TextureY03;
		uniform sampler2D _TextureXZ;
		uniform float _MetallicXZ;
		uniform float _MetallicY01;
		uniform float _MetallicY02;
		uniform float _MetallicY03;
		uniform float _GlossinessXZ;
		uniform float _GlossinessY01;
		uniform float _GlossinessY02;
		uniform float _GlossinessY03;


		float3 PowerNormal218( float3 worldNormal, float triplanarHardness )
		{
			float3 powerNormal;	
			powerNormal = abs(normalize(worldNormal));
			powerNormal = pow(max(0.0001, powerNormal - 0.2) * 7,  triplanarHardness);
			powerNormal = normalize(max(powerNormal, 0.0001));
			powerNormal /= powerNormal.x + powerNormal.y + powerNormal.z;
			return powerNormal;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 appendResult40_g8 = (float2(ase_worldPos.z , ase_worldPos.y));
			float4 temp_output_4_0_g8 = ( 10.0 * _ScaleandOffsetXZ );
			float2 temp_output_24_0_g8 = (temp_output_4_0_g8).xy;
			float2 temp_output_25_0_g8 = (temp_output_4_0_g8).zw;
			float2 temp_output_328_0 = ( ( ( appendResult40_g8 + float2( 0.33,0.33 ) ) / temp_output_24_0_g8 ) + temp_output_25_0_g8 );
			float3 tex2DNode31 = UnpackNormal( tex2D( _NormalXZ1, temp_output_328_0 ) );
			float3 appendResult285 = (float3(0.0 , tex2DNode31.g , tex2DNode31.r));
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 worldNormal218 = ase_worldNormal;
			float triplanarHardness218 = _TriplanarHardness;
			float3 localPowerNormal218 = PowerNormal218( worldNormal218 , triplanarHardness218 );
			float3 break109 = localPowerNormal218;
			float2 appendResult41_g8 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 temp_output_328_55 = ( ( appendResult41_g8 / temp_output_24_0_g8 ) + temp_output_25_0_g8 );
			float3 tex2DNode294 = UnpackNormal( tex2D( _NormalXZ1, temp_output_328_55 ) );
			float3 appendResult299 = (float3(tex2DNode294.r , 0.0 , tex2DNode294.g));
			float2 appendResult42_g8 = (float2(( 1.0 - ase_worldPos.x ) , ase_worldPos.y));
			float2 temp_output_328_29 = ( ( ( appendResult42_g8 + float2( -0.33,-0.33 ) ) / temp_output_24_0_g8 ) + temp_output_25_0_g8 );
			float3 tex2DNode82 = UnpackNormal( tex2D( _NormalXZ1, temp_output_328_29 ) );
			float3 appendResult286 = (float3(tex2DNode82.r , tex2DNode82.g , 0.0));
			float3 break11_g12 = i.vertexColor.rgb;
			float4 temp_output_1_0_g8 = ( _ScaleandOffsetY01 * 10.0 );
			float2 temp_output_328_5 = ( ( appendResult41_g8 / (temp_output_1_0_g8).xy ) + (temp_output_1_0_g8).zw );
			float smoothstepResult15_g12 = smoothstep( 0.7 , 1.0 , ( ( 1.0 - max( ( 1.0 - ( break109.y * (sign( ase_worldNormal )).y ) ) , max( break11_g12.y , break11_g12.z ) ) ) + ( tex2D( _BlendY01, temp_output_328_5 ).r * ( _HeightBlendHardness * 0.78 ) ) ));
			float temp_output_152_0 = smoothstepResult15_g12;
			float temp_output_152_5 = saturate( ( break11_g12.z - smoothstepResult15_g12 ) );
			float temp_output_152_6 = saturate( ( break11_g12.y - smoothstepResult15_g12 ) );
			float temp_output_320_0 = ( 1.0 - saturate( ( temp_output_152_0 + temp_output_152_5 + temp_output_152_6 ) ) );
			float3 tex2DNode13 = UnpackNormal( tex2D( _NormalY01, temp_output_328_5 ) );
			float3 appendResult293 = (float3(tex2DNode13.r , 0.0 , tex2DNode13.g));
			float4 temp_output_2_0_g8 = ( _ScaleandOffsetY02 * 10.0 );
			float2 temp_output_328_6 = ( ( appendResult41_g8 / (temp_output_2_0_g8).xy ) + (temp_output_2_0_g8).zw );
			float3 tex2DNode19 = UnpackNormal( tex2D( _NormalY02, temp_output_328_6 ) );
			float3 appendResult292 = (float3(tex2DNode19.r , 0.0 , tex2DNode19.g));
			float4 temp_output_3_0_g8 = ( _ScaleandOffsetY03 * 10.0 );
			float2 temp_output_328_7 = ( ( appendResult41_g8 / (temp_output_3_0_g8).xy ) + (temp_output_3_0_g8).zw );
			float3 tex2DNode26 = UnpackNormal( tex2D( _NormalY03, temp_output_328_7 ) );
			float3 appendResult291 = (float3(tex2DNode26.r , 0.0 , tex2DNode26.g));
			float3 normalizeResult244 = normalize( ( ( ( ( appendResult285 * break109.x ) + ( appendResult299 * break109.y ) + ( appendResult286 * break109.z ) ) * temp_output_320_0 ) + ( ( ( appendResult293 * temp_output_152_0 ) + ( appendResult292 * temp_output_152_5 ) + ( appendResult291 * temp_output_152_6 ) ) * break109.y ) + ase_worldNormal ) );
			float3 ase_worldTangent = WorldNormalVector( i, float3( 1, 0, 0 ) );
			float3 ase_worldBitangent = WorldNormalVector( i, float3( 0, 1, 0 ) );
			float3x3 ase_worldToTangent = float3x3( ase_worldTangent, ase_worldBitangent, ase_worldNormal );
			float3 worldToTangentPos331 = mul( ase_worldToTangent, normalizeResult244);
			o.Normal = worldToTangentPos331;
			float4 temp_output_148_0 = ( ( ( ( tex2D( _TextureY01, temp_output_328_5 ) * temp_output_152_0 ) + ( tex2D( _TextureY02, temp_output_328_6 ) * temp_output_152_5 ) + ( tex2D( _TextureY03, temp_output_328_7 ) * temp_output_152_6 ) ) * break109.y ) + ( temp_output_320_0 * ( ( tex2D( _TextureXZ, temp_output_328_0 ) * break109.x ) + ( tex2D( _TextureXZ, temp_output_328_55 ) * break109.y ) + ( tex2D( _TextureXZ, temp_output_328_29 ) * break109.z ) ) ) );
			o.Albedo = temp_output_148_0.rgb;
			o.Metallic = ( ( temp_output_320_0 * _MetallicXZ ) + ( ( ( _MetallicY01 * temp_output_152_0 ) + ( _MetallicY02 * temp_output_152_5 ) + ( _MetallicY03 * temp_output_152_6 ) ) * break109.y ) );
			o.Smoothness = ( (temp_output_148_0).a * ( ( temp_output_320_0 * _GlossinessXZ ) + ( ( ( _GlossinessY01 * temp_output_152_0 ) + ( _GlossinessY02 * temp_output_152_5 ) + ( _GlossinessY03 * temp_output_152_6 ) ) * break109.y ) ) );
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				half4 color : COLOR0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.color = v.color;
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				surfIN.vertexColor = IN.color;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "NOT_Lonely/Ground Vertex Blend (Lambert, No normal)"
}
/*ASEBEGIN
Version=18933
0;73;2560;1286;2586.31;-996.2092;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;220;-6421.478,500.9414;Inherit;False;1090.223;359.4515;Normal direct mask;6;109;218;10;233;314;315;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;75;-6471.606,939.6324;Inherit;False;1139.697;1053.447;UV coordinates;9;222;225;224;223;221;62;63;64;61;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;233;-6075.832,577.7766;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;10;-6063.155,745.5916;Inherit;False;Property;_TriplanarHardness;Triplanar Hardness;0;0;Create;False;0;0;0;False;0;False;5;5.9;1.1;25;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;62;-6432.917,1312.091;Inherit;False;Property;_ScaleandOffsetY02;Ground_1 Scale and Offset;10;0;Create;False;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;63;-6444.284,1577.685;Inherit;False;Property;_ScaleandOffsetY03;Ground_2 Scale and Offset;15;0;Create;False;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;222;-6170.522,1443.9;Inherit;False;Constant;_Float0;Float 0;23;0;Create;True;0;0;0;False;0;False;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;61;-6420.619,1034.576;Inherit;False;Property;_ScaleandOffsetY01;Grass Scale and Offset;3;0;Create;False;0;0;0;False;0;False;1,1,0,0;5,5,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;64;-6412.743,1811.161;Inherit;False;Property;_ScaleandOffsetXZ;Rock Scale and Offset;20;0;Create;False;0;0;0;False;0;False;1,1,0,0;10,10,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CustomExpressionNode;218;-5765.757,586.4744;Inherit;False;float3 powerNormal@	$powerNormal = abs(normalize(worldNormal))@$powerNormal = pow(max(0.0001, powerNormal - 0.2) * 7,  triplanarHardness)@$powerNormal = normalize(max(powerNormal, 0.0001))@$powerNormal /= powerNormal.x + powerNormal.y + powerNormal.z@$return powerNormal@;3;Create;2;True;worldNormal;FLOAT3;0,0,0;In;;Inherit;False;True;triplanarHardness;FLOAT;0;In;;Inherit;False;PowerNormal;True;False;0;;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;224;-5976.716,1491.794;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;221;-5976.973,1272.755;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;10;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;225;-5980.765,1617.263;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;223;-5975.368,1377.119;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.FunctionNode;328;-5776.803,1414.711;Inherit;False;NOTLonely_GroundVertexBlend_UVcoordinate;-1;;8;099b126e870f78747a4bfba7e2948403;0;4;1;FLOAT4;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;3;FLOAT4;0,0,0,0;False;4;FLOAT4;0,0,0,0;False;6;FLOAT2;5;FLOAT2;6;FLOAT2;7;FLOAT2;0;FLOAT2;55;FLOAT2;29
Node;AmplifyShaderEditor.SignOpNode;314;-5713.607,721.0425;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;89;-4154.58,-1030.938;Inherit;False;1084.051;611.5423;Blend Map;5;83;152;86;14;327;;1,1,1,1;0;0
Node;AmplifyShaderEditor.BreakToComponentsNode;109;-5483.254,578.2073;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.CommentaryNode;27;-4255.68,1757.735;Inherit;False;844.0945;1547.801;Ground_01 input;10;294;82;81;29;30;31;80;28;79;306;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ComponentMaskNode;315;-5550.373,715.7603;Inherit;False;False;True;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;127;-4258.022,1657.82;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;302;-4255.15,1605.765;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;129;-4253.068,1551.655;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;83;-4130.2,-788.4954;Inherit;False;Property;_HeightBlendHardness;Height Blend Hardness;7;0;Create;False;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;147;-4336.348,2835.553;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;146;-4334.062,2709.482;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;297;-4335.787,2770.93;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;189;-4307.283,2734.561;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;16;-3817.607,-346.9997;Inherit;False;387.5981;632.4281;Grass input;4;15;13;11;12;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;81;-4204.129,3073.686;Inherit;True;Property;_NormalXZ1;Rock Normal;19;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;None;None;True;bump;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.WireNode;298;-4303.877,2796.257;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;190;-4304.283,2865.561;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;21;-3817.398,307.5923;Inherit;False;391.1689;627.8712;Ground_01 input;4;17;18;20;19;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WireNode;128;-4229.026,1670.918;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-4110.579,-980.9368;Inherit;True;Property;_BlendY01;Grass Blend Map;6;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;13;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;22;-3826.753,958.8231;Inherit;False;401.1675;602.8707;Ground_01 input;4;26;25;23;24;;1,1,1,1;0;0
Node;AmplifyShaderEditor.VertexColorNode;86;-4101.107,-688.3864;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;316;-4385.255,-748.6683;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;327;-3813.547,-811.4271;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.78;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;130;-4226.395,1567.249;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;303;-4226.721,1618.4;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-3762.249,62.4649;Inherit;True;Property;_NormalY01;Grass Normal;2;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;14;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;26;-3773.907,1357.407;Inherit;True;Property;_NormalY03;Ground_2 Normal;14;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;14;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;187;-4305.607,1969.35;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;294;-3760.32,2816.831;Inherit;True;Property;_TextureSample0;Texture Sample 0;21;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;131;-3414.516,1564.694;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;19;-3764.552,706.1761;Inherit;True;Property;_NormalY02;Ground_1 Normal;9;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;14;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;152;-3583.51,-841.0732;Inherit;False;NOTLonely_GroundVertexBlend_TopTexturesMasks;-1;;12;42dddd6b014cdd142b23116d9318c762;0;4;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;3;FLOAT;0;FLOAT;5;FLOAT;6
Node;AmplifyShaderEditor.WireNode;145;-4305.496,2065.481;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;132;-3414.2,1669.512;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;304;-3421.259,1616.294;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;79;-4190.92,2327.934;Inherit;True;Property;_TextureXZ;Rock, Albedo(RGB), Smoothness(A);18;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.WireNode;307;-4307.22,2004.88;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;122;-3936.672,-368.8249;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;31;-3769.873,2617.278;Inherit;True;Property;_nXZ0;nXZ0;21;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;82;-3770.77,3034.358;Inherit;True;Property;_nXZ1;nXZ1;21;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;299;-3132.168,2809.126;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;291;-3233.091,1387.723;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;292;-3263.341,751.3339;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;286;-3122.751,2952.903;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;293;-3307.298,141.4439;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;285;-3130.326,2684.135;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode;134;-3389.581,1689.897;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;306;-3791.174,2013.984;Inherit;True;Property;_TextureSample1;Texture Sample 1;18;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;80;-3785.944,2219.515;Inherit;True;Property;_xz1;xz1;18;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;28;-3774.183,1807.735;Inherit;True;Property;_xz0;xz0;18;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;23;-3776.753,1008.823;Inherit;True;Property;_TextureY03;Ground_2, Albedo(RGB), Smoothness(A);13;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;13;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;123;-3871.175,-384.5443;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;305;-3382.302,1632.087;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;17;-3767.398,357.5923;Inherit;True;Property;_TextureY02;Ground_1, Albedo(RGB), Smoothness(A);8;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;13;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;11;-3765.095,-288.0797;Inherit;True;Property;_TextureY01;Grass, Albedo(RGB), Smoothness(A);1;2;[NoScaleOffset];[SingleLineTexture];Create;False;0;0;0;False;0;False;13;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;317;-2887.725,1794.925;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;133;-3386.928,1577.015;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;312;-2731.62,2790.558;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;139;-2735.759,2666.156;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;143;-2734.354,2922.367;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;114;-2446.746,318.7346;Inherit;False;440.8467;630.893;Combine top textures;8;104;105;106;107;110;113;112;111;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;301;-2801.486,2056.341;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;99;-2763.199,765.284;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;136;-2797.419,2175.915;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;-2762.126,-242.6545;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;135;-2797.716,1942.433;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-3768.218,1279.153;Inherit;False;Property;_GlossinessY03;Ground_2 Smoothness;17;0;Create;False;0;0;0;False;0;False;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-3758.864,627.9222;Inherit;False;Property;_GlossinessY02;Ground_1 Smoothness;12;0;Create;False;0;0;0;False;0;False;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;-2770.239,1068.83;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;121;-2482.618,-387.1645;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;-2768.309,1410.405;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;318;-2747.106,1799.235;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;-2728.598,398.4087;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-3756.56,-17.7504;Inherit;False;Property;_GlossinessY01;Grass Smoothness;5;0;Create;False;0;0;0;False;0;False;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;-2794.919,139.2333;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-3768.218,1200.899;Inherit;False;Property;_MetallicY03;Ground_2 Metallic;16;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-3758.864,549.6685;Inherit;False;Property;_MetallicY02;Ground_1 Metallic;11;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-2763.379,1293.076;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;107;-2396.746,790.6283;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-3756.56,-96.00397;Inherit;False;Property;_MetallicY01;Grass Metallic;4;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;120;-2438.08,-340.0054;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;313;-2468.93,2643.061;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;104;-2393.059,368.7345;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;310;-2484.624,1933.167;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;94;-2783.809,12.8713;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;320;-2602.503,1796.333;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;-2755.254,648.5559;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;-2177.262,790.3522;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;106;-2390.409,646.9855;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;110;-2167.899,382.3701;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;-2738.786,531.1082;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;319;-2315.503,1841.333;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;101;-2765.498,1186.025;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;-2771.769,-113.2909;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-3706.762,2517.033;Inherit;False;Property;_GlossinessXZ;Rock Smoothness;22;0;Create;False;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;321;-2301.504,2500.183;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector;288;-1853.702,2144.168;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;148;-1600.685,1228.303;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;105;-2390.409,503.3427;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;323;-2279.32,2288.847;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-3734.523,2420.234;Inherit;False;Property;_MetallicXZ;Rock Metallic;21;0;Create;False;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-2177.263,642.8561;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;151;-1625.81,1705.396;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;322;-2270.363,2117.277;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;111;-2175.322,505.0638;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;150;-1594.905,1558.761;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;244;-1449.682,1697.698;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;203;-1433.811,1500.501;Inherit;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;149;-1603.309,1395.263;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;202;-1211.001,1532.558;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TransformPositionNode;331;-1231.31,1687.209;Inherit;False;World;Tangent;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;329;-861.6658,1365.615;Float;False;True;-1;2;;0;0;Standard;NOT_Lonely/Ground Vertex Blend (PBR);False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;NOT_Lonely/Ground Vertex Blend (Lambert, No normal);-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;218;0;233;0
WireConnection;218;1;10;0
WireConnection;224;0;63;0
WireConnection;224;1;222;0
WireConnection;221;0;61;0
WireConnection;221;1;222;0
WireConnection;225;0;222;0
WireConnection;225;1;64;0
WireConnection;223;0;62;0
WireConnection;223;1;222;0
WireConnection;328;1;221;0
WireConnection;328;2;223;0
WireConnection;328;3;224;0
WireConnection;328;4;225;0
WireConnection;314;0;233;0
WireConnection;109;0;218;0
WireConnection;315;0;314;0
WireConnection;127;0;109;2
WireConnection;302;0;109;1
WireConnection;129;0;109;0
WireConnection;147;0;328;29
WireConnection;146;0;328;0
WireConnection;297;0;328;55
WireConnection;189;0;146;0
WireConnection;298;0;297;0
WireConnection;190;0;147;0
WireConnection;128;0;127;0
WireConnection;14;1;328;5
WireConnection;316;0;109;1
WireConnection;316;1;315;0
WireConnection;327;0;83;0
WireConnection;130;0;129;0
WireConnection;303;0;302;0
WireConnection;13;1;328;5
WireConnection;26;1;328;7
WireConnection;187;0;328;0
WireConnection;294;0;81;0
WireConnection;294;1;298;0
WireConnection;131;0;130;0
WireConnection;19;1;328;6
WireConnection;152;1;14;1
WireConnection;152;2;327;0
WireConnection;152;3;86;0
WireConnection;152;4;316;0
WireConnection;145;0;328;29
WireConnection;132;0;128;0
WireConnection;304;0;303;0
WireConnection;307;0;328;55
WireConnection;122;0;109;1
WireConnection;31;0;81;0
WireConnection;31;1;189;0
WireConnection;82;0;81;0
WireConnection;82;1;190;0
WireConnection;299;0;294;1
WireConnection;299;2;294;2
WireConnection;291;0;26;1
WireConnection;291;2;26;2
WireConnection;292;0;19;1
WireConnection;292;2;19;2
WireConnection;286;0;82;1
WireConnection;286;1;82;2
WireConnection;293;0;13;1
WireConnection;293;2;13;2
WireConnection;285;1;31;2
WireConnection;285;2;31;1
WireConnection;134;0;132;0
WireConnection;306;0;79;0
WireConnection;306;1;307;0
WireConnection;80;0;79;0
WireConnection;80;1;145;0
WireConnection;28;0;79;0
WireConnection;28;1;187;0
WireConnection;23;1;328;7
WireConnection;123;0;122;0
WireConnection;305;0;304;0
WireConnection;17;1;328;6
WireConnection;11;1;328;5
WireConnection;317;0;152;0
WireConnection;317;1;152;5
WireConnection;317;2;152;6
WireConnection;133;0;131;0
WireConnection;312;0;299;0
WireConnection;312;1;305;0
WireConnection;139;0;285;0
WireConnection;139;1;133;0
WireConnection;143;0;286;0
WireConnection;143;1;134;0
WireConnection;301;0;306;0
WireConnection;301;1;305;0
WireConnection;99;0;292;0
WireConnection;99;1;152;5
WireConnection;136;0;80;0
WireConnection;136;1;134;0
WireConnection;92;0;11;0
WireConnection;92;1;152;0
WireConnection;135;0;28;0
WireConnection;135;1;133;0
WireConnection;100;0;23;0
WireConnection;100;1;152;6
WireConnection;121;0;123;0
WireConnection;103;0;291;0
WireConnection;103;1;152;6
WireConnection;318;0;317;0
WireConnection;96;0;17;0
WireConnection;96;1;152;5
WireConnection;95;0;293;0
WireConnection;95;1;152;0
WireConnection;102;0;25;0
WireConnection;102;1;152;6
WireConnection;107;0;95;0
WireConnection;107;1;99;0
WireConnection;107;2;103;0
WireConnection;120;0;121;0
WireConnection;313;0;139;0
WireConnection;313;1;312;0
WireConnection;313;2;143;0
WireConnection;104;0;92;0
WireConnection;104;1;96;0
WireConnection;104;2;100;0
WireConnection;310;0;135;0
WireConnection;310;1;301;0
WireConnection;310;2;136;0
WireConnection;94;0;15;0
WireConnection;94;1;152;0
WireConnection;320;0;318;0
WireConnection;98;0;20;0
WireConnection;98;1;152;5
WireConnection;113;0;107;0
WireConnection;113;1;120;0
WireConnection;106;0;94;0
WireConnection;106;1;98;0
WireConnection;106;2;102;0
WireConnection;110;0;104;0
WireConnection;110;1;120;0
WireConnection;97;0;18;0
WireConnection;97;1;152;5
WireConnection;319;0;320;0
WireConnection;319;1;310;0
WireConnection;101;0;24;0
WireConnection;101;1;152;6
WireConnection;93;0;12;0
WireConnection;93;1;152;0
WireConnection;321;0;313;0
WireConnection;321;1;320;0
WireConnection;148;0;110;0
WireConnection;148;1;319;0
WireConnection;105;0;93;0
WireConnection;105;1;97;0
WireConnection;105;2;101;0
WireConnection;323;0;320;0
WireConnection;323;1;30;0
WireConnection;112;0;106;0
WireConnection;112;1;120;0
WireConnection;151;0;321;0
WireConnection;151;1;113;0
WireConnection;151;2;288;0
WireConnection;322;0;320;0
WireConnection;322;1;29;0
WireConnection;111;0;105;0
WireConnection;111;1;120;0
WireConnection;150;0;323;0
WireConnection;150;1;112;0
WireConnection;244;0;151;0
WireConnection;203;0;148;0
WireConnection;149;0;322;0
WireConnection;149;1;111;0
WireConnection;202;0;203;0
WireConnection;202;1;150;0
WireConnection;331;0;244;0
WireConnection;329;0;148;0
WireConnection;329;1;331;0
WireConnection;329;3;149;0
WireConnection;329;4;202;0
ASEEND*/
//CHKSM=A6D9BFCC9D0B9F380322E58F63AFC472900D8DC6