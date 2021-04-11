using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_VM
{
    public class Matrix
    {
        private double[,] matrix = null;

        private int n;
        private int m;

        public Matrix(int x = 1, int y = 1)
        {
            matrix = new double[x, y];

            n = x;
            m = y;
        }

        public int N { get => n; }
        public int M { get => m; }

        public double this[int y, int x]
        {
            get { return matrix[y, x]; }
            set { matrix[y, x] = value; }
        }

        public double getDeterminant()
        {
            double Deter = 0;
            if (this.M == 1)
            {
                return matrix[0, 0];
            }

            for (int i = 0; i < N; ++i)
            {
                Matrix minor = new Matrix(N - 1, M - 1);
                int x = 0, y = 0;
                for (int j = 1; j < N; ++j)
                {
                    for (int k = 0; k < M; ++k)
                    {
                        if (k != i)
                        {
                            minor[y, x] = this.matrix[j,k];
                            x++;
                        }
                    }
                    x = 0;
                    y++;
                }

                Deter += Math.Pow(-1, i) * this.matrix[0,i]*minor.getDeterminant();
            }

            return Deter;
        }
    }
}
