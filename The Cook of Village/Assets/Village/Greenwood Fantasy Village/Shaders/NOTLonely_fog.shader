// Upgrade NOTE: upgraded instancing buffer 'NOT_LonelyNOTLonely_Fog' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NOT_Lonely/NOTLonely_Fog"
{
	Properties
	{
		[NoScaleOffset][SingleLineTexture]_Mask("Mask", 2D) = "white" {}
		[NoScaleOffset][SingleLineTexture]_Noise("Noise", 2D) = "white" {}
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_NoiseContrast("Noise Contrast", Range( 0 , 10)) = 2
		_AnimSpeed("Anim Speed", Float) = 1
		_DepthFade("Depth Fade", Range( 0 , 20)) = 1
		_NoiseTiling("Noise Tiling", Vector) = (1,1,0,0)
		_NoiseOffset("Noise Offset", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.5
		#pragma multi_compile_instancing
		#pragma surface surf Unlit alpha:fade keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap vertex:vertexDataFunc 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
			float4 screenPos;
			float eyeDepth;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform sampler2D _Mask;
		uniform sampler2D _Noise;
		uniform float _AnimSpeed;
		uniform float2 _NoiseTiling;
		uniform float _NoiseContrast;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _DepthFade;

		UNITY_INSTANCING_BUFFER_START(NOT_LonelyNOTLonely_Fog)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
#define _Color_arr NOT_LonelyNOTLonely_Fog
			UNITY_DEFINE_INSTANCED_PROP(float2, _NoiseOffset)
#define _NoiseOffset_arr NOT_LonelyNOTLonely_Fog
		UNITY_INSTANCING_BUFFER_END(NOT_LonelyNOTLonely_Fog)

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.eyeDepth = -UnityObjectToViewPos( v.vertex.xyz ).z;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 _Color_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color_arr, _Color);
			o.Emission = ( i.vertexColor * _Color_Instance ).rgb;
			float2 uv_Mask5 = i.uv_texcoord;
			float mulTime56 = _Time.y * -0.02;
			float2 appendResult26 = (float2(_AnimSpeed , 0.0));
			float2 _NoiseOffset_Instance = UNITY_ACCESS_INSTANCED_PROP(_NoiseOffset_arr, _NoiseOffset);
			float2 uv_TexCoord29 = i.uv_texcoord * _NoiseTiling + _NoiseOffset_Instance;
			float2 panner24 = ( mulTime56 * appendResult26 + uv_TexCoord29);
			float4 tex2DNode7 = tex2D( _Noise, panner24 );
			float clampResult104 = clamp( tex2DNode7.r , 0.0 , 1.0 );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth34 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth34 = abs( ( screenDepth34 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DepthFade ) );
			float clampResult42 = clamp( distanceDepth34 , 0.0 , 1.0 );
			float cameraDepthFade43 = (( i.eyeDepth -_ProjectionParams.y - 0.0 ) / 1.0);
			float clampResult45 = clamp( cameraDepthFade43 , 0.0 , 1.0 );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV47 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode47 = ( 0.0 + 1.5 * pow( 1.0 - fresnelNdotV47, 5.0 ) );
			float clampResult49 = clamp( fresnelNode47 , 0.0 , 1.0 );
			o.Alpha = saturate( ( i.vertexColor.a * ( ( tex2D( _Mask, uv_Mask5 ).r * pow( abs( ( clampResult104 + tex2DNode7.r ) ) , _NoiseContrast ) ) * _Color_Instance.a * clampResult42 * clampResult45 * ( 1.0 - clampResult49 ) ) ) );
		}

		ENDCG
	}
}
/*ASEBEGIN
Version=18933
0;73;2560;1286;800.065;468.4063;1.020302;True;False
Node;AmplifyShaderEditor.RangedFloatNode;27;-1234.108,684.7516;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;103;-1543.093,473.4969;Inherit;False;InstancedProperty;_NoiseOffset;Noise Offset;7;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;102;-1552.093,299.1969;Inherit;False;Property;_NoiseTiling;Noise Tiling;6;0;Create;True;0;0;0;False;0;False;1,1;0.01,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;25;-1243.579,592.6126;Float;False;Property;_AnimSpeed;Anim Speed;4;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;56;-1053.361,764.588;Inherit;False;1;0;FLOAT;-0.02;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;26;-1023.108,619.7516;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-1160.77,351.9935;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;1,1;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;24;-829.4327,502.1707;Inherit;False;3;0;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT;-0.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;7;-501,146;Inherit;True;Property;_Noise;Noise;1;2;[NoScaleOffset];[SingleLineTexture];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;104;-155.9641,54.7849;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;105;-9.571198,111.2353;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;94;-494.7008,459.0133;Float;False;Property;_NoiseContrast;Noise Contrast;3;0;Create;True;0;0;0;False;0;False;2;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;92;-401.8681,562.5851;Float;False;Property;_DepthFade;Depth Fade;5;0;Create;True;0;0;0;False;0;False;1;0;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;106;158.4549,137.2023;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;47;77.04131,575.3717;Inherit;False;Standard;TangentNormal;ViewDir;False;False;5;0;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1.5;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.CameraDepthFade;43;239.6004,424.7783;Inherit;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-506,-75;Inherit;True;Property;_Mask;Mask;0;2;[NoScaleOffset];[SingleLineTexture];Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;49;297.0064,575.5931;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;34;234.9236,309.1715;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;93;326.4993,145.213;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;45;477.6917,396.179;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;506.9669,41.4697;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;42;479.1007,264.2784;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-392.3517,-322.2339;Float;False;InstancedProperty;_Color;Color;2;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;48;459.1518,571.5311;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;107;685.189,-308.5581;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;789.926,197.1654;Inherit;False;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;1001.089,25.54199;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;1006.289,-135.6577;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;111;1262.986,127.45;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;110;1438.174,-17.25109;Float;False;True;-1;3;;0;0;Unlit;NOT_Lonely/NOTLonely_Fog;False;False;False;False;True;True;True;True;True;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;26;0;25;0
WireConnection;26;1;27;0
WireConnection;29;0;102;0
WireConnection;29;1;103;0
WireConnection;24;0;29;0
WireConnection;24;2;26;0
WireConnection;24;1;56;0
WireConnection;7;1;24;0
WireConnection;104;0;7;1
WireConnection;105;0;104;0
WireConnection;105;1;7;1
WireConnection;106;0;105;0
WireConnection;49;0;47;0
WireConnection;34;0;92;0
WireConnection;93;0;106;0
WireConnection;93;1;94;0
WireConnection;45;0;43;0
WireConnection;6;0;5;1
WireConnection;6;1;93;0
WireConnection;42;0;34;0
WireConnection;48;0;49;0
WireConnection;23;0;6;0
WireConnection;23;1;20;4
WireConnection;23;2;42;0
WireConnection;23;3;45;0
WireConnection;23;4;48;0
WireConnection;109;0;107;4
WireConnection;109;1;23;0
WireConnection;108;0;107;0
WireConnection;108;1;20;0
WireConnection;111;0;109;0
WireConnection;110;2;108;0
WireConnection;110;9;111;0
ASEEND*/
//CHKSM=060CD7A08DC341AB22ECE7E30A425D8401C859C2