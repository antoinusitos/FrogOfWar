Shader "Custom/FogOfWarMask" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_BlurPower("BlurPower", Float) = 0.002
	}

	SubShader{
	Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "LightMode"="ForwardBase"}
	Blend SrcAlpha OneMinusSrcAlpha
	Lighting Off
	LOD 200

	CGPROGRAM
	#pragma target 3.0
	#pragma surface surf Lambert vertex:vert alpha:blend

	fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, float aten)
	{
		fixed4 _Color;
		color.rgb = s.Albedo;
		color.a = s.Alpha;
		return color;
	}

	fixed4 _Color;
	sampler2D _MainTex;
	float _BlurPower;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		half4 baseColor1 = Tex2D(_MainTex, IN.uv_MainTex * float2(-BlurPower, 0));
		half4 baseColor2 = Tex2D(_MainTex, IN.uv_MainTex + float2(0, -BlurPower));
		half4 baseColor3 = Tex2D(_MainTex, IN.uv_MainTex + float2(-BlurPower, 0));
		half4 baseColor4 = Tex2D(_MainTex, IN.uv_MainTex * float2(0, -BlurPower));
		half4 baseColor = 0.25 * (baseColor1 + baseColor2 + baseColor3 + baseColor4);
	}

	o.Albedo = _Color.rgb * baseColor.b;
	o.Alpha = _Color.A - baseColor.g;

	ENDCG
	}

	Fallback "Diffuse"
}