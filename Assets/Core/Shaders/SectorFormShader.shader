Shader "Unlit/SectorFormShader"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _Angle("Angle", Range(0, 360)) = 45.0
    }
        SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "PreviewType"="Plane"}

        Pass
        {
            Blend SrcAlpha OneminusSrcAlpha
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            half _Angle;
            half4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 pos = i.uv * 2.0 - 1.0;
                float theta = degrees(atan2(pos.x, pos.y)) + 180.0;

                float circle = length(pos) <= 1.0;
                float sector = theta <= _Angle;

                return _Color * (circle * sector);
            }
            ENDCG
        }
    }
}
