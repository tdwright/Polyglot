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
    class ColourMajMin5th : TransformationBase
    {
        public override string ModuleName { get { return "Colour: Maj, Min, 5th"; } }
        public override string ModuleID { get { return "1d12d840-59ca-4cae-b5ea-ea26699eced8"; } }

        private readonly float[] frequencies = new float[3]
        {
            523f,
            659f,
            784f
        };

        public ColourMajMin5th()
        {
            this.Ready = false;
        }

        public override void LoadModules(IAcquisition AcquisitionModule, IPointer PointerModule, IOutput OutputModule)
        {
            this.AcquisitionModule = AcquisitionModule;
            this.PointerModule = PointerModule;
            this.OutputType = OutputModule.GetType();
            this.Outputs = new IOutput[3];
            this.Ready = true;
        }

        public override void ModuleActivate()
        {
            if (this.Ready)
            {

                for (int i = 0; i < 3; i++)
                {
                    this.Outputs[i] = (IOutput)Activator.CreateInstance(this.OutputType);
                    this.Outputs[i].Frequency = this.frequencies[i];
                    this.Outputs[i].Play();
                }
                
                if (this.AcquisitionModule is ModuleForm)
                {
                    ((ModuleForm)this.AcquisitionModule).ModuleFormClosed += this.ModuleFormClosed;
                }

                this.AcquisitionModule.Mode = ModuleMode.Active;
                this.AcquisitionModule.NewImage += AcquisitionModule_NewImage;

                try
                {
                    this.PointerModule.Mode = ModuleMode.Active;
                    this.PointerModule.NewPosition += PointerModule_NewPosition;
                }
                catch (ModeNotSupportedException mnse)
                {
                    Console.WriteLine(mnse.Message);
                    this.PointerModule.Mode = ModuleMode.Passive;
                }

                this.AcquisitionModule.ModuleActivate();
                this.PointerModule.ModuleActivate();
            }
        }

        private void Process()
        {

            if (this.PointerModule.Mode == ModuleMode.Passive) this.Coordinates = this.PointerModule.GetPosition();
            if (this.AcquisitionModule.Mode == ModuleMode.Passive) this.Image = this.AcquisitionModule.GetImage();

            Color colourPixel;

            if ((this.Image != null) && (this.Coordinates != null))
            {
                //Console.WriteLine("{0}:{1}", DateTime.Now.Second, DateTime.Now.Millisecond);
                //Console.WriteLine("Invalidated - {0}:{1}", DateTime.Now.Second, DateTime.Now.Millisecond);


                colourPixel = PixelTools.GetGaussianPixel(this.Image, this.Coordinates,5);

                int R = BitConverter.ToUInt16(new byte[] { colourPixel.R, 0x00 }, 0);
                int G = BitConverter.ToUInt16(new byte[] { colourPixel.G, 0x00 }, 0);
                int B = BitConverter.ToUInt16(new byte[] { colourPixel.B, 0x00 }, 0);
                //Console.WriteLine("R:{0:0.000};G:{1:0.000};B:{2:0.000}", R, G, B);

                float Rprop = (float)R / 256f;
                float Gprop = (float)G / 256f;
                float Bprop = (float)B / 256f;

                this.Outputs[0].Amplitude = Rprop;
                this.Outputs[1].Amplitude = Gprop;
                this.Outputs[2].Amplitude = Bprop;
                //Console.WriteLine("R:{0:0.000};G:{1:0.000};B:{2:0.000}", Rprop, Gprop, Bprop);

                float lum = PixelTools.LumFromColour(colourPixel);
                //this.PointerModule.SetIntensity(lum);
                this.PointerModule.SetIntensity(0f);
            }
        }

        private void AcquisitionModule_NewImage(object sender, NewImageEventArgs e)
        {
            this.Image = e.Image;
            this.Process();
        }

        private void PointerModule_NewPosition(object sender, NewPositionEventArgs e)
        {
            this.Coordinates = e.Position;
            this.Process();
        }

        public override void ModuleDeactivate()
        {
            this.AcquisitionModule.ModuleDeactivate();
            this.PointerModule.ModuleDeactivate();
            foreach (IOutput output in this.Outputs) output.Stop();
        }
    }
}
