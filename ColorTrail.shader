// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ColorTrail"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		ScaleX("X Scale", Range(0, 100)) = 0.0
		ScaleY("Y Scale", Range(0, 100)) = 0.0
		OffsetX("X Offset", Range(0, 100)) = 0.0
		OffsetY("Y Offset", Range(0, 100)) = 0.0
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			#pragma enable_d3d11_debug_symbols

			sampler2D	_MainTex;
			float4		_MainTex_TexelSize;
			float		ScaleX;
			float		ScaleY;
			float		OffsetX;
			float		OffsetY;

			struct vertIn
			{
				float4 vertex : POSITION;
			};

			struct fragIn
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			fragIn vert(vertIn IN)
			{
				fragIn toFrag;

				toFrag.pos = UnityObjectToClipPos(IN.vertex);

				toFrag.uv.x = ((IN.vertex.x + OffsetX) * ScaleX) / _MainTex_TexelSize.z;
				toFrag.uv.y = ((IN.vertex.y + OffsetY) * ScaleY) / _MainTex_TexelSize.w;

				return toFrag;
			}

			fixed4 frag(fragIn IN) : SV_TARGET
			{
				float2 uv = frac(IN.uv);
				return tex2Dgrad(_MainTex, uv, ddx(IN.uv), ddy(IN.uv));
			}

			ENDCG
		}
	}
}