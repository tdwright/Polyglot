using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace PolyglotFramework.Acquisition
{
    [Export(typeof(IAcquisition))]
    public partial class ImageFileForm : ModuleForm, IAcquisition
    {
        public string ModuleName { get { return "Image File (form)"; } }
        public string ModuleID { get { return "bbf4052e-fc1d-48e5-8a25-34bff2dc47d9"; } }

        public event EventHandler<NewImageEventArgs> NewImage;

        public ModuleMode Mode { get; set; }

        private Bitmap bm;

        public void ModuleActivate()
        {
            InitializeComponent();
            this.Show();
            this.SelectNewImage();
        }

        public void ModuleDeactivate()
        {
            this.Close();
        }

        public Bitmap GetImage()
        {
            if (this.bm == null) throw new NoDataYetException("No image has been selected yet.", this);
            return this.bm;
        }

        protected virtual void OnNewImage(NewImageEventArgs args)
        {
            NewImage(this, args);
        }

        private void SelectNewImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                this.bm = new Bitmap(open.FileName);
                this.pictureBox1.Image = bm;
                if(this.Mode==ModuleMode.Active) NewImage(this, new NewImageEventArgs(bm));
            }
        }

        private void selectImageButton_Click(object sender, EventArgs e)
        {
            SelectNewImage();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            SelectNewImage();
        }
    }
}
