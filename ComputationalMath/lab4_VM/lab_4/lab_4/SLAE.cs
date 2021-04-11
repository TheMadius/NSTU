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

        public Vector Sweeps()
        {
            Vector result = new Vector(this.B.N);

            double[] a = new double[B.N -1];
            double[] b = new double[B.N -1];

            if(B.N == 1)
            {
                result[0] = B[0]/A[0, 0];
                return result;
            }

            a[0] = this.A[0, 1] / this.A[0, 0];

            b[0] = this.B[0] / this.A[0, 0];

            Matrix newMat = new Matrix(A.M, A.M);
            Vector newVec = new Vector(this.B.N);

            newMat[0, 0] = this.A[0, 0];
            newMat[0, 1] = this.A[0, 1];

            newVec[0] = B[0];

            for (int i = 1; i < A.M; ++i)
            {
                newMat[i, i] = this.A[i, i] - this.A[i, i - 1] * a[i - 1];

                newVec[i] = this.B[i] - this.A[i, i - 1] * b[i - 1];

                if (i < A.M - 1)
                {
                    newMat[i, i + 1] = this.A[i, i + 1];
                }
                else
                {
                    break;
                }

                a[i] = newMat[i, i + 1] / newMat[i, i];
                b[i] = newVec[i] / newMat[i, i];
            }

            result[B.N - 1] = newVec[B.N - 1] / newMat[B.N - 1, B.N - 1];

            for (int i = B.N - 2; i >= 0;--i)
            {
                result[i] = b[i] - a[i] * result[i + 1];
            }

            return result;
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
