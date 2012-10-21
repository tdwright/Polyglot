using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace PolyglotFramework.TouchPad
{
    [Export(typeof(IPointer))]
    public class TouchPadReceiver : IPointer
    {
        public string ModuleName { get { return "Android Touch Pad"; } }
        public string ModuleID { get { return "927de697-d422-45d4-849e-7a7021a83a89"; } }

        private readonly int port = 5574;
        private IPEndPoint ep;
        private bool connected;
        private Socket s;
        private BackgroundWorker backgroundConnecter;
        private BackgroundWorker backgroundReceiver;

        private bool pointerDown;
        public event EventHandler<NewPositionEventArgs> NewPosition;
        public event EventHandler<PointerStateChangeEventArgs> PointerStateChange;
        private ProportionPoint p;

        public ModuleMode Mode { get; set; }

        public TouchPadReceiver()
        {
            this.connected = false;
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),port);
        }

        public void ModuleActivate()
        {
            AdbStart();

            // BG workers for connecting and for listening
            backgroundConnecter = new BackgroundWorker();
            backgroundConnecter.DoWork += new DoWorkEventHandler(backgroundConnecter_Work);
            backgroundConnecter.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundConnecter_Completed);

            backgroundReceiver = new BackgroundWorker();
            backgroundReceiver.WorkerSupportsCancellation = true; // required since will never complete
            backgroundReceiver.DoWork += new DoWorkEventHandler(backgroundReceiver_Work);

            backgroundConnecter.RunWorkerAsync();
        }

        public void ModuleDeactivate()
        {
        }

        public ProportionPoint GetPosition()
        {
            if (this.p == null) throw new NoDataYetException("No point has yet been specified", this);
            return this.p;
        }

        public void SetIntensity(float intensity)
        {
            if (this.connected)
            {
                // intensity is a 0 <= float <= 1
                // powerstring will be the 1st 3 decimal places of intensity, but not the leading 0 or the decimal point
                string powerString;
                if (intensity == 1f)
                {
                    // special case to avoid 1 == 0 (substring(2) of 1.000 == 0.000)
                    // not elegant, but hey ho
                    powerString = "999";
                }
                else
                {
                    powerString = intensity.ToString("0.000").Substring(2);
                }
                powerString += "\n";
                byte[] outBuffer = Encoding.UTF8.GetBytes(powerString);
                this.s.Send(outBuffer);
            }
        }

        private void backgroundReceiver_Work(object sender, DoWorkEventArgs e)
        {
            string latest;
            while (true)
            {
                latest = this.ReceiveString();

                //Console.Write(latest);


                if (latest.Contains(";"))
                {
                    float[] coords = ExtractCoords(latest);
                    this.p = new ProportionPoint(coords);
                    if (this.Mode == ModuleMode.Active)
                    {
                        NewPosition(this, new NewPositionEventArgs(this.p));
                    }

                    if (this.pointerDown == false)
                    {
                        this.pointerDown = true;
                        if (this.Mode == ModuleMode.Active)
                        {
                            //PointerStateChange(this, new PointerStateChangeEventArgs(this.pointerDown));
                        }
                    }
                }
                else
                {
                    // if there isn't a semi-colon, not coordinates
                    // most likely "finger___up" (could be some other, yet to be implemented, message)
                    if(latest=="finger___up")
                    {
                        this.pointerDown = false;
                        if (this.Mode == ModuleMode.Active)
                        {
                            //PointerStateChange(this,new PointerStateChangeEventArgs(this.pointerDown));
                        }
                    }
                }
            }
        }

        private void backgroundConnecter_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.connected)
            {
                //Console.Write(ReceiveString());
                
                backgroundReceiver.RunWorkerAsync();
            }
        }

        private void backgroundConnecter_Work(object sender, DoWorkEventArgs e)
        {
            try
            {
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(this.ep);
                this.connected = s.Connected;
                if (this.connected) Console.WriteLine("Connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string ReceiveString()
        {
            byte[] bytes = new byte[12];
            s.Receive(bytes);
            if (bytes.Length > 0)
            {
                return Encoding.ASCII.GetString(bytes);
            }
            return String.Empty;
        }

        private float[] ExtractCoords(string fromPad)
        {
            float tmp;
            List<float> list = new List<float>();

            string[] segments = fromPad.Split(';');
            foreach (string segment in segments)
            {
                if (float.TryParse(segment, out tmp)) list.Add(tmp);
            }
            return list.ToArray();
        }

        private void AdbStart()
        {

            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resources = new string[3] {
                "adb.exe",
                "AdbWinApi.dll",
                "AdbWinUsbApi.dll"
            };
            try
            {
                foreach (string resource in resources)
                {
                    string filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), resource);
                    if (!File.Exists(filename))
                    {
                        using (Stream input = assembly.GetManifestResourceStream("TouchPad." + resource))
                        {
                            using (Stream output = File.Create(filename))
                            {
                                input.CopyStream(output);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }

            string AdbExe = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), resources[0]);

            Process p = new Process();
            ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = String.Format("/C \"{0}\" forward tcp:{1} tcp:5574", AdbExe, port.ToString());
            Console.WriteLine(startInfo.Arguments);
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            p.StartInfo = startInfo;
            p.Start();
            string adbResponse = p.StandardOutput.ReadToEnd();
            adbResponse += p.StandardError.ReadToEnd();
            p.WaitForExit();
            Console.WriteLine(adbResponse);
            if (adbResponse.Contains("not recognized") || adbResponse.Contains("error"))
            {
                // Possible causes:
                //  ~ ADB not installed on this PC (1st match)
                //  ~ Device not connected (2nd match)
                //  ~ Debugging not enabled on device (2nd match)
                throw new ModuleImplementationException(adbResponse);
            }
        }
    }
}
