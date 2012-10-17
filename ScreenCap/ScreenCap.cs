using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyglotFramework;
using System.ComponentModel.Composition;
using System.Drawing;

namespace PolyglotFramework.Acquisition
{
    [Export(typeof(IAcquisition))]
    public class ScreenCap : IAcquisition
    {
        public string Name = "Screen capture";

        public event EventHandler<NewImageEventArgs> NewImage;

        private Bitmap bm;

        public void ModuleActivate()
        {
        }

        public void ModuleDeactivate()
        {
        }

        protected virtual void OnNewImage(NewImageEventArgs args)
        {
            NewImage(this, args);
        }
    }
}
