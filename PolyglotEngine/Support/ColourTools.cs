using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyglotEngine.Support
{
    static class ColourTools
    {
        static public HSL RGB2HSL(Color pixel)
        {
            int rInt = pixel.R;
            int gInt = pixel.G;
            int bInt = pixel.B;

            float R = (float)rInt / 255f;
            float G = (float)gInt / 255f;
            float B = (float)bInt / 255f;

            float Max = Math.Max(B, Math.Max(R, G));
            float Min = Math.Min(B, Math.Min(R, G));
            float H, S, L = (Max + Min) / 2;

            if (Max == Min)
            {
                // Achromatic
                H = S = 0;
            }
            else
            {
                float Delta = Max - Min;
                S = (L > 0.5) ? Delta / (2 - Max - Min) : Delta / (Max + Min);
                if (Max == R)
                {
                    float Bolster = (G < B) ? 6f : 0f;
                    H = (G - B) / Delta + Bolster;
                }
                else if (Max == G)
                {
                    H = (B - R) / Delta + 2;
                }
                else
                {
                    H = (R - G) / Delta + 4;
                }
                H = H / 6f;
            }
            return new HSL(H, S, L);
        }

        /// <summary>
        /// Converts an RGB pixel into a float luminance
        /// </summary>
        /// <param name="pixel">A Color object representing a single pixel</param>
        /// <returns>Returns a float value (0&lt;=V&lt;1) corresponding to the luminance of a pixel</returns>
        static public float LumFromColour(Color pixel)
        {
            return ((float)(pixel.R + pixel.G + pixel.B)) / (256f * 3f);
        }

        static public float HueFromColor(Color pixel)
        {
            HSL h = RGB2HSL(pixel);
            return h.Hue;
        }

        static public float SaturationFromColor(Color pixel)
        {
            HSL h = RGB2HSL(pixel);
            return h.Saturation;
        }

        static public float LightnessFromColor(Color pixel)
        {
            HSL h = RGB2HSL(pixel);
            return h.Lightness;
        }
    }

    struct HSL
    {
        public float Hue;
        public float Saturation;
        public float Lightness;

        public HSL(float h, float s, float l)
        {
            this.Hue = h;
            this.Saturation = s;
            this.Lightness = l;
        }

        public override string ToString()
        {
            return (String.Format("{0:0.000},{1:0.000},{2:0.000}", Hue, Saturation, Lightness));
        }
    }
}
