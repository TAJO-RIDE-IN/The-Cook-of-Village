// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NOT_Lonely/NOTLonely_skybox"
{
	Properties
	{
		_Color("Sky Color", Color) = (0.2971698,0.6498447,1,1)
		_Exposure("Exposure", Float) = 1
		[Header(Fog)]_FogHeight("Fog Height", Range( 0 , 1)) = 1
		_FogSmoothness("Fog Smoothness", Range( 0.01 , 1)) = 0.01
		_FogFill("Fog Fill", Range( 0 , 1)) = 0.5
		_GroundFog("Ground Fog", Range( 0 , 1)) = 0.5
		[Header(Sun)]_SunDiskSize("Sun Disk Size", Range( 0 , 0.5)) = 0
		_SunDiskSharpness("Sun Disk Sharpness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Background"  "Queue" = "Background+0" "IgnoreProjector" = "True" "ForceNoShadowCasting" = "True" "IsEmissive" = "true"  "PreviewType"="Skybox" }
		Cull Off
		ZWrite Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 2.0
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float3 worldPos;
			float3 viewDir;
			float2 uv_texcoord;
		};

		uniform float _SunDiskSize;
		uniform float _SunDiskSharpness;
		uniform float4 _Color;
		uniform float _Exposure;
		uniform half _FogHeight;
		uniform half _FogSmoothness;
		uniform float _GroundFog;
		uniform half _FogFill;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float temp_output_41_0 = ( 1.0 - _SunDiskSize );
			float temp_output_45_0 = ( temp_output_41_0 * ( 1.0 - ( 0.99 + (0.0 + (_SunDiskSharpness - 0.0) * (0.01 - 0.0) / (1.0 - 0.0)) ) ) );
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult34 = dot( -ase_worldlightDir , i.viewDir );
			float temp_output_44_0 = saturate( dotResult34 );
			float dotResult46 = dot( temp_output_44_0 , temp_output_44_0 );
			float smoothstepResult53 = smoothstep( ( temp_output_41_0 - temp_output_45_0 ) , ( temp_output_41_0 + temp_output_45_0 ) , dotResult46);
			float dotResult52 = dot( ase_worldPos.y , 1.0 );
			#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float3 SunDisk59 = ( saturate( ( smoothstepResult53 * saturate( dotResult52 ) ) ) * ase_lightColor.a * ase_lightColor.rgb );
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float lerpResult21 = lerp( saturate( ( pow( (0.0 + (abs( i.uv_texcoord.y ) - 0.0) * (1.0 - 0.0) / (_FogHeight - 0.0)) , ( 1.0 - _FogSmoothness ) ) * ( 1.0 - ( step( ase_vertex3Pos.y , 0.0 ) * _GroundFog ) ) ) ) , 0.0 , _FogFill);
			half FOG_MASK22 = lerpResult21;
			float4 lerpResult24 = lerp( unity_FogColor , ( ( float4( SunDisk59 , 0.0 ) + _Color ) * _Exposure ) , FOG_MASK22);
			o.Emission = lerpResult24.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
}
/*ASEBEGIN
Version=18933
0;73;2560;1287;963.93;1120.874;1.70555;True;False
Node;AmplifyShaderEditor.CommentaryNode;30;-3170.477,-1060.153;Inherit;False;2420.706;734.9695;;25;59;58;56;57;55;53;54;51;52;49;50;48;46;45;44;42;40;41;34;38;36;33;32;31;66;Sun Disk;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-3095.209,-812.4874;Float;False;Property;_SunDiskSharpness;Sun Disk Sharpness;8;0;Create;True;0;0;0;False;0;False;0;0.73;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;31;-3131.158,-637.7501;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NegateNode;33;-2868.092,-506.7691;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCRemapNode;66;-2783.662,-802.5994;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;32;-3130.724,-493.8741;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;34;-2695.569,-505.553;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-2578.569,-827.976;Inherit;False;2;2;0;FLOAT;0.99;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-2930.671,-1017.503;Float;False;Property;_SunDiskSize;Sun Disk Size;7;1;[Header];Create;True;1;Sun;0;0;False;0;False;0;0.01;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;42;-2409.642,-823.6839;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;44;-2294.297,-571.0461;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;41;-2589.657,-1011.145;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-2188.384,-842.6262;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;5;-2648.674,12.31704;Inherit;False;2363;484;;20;17;21;22;69;19;15;11;13;18;16;14;9;12;10;29;73;75;76;78;79;FOG MASK;0,0.4035376,0.7830189,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;48;-1955.287,-503.132;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;46;-2080.343,-593.1281;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;69;-1698.525,270.8871;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DotProductOpNode;52;-1749.533,-456.4964;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;-1915.21,-873.0551;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-2462.932,60.54001;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;49;-1914.154,-1002.584;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;51;-1806.744,-725.4631;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-1556.093,416.316;Inherit;False;Property;_GroundFog;Ground Fog;6;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-2600.674,380.3169;Half;False;Property;_FogSmoothness;Fog Smoothness;4;0;Create;True;0;0;0;False;0;False;0.01;0.49;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-2225.167,335.6879;Half;False;Constant;_Float40;Float 40;55;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;75;-1499.879,286.4352;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;12;-2190.252,81.79713;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;54;-1615.283,-459.9592;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;53;-1744.81,-924.621;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-2225.488,244.8277;Half;False;Constant;_Float39;Float 39;55;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-2600.674,252.3171;Half;False;Property;_FogHeight;Fog Height;3;1;[Header];Create;True;1;Fog;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-1464.104,-879.1121;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-1345.267,257.6675;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;14;-1920.167,385.0876;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;15;-1982.857,66.08779;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;76;-1216.053,137.9017;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;57;-1291.89,-872.9293;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;16;-1728.167,65.08779;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LightColorNode;56;-1324.593,-773.4542;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-1041.44,63.26032;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-1124.913,-867.5862;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-888.4421,175.1295;Half;False;Constant;_Float41;Float 41;55;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;18;-851.8923,57.34613;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-960.4421,268.1293;Half;False;Property;_FogFill;Fog Fill;5;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;59;-979.9069,-868.2901;Float;False;SunDisk;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;21;-690.4431,57.12947;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;23;90.13216,53.82786;Inherit;False;Property;_Color;Sky Color;1;0;Create;False;0;0;0;False;0;False;0.2971698,0.6498447,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;65;123.4539,-67.5492;Inherit;False;59;SunDisk;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;22;-479.791,52.58797;Half;False;FOG_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;61;331.2397,-20.31915;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;68;326.5586,114.011;Inherit;False;Property;_Exposure;Exposure;2;0;Create;True;0;0;0;False;0;False;1;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;523.5582,-20.3887;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FogAndAmbientColorsNode;25;471.8307,-130.4983;Inherit;False;unity_FogColor;0;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;64;514.3024,95.77274;Inherit;False;22;FOG_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;24;755.7507,-42.76809;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;90;1031.561,-43.33808;Float;False;True;-1;0;;0;0;Unlit;NOT_Lonely/NOTLonely_skybox;False;False;False;False;True;True;True;True;True;True;True;True;False;False;True;True;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0;True;False;0;False;Background;;Background;All;16;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;1;PreviewType=Skybox;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;33;0;31;0
WireConnection;66;0;38;0
WireConnection;34;0;33;0
WireConnection;34;1;32;0
WireConnection;40;1;66;0
WireConnection;42;0;40;0
WireConnection;44;0;34;0
WireConnection;41;0;36;0
WireConnection;45;0;41;0
WireConnection;45;1;42;0
WireConnection;46;0;44;0
WireConnection;46;1;44;0
WireConnection;52;0;48;2
WireConnection;50;0;41;0
WireConnection;50;1;45;0
WireConnection;49;0;41;0
WireConnection;49;1;45;0
WireConnection;51;0;46;0
WireConnection;75;0;69;2
WireConnection;12;0;29;2
WireConnection;54;0;52;0
WireConnection;53;0;51;0
WireConnection;53;1;49;0
WireConnection;53;2;50;0
WireConnection;55;0;53;0
WireConnection;55;1;54;0
WireConnection;78;0;75;0
WireConnection;78;1;79;0
WireConnection;14;0;10;0
WireConnection;15;0;12;0
WireConnection;15;1;13;0
WireConnection;15;2;9;0
WireConnection;15;3;13;0
WireConnection;15;4;11;0
WireConnection;76;0;78;0
WireConnection;57;0;55;0
WireConnection;16;0;15;0
WireConnection;16;1;14;0
WireConnection;73;0;16;0
WireConnection;73;1;76;0
WireConnection;58;0;57;0
WireConnection;58;1;56;2
WireConnection;58;2;56;1
WireConnection;18;0;73;0
WireConnection;59;0;58;0
WireConnection;21;0;18;0
WireConnection;21;1;19;0
WireConnection;21;2;17;0
WireConnection;22;0;21;0
WireConnection;61;0;65;0
WireConnection;61;1;23;0
WireConnection;67;0;61;0
WireConnection;67;1;68;0
WireConnection;24;0;25;0
WireConnection;24;1;67;0
WireConnection;24;2;64;0
WireConnection;90;2;24;0
ASEEND*/
//CHKSM=E5BD5200C1D84A6D6FFB3EC0DFD0DF1A25916C15