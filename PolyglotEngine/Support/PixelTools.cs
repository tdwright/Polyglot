using MathNet.Numerics.Distributions;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace PolyglotFramework
{
    static class PixelTools
    {

        const int MaxGausRadius = 5;

        static public Color GetPixel(Bitmap i, ProportionPoint p)
        {
            return GetGaussianPixel(i, p, 0);
        }

        static public Color GetGaussianPixel(Bitmap Source, ProportionPoint Coords, int Radius)
        {
            if ((Radius > MaxGausRadius) || (Radius < 0)) throw new ArgumentOutOfRangeException("Radius", "Radius must be >= 0 and < MaxGausRadius");

            int diameter = 1 + (2 * Radius);
            int centre = Radius + 1;

            double sigma = (double)Radius / 2d;
            Normal dist = new Normal(Radius, sigma);

            // create new bitmap with known PixelFormat
            Bitmap bm = new Bitmap(Source.Width, Source.Height, PixelFormat.Format24bppRgb);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(Source, new Rectangle(0, 0, bm.Width, bm.Height));
            }
            //Source.Dispose();

            // Now we know there are 24 bits per pixel. 24 / 8 (bits / byte) = 3 bytes
            const int BytesPerPixel = 3;

            Color colourPixel;

            int x, y;
            x = (int)Math.Round((double)(Coords.x * (float)bm.Width));
            if (x >= bm.Width) x = bm.Width - 1;
            y = (int)Math.Round((double)(Coords.y * (float)bm.Height));
            if (y >= bm.Height) y = bm.Height - 1;

            int stride;
            byte[] rgbValues = GetBytesFromImage(bm, out stride);
            bm.Dispose();

            int centrePixel = (Math.Abs(stride) * y) + (x * BytesPerPixel);

            if (Radius == 0)
            {
                // get the RGB data from the byte array
                colourPixel = Color.FromArgb(0, rgbValues[centrePixel], rgbValues[centrePixel + 1], rgbValues[centrePixel + 2]);
            }
            else
            {
                // work out where the origin of our sub-region will be
                int TLcorner = centrePixel - (BytesPerPixel * (Radius + (Radius * stride)));
                double weightCumulative = 0;
                double[] rgbCumulative = new double[3] { 0d, 0d, 0d };
                for (int row = 0; row < diameter; row++)
                {
                    for (int col = 0; col < diameter; col++)
                    {
                        // find the start of this pixel
                        int pixelStart = TLcorner + (BytesPerPixel * (col + (row * stride)));
                        int centrePixelStart = pixelStart - (col - Radius);
                        int line = (int)Math.Floor((double)centrePixelStart / (double)stride);

                        // check pixel lies within bound of image
                        if ((pixelStart >= 0) && (pixelStart < rgbValues.Length))
                        {
                            // check pixel hasn't wrapped to different line
                            int lineOfThisPixel = (int)Math.Floor((double)pixelStart / (double)stride);
                            if (line == lineOfThisPixel)
                            {

                                // work out the distance from the centre of the sub-region
                                int relX = Math.Abs((col + 1) - centre);
                                int relY = Math.Abs((row + 1) - centre);
                                double hyp = Math.Sqrt((double)(Math.Pow(relX, 2)) + (Math.Pow(relY, 2)));

                                // get a gaussian weight for this distance
                                double weight = dist.CumulativeDistribution(Radius - hyp);
                                weightCumulative += weight;

                                // add the weighted RGB values to the array
                                for (int p = 0; p < 3; p++)
                                {
                                    rgbCumulative[p] += rgbValues[pixelStart + p] * weight;
                                }
                            }
                        }
                    }
                }
                // compose the resulting color pixel
                byte[] rgbFinal = new byte[3];
                for (int p = 0; p < 3; p++)
                {
                    int final = (int)Math.Round(rgbCumulative[p] / weightCumulative);
                    if (final > 255) final = 255;
                    rgbFinal[p] = (byte)(final & 0xff);
                }
                return Color.FromArgb(0, rgbFinal[0], rgbFinal[1], rgbFinal[2]);
            }

            return colourPixel;
        }

        private static byte[] GetBytesFromImage(Bitmap bm, out int stride)
        {
            Rectangle r = new Rectangle(0, 0, bm.Width, bm.Height);
            BitmapData bmd = bm.LockBits(r, ImageLockMode.ReadOnly, bm.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmd.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            int bytes = Math.Abs(bmd.Stride) * bm.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            stride = bmd.Stride;

            bm.UnlockBits(bmd);

            return rgbValues;
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
    }
}
