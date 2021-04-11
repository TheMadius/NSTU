using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_VM
{
    public class Interpolation
    {
        private double[,] tabl = null;
        private int N ;

        public Interpolation(double[,] tab,int count)
        {
            tabl = tab;
            N = count;
        }

        public Polynomial Lagrange()
        {
            Polynomial res = new Polynomial(1);

            res[0] = 0;

            for (int i = 0; i < N; ++i)
            {
                Polynomial pol = new Polynomial(0);
                pol[0] = 1;
                double kf = tabl[1, i];

                for (int j = 0; j < N; ++j)
                {
                    if (i != j)
                    {
                        Polynomial lag = new Polynomial(1);
                        lag[0] = (-1) * tabl[0, j];
                        lag[1] = 1;

                        pol = pol * lag;

                        kf = kf / (tabl[0, i] - tabl[0, j]);
                    }
                }
                res = res + kf * pol;
            }
            return res;
        }
        public Polynomial Newton()
        {
            Polynomial res = new Polynomial(1);

            res[0] = 0;

            double[,] tablSeparDiffer = new double[N, N];

            for (int i = 0; i < N; ++i)
            {
                tablSeparDiffer[i, 0] = tabl[1, i];
            }

            for (int i = 1; i < N; ++i)
            {
                for (int j = 0; j < N - i; ++j)
                {
                    tablSeparDiffer[j, i] = (tablSeparDiffer[j + 1, i - 1] - tablSeparDiffer[j, i - 1])/(tabl[0, j+i] - tabl[0, j]);
                }
            }

            for (int i = 0; i < N; ++i)
            {
                Polynomial pol = new Polynomial(0);
                pol[0] = tablSeparDiffer[0,i];

                for (int j = 0; j < i; ++j)
                {
                    Polynomial lag = new Polynomial(1);
                    lag[0] = (-1) * tabl[0, j];
                    lag[1] = 1;

                    pol = pol * lag;
                }
                res = res + pol;
            }


            return res;
        }

        public Polynomial Smoothing(int n)
        {
            Polynomial res = new Polynomial(n);

            Matrix matrix = new Matrix(n + 1, n + 1);

            Vector vect = new Vector(n + 1);

            double[] C = new double[2 * n + 1];

            for (int m = 0; m < 2 * n + 1; ++m)
            {
                double sum = 0;
                for (int i = 0; i < N; ++i)
                {
                    sum += Math.Pow(tabl[0, i], m);
                }
                C[m] = sum;
            }

            for (int k = 0; k < n + 1; ++k)
            {
                double sum = 0;
                for (int i = 0; i < N; ++i)
                {
                    sum += tabl[1, i] * Math.Pow(tabl[0, i], k);
                }
                vect[k] = sum;
            }


            for(int i = 0;i < n + 1;++i)
            {
                matrix[i, i] = C[i + i];
                for (int j = i+1;j < n + 1;++j )
                {
                    matrix[i,j] = C[i+j];
                    matrix[j, i] = C[i + j];
                }
            }

            SLAE lsolve = new SLAE(matrix, vect);

            Vector aprmn = lsolve.GaussMethod();

            for(int i = 0; i < aprmn.N;i++)
            {
                res[i] = aprmn[i];
            }

            return res;
        }

    }
}
