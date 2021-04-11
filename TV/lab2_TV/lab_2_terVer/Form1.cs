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

namespace lab_2_terVer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static Random rd= new Random();
        public static int countE = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.chart1.ChartAreas[0].AxisX.Minimum = 0;
            this.chart1.ChartAreas[0].AxisY.Maximum = 4;
            this.chart2.ChartAreas[0].AxisX.Minimum = 0;
            this.chart2.Series["Ave"].Points.AddXY(countE, 0);
            countE++;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.chart1.Series["S"].Points.Clear();

            int nowS, countShoot;
            double r;

            countShoot = 0;

            double[,] matrix = new double[3, 3];

            matrix[0, 0] = Convert.ToDouble(TB00.Text);
            matrix[0, 1] = Convert.ToDouble(TB01.Text);
            matrix[0, 2] = Convert.ToDouble(TB02.Text);
            matrix[1, 0] = Convert.ToDouble(TB10.Text);
            matrix[1, 1] = Convert.ToDouble(TB11.Text);
            matrix[1, 2] = Convert.ToDouble(TB12.Text);
            matrix[2, 0] = Convert.ToDouble(TB20.Text);
            matrix[2, 1] = Convert.ToDouble(TB21.Text);
            matrix[2, 2] = Convert.ToDouble(TB22.Text);

            double[] vector = new double[3];

            vector[0] = Convert.ToDouble(textBox1.Text);
            vector[1] = Convert.ToDouble(textBox2.Text);
            vector[2] = Convert.ToDouble(textBox3.Text);

            if ((matrix[0, 0] + matrix[0, 1] + matrix[0, 2]) != 1)
            {
                MessageBox.Show("S0:Ошибка! Сумма должна быть 1");
                return;
            }
            if ((matrix[1, 0] + matrix[1, 1] + matrix[1, 2]) != 1)
            {
                MessageBox.Show("S1:Ошибка! Сумма должна быть 1");
                return;
            }
            if ((matrix[2, 0] + matrix[2, 1] + matrix[2, 2]) != 1)
            {
                MessageBox.Show("S2:Ошибка! Сумма должна быть 1");
                return;
            }
            if ((vector[0] + vector[1] + vector[2]) != 1)
            {
                MessageBox.Show("P0:Ошибка! Сумма должна быть 1");
                return;
            }



            r = rd.NextDouble();

            if (r > vector[0] + vector[1])
                nowS = 2;
            else if (r > vector[0])
                nowS = 1;
            else
                nowS = 0;

            this.chart1.Series["S"].Points.AddXY(countShoot, nowS+1);

            while (nowS != 2)
            {
                r = rd.NextDouble();

                if (r > matrix[nowS,0] + matrix[nowS,1])
                    nowS = 2;
                else if (r > matrix[nowS,0])
                    nowS = 1;
                else
                    nowS = 0;

                countShoot++;

                this.chart1.Series["S"].Points.AddXY(countShoot, nowS+1);

            }
           
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int countEx;
            double Aver = 0;
            int nowS, countShoot;
            double r;

            countEx = Convert.ToInt32(textBox4.Text);
                 
            double[,] matrix = new double[3, 3];

            matrix[0, 0] = Convert.ToDouble(TB00.Text);
            matrix[0, 1] = Convert.ToDouble(TB01.Text);
            matrix[0, 2] = Convert.ToDouble(TB02.Text);
            matrix[1, 0] = Convert.ToDouble(TB10.Text);
            matrix[1, 1] = Convert.ToDouble(TB11.Text);
            matrix[1, 2] = Convert.ToDouble(TB12.Text);
            matrix[2, 0] = Convert.ToDouble(TB20.Text);
            matrix[2, 1] = Convert.ToDouble(TB21.Text);
            matrix[2, 2] = Convert.ToDouble(TB22.Text);

            double[] vector = new double[3];

            vector[0] = Convert.ToDouble(textBox1.Text);
            vector[1] = Convert.ToDouble(textBox2.Text);
            vector[2] = Convert.ToDouble(textBox3.Text);

           

            if ((matrix[0, 0] + matrix[0, 1] + matrix[0, 2])!= 1)
            {
                MessageBox.Show("S0:Ошибка! Сумма должна быть 1");
                return;
            }
            if ((matrix[1, 0] + matrix[1, 1] + matrix[1, 2])!= 1)
            {
                MessageBox.Show("S1:Ошибка! Сумма должна быть 1");
                return;
            }
            if ((matrix[2, 0] + matrix[2, 1] + matrix[2, 2])!= 1)
            {
                MessageBox.Show("S2:Ошибка! Сумма должна быть 1");
                return;
            }
            if ((vector[0] + vector[1] + vector[2])!= 1)
            {
                MessageBox.Show("P0:Ошибка! Сумма должна быть 1");
                return;
            }



            for (int i = 0; i < countEx;i++ )
            {
                countShoot = 0;

                r = rd.NextDouble();

                if (r > vector[0] + vector[1])
                    nowS = 2;
                else if (r > vector[0])
                    nowS = 1;
                else
                    nowS = 0;

                while (nowS != 2)
                {
                    r = rd.NextDouble();

                    if (r > matrix[nowS, 0] + matrix[nowS, 1])
                        nowS = 2;
                    else if (r > matrix[nowS, 0])
                        nowS = 1;
                    else
                        nowS = 0;

                    countShoot++;

                }

                Aver += countShoot;

            }

            string path = Path.Combine(Environment.CurrentDirectory, "test.txt");

            this.chart2.Series["Ave"].Points.AddXY(countE, Aver / countEx);

            countE++;

            File.AppendAllText(path, Convert.ToString(Aver/ countEx) + "\n");

        }
    }
}
