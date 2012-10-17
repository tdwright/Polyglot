using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyglotFramework.Transformation
{
    abstract class TransformationBase : ITransformation
    {
        public abstract string ModuleName { get; }
        public abstract string ModuleID { get; }
        public abstract void ModuleActivate();
        public abstract void ModuleDeactivate();
        public abstract void LoadModules(IAcquisition Acquisition, IPointer Pointer, IOutput Output);

        protected IAcquisition AcquisitionModule;
        protected IPointer PointerModule;
        protected Type OutputType;
        protected IOutput[] Outputs;

        public bool Ready { get; protected set; }

        protected ProportionPoint Coordinates;


        private static object syncRoot = new Object();
        private Bitmap _image;
        private bool gotImage = false;
        protected Bitmap Image
        {
            get
            {
                lock (syncRoot)
                {
                    if (gotImage) return this._image.DeepClone();
                    else return null;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    this._image = value.DeepClone();
                    this.gotImage = true;
                }
            }
        }

        public event EventHandler<EngineStoppedEventArgs> Stopped;

        protected void ModuleFormClosed(object sender, ModuleFormClosedEventArgs e)
        {
            this.Stopped(this, new EngineStoppedEventArgs());
            this.ModuleDeactivate();
        }
    }
}
