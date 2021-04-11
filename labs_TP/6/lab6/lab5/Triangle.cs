using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    class Triangle
    {
        public float a { get; set; }
        public float b { get; set; }
        public float c { get; set; }

        public void TestTang()
        {
            MaxA();

            if (a <= 0 || b <= 0 || c <= 0)
            {
                MessageBox.Show("У треугольника стороны не могут быть меньше нуля или равны ему.");
                return;
            }

            if (a >= b + c)
            {
                MessageBox.Show("У треугольника не может одна сторона быть больше суммы двух других или равна ей.");
                return;
            }

            if (a == b && b == c && c == a)
            {
                MessageBox.Show("Это равносторонний треугольник.");
            }
            else
            {
                if (Math.Abs(a * a - (b * b + c * c)) < 0.01)
                {
                    if (a == b || b == c || c == a) 
                        MessageBox.Show("Это прямоугольный и равнобедренный треугольник.");
                    else
                        MessageBox.Show("Это прямоугольный и разносторонний треугольник.");
                }
                else
                {
                    if (a == b || b == c || c == a)
                        MessageBox.Show("Это равнобедренный треугольник.");
                    else
                        MessageBox.Show("Это разносторонний треугольник.");
                }
                    
            }

        }

        private void MaxA()
        {
            float temp;

            if (b >= a)
            {
                if (b >= c)
                {
                    temp = a;
                    a = b;
                    b = temp;
                }
                else
                {
                    temp = a;
                    a = c;
                    c = temp;
                }
            }
            else
                if (c >= a)
                {
                    temp = a;
                    a = c;
                    c = temp;
                }
        }


    }
}
