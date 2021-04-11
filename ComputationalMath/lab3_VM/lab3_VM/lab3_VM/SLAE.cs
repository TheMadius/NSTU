using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_VM
{
    public class SLAE
    {
        private Matrix A = null;
        private Vector B = null;
        public SLAE(Matrix a, Vector b)
        {
            A = a;
            B = b;
        }

        public Vector GaussMethod()
        {
            Vector result = new Vector(this.B.N);

            Matrix newMat = A;
            Vector newVec = B;


            for (int k = 0; k < newMat.N - 1; k++)
            {
                for (int i = k; i < newMat.N - 1; ++i)
                {
                    double kaf;
                    kaf = newMat[i + 1,k] / newMat[k,k];

                    for (int j = 0; j < newMat.N; ++j)
                    {
                        newMat[i + 1,j] = newMat[i + 1,j] - kaf * newMat[k,j];
                    }
                    newVec[i + 1] = newVec[i + 1] - kaf * newVec[k];
                }
            }


            for (int i = newMat.N - 1; i >= 0; --i)
            {
                double sum = 0;
                for (int j = i + 1; j < newMat.N; ++j)
                {
                    sum += newMat[i,j] * result[j];
                }
                result[i] = (newVec[i] - sum) / newMat[i,i];
            }


            return result;
        }

        public double moda(Vector vect1, Vector vect2)
        {
            double sum = 0;
            for (int i = 0; i < vect2.N; ++i)
            {
                sum += Math.Abs(vect2[i] - vect1[i]);
            }
            return sum;
        }

        public Vector simIter(double e)
        {
            Matrix newMat = new Matrix(A.N, A.M);
            Vector newVec = new Vector(this.B.N);

            Vector kX = new Vector(this.B.N);
            Vector kX2 = new Vector(this.B.N);

            for (int i = 0; i < newMat.N; i++)
                for (int j = 0; j < newMat.N; j++)
                {
                    if (i == j)
                        newMat[i,j] = 0;
                    else
                        newMat[i,j] = (-1) * (A[i,j] / A[i,i]);
                }


            for (int i = 0; i < newMat.N; i++)
            {
                newVec[i] = B[i] / A[i,i];
                kX[i] = newVec[i];
            }

            while (true)
            {
                for (int i = 0; i < newMat.N; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < newMat.N; j++)
                    {
                        sum += newMat[i,j] * kX[j];
                    }

                    kX2[i] = newVec[i] + sum;
                }

                if (moda(kX, kX2) <= e)
                {
                    return kX2;
                }

                kX = kX2;

            }

        }

    }
}
