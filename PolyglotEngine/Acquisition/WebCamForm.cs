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
using AForge.Video;
using AForge.Video.DirectShow;
using System.Runtime.Serialization;

namespace PolyglotFramework.Acquisition
{
    [Export(typeof(IAcquisition))]
    public partial class WebCamForm : ModuleForm, IAcquisition
    {
        public string ModuleName { get { return "Web Cam"; } }
        public string ModuleID { get { return "1e563322-f7b3-43a7-8fa8-0a68c7b1a73b"; } }

        public event EventHandler<NewImageEventArgs> NewImage;

        public ModuleMode Mode { get; set; }

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource = null;
        private int camIndex = 0; // default to 0 (i.e. only 1 cam attached)

        private Bitmap bm;

        public void ModuleActivate()
        {
            InitializeComponent();
            this.Show();

            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0) throw new WebCamMissingException();
                if (videoDevices.Count > 1)
                {
                    this.selectCamButton.Enabled = true;
                    this.camIndex = videoDevices.Count - 1;
                    //SelectCam();
                }
                UseCam();
                foreach (FilterInfo device in videoDevices)
                    Console.WriteLine(device.Name);
            }
            catch (WebCamMissingException wcmEx)
            {
            }

        }

        public Bitmap GetImage()
        {
            if (this.bm == null)
            {
                throw new NoDataYetException("Waiting for webcam", this);
            }
            else
            {
                pictureBox1.Image = this.bm;
                return this.bm;
            }
        }

        private void SelectCam()
        {
            WebcamSelection wcs = new WebcamSelection();
            string[] camLabels = new string[videoDevices.Count];
            for (int i = 0; i < videoDevices.Count; i++) camLabels[i] = videoDevices[i].Name;
            wcs.Options = camLabels;
            wcs.ShowDialog();
            this.camIndex = wcs.Choice;
        }

        private void UseCam()
        {
            this.videoSource = new VideoCaptureDevice(videoDevices[camIndex].MonikerString);
            Console.WriteLine("Using camera {0} ({1})", camIndex, videoDevices[camIndex].Name);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            this.bm = img;
            if (this.Mode == ModuleMode.Active)
            {
                pictureBox1.Image = (Bitmap)img.Clone();
                NewImage(this, new NewImageEventArgs(img));
            }
        }

        public void ModuleDeactivate()
        {
            this.videoSource.SignalToStop();
            this.Close();
        }

        protected virtual void OnNewImage(NewImageEventArgs args)
        {
            NewImage(this, args);
        }

        private void selectCamButton_Click(object sender, EventArgs e)
        {
            SelectCam();
            UseCam();
        }
    }

    [Serializable]
    public class WebCamMissingException : Exception
    {
        public WebCamMissingException() { }
        public WebCamMissingException(string message) : base(message) { }
        public WebCamMissingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
