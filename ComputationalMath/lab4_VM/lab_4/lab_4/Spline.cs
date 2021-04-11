using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab3_VM;

namespace lab_4
{
    public class Spline
    {
        private Polynomial[] polinoms;
        private double[,] tabl;
        private int count;

        public Spline(double[,] tabl, int count)
        {

            this.tabl = tabl;
            this.count = count;

            Interpolation inter = new Interpolation(tabl, count);

            polinoms = inter.Spline();
        }

        public double getY(double x)
        {
            int index = 0;

            for (int i = 1; i < count; ++i)
            {
                if (x <= tabl[0, i])
                {
                    break;
                }

                index++;
            }

            double Y = polinoms[index].F(x);

            return Y;
        }

    }
}
