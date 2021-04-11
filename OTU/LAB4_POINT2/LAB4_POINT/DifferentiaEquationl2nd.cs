using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4_POINT
{
    class DifferentiaEquationl2nd
    {
        double a0;
        double a1;
        double a2;
        double k;
        private double F1(double x1,double x2)
        {
            return (-a1 / a0) * x2 + (-a2 / a0) * x1 + k;
        }
        private double F2(double x)
        {
            return x;
        }

        public string toStr()
        {
            return "" + a0 + "y'' + " + a1 + "y' + " + a2 + "y = " + k;
        }
        public DifferentiaEquationl2nd(double a0, double a1, double a2, double k)
        {
            this.a0 = a0;
            this.a1 = a1;
            this.a2 = a2;
            this.k = k;
        }

        public double[] decision(double step, double max, double y0,double dy0)
        {
            List<double> list = new List<double>();
            double x2 = dy0;
            double x1 = y0;
            list.Add(x1);

            for (double i = step; i <= max; i += step)
            {
                double k1, k2, k3, k_1, k_2, k_3;
                k1 = step * F2(x2);
                k2 = step * F2(x2 + k1 / 2);
                k3 = step * F2(x2 + k2);

                k_1 = step * F1(x1, x2);
                k_2 = step * F1(x1 + k1 / 2, x2 + k1 / 2);
                k_3 = step * F1(x1 + k2, x2 + k2);

                x1 = x1 + (k1 + 2*k2 + k3)/ 4;
                x2 = x2 + (k_1 + 2 * k_2 + k_3) / 4;

                list.Add(x1);
            }

            return list.ToArray();
        }
    }
}
