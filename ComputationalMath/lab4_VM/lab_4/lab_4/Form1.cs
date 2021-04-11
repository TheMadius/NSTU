using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab3_VM;

namespace lab_4
{
    public partial class Form1 : Form
    {

        public enum Increment { Right, Left, Both_Sides };
        public Form1()
        {
            InitializeComponent();

            this.textBox1.Visible = false;
            this.label1.Visible = false;
        }

        public double Df2(double x, Spline spline, double h, Increment increment)
        {
            double dF;

            switch (increment)
            {
                case Increment.Right:
                    dF = (spline.getY(x + h + h) - 2*spline.getY(x + h) + spline.getY(x)) / (h*h);
                    break;
                case Increment.Left:
                    dF = (spline.getY(x) - 2*spline.getY(x - h) + spline.getY(x - h - h)) / (h*h);
                    break;
                case Increment.Both_Sides:
                    dF = (spline.getY(x + h + h) - 2 * spline.getY(x) + spline.getY(x - h - h)) / (4 * h * h);
                    break;
                default:
                    dF = 0;
                    break;
            }

            return dF;
        }


        public double Df(double x, Spline spline, double h, Increment increment )
        {
            double dF;

            switch (increment)
            {
                case Increment.Right:
                    dF = (spline.getY(x + h) - spline.getY(x)) / h;
                    break;
                case Increment.Left:
                    dF = (spline.getY(x) - spline.getY(x - h)) / h;
                    break;
                case Increment.Both_Sides:
                    dF = (spline.getY(x + h) - spline.getY(x - h)) / (2 * h);
                    break;
                default:
                    dF = 0;
                    break;
            }

            return dF;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.chart1.Series["f"].Points.Clear();
            this.chart1.Series["S(x)"].Points.Clear();
            Spline spline;

            int Connt = 5;

            double[,] tabl = new double[2, 5];
            double[,] tablDf = tabl;
            double[,] tablDif2;
            double H = 0.05;

            tabl[0, 0] = 1;
            tabl[0, 1] = 2;
            tabl[0, 2] = 3;
            tabl[0, 3] = 4;
            tabl[0, 4] = 5;

            tabl[1, 0] = 4;
            tabl[1, 1] = 2;
            tabl[1, 2] = 8;
            tabl[1, 3] = 1;
            tabl[1, 4] = -1;

            Spline spInit = new Spline(tabl, Connt);

            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    spline = spInit;
                    for (int i = 0; i < Connt; i++)
                    {
                        this.chart1.Series["f"].Points.AddXY(tabl[0, i], tabl[1, i]);
                    }
                    break;
                case 1:
                    {
                        double h = Convert.ToDouble(this.textBox1.Text);

                        tablDf[1, 0] = Df(tablDf[0, 0], spInit, h, Increment.Right);

                        for (int i = 1; i < Connt - 1; ++i)
                        {
                            tablDf[1, i] = Df(tablDf[0, i], spInit, h, Increment.Both_Sides);
                        }

                        tablDf[1, Connt - 1] = Df(tablDf[0, Connt - 1], spInit, h, Increment.Left);

                        Spline spDf = new Spline(tablDf, Connt);

                        spline = spDf;
                        for (int i = 0; i < Connt; i++)
                        {
                            this.chart1.Series["f"].Points.AddXY(tablDf[0, i], tablDf[1, i]);
                        }

                    }
                    break;
                case 2:
                    {
                        double h = Convert.ToDouble(this.textBox1.Text);
                        int countPoin = 9;
                        double start = tabl[0,0];
                        double step = (tabl[0,4] - start)/(countPoin-1);
                        tablDif2 = new double[2, countPoin];

                        for(int i = 0; i < countPoin; i++ )
                        {
                            tablDif2[0, i] = start;
                            start += step;
                        }

                        tablDif2[1, 0] = Df2(tablDif2[0, 0], spInit, h, Increment.Right);

                        for (int i = 1; i < countPoin - 1; ++i)
                        {
                            tablDif2[1, i] = Df2(tablDif2[0, i], spInit, h, Increment.Both_Sides);
                        }

                        tablDif2[1, Connt - 1] = Df2(tablDif2[0, Connt - 1], spInit, h, Increment.Left);

                        Spline spDf = new Spline(tablDif2, countPoin);

                        spline = spDf;

                        for (int i = 0; i < countPoin; i++)
                        {
                            this.chart1.Series["f"].Points.AddXY(tablDif2[0, i], tablDif2[1, i]);
                        }
                    }

                    break;
                default:
                    spline = spInit;
                    for (int i = 0; i < Connt; i++)
                    {
                        this.chart1.Series["f"].Points.AddXY(tabl[0, i], tabl[1, i]);
                    }
                    break;
            }

            for (double i = tabl[0, 0]; i <= tabl[0, Connt - 1]; i += H)
            {
                if (i == tabl[0, Connt - 1])
                {
                    int t;
                    t = 8;
                }
                this.chart1.Series["S(x)"].Points.AddXY(i, spline.getY(i));
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    this.textBox1.Visible = false;
                    this.label1.Visible = false;
                    break;
                default:
                    this.textBox1.Visible = true;
                    this.label1.Visible = true;
                    break;
            }
        }
    }
}
