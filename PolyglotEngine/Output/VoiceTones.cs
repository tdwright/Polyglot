using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Diagnostics.CodeAnalysis;

namespace PolyglotFramework.Output
{
    [Export(typeof(IOutput))]
    class VoiceTones : WaveProvider32, IOutput
    {
        public string ModuleName { get { return "vOICe tones"; } }
        public string ModuleID { get { return "cfad539b-8b4d-4c4b-b835-71de05590222"; } }

        int sample;
        private WaveOut waveOut;

        public float Angle { get; set; }

        private float amplitude;
        public float Amplitude
        {
            get
            {
                return this.amplitude;
            }
            set
            {
                if (value >= 0)
                    this.amplitude = value;
                else
                    throw new ArgumentOutOfRangeException("Amplitude may not be negative");
            }
        }

        private float frequency;
        public float Frequency
        {
            get
            {
                return this.frequency;
            }
            set
            {
                if (value > 0)
                    this.frequency = value;
                else if (value < 0)
                    this.frequency = Math.Abs(value);
                else
                    throw new ArgumentOutOfRangeException("Frequency may not be 0");
            }
        }

        public VoiceTones()
        {
            this.Frequency = 100;
            this.Amplitude = 0;
            this.Angle = 0;
        }

        public void Play()
        {
            if (this.waveOut == null)
            {
                this.waveOut = new WaveOut();
                this.waveOut.Init(this);
                this.waveOut.Play();
            }
        }

        public void Stop()
        {
            if (this.waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
        }

        [System.Obsolete("Use SetWaveFormat(int sampleRate). (Localisation requires exactly two channels.)")]
        public new void SetWaveFormat(int sampleRate, int channels)
        {
            // Localisation needs two channels
            if (channels == 2)
                this.SetWaveFormat(sampleRate);
            else
                throw new NotSupportedException("Localised sounds must be stereo");
        }

        public void SetWaveFormat(int sampleRate)
        {
            #pragma warning disable 618 // pragma disable to allow us to mask the obsolete version
            this.SetWaveFormat(sampleRate, 2);
            #pragma warning restore 618 // don't forget to restore!
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            float localAngle = this.Angle;
            while(localAngle>90f)localAngle=localAngle-90f;
            while(localAngle<-90f)localAngle=localAngle+90f;
            float ampAdjust = (localAngle / 180f)+0.5f;
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n = n + this.WaveFormat.Channels)
            {
                buffer[n + offset] = (float)((1-ampAdjust) * Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                buffer[n + offset + 1] = (float)(ampAdjust * Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                sample++;
                if (sample >= sampleRate) sample = 0;
            }
            return sampleCount;
        }
    }
}
