Shader "Custom/AdjustableCurvedWorld"
{
    Properties
    {
		// Diffuse texture
		 _MainTex("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
		//This tells the shader that it will make adjustments to the vertex based on the vert function
		#pragma surface surf Lambert vertex:vert
		#pragma multi_compile __ HORIZON_WAVES 
		#pragma multi_compile __ BEND_ON
		
		// Global properties to be set by BendControllerRadial script
		uniform half3 _CurveOrigin;
		uniform half3 _ReferenceDirection;
		uniform half _Curvature;
		uniform half3 _Scale;
		uniform half _FlatMargin;

		// Per material properties
		sampler2D _MainTex;

		struct Input
		{
			half2 uv_MainTex;
		};

		half4 Bend(half4 v)
		{
			half4 wpos = mul(unity_ObjectToWorld, v);

			half2 xzDist = (wpos.xz - _CurveOrigin.xz) / _Scale.xz;
			half dist = length(xzDist);

			dist = max(0, dist - _FlatMargin);

			wpos.y -= dist * dist * _Curvature;

			wpos = mul(unity_WorldToObject, wpos);

			return wpos;
		}

		void vert(inout appdata_full v)
		{
			v.vertex = Bend(v.vertex);
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}

	Fallback "Diffuse"
}
