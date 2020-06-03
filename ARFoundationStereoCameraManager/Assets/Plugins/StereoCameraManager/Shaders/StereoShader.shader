// 単眼カメラの映像を左右に分割してステレオ表示するシェーダー
Shader "XR/StereoShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LeftCenterX("Left Center X", float) = 0.25
        _RightCenterX("Right Center X", float) = 0.75
        _CenterY("Center Y", float) = 0.5
        _MagScale("Magnification Scale", float) = 0.5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float _LeftCenterX;
            float _RightCenterX;
            float _CenterY;
            float _MagScale;
            fixed4 frag (v2f i) : SV_Target
            {
                float2 new_uv;
                float scale = 1.0f / _MagScale;
                // スクリーンの左右でそれぞれの中心からの相対位置を考慮してUVを決定する
                float centerX = i.uv.x < 0.5 ? _LeftCenterX : _RightCenterX;
                new_uv.x = (i.uv.x - centerX) * scale + 0.5; 
                new_uv.y = (i.uv.y - _CenterY) * scale + 0.5;           
                // 参照範囲以外は黒で埋める
                if( new_uv.x < 0 || 1 < new_uv.x || new_uv.y < 0 || 1 < new_uv.y )
                {
                    return fixed4(0,0,0,0);
                }  
                fixed4 col = tex2D(_MainTex, new_uv);
                return col;
            }
            ENDCG
        }
    }
}