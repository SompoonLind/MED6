Shader "Unlit/Draw"
{
    // Define shader properties
    Properties
    {
        // Base texture for drawing
        _MainTex ("Texture", 2D) = "white" {}
        // Position to draw on the texture
        _Coordinate("Coordinate",Vector)=(0,0,0,0)
        // Color of the paint used for drawing
        _Color("Paint Color",Color)=(0,0,0,0)
    }
    
    // Define subshader properties
    SubShader
    {
        // Shader is meant to be used for opaque objects
        Tags { "RenderType"="Opaque" }
        // Fixed level of detail set at 100
        LOD 100
        
        // Define pass properties
        Pass
        {
            // Include vertex and fragment programs written in CG
            CGPROGRAM
            // Specify vertex program
            #pragma vertex vert
            // Specify fragment program
            #pragma fragment frag
            // Include UnityCG.cginc for built-in shader helper functions
            #include "UnityCG.cginc"
            
            // Define the input vertex data structure
            struct appdata
            {
                // Vertex position
                float4 vertex : POSITION;
                // Texture coordinate
                float2 uv : TEXCOORD0;
            };
            
            // Define the output vertex data structure
            struct v2f
            {
                // Transformed texture coordinate
                float2 uv : TEXCOORD0;
                // Transformed vertex position
                float4 vertex : SV_POSITION;
            };
            
            // Define shader properties
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Coordinate,_Color;
            
            // Define vertex program
            v2f vert (appdata v)
            {
                // Output data structure
                v2f o;
                // Transform vertex position from object space to clip space
                o.vertex = UnityObjectToClipPos(v.vertex);
                // Transform texture coordinate from object space to texture space
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // Return output data structure
                return o;
            }
            
            // Define fragment program
            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture using transformed texture coordinate
                fixed4 col = tex2D(_MainTex, i.uv);
                // Calculate distance between transformed texture coordinate and drawing position
                float draw =pow(saturate(1-distance(i.uv,_Coordinate.xy)),300);
                // Calculate paint color based on strength of the paint
                fixed4 drawcol = _Color * (draw * 1);
                // Combine base texture color and paint color to produce final output color
                return drawcol +col;
            }
            // End CG program
            ENDCG
        }
    }
}
