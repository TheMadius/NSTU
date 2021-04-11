using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_VM
{
    public class Polynomial
    {
        private int Power;
        private double[] coefficient = null;


        public Polynomial(int pow)
        {
            this.Power = pow;
            coefficient = new double[pow + 1];
            for(int i = 0; i < pow + 1; ++i)
            {
                coefficient[i] = 0;
            }
        }

        public double this[int x]
        {
            get { return coefficient[x]; }
            set { coefficient[x] = value; }
        }

        public static Polynomial operator *(Polynomial pol1, Polynomial pol2)
        {
            var result = new Polynomial(pol1.Power + pol2.Power);

            for (int i = 0; i < pol1.Power+1;++i)
            {

                for(int j = 0; j < pol2.Power+1;++j)
                {

                    result[i + j] = result[i + j] + pol1[i] * pol2[j];

                }

            }

            return result;
        }

        public override string ToString()
        {

            string str = this.coefficient[0].ToString() ;

            for(int i = 1; i < this.Power +1; ++i)
            {
                if( this.coefficient[i] >= 0)
                {
                    str += " + ";
                }

                str += Math.Round(this.coefficient[i],3).ToString() + "x^" + i.ToString();

            }

            return str;
        }

        public static Polynomial operator *(double pol1, Polynomial pol2)
        {
            var result = new Polynomial(pol2.Power);

             for (int j = 0; j < pol2.Power + 1; ++j)
             {
                result[j] = result[j] + pol1 * pol2[j];
             }
            return result;
        }

        public static Polynomial operator + ( Polynomial pol1, Polynomial pol2)
        {
            var result = new Polynomial(Math.Max(pol1.Power, pol2.Power));

            for (int i = 0; i < Math.Max(pol1.Power, pol2.Power) + 1; ++i)
            {
                if(i <= pol1.Power)
                {
                    result[i] += pol1.coefficient[i];
                }

                if( i <= pol2.Power)
                {
                    result[i] += pol2.coefficient[i];
                }
            }

            return result;
        }

        public static Polynomial operator +(double pol1, Polynomial pol2)
        {
            var result = pol2;

            result[0] += pol1;

            return result;
        }

        public double F(double x)
        {
            double summ = 0;

            for(int i = 0; i < this.Power + 1 ; ++i)
            {
                summ += Math.Pow(x,i)*(this.coefficient[i]);
            }

            return summ;
        }

    }
}

