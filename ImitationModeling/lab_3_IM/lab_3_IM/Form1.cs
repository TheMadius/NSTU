using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_3_IM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            double timeModel = Convert.ToDouble(this.textBox3.Text);
            double X1 = Convert.ToDouble(this.textBox1.Text);
            double X2 = Convert.ToDouble(this.textBox2.Text);
            double Y = Convert.ToDouble(this.textBox5.Text);
            double CA = Convert.ToDouble(this.textBox8.Text);
            double CB = Convert.ToDouble(this.textBox9.Text);
            double A0 = Convert.ToDouble(this.textBox6.Text);
            double B0 = Convert.ToDouble(this.textBox7.Text);
            double PA = Convert.ToDouble(this.textBox10.Text);
            double PB = Convert.ToDouble(this.textBox11.Text);

            DynamicModeling stat = new DynamicModeling(X1, X2,Y,CA,CB,A0,B0,PA,PB);

            stat.work(timeModel);

            List<double> Det;

            switch (this.comboBox1.SelectedIndex)
            {
                case 0:
                    Det = stat.DaneDit1;
                    break;
                case 1:
                    Det = stat.Y0A1;
                    break;
                case 2:
                    Det = stat.Y0B1;
                    break;
                case 3:
                    {
                        string t1, t2, t3, t4, t5;
                        t1 = "Y11";
                        t2 = "Y12";
                        t3 = "Y21";
                        t4 = "Y22";
                        t5 = "Y23";

                        chart1.Series.Add(t1);
                        chart1.Series.Add(t2);
                        chart1.Series.Add(t3);
                        chart1.Series.Add(t4);
                        chart1.Series.Add(t5);

                        chart1.Series[t1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        chart1.Series[t2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        chart1.Series[t3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        chart1.Series[t4].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        chart1.Series[t5].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                        List<double> Y11,Y12,Y21,Y22,Y13;

                        Y11 = stat.Y111;
                        Y12 = stat.Y121;
                        Y13 = stat.Y131;
                        Y21 = stat.Y211;
                        Y22 = stat.Y221;

                        for (int i = 0; i < Y11.Count; ++i)
                        {
                            chart1.Series[t1].Points.AddXY(i, Y11[i]);
                            chart1.Series[t2].Points.AddXY(i, Y12[i]);
                            chart1.Series[t3].Points.AddXY(i, Y13[i]);
                            chart1.Series[t4].Points.AddXY(i, Y21[i]);
                            chart1.Series[t5].Points.AddXY(i, Y22[i]);
                        }
                        return;
                    }
                default:
                    return;
            }
            string text = this.comboBox1.Text;

            chart1.Series.Add(text);

            chart1.Series[text].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            chart1.Series[text].BorderWidth = 3;

            for (int i = 0; i < Det.Count;++i)
            {
                chart1.Series[text].Points.AddXY(i, Det[i]);
            }

            this.textBox4.Text = stat.DaneDit1[stat.DaneDit1.Count - 1].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
