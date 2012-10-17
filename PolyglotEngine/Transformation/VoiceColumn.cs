using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyglotFramework.Transformation
{
    [Export(typeof(ITransformation))]
    class VoiceColumn : ITransformation
    {
        public string ModuleName { get { return "vOICe column"; } }
        public string ModuleID { get { return "2ecdf0eb-5d58-4eba-b404-841ee2ae5f86"; } }

        public bool Ready { get; private set; }

        public event EventHandler<EngineStoppedEventArgs> Stopped;

        private IAcquisition AcquisitionModule;
        private IPointer PointerModule;
        private Type OutputType;
        private IOutput[] Outputs;

        private ProportionPoint p;
        //private Bitmap bm;
        private byte[, ,] bmdb;

        private int yRes = 80;
        private int xRes = 80;

        private float lastX;
        private int lastCol;

        public VoiceColumn()
        {
            this.Ready = false;
        }

        public void LoadModules(IAcquisition AcquisitionModule, IPointer PointerModule, IOutput OutputModule)
        {
            this.AcquisitionModule = AcquisitionModule;
            this.PointerModule = PointerModule;
            this.OutputType = OutputModule.GetType();
            this.Outputs = new IOutput[yRes];
            this.Ready = true;
        }

        public void ModuleActivate()
        {
            if (this.Ready)
            {
                this.AcquisitionModule.Mode = ModuleMode.Passive;
                this.AcquisitionModule.ModuleActivate();

                for (int i = 0; i < yRes; i++)
                {
                    this.Outputs[i] = (IOutput)Activator.CreateInstance(this.OutputType);
                    this.Outputs[i].Frequency = 500 + (100 * i);
                }

                this.PointerModule.Mode = ModuleMode.Active;
                this.PointerModule.NewPosition += PointerModule_NewPosition;
                this.PointerModule.ModuleActivate();
            }
        }

        private void PointerModule_NewPosition(object sender, NewPositionEventArgs e)
        {
            this.p = e.Position;

            int col = (int)Math.Round((double)(p.x * (float)xRes));

            // Do we need a new image?
            if ((col < this.lastCol)||(this.bmdb==null))
            {
                try
                {
                    // Get a new image
                    Bitmap orig = this.AcquisitionModule.GetImage();
                    // Now resize it so that it's height is equal to yRes
                    Bitmap resized = this.ResizeBitmap(orig);
                    Bitmap greyscale = this.ToGreyscale(resized);
                    // Save this resized bitmap for posterity
                    //string tmpPath = Path.GetTempFileName();
                    //resized.Save(tmpPath);
                    //Console.WriteLine("frame saved to {0}", tmpPath);
                    //this.bm = resized;
                    this.bmdb = this.ImageToBytes(greyscale);
                }
                catch (NoDataYetException ndye)
                {
                    Console.Write(ndye.Message);
                }
            }

            if ((this.bmdb != null)&&(this.lastCol!=col))
            {
                // Now read a column
                //for (int i = 0; i < yRes; i++)
                //{
                //    byte[] pixel = new byte[2];
                //    pixel[0] = this.bmdb[0, col, i];
                //    pixel[1] = this.bmdb[1, col, i];
                //    int pixelVal = BitConverter.ToInt16(pixel, 0);

                //    float pixelValProp = (float)pixelVal / 256f;
                //    this.Outputs[i].Amplitude = pixelValProp;
                //}
            }

            this.lastCol = col;
        }

        private Bitmap ToGreyscale(Bitmap orig)
        {
            lock (orig)
            {
                Bitmap clone = new Bitmap(orig.Width, orig.Height, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
                using (Graphics g = Graphics.FromImage(clone))
                {
                    g.DrawImage(orig, new Rectangle(0, 0, clone.Width, clone.Height));
                }
                return clone;
            }
        }

        private Bitmap ResizeBitmap(Bitmap orig)
        {
            lock (orig)
            {
                Bitmap resized = new Bitmap(xRes, yRes);
                resized.SetResolution(orig.HorizontalResolution, orig.VerticalResolution);
                using (Graphics g = Graphics.FromImage(resized))
                {
                    //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawImage(orig, 0, 0, resized.Width, resized.Height);
                }
                return resized;
            }
        }

        /// <summary>
        /// Converts a bitmap to a 3D byte array.
        /// </summary>
        /// <param name="image">A bitmap image</param>
        /// <param name="BytesPerPixel">Number of bytes per pixel. (16 bit/pixel = 2 bytes / pixel)</param>
        /// <returns>A 3 dimensional byte array. 1st dimension equal to BytesPerPixel. 2nd equal to the width of the image. 3rd equal to the height of the image.</returns>
        private byte[, ,] ImageToBytes(Bitmap image, int BytesPerPixel = 2)
        {
            BitmapData bmd = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format16bppGrayScale);
            byte[] bmdb = new byte[bmd.Stride * bmd.Height];
            System.Runtime.InteropServices.Marshal.Copy(bmd.Scan0, bmdb, 0, bmdb.Length);
            image.UnlockBits(bmd);
            byte[, ,] result = new byte[BytesPerPixel, image.Width, image.Height];
            for (int k = 0; k < BytesPerPixel; k++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        result[k, i, j] = bmdb[j * bmd.Stride + i * 3 + k];
                    }
                }
            }
            return result;
        }

        public void ModuleDeactivate()
        {
            this.AcquisitionModule.ModuleDeactivate();
            this.PointerModule.ModuleDeactivate();
            //this.OutputModule.ModuleDeactivate();
        }
    }
}
