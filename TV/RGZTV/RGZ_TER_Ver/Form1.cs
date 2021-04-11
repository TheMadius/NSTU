using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;


namespace RGZ_TER_Ver
{
        public partial class Form1 : Form
    {
        string pash;
        decimal d, sqrtD, m, mr, staps;
        string[] dataS;
        decimal[] data, midInt, endInterval;
        int[] V;
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: drawGist(); break;
                case 1: drawVar(); break;
                case 2: drawEmpirical(); break;
                case 3: drawEmpirical(0); break;
            }
        }
        public void drawGist()
        {
            this.chart1.Series["Выборка"].Points.Clear();

            this.chart1.Series["Выборка"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            for (int i = 0; i < V.Length; i++)
            {
                this.chart1.Series["Выборка"].Points.AddXY(midInt[i], V[i] / (double)data.Length);
            }

        }

        public void drawVar()
        {
            this.chart1.Series["Выборка"].Points.Clear();

            this.chart1.Series["Выборка"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            for (int i = 0; i < V.Length; i++)
            {
                this.chart1.Series["Выборка"].Points.AddXY(midInt[i], V[i] / (double)data.Length);
            }

        }

        public void drawEmpirical()
        {
            int empV;
            this.chart1.Series["Выборка"].Points.Clear();

            this.chart1.Series["Выборка"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;

            this.chart1.Series["Выборка"].Points.AddXY(data.Min(), 0);

            this.chart1.Series["Выборка"].BorderWidth = 3;

            empV = V[0];

            for (int i = 0; i < V.Length; i++)
            {
                this.chart1.Series["Выборка"].Points.AddXY(endInterval[i], empV / (double)data.Length);
                empV += V[(i + 1 < V.Length) ? i + 1 : i];
            }

        }

        public void drawEmpirical(decimal j)
        {
            int empV;
            j = staps;
            this.chart1.Series["Выборка"].Points.Clear();

            this.chart1.Series["Выборка"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            this.chart1.Series["Выборка"].BorderWidth = 3;

            this.chart1.Series["Выборка"].Points.AddXY(data.Min() - j , 0);

            this.chart1.Series["Выборка"].Points.AddXY(data.Min(), 0);
            
            empV = V[0];

            this.chart1.Series["Выборка"].Points.AddXY(data.Min(), empV / (double)data.Length);

            for (int i = 0; i < V.Length; i++)
            {
                this.chart1.Series["Выборка"].Points.AddXY(endInterval[i], empV / (double)data.Length);
                empV += (i + 1 < V.Length) ? V[i+1] : 0 ;
                this.chart1.Series["Выборка"].Points.AddXY((i + 1 < V.Length) ? endInterval[i] : endInterval[i] + j, empV / (double)data.Length);
            }

        }


        public decimal expectation(decimal[] arr)
        {
            decimal sum = 0;
            decimal expec = 0;

            for (int i = 0; i < arr.Length; ++i)
            {
                sum += arr[i];
            }

            expec = sum / arr.Length;

            return expec;
        }
        public decimal dispersion(decimal[] arr, decimal m)
        {
            decimal sum = 0;
            decimal dis = 0;

            for (int i = 0; i < arr.Length; ++i)
            {
                sum += (arr[i] * arr[i] - m * m);
            }

            dis = sum / (arr.Length - 1);

            return dis;
        }

        public decimal exes(decimal[] arr, decimal d)
        {
            decimal sum = 0;
            decimal ex = 0;

            for (int i = 0; i < arr.Length; ++i)
            {
                sum += (arr[i] - m)* (arr[i] - m)* (arr[i] - m)* (arr[i] - m);
            }

            ex = (sum / ((arr.Length - 1)*d*d*d*d))+3;

            return ex;
        }

        public decimal asimetri(decimal[] arr, decimal d)
        {
            decimal sum = 0;
            decimal ex = 0;

            for (int i = 0; i < arr.Length; ++i)
            {
                sum += (arr[i] - m)* (arr[i] - m)* (arr[i] - m);
            }

            ex = (sum / ((arr.Length - 1) * d * d * d));

            return ex;
        }

        public int[] getFrequency(decimal[] arr)
        {
            int count = 0, n;
            int[] Ver;
            decimal max, min, stap = 0, start;

            n = Convert.ToInt32(Math.Floor(1 + 3.322 * Math.Log10(data.Length)));

            Ver = new int[n];
            midInt = new decimal[n];
            endInterval = new decimal[n];

            max = arr.Max();
            min = arr.Min();
            start = min;
            stap = (max - min) / n;
            staps = stap;
            for (int i = 0; i < n; ++i)
            {
                foreach (var x in arr)
                {
                    if (x >= start && x < start + stap)
                    {
                        count++;
                    }
                }
                Ver[i] = count;
                midInt[i] = start + stap / 2;
                endInterval[i] = start + stap;
                start = start + stap;
                count = 0;
            }

            Ver[n - 1]++;

            return Ver;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pash = Path.Combine(Environment.CurrentDirectory, "test.csv");

            using (TextFieldParser tfp = new TextFieldParser(pash))
            {
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters(";");
                while (!tfp.EndOfData)
                {
                    dataS = tfp.ReadFields();
                }
            }
            
            data = new decimal[dataS.Length];

            for (int i = 0; i < dataS.Length; ++i)
            {
                data[i] = Convert.ToDecimal(dataS[i]);
            }

          
            /*
             GenRandomNum RS = new GenRandomNum(10, 0, 1);

            data = new decimal[10000];

            for (int i = 0; i < 10000; i++)
              {
               data[i] = Convert.ToDecimal(RS.getRand());
               }
            */

            m = expectation(data);

            d = dispersion(data, m);

            sqrtD = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(d)));

            V = getFrequency(data);

            int sum = 0;
            foreach (var item in V)
            {
                sum += item;
            }

            mr = sqrtD / (decimal)Math.Sqrt(data.Length);

            this.textBox4.Text = Convert.ToString(Math.Round(m, 2));
            this.textBox2.Text = Convert.ToString(Math.Round(d, 2));
            this.textBox3.Text = Convert.ToString(Math.Round(sqrtD, 2));

            this.textBox1.Text = "0.95%";

            
            this.textBox6.Text = Convert.ToString(Math.Round(mr, 2));
            //    this.textBox7.Text = Convert.ToString(Math.Round(exes(data, d), 2));
         
            this.textBox8.Visible = false;
            this.label7.Visible = false;
            this.label8.Visible = false;
            this.textBox7.Visible = false;
        //    this.textBox8.Text = Convert.ToString(Math.Round(asimetri(data, d), 2));
            this.textBox5.Text = Convert.ToString(Math.Round(m, 2)) + " +- " + Convert.ToString(Math.Round(2 * mr, 2));

        }
    }

    public class GenRandomNum
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
