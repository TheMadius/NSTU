using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace lab1_terver
{
  public partial class Form1 : Form
    {
       public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public double integNorm(double start, double end, double m, double d)
        {
            double sum = 0;
            double stap = (end - start) / 100;
            double vel;


            for (double i = start; i < end; i += stap)
            {
                vel = (1 / (d * Math.Sqrt(2 * Math.PI)))*Math.Exp((-(i - m) * (i - m))/(2*d*d));

                sum += vel * stap;
            }

            return sum;
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            int con = 50;

            RStat.Series.Clear();

            RStat.Series.Add("Количество");

            double m = Convert.ToDouble(textBox2.Text);

            double d = Convert.ToDouble(textBox3.Text);

            int n = Convert.ToInt32(textBox5.Text);

            con = Convert.ToInt32(textBox4.Text);

            GenRandomNum RS = new GenRandomNum(n, m, d);

            int size = Convert.ToInt32(textBox1.Text);

            double[] arr = new double[size];

            for (int k = 0; k < size; k++)
             {
                arr[k] = RS.getRand();
             }

            double max = arr.Max();
            double min = arr.Min();

            double step = (max - min)/con;

            double start = min + step;

            int j;
            double i;

            double[] chasrot = new double[con];
            double[] oChasrot = new double[con];

            for ( i = start,j = 0; i < max; i+=step,j++)
            await Task.Run(() =>
            {
                this.Invoke(new Action(()=>
                {
                    chasrot[j] = AddDate(arr, i, step, size);
                }
                ));
            }
            );

            for (i = min, j = 0; i < max-step; i += step,j++)
            {
                oChasrot[j] = integNorm(i,i+step,m,d);
            }

            double Xi = 0;

            for (int l = 0; l < con - 1; l++)
                Xi += (size * (chasrot[l] - oChasrot[l]) * (chasrot[l] - oChasrot[l])) / oChasrot[l];



            textBox6.Text = Xi.ToString();

        }

        public double AddDate(double[] arr, double start, double step,int size)
        {
            int count = 0;
            foreach (var x in arr)
            {
                if (x < start && x > start - step)
                {
                    count++;
                }
            }

            RStat.Series["Количество"].Points.AddXY(start - (step / 2), (double)count/size);
            return ((double)count / (double)size);
        }
    }

 class GenRandomNum
    {
        public GenRandomNum(int n, double m, double G)
        {
            this.n = n;
            this.m = m;
            this.G = G;
        }

        public double getRand()
        {
            double v, z, x, r;
            
            v = 0;

            for (int i = 0; i < this.n; i++)
            {
                r = rand.NextDouble();
                v += r;
            }

            z = (v - ((double)n / 2)) / Math.Sqrt(((double)n / 12));


            x = z * this.G + this.m;

            return x;
        }

        static Random rand = new Random();
        private int n;
        private double m;
        private double G;
    };
}
