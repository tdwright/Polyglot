using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroLibrary;

namespace PolyglotFramework.Pointer
{
    [Export(typeof(IPointer))]
    class ScanningPoint : IPointer
    {
        public string ModuleName { get { return "Scanning Point"; } }
        public string ModuleID { get { return "352449a6-d6de-43c0-bf21-4297108cef32"; } }

        private readonly float ScanPeriod = 1f; // 1s per scan
        private readonly int xRes = 100; // 200 columns
        private long timerPeriod;
        private long ticksPerFrame;
        private long ticksAtStartOfFrame;

        private ProportionPoint p;

        private MicroTimer mt;

        public event EventHandler<NewPositionEventArgs> NewPosition;
        public event EventHandler<PointerStateChangeEventArgs> PointerStateChange;

        public ScanningPoint()
        {
            float secondsPerCol = ScanPeriod / (float)xRes;
            this.timerPeriod = (long)(500000 * secondsPerCol);
            this.ticksPerFrame = (long)(ScanPeriod * TimeSpan.TicksPerSecond);
            this.p = new ProportionPoint(0f, 0.5f);
        }

        public void ModuleActivate()
        {
            this.mt = new MicroTimer(this.timerPeriod);
            this.mt.IgnoreEventIfLateBy = this.timerPeriod;
            this.mt.MicroTimerElapsed += mt_MicroTimerElapsed;

            this.ticksAtStartOfFrame = DateTime.Now.Ticks;

            this.mt.Start();
        }

        void mt_MicroTimerElapsed(object sender, MicroTimerEventArgs timerEventArgs)
        {
            long tickDiff = DateTime.Now.Ticks - this.ticksAtStartOfFrame;
            float newX = ((float)tickDiff / (float)ticksPerFrame);
            if (newX > 1)
            {
                //Console.WriteLine("reset");
                this.ticksAtStartOfFrame = DateTime.Now.Ticks;
                p.x = (newX) - 1;
            }
            else p.x = newX;
            if (this.Mode == ModuleMode.Active) NewPosition(this, new NewPositionEventArgs(this.p));
        }

        public void ModuleDeactivate()
        {
            this.mt.Stop();
        }

        public ModuleMode Mode { get; set; }

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
