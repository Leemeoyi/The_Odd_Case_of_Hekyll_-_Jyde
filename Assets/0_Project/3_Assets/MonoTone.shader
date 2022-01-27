Shader "Hidden/Mono Tone" 
{
    Properties
    {
       _MainTex ("Base (RGB)", 2D) = "white" {}

       _HighlightColor("Highlight Color", 2D) = "white" {}
       _HighlightThreshold("Highlight Threshold", Float) = 0.66

       _MidColor("Mid Color", 2D) = "white" {}

       _ShadowThreshold("Shadow Threshold", Float) = 0.33
       _ShadowColor("Shadow Color", 2D) = "white" {}       

       _Brightness("Overall Brightness", Float) = 3
       _EffectAlpha("Effect Alpha", Float) = 1
    }
 
    SubShader 
    {
       Pass 
       {
           ZTest Always Cull Off ZWrite Off
               
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            uniform sampler2D _MainTex;
            
            uniform float _Brightness;

            uniform fixed4 _HighlightColor;
            uniform float _HighlightThreshold;

            uniform fixed4 _MidColor;

            uniform float _ShadowThreshold;
            uniform fixed4 _ShadowColor;          

            uniform float _Alpha;


            fixed4 ramp (float alpha, float threshold, float range)
            {
                float min = (threshold * 0.5) - (range * threshold);
                float max = (threshold * 0.5) + (range * threshold);
                fixed4 result = smoothstep(alpha, min, max);
                return result;

            }
 
            fixed4 frag (v2f_img i) : SV_Target
            {  
               fixed4 original = tex2D(_MainTex, i.uv);
   
               fixed grayscale = Luminance(original.rgb) * _Brightness;

               fixed4 shadowRamp = ramp(grayscale.r, _ShadowThreshold, 1);
               fixed4 highlightRamp = ramp(grayscale.r, _HighlightThreshold, 1);

               fixed4 color = lerp(_ShadowColor, _MidColor, shadowRamp);
               color = lerp(color, _HighlightColor, highlightRamp);

               fixed4 output = color * grayscale;

               output.a = original.a;

               output = lerp(grayscale, output, color.a);
               output = lerp(original, output, _Alpha);

               return output;
            }            

            ENDCG
 
        }
    }
 
    Fallback off
 
}