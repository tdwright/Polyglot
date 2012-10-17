using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyglotFramework.Output
{
    [Export(typeof(IOutput))]
    class Dummy : IOutput
    {
        public string ModuleName { get { return "Dummy output"; } }
        public string ModuleID { get { return "de75a30f-3d72-4b92-a098-618dbd575eed"; } }

        public Dummy()
        {
            this.Frequency = 100;
            this.Amplitude = 0;
            this.Angle = 0;
        }

        public float Angle { get; set; }

        public float Amplitude { get; set; }

        public float Frequency { get; set; }

        public void Play()
        {
        }

        public void Stop()
        {
        }
    }
}
