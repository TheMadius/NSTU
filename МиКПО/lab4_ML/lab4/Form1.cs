using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {

        Random gen = new Random();
        public Form1()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
        }
        public double genSimple()
        {
            return gen.NextDouble() * 20.0;
        }
        public double raylei(double sigma)
        {
            return Math.Sqrt(sigma * (-2) * Math.Log(gen.NextDouble()));
        }
        public double Exp(double lambda)
        {
            double r = gen.NextDouble();

            return func(lambda, r);
        }
        double func(double lambda, double x)
        {
            return -(1 / lambda) * Math.Log(x);
        }
        double generator()
        {
            double r;
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    r = genSimple();
                    break;
                case 1:
                    r = Exp(1.0 / 10.0);
                    break;
                case 2:
                    r = raylei(64);
                    break;
                default:
                    r = 0;
                    break;
            }

            return r;
        }
        List<double> getX(int n)
        {
            List<double> X = new List<double>();

            for (int i = 0; i < n; i++)
                X.Add(generator());

            return X;
        }
        public double F(double m, int n)
        {
            double f = 0;
            for(int i = 1; i <= n; i++ )
            {
                f += 1 / ( m - i );
            }
            return f;
        }
        public double G(double m, double A, int n)
        {
            return n / (m - A);
        }
        public double getA(List<double> X)
        {
            double A = 0;
            double sum = 0;
            for(int i = 1; i <= X.Count; i++)
            {
                A += i * X[i - 1];
                sum += X[i - 1];
            }
            return A / sum;
        }
        public List<double> conditionA(List<double> X)
        {
            while (true)
            {
                double A = getA(X);
                double res = ((X.Count + 1) / 2.0);
                if (A > res)
                    break;
                else
                    X.Add(generator());
            }

            return X;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(this.textBox3.Text);
            List<double> X = getX(n);

            X = conditionA(X);

            int m = X.Count + 1;
            n = X.Count;
            double A = getA(X);
            double oldabs = 0;

            this.dataGridView1.Rows.Clear();
            while (true)
            {
                double f = F(m, n);
                double g = G(m, A, n);
                double abs = Math.Abs(f - g);

                this.dataGridView1.Rows.Add(new object[] { m, Math.Round(f,2) , Math.Round(g, 2), Math.Round(abs, 5) });

                if (this.dataGridView1.Rows.Count != 2)
                {
                    if (oldabs < abs)
                        break;
                }
                oldabs = abs;
                m++;
            }

            this.textBox1.Text = n.ToString();
            this.textBox2.Text = (m - 1).ToString();
        }
    }
}
