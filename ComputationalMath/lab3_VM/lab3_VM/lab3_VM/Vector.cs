using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_VM
{
    public class Vector
    {
        private double[] vec = null;

        private int n;

        public Vector(int x = 1)
        {
            vec = new double[x];
            n = x;
        }

        public int N { get => n; }


        public double this[int x]
        {
            get { return vec[x]; }
            set { vec[x] = value; }
        }

        public override string ToString()
        {

            string str = "";

            for(int i = 0; i < this.n; ++i)
            {
                str += Math.Round(vec[i],3).ToString() + " ";
            }

            return str;
        }

    }
}
