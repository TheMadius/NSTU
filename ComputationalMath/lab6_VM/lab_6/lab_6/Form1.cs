using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_6
{
    public partial class Form1 : Form
    {

        bool[] flag = new bool[5];
        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 4; ++i)
            {
                flag[i] = true;
            }

            this.label2.Visible = false;
            this.textBox2.Visible = false;

            this.button2.Enabled = false;

        }

        public double Func(double x, double y)
        {
            return 3 - y - x;
        }
        public double exactFun(double x)
        {
            return 4 - x - 4 * Math.Exp(-x);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.chart1.Series.Clear();
            Differential diff = new Differential(Func, 0,0);
            string name;
            double[][] points;
            double x = Convert.ToDouble(this.textBox3.Text);
            double h = Convert.ToDouble(this.textBox1.Text);
            int BorderWidth = 1;


            for (int i = 0; i < 5; ++i)
            {
                flag[i] = true;
            }

            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        name = "Euler";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Red;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        flag[this.comboBox1.SelectedIndex] = false;

                        points = diff.Euler(h, x);
                        break;
                    }
                case 1:
                    {
                        double er = Convert.ToDouble(this.textBox2.Text);
                        name = "Merson";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Blue;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        points = diff.Merson(h, x, er);

                        flag[this.comboBox1.SelectedIndex] = false;

                        break;
                    }
                case 2:
                    {
                        name = "CorrectEuler";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Lime;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        points = diff.CorrectedEuler(h, x);

                        flag[this.comboBox1.SelectedIndex] = false;

                        break;
                    }

                case 3:
                    {
                        name = "Adams5";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Orange;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        points = diff.Adams5(h, x);

                        flag[this.comboBox1.SelectedIndex] = false;

                        break;
                    }
                case 4:
                    {
                        name = "exactFun";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Green;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        points = new double[2][];
                        List<double> xi = new List<double>();
                        List<double> yi = new List<double>();

                        for(double i = 0;i < x ; i+= h)
                        {
                            xi.Add(i);
                            yi.Add( exactFun(i));
                        }

                        points[0] = xi.ToArray();
                        points[1] = yi.ToArray();

                        flag[this.comboBox1.SelectedIndex] = false;
                        break;

                    }
                default:
                    {
                        name = "Euler";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Red;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        points = diff.Euler(h, x);

                        flag[0] = false;

                        break;
                    }
            }

            for (int i = 0; i < points[0].Length; ++i)
            {
                this.chart1.Series[name].Points.AddXY(points[0][i], points[1][i]);
            }

            this.button2.Enabled = true;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBox1.SelectedIndex)
            {
                case 1:
                    this.label2.Visible = true;
                    this.textBox2.Visible = true;
                    break;
                default:
                    this.label2.Visible = false;
                    this.textBox2.Visible = false;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Differential diff = new Differential(Func, 0, 0);
            string name;
            double[][] points;
            double x = Convert.ToDouble(this.textBox3.Text);
            double h = Convert.ToDouble(this.textBox1.Text);
            int BorderWidth = 1;

            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        if (!flag[this.comboBox1.SelectedIndex])
                        {
                            return;
                        }
                            name = "Euler";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Red;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        flag[this.comboBox1.SelectedIndex] = false;

                        points = diff.Euler(h, x);
                        break;
                    }
                case 1:
                    {
                        if (!flag[this.comboBox1.SelectedIndex])
                        {
                            return;
                        }
                        double er = Convert.ToDouble(this.textBox2.Text);
                        name = "Merson";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Blue;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        points = diff.Merson(h, x, er);

                        flag[this.comboBox1.SelectedIndex] = false;

                        break;
                    }
                case 2:
                    {
                        if (!flag[this.comboBox1.SelectedIndex])
                        {
                            return;
                        }
                        name = "CorrectEuler";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Black;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        points = diff.CorrectedEuler(h, x);

                        flag[this.comboBox1.SelectedIndex] = false;

                        break;
                    }

                case 3:
                    {
                        if (!flag[this.comboBox1.SelectedIndex])
                        {
                            return;
                        }
                        name = "Adams5";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Orange;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        flag[this.comboBox1.SelectedIndex] = false;

                        points = diff.Adams5(h, x);

                        break;
                    }
                case 4:
                    {
                        if (!flag[this.comboBox1.SelectedIndex])
                        {
                            return;
                        }
                        name = "exactFun";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].Color = Color.Green;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        points = new double[2][];

                        List<double> xi = new List<double>();
                        List<double> yi = new List<double>();

                        for (double i = 0; i < x; i += h)
                        {
                            xi.Add(i);
                            yi.Add(exactFun(i));
                        }

                        points[0] = xi.ToArray();
                        points[1] = yi.ToArray();

                        flag[this.comboBox1.SelectedIndex] = false;
                        break;

                    }

                default:
                    {
                        if (!flag[0])
                        {
                            return;
                        }
                        name = "Euler";
                        this.chart1.Series.Add(name);
                        this.chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        this.chart1.Series[name].BorderWidth = BorderWidth;

                        flag[0] = false;

                        points = diff.Euler(h, x);
                        break;
                    }
            }

            for (int i = 0; i < points[0].Length; ++i)
            {
                this.chart1.Series[name].Points.AddXY(points[0][i], points[1][i]);
            }

        }
    }
}
