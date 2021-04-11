using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2_IM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Hous = Convert.ToInt32(this.textBox1.Text);

            double m = Convert.ToDouble(this.textBox6.Text);
            double ver = Convert.ToDouble(this.textBox5.Text);
            double dit = Convert.ToDouble(this.textBox8.Text);
            int size = Convert.ToInt32(this.textBox7.Text);

            Conveyor con = new Conveyor(ver, m, size, dit);

            con.work(Hous);

            this.textBox2.Text = ((double)con.CountFork / (double)con.CountPos).ToString();
            this.textBox3.Text = ((double)con.TimeWorkAll / (double)con.CountFork).ToString();
            this.textBox4.Text = (((double)con.TimeWork/ (double)Hous)).ToString();
        }

        private enum Quality { Avg, Congestion, ProbMachPart }

        double getM(List<double> list)
        {
            double summ = 0;

            for (int i = 0; i < list.Count; ++i)
            {
                summ += list[i];
            }

            return summ / (double)list.Count;
        }
        double getD(List<double> list)
        {
            double M = getM(list);
            double summ = 0;

            for (int i = 0; i < list.Count; ++i)
            {
                summ += (list[i] - M) * (list[i] - M);
            }

            return summ / (list.Count - 1);
        }
        private int levelOfQualityVer(double e, double t, Quality quality, double m, double ver, double dit, int size, int Hous)
        {
            int N = 50;
            Conveyor con = new Conveyor(ver, m, size, dit);
            List<double> result = new List<double>();

            while (true)
            {
                for (int i = 0; i < N; i++)
                {
                    con.work(Hous);
                    switch (quality)
                    {
                        case Quality.Congestion:
                            result.Add(con.TimeWork / Hous);
                            break;
                        case Quality.ProbMachPart:
                            result.Add(con.CountFork / con.CountPos);
                            break;
                    }
                }

                double p = getM(result);
                double newN = ( p*(1-p)* t * t) / (e * e);

                if (newN < N || N > 1000 || newN > 1000)
                {
                    if (N > 1000 || newN > 1000)
                        N = 1000;
                    return N;
                }

                N = Convert.ToInt32(newN);
                result.Clear();
            }
        }
        private int levelOfQuality(double e, double t, Quality quality, double m, double ver, double dit, int size, int Hous)
        {
            int N = 50;
            Conveyor con = new Conveyor(ver, m, size, dit);
            List<double> result = new List<double>();

            while (true)
            {
                for (int i = 0; i < N; i++)
                {
                    con.work(Hous);
                    result.Add(con.TimeWorkAll / con.CountFork);
                }

                double d = getD(result);
                double newN = (d * t * t) / (e * e);

                if (newN < N || N > 1000 || newN > 1000)
                {
                    if (N > 1000 || newN > 1000)
                        N = 1000;
                    return N;
                }

                N = Convert.ToInt32(newN);
                result.Clear();
            }
        }
        private int GetN(double e, double t, Quality quality, double m, double ver, double dit, int size, int Hous)
        {
            switch (quality)
            {
                case Quality.Avg:
                    return levelOfQuality(e, t, quality, m, ver, dit, size, Hous);
                case Quality.ProbMachPart:
                    return levelOfQualityVer(e, t, quality, m, ver, dit, size, Hous);
                case Quality.Congestion:
                    return levelOfQualityVer(e, t, quality, m, ver, dit, size, Hous);
            }
            return 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Conveyor con;

            List<double> Avg = new List<double>();
            List<double> Prob = new List<double>();
            List<double> Conge = new List<double>();
            List<double> CongeAll = new List<double>();

            int Hous = Convert.ToInt32(this.textBox1.Text);

            double m = Convert.ToDouble(this.textBox6.Text);
            double ver = Convert.ToDouble(this.textBox5.Text);
            double dit = Convert.ToDouble(this.textBox8.Text);
            int size = Convert.ToInt32(this.textBox7.Text);

            double start = Convert.ToDouble(this.textBox9.Text);
            double end = Convert.ToDouble(this.textBox10.Text);
            double step = Convert.ToDouble(this.textBox11.Text);

            int j = 0;

            for (double i = start; i < end; i += step)
            {
                Avg.Add(0);
                Prob.Add(0);
                Conge.Add(0);
                CongeAll.Add(0);

                int N;

                switch (this.comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            N = GetN(E, T, quality, m, i, dit, size, Hous);
                            break;
                        }
                    case 1:
                        {
                            N = GetN(E, T, quality, i, ver, dit, size, Hous);
                            break;
                        }
                    case 2:
                        {
                            N = GetN(E, T, quality, m, ver, dit, Convert.ToInt32(i), Hous);
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }

                for (int k = 0; k < N; k++)
                {
                    switch (this.comboBox1.SelectedIndex)
                    {
                        case 0:
                            {
                                con = new Conveyor(i, m, size, dit);
                                break;
                            }
                        case 1:
                            {
                                con = new Conveyor(ver, i, size, dit);
                                break;
                            }
                        case 2:
                            {
                                con = new Conveyor(ver, m, Convert.ToInt32(i), dit);
                                break;
                            }
                        default:
                            {
                                return;
                            }
                    }

                    con.work(Hous);

                    Avg[j] += con.CountPos;
                    Prob[j] += con.CountFork;
                    Conge[j] += con.TimeWork;
                    CongeAll[j] += con.TimeWorkAll;
                }
                Avg[j] = Prob[j] / Avg[j];
                Prob[j] = CongeAll[j] / Prob[j];
                Conge[j] = Conge[j] / (N * Hous);
                j++;
            }

            j = 0;
            this.chart1.Series["Вер. обработки"].Points.Clear();
            this.chart2.Series["Время обработки(дет/ч)"].Points.Clear();
            this.chart3.Series["Загрузка"].Points.Clear();

            this.chart1.Series["Вер. обработки"].Color = Color.Red;
            this.chart2.Series["Время обработки(дет/ч)"].Color = Color.Orange;

            for (double i = start; i < end; i += step)
            {
                this.chart1.Series["Вер. обработки"].Points.AddXY(i, Avg[j]);
                this.chart2.Series["Время обработки(дет/ч)"].Points.AddXY(i, Prob[j]);
                this.chart3.Series["Загрузка"].Points.AddXY(i, Conge[j]);
                j++;
            }

        }

        double E;
        double T;
        Quality quality;

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            double er = Convert.ToDouble(comboBox3.Text);

            switch (er)
            {
                case 0.98:
                    T = 2.054;
                    break;
                case 0.975:
                    T = 1.960;
                    break;
                case 0.95:
                    T = 1.645;
                    break;
                case 0.9:
                    T = 1.282;
                    break;
            }

            E = 1 - er;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(this.comboBox2.SelectedIndex)
            {
                case 0:
                    quality = Quality.Avg;
                    break;
                case 1:
                    quality = Quality.Congestion;
                    break;
                case 2:
                    quality = Quality.ProbMachPart;
                    break;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
