using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4_POINT
{
    public class Settings
    {
        private double step;
        private double y0;
        private double dy0;
        private double max;
        private double k;

        public Settings(double step, double y0, double dy0, double max, double k)
        {
            this.k = k;
            this.y0 = y0;
            this.dy0 = dy0;
            this.max = max;
            this.step = step;
        }

        public double Step { get => step; set => step = value; }
        public double Y0 { get => y0; set => y0 = value; }
        public double Dy0 { get => dy0; set => dy0 = value; }
        public double Max { get => max; set => max = value; }
        public double K { get => k; set => k = value; }
    }
}
