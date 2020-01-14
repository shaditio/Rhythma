Shader "Custom/FogShader"
{
    Properties
    {
        [Header(Textures and color)]
        _MainTex ("Fog texture", 2D) = "white" {}
 
        [Header(Behaviour)]
        _ScrollDirX ("Scroll along X", Range(-1.0, 1.0)) = 1.0
        _ScrollDirY ("Scroll along Y", Range(-1.0, 1.0)) = 1.0
        _Speed ("Speed", float) = 1.0
		_FogIntensity ("Fog Intensity", Range(0, 0.7)) = 0.5
    }
 
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"  }

		// turn on the blending for the alpha value
        Blend SrcAlpha OneMinusSrcAlpha

		// off for transparent or semi-transparent object
        ZWrite Off
		Cull Off
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           
            #include "UnityCG.cginc"
 
            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
 
            v2f vert(appdata_full v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }
 
            float _Speed;
            fixed _ScrollDirX;
            fixed _ScrollDirY;
			fixed _FogIntensity;
 
            fixed4 frag(v2f i) : SV_Target
            {
				// to control the fog directions
                i.uv += fixed2(_ScrollDirX, _ScrollDirY) * _Speed * _Time.x;
                fixed4 col = tex2D(_MainTex, i.uv);
				
				// vary the transparency value of the fog
                col.a *= _FogIntensity;

                return col;
            }
            ENDCG
        }
    }
}