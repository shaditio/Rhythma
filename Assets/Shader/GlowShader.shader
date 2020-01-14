Shader "Custom/GlowShader" 
{
 Properties 
 {
  _ColorGradient("Color Gradient", Color) = (1, 1, 1, 1)
  _MainTex("Base (RGB)", Color) = (1, 1, 1, 1)
  _BumpMap("Bump Map", 2D) = "bump" {}
  _RimColor("Rim Color", Color) = (1, 1, 1, 1)
  _RimPower("Rim Power", float) = 1.0
 }
 SubShader {
  Tags { "RenderType"="Opaque" }

  CGPROGRAM
  #pragma surface surf Lambert

  struct Input {
   float4 color: Color;
   float2 uv_MainTex;
   float2 uv_BumpMap;
   float3 viewDir;
  };

  float4 _ColorGradient;
  float4 _MainTex;
  sampler2D _BumpMap;
  float4 _RimColor;
  float _RimPower;

  void surf (Input IN, inout SurfaceOutput o) 
  {
   IN.color = _ColorGradient;
   o.Albedo = _MainTex.rgb * IN.color;
   o.Normal = UnpackNormal(tex2D(_BumpMap,IN.uv_BumpMap));

   half rim  = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
   o.Emission = _RimColor.rgb * pow(rim, _RimPower);
  }
  ENDCG
 } 
 FallBack "Diffuse"
}