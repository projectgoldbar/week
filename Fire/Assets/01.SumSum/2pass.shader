﻿Shader "Custom/2pass"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("NormalMap", 2D) = "bump"{}
		_OutLine("아웃라인",Range(0,10)) = 0.0
		_OutLineColor("아웃라인컬러", Color) = (0,0,0,0)
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }

			cull front
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Nolight vertex:vert noshadow noambient
			half _OutLine;
		fixed4 _OutLineColor;
			void vert(inout appdata_full v) {
			v.vertex.xyz = v.vertex.xyz + v.normal.xyz * _OutLine;
	}

			struct Input
			{
				float4 color:COLOR;
			};
			void surf(Input IN, inout SurfaceOutput o) {
			}
			float4 LightingNolight(SurfaceOutput s, float3 lightDir, float atten) {
			return _OutLineColor;
			}
			ENDCG

				cull back

				CGPROGRAM
	#pragma surface surf Toon
				sampler2D _MainTex;
			sampler2D _BumpMap;
			struct Input {
				float2 uv_MainTex;
				float2 uv_BumpMap;
			};

			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}

			float4 LightingToon(SurfaceOutput s, float3 lightDir, float atten) {
				float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

				ndotl = ndotl * 5;
				ndotl = ceil(ndotl) / 5;

				/*if (ndotl > 0.7) {
					ndotl = 1;
				}
				else {
					ndotl = 0.3;
				}
				*/
				float4 final;
				final.rgb = s.Albedo * ndotl * _LightColor0.rgb;
				final.a = s.Alpha;

				return final;
			}

			ENDCG
		}
			FallBack "Diffuse"
}