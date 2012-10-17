using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyglotFramework.Pointer
{
    [Export(typeof(IPointer))]
    class CentrePoint : IPointer
    {
        public string ModuleName { get { return "Centre Point"; } }
        public string ModuleID { get { return "fd878f50-6149-4a55-9481-7843bb8b7387"; } }

        private ProportionPoint p;

        public event EventHandler<NewPositionEventArgs> NewPosition;
        public event EventHandler<PointerStateChangeEventArgs> PointerStateChange;

        public CentrePoint()
        {
            this.p = new ProportionPoint(0.5f, 0.5f);
        }

        public void ModuleActivate()
        {
        }

        public void ModuleDeactivate()
        {
        }

        public ModuleMode Mode
        {
            get
            {
                return ModuleMode.Passive;
            }
            set
            {
                if (value != ModuleMode.Passive)
                {
                    throw new PolyglotFramework.ModeNotSupportedException("CentrePoint module operates only in passive mode", ModuleMode.Active, this);
                }
            }
        }

        public ProportionPoint GetPosition()
        {
            if (this.p == null) throw new NoDataYetException("No point has yet been specified", this);
            return this.p;
        }

        public void SetIntensity(float intensity)
        {
            return;
        }
    }
}
