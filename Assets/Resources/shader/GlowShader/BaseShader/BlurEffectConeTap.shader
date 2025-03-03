// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ImageEffect/Glow/BlurEffectConeTap" {
 Properties { 
     _MainTex ("", any) = "" {}
     _Strength ("Strength", Float) = 1.0
 }

 CGINCLUDE
 #include "UnityCG.cginc"
 struct v2f {
     float4 pos : POSITION;
     half2 uv : TEXCOORD0;
     half2 taps[4] : TEXCOORD1; 
 };
 sampler2D _MainTex;
 half4 _MainTex_TexelSize;
 half4 _BlurOffsets;
 uniform fixed _Strength;
 v2f vert( appdata_img v ) {
     v2f o; 
     o.pos = UnityObjectToClipPos(v.vertex);
     o.uv = v.texcoord - _BlurOffsets.xy * _MainTex_TexelSize.xy; // hack, see BlurEffect.cs for the reason for  let's make a new blur effect soon
     o.taps[0] = o.uv + _MainTex_TexelSize * _BlurOffsets.xy;
     o.taps[1] = o.uv - _MainTex_TexelSize * _BlurOffsets.xy;
     o.taps[2] = o.uv + _MainTex_TexelSize * _BlurOffsets.xy * half2(1,-1);
     o.taps[3] = o.uv - _MainTex_TexelSize * _BlurOffsets.xy * half2(1,-1);
     return o;
 }
 half4 frag(v2f i) : COLOR {
     half4 color = tex2D(_MainTex, i.taps[0]);
     color += tex2D(_MainTex, i.taps[1]);
     color += tex2D(_MainTex, i.taps[2]);
     color += tex2D(_MainTex, i.taps[3]); 
     return color * _Strength;
 }
 ENDCG
 SubShader {
      Pass {
           ZTest Always Cull Off ZWrite Off
           Fog { Mode off }      

           CGPROGRAM
           #pragma fragmentoption ARB_precision_hint_fastest
           #pragma vertex vert
           #pragma fragment frag
           ENDCG
       }
 }
 Fallback off
}