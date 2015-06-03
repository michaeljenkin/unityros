Shader "Custom/SkysphereShader" {
          Properties {
             _MainTex ("Base (RGB)", 2D) = "white" {}
         }
          SubShader {
            
            Tags { "RenderType" = "Opaque" }
            
            Cull Front
            
            CGPROGRAM
            
            #pragma surface surf SimpleLambert vertex:vert
            sampler2D _MainTex;
     
             struct Input {
                 float2 uv_MainTex;
                 float4 color : COLOR;
             };
            
            
            void vert(inout appdata_full v)
            {
                v.normal.xyz = v.normal * -1;
            }
            
         half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
              half4 c;
              c.rgb = s.Albedo;
              c.a = s.Alpha;
              return c;
          }
            
            void surf (Input IN, inout SurfaceOutput o) {
            /*
            float d2 = (IN.uv_MainTex.x - 0.5f) * (IN.uv_MainTex.x - 0.5f) + (IN.uv_MainTex.y - 0.5f) * (IN.uv_MainTex.y - 0.5f);
            
            if(d2 < 0.5 * 0.5) {
            	IN.uv_MainTex.x = IN.uv_MainTex.x;
            	IN.uv_MainTex.y = IN.uv_MainTex.y;
            } else {
            	IN.uv_MainTex.x = 0;
            	IN.uv_MainTex.y = 0;
            	}
            	*/
                      fixed3 result = tex2D(_MainTex, IN.uv_MainTex);
                 o.Albedo = result.rgb;

            }
            
            ENDCG
            
          }
          
          Fallback "Diffuse"
     }
