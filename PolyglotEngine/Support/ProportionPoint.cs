using System;

namespace PolyglotFramework
{
    public class ProportionPoint
    {
        private float xVal, yVal;
        public float x
        {
            get
            {
                return xVal;
            }
            set
            {
                if ((value <= 1f) && (value >= 0f)) xVal = value;
                else throw new ArgumentOutOfRangeException("ProportionPoint.x", "values must be between 0f and 1f inclusive");
            }
        }

        public float y
        {
            get
            {
                return yVal;
            }
            set
            {
                if ((value <= 1f) && (value >= 0f)) yVal = value;
                else throw new ArgumentOutOfRangeException("ProportionPoint.y", "values must be between 0f and 1f inclusive");
            }
        }

        public ProportionPoint(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public ProportionPoint(float[] coords)
        {
            if (coords.Length != 2) throw new ArgumentOutOfRangeException("coords", "coords must have a length of 2");
            this.x = coords[0];
            this.y = coords[1];
        }

        public override string ToString()
        {
            return String.Format("{0:0.000};{1:0.000}", this.x, this.y);
        }
    }
}
