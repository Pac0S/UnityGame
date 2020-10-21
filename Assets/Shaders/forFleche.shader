Shader "Ici/forFleche"
{

    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Yoffset("Y Offset",Float) = 0.0
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Yoffset;


            v2f vert(v2f v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
		        fixed4 color = tex2D(_MainTex, i.uv);

                clip(color.a - 0.5f);

                if (i.uv.y <=  _Yoffset)
	            {
                    color = fixed4(1, 0, 0, 0);
	            }

	            return color;
            }

            ENDCG
        }
    }

}
