// Made with Amplify Shader Editor v1.9.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ASE/Additive_UV_D"
{
	Properties
	{
		_MainTexture("主贴图", 2D) = "white" {}
		_Desaturate("去色", Float) = 0
		_Refine("Refine", Vector) = (0,0,0,0)
		[Toggle(_ONCEUV_ON)] _OnceUV("OnceUV(T)_LoopUV(F)", Float) = 0
		_MainTexTilingandOffset("主贴图平铺及UV速度", Vector) = (0,0,0,0)
		[Toggle(_NOISE_ON)] _Noise("Noise", Float) = 0
		_NoiseTex("扰动贴图", 2D) = "white" {}
		_DisolveTexSpeed("扰动贴图UV速度", Vector) = (0,0,0,0)
		[Toggle(_DISSOLVE_ON)] _Dissolve("Dissolve", Float) = 0
		_DissolveTex("溶解贴图", 2D) = "white" {}
		_Smooth("Smooth", Float) = 0
		_Vector0("溶解UV速度", Vector) = (0,0,0,0)

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One One
		AlphaToMask Off
		Cull Off
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"

			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _NOISE_ON
			#pragma shader_feature_local _ONCEUV_ON
			#pragma shader_feature_local _DISSOLVE_ON


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_color : COLOR;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _MainTexture;
			uniform half2 _MainTexTilingandOffset;
			uniform half4 _MainTexture_ST;
			uniform sampler2D _NoiseTex;
			uniform half4 _NoiseTex_ST;
			uniform half2 _DisolveTexSpeed;
			uniform half _Desaturate;
			uniform half4 _Refine;
			uniform sampler2D _DissolveTex;
			uniform half2 _Vector0;
			uniform half4 _DissolveTex_ST;
			uniform half _Smooth;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_color = v.color;
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				o.ase_texcoord2 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				half2 uv_MainTexture = i.ase_texcoord1.xy * _MainTexture_ST.xy + _MainTexture_ST.zw;
				half2 appendResult15 = (half2(( ( _MainTexTilingandOffset.x * _Time.y ) + uv_MainTexture.x ) , ( ( _Time.y * _MainTexTilingandOffset.y ) + uv_MainTexture.y )));
				half4 texCoord2 = i.ase_texcoord2;
				texCoord2.xy = i.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				half2 appendResult4 = (half2(texCoord2.x , texCoord2.y));
				#ifdef _ONCEUV_ON
				half2 staticSwitch6 = ( uv_MainTexture + appendResult4 );
				#else
				half2 staticSwitch6 = appendResult15;
				#endif
				half2 uv_NoiseTex = i.ase_texcoord1.xy * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
				half2 appendResult21 = (half2(( uv_NoiseTex.x + ( _DisolveTexSpeed.x * _Time.y ) ) , ( uv_NoiseTex.y + ( _Time.y * _DisolveTexSpeed.y ) )));
				#ifdef _NOISE_ON
				half2 staticSwitch31 = ( staticSwitch6 + ( tex2D( _NoiseTex, appendResult21 ).r * texCoord2.z ) );
				#else
				half2 staticSwitch31 = staticSwitch6;
				#endif
				half4 tex2DNode1 = tex2D( _MainTexture, staticSwitch31 );
				half3 desaturateInitialColor60 = tex2DNode1.rgb;
				half desaturateDot60 = dot( desaturateInitialColor60, float3( 0.299, 0.587, 0.114 ));
				half3 desaturateVar60 = lerp( desaturateInitialColor60, desaturateDot60.xxx, _Desaturate );
				half3 temp_cast_1 = (_Refine.x).xxx;
				half3 lerpResult59 = lerp( ( pow( desaturateVar60 , temp_cast_1 ) * _Refine.y ) , ( desaturateVar60 * _Refine.z ) , _Refine.w);
				half2 uv_DissolveTex = i.ase_texcoord1.xy * _DissolveTex_ST.xy + _DissolveTex_ST.zw;
				half2 panner64 = ( 1.0 * _Time.y * _Vector0 + uv_DissolveTex);
				half4 texCoord47 = i.ase_texcoord2;
				texCoord47.xy = i.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				half lerpResult45 = lerp( _Smooth , -1.0 , texCoord47.w);
				half clampResult49 = clamp( ( ( tex2D( _DissolveTex, panner64 ).r * _Smooth ) - lerpResult45 ) , 0.0 , 1.0 );
				#ifdef _DISSOLVE_ON
				half staticSwitch50 = clampResult49;
				#else
				half staticSwitch50 = 1.0;
				#endif
				
				
				finalColor = ( i.ase_color * half4( lerpResult59 , 0.0 ) * staticSwitch50 * i.ase_color.a * tex2DNode1.a );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19200
Node;AmplifyShaderEditor.SimpleTimeNode;25;-1908.867,392.8531;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;52;-2257.792,295.6087;Inherit;False;Property;_DisolveTexSpeed;扰动贴图UV速度;7;0;Create;False;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;30;-2285.526,-684.6255;Inherit;False;Property;_MainTexTilingandOffset;主贴图平铺及UV速度;4;0;Create;False;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-2284.307,41.81162;Inherit;False;0;28;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1701.288,466.1421;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;10;-1870.924,-554.1831;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1703.467,219.6702;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1673.725,-451.4828;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-1478.108,439.36;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-2293.812,-198.4387;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1682.825,-647.7826;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-2295.172,-404.1259;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-1485.899,135.3361;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;21;-1312.302,334.7484;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;4;-1984.723,-173.3175;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-1446.226,-643.8828;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-1443.625,-433.2829;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;15;-1285.025,-561.9828;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;28;-990.4594,-21.56461;Inherit;True;Property;_NoiseTex;扰动贴图;6;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-1678.523,-242.1832;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;6;-965.0718,-270.1908;Inherit;False;Property;_OnceUV;OnceUV(T)_LoopUV(F);3;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-644.122,208.3717;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-560.9614,-216.3574;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;31;-331.4357,-287.6677;Inherit;False;Property;_Noise;Noise;5;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-88.09094,-350.9929;Inherit;True;Property;_MainTexture;主贴图;0;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;44;-705.256,642.6679;Inherit;False;Property;_Smooth;Smooth;10;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;47;-781.163,891.3371;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;46;-703.7319,755.1094;Inherit;False;Constant;_Float;Float;9;0;Create;True;0;0;0;False;0;False;-1;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;42;-868.113,419.3919;Inherit;True;Property;_DissolveTex;溶解贴图;9;0;Create;False;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;61;280.0543,-580.6156;Inherit;False;Property;_Desaturate;去色;1;0;Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;45;-507.489,704.7742;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;60;231.6547,-355.6199;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector4Node;56;51.57809,-55.32443;Inherit;False;Property;_Refine;Refine;2;0;Create;False;0;0;0;False;0;False;0,0,0,0;1,1,1,0.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-489.7487,558.3781;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;48;-284.2158,611.16;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;55;445.3661,-409.5443;Inherit;False;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;49;-132.1705,607.9925;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-127.4189,468.6174;Inherit;False;Constant;_Float1;Float;9;0;Create;True;0;0;0;False;0;False;1;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;651.4677,-411.9898;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;649.4059,-302.7547;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;59;877.7736,-377.6981;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.VertexColorNode;34;867.1893,-573.5261;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;50;68.97322,495.5422;Inherit;False;Property;_Dissolve;Dissolve;8;0;Create;False;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;1430.046,-534.76;Inherit;False;5;5;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;62;1692.774,-544.4701;Half;False;True;-1;2;ASEMaterialInspector;100;5;ASE/Additive_UV_D;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;4;1;False;;1;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;2;False;;True;3;False;;True;True;0;False;;0;False;;True;2;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.PannerNode;64;-1106.996,506.7882;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;65;-1284.587,672.9553;Inherit;False;Property;_Vector0;溶解UV速度;11;0;Create;False;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;63;-1376.872,533.9376;Inherit;False;0;42;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
WireConnection;22;0;25;0
WireConnection;22;1;52;2
WireConnection;24;0;52;1
WireConnection;24;1;25;0
WireConnection;12;0;10;0
WireConnection;12;1;30;2
WireConnection;19;0;18;2
WireConnection;19;1;22;0
WireConnection;11;0;30;1
WireConnection;11;1;10;0
WireConnection;20;0;18;1
WireConnection;20;1;24;0
WireConnection;21;0;20;0
WireConnection;21;1;19;0
WireConnection;4;0;2;1
WireConnection;4;1;2;2
WireConnection;13;0;11;0
WireConnection;13;1;3;1
WireConnection;14;0;12;0
WireConnection;14;1;3;2
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;28;1;21;0
WireConnection;5;0;3;0
WireConnection;5;1;4;0
WireConnection;6;1;15;0
WireConnection;6;0;5;0
WireConnection;29;0;28;1
WireConnection;29;1;2;3
WireConnection;32;0;6;0
WireConnection;32;1;29;0
WireConnection;31;1;6;0
WireConnection;31;0;32;0
WireConnection;1;1;31;0
WireConnection;42;1;64;0
WireConnection;45;0;44;0
WireConnection;45;1;46;0
WireConnection;45;2;47;4
WireConnection;60;0;1;0
WireConnection;60;1;61;0
WireConnection;43;0;42;1
WireConnection;43;1;44;0
WireConnection;48;0;43;0
WireConnection;48;1;45;0
WireConnection;55;0;60;0
WireConnection;55;1;56;1
WireConnection;49;0;48;0
WireConnection;57;0;55;0
WireConnection;57;1;56;2
WireConnection;58;0;60;0
WireConnection;58;1;56;3
WireConnection;59;0;57;0
WireConnection;59;1;58;0
WireConnection;59;2;56;4
WireConnection;50;1;51;0
WireConnection;50;0;49;0
WireConnection;35;0;34;0
WireConnection;35;1;59;0
WireConnection;35;2;50;0
WireConnection;35;3;34;4
WireConnection;35;4;1;4
WireConnection;62;0;35;0
WireConnection;64;0;63;0
WireConnection;64;2;65;0
ASEEND*/
//CHKSM=4A13ADCDDCD7F4C76DD6FC2FE0BCBCD3C0E97D0A