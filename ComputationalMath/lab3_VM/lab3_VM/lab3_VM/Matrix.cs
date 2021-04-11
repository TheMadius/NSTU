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

        public double this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { matrix[x, y] = value; }
        }

        public void ProcessFunctionOverData(Action<int, int> func)
        {
            for (var i = 0; i < this.M; i++)
            {
                for (var j = 0; j < this.N; j++)
                {
                    func(i, j);
                }
            }
        }

        public static Matrix operator *(Matrix matrix, double value)
        {
            var result = new Matrix(matrix.M, matrix.N);
            result.ProcessFunctionOverData((i, j) =>
                result[i, j] = matrix[i, j] * value);
            return result;
        }

        public static Matrix operator *(Matrix matrix, Matrix matrix2)
        {
            if (matrix.N != matrix2.M)
            {
                throw new ArgumentException("matrixes can not be multiplied");
            }

            var result = new Matrix(matrix.M, matrix2.N);

            result.ProcessFunctionOverData((i, j) => {
                for (var k = 0; k < matrix.N; k++)
                {
                    result[i, j] += matrix[i, k] * matrix2[k, j];
                }
            });
            return result;
        }

    }
}
