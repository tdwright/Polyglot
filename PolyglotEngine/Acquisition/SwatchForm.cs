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
using System.Reflection;

namespace PolyglotFramework.Acquisition
{
    [Export(typeof(IAcquisition))]
    public partial class SwatchForm : ModuleForm, IAcquisition
    {
        public string ModuleName { get { return "Swatch file"; } }
        public string ModuleID { get { return "2d401f04-69e0-4d0b-9435-d293e6394e79"; } }

        public event EventHandler<NewImageEventArgs> NewImage;

        public ModuleMode Mode { get; set; }

        private Bitmap bm;

        public void ModuleActivate()
        {
            InitializeComponent();

            foreach (string s in Assembly.GetExecutingAssembly().GetManifestResourceNames()) Console.WriteLine(s);

            this.bm = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("PolyglotEngine.Acquisition.swatch.png"));
            this.pictureBox1.Image = this.bm;
            this.Show();
            if (this.Mode == ModuleMode.Active) this.NewImage(this, new NewImageEventArgs(this.bm));
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
    }
}
