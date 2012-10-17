using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace PolyglotFramework.Acquisition
{
    [Export(typeof(IAcquisition))]
    class ImageFile : IAcquisition
    {
        public string ModuleName { get { return "Image File"; } }
        public string ModuleID { get { return "efb64836-0468-4bd0-9b20-053d1a996ffe"; } }

        public event EventHandler<NewImageEventArgs> NewImage;

        public ModuleMode Mode {get; set; }

        private Bitmap bm;

        public ImageFile()
        {
            //MessageBox.Show("boo");
        }

        public void ModuleActivate()
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    bm = new Bitmap(open.FileName);
                    
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading image");
            }

        }

        public void ModuleDeactivate()
        {
        }

        protected virtual void OnNewImage(NewImageEventArgs args)
        {
            NewImage(this, args);
        }

        public Bitmap GetImage()
        {
            return this.bm;
        }
    }
}
