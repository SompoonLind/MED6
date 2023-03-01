Shader "Unlit/Testing shader"
{
    Properties {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _MaxLookTime ("Max Look Time", Range(0, 10)) = 5
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float4 color : COLOR;
            };

            float4 _BaseColor;
            float _MaxLookTime;
            float _ElapsedTime;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.color = _BaseColor;
                return o;
            }

            float4 frag (v2f i) : COLOR {
                float4 color = i.color;
                color.r = lerp(color.r, 1, _ElapsedTime / _MaxLookTime);
                color.b = lerp(color.b, 1, 1 - (_ElapsedTime / _MaxLookTime));
                return color;
            }

            void update (v2f i) {
                _ElapsedTime += _TimeDelta;
                if (_ElapsedTime > _MaxLookTime)
                    _ElapsedTime = _MaxLookTime;
            }

            ENDHLSL
        }
    }
}