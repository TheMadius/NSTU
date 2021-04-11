using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB4_POINT
{
    public partial class Form1 : Form
    {
        Settings set;
        private Graphics graphics;
        private RectangleF point1;
        private RectangleF point2;
        public Form1()
        {
            InitializeComponent();

            set = new Settings(0.01, 0, 0, 10, 1);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            point1 = new RectangleF(pictureBox1.Width / 2 - 100, pictureBox1.Height / 2 , 10, 10);
            point2 = new RectangleF(pictureBox1.Width / 2 + 100, pictureBox1.Height / 2 , 10, 10);
            timer1.Start();
        }
        private void tick()
        {
            int i;
            graphics.Clear(Color.White);
            graphics.DrawLine(Pens.Black, pictureBox1.Width / 2 + 5, 0, pictureBox1.Width / 2+5, pictureBox1.Height);
            graphics.DrawLine(Pens.Black, 0, pictureBox1.Height / 2 + 5, pictureBox1.Width, pictureBox1.Height / 2 + 5);

            i = 100;
            while (pictureBox1.Width / 2 + i < pictureBox1.Width)
            {
                graphics.DrawLine(Pens.Gray, pictureBox1.Width / 2 + i + 5, 0, pictureBox1.Width / 2 + i + 5, pictureBox1.Height);
                graphics.DrawLine(Pens.Gray, pictureBox1.Width / 2 - i + 5, 0, pictureBox1.Width / 2 - i + 5, pictureBox1.Height);
                i += 100;
            }

            i = 100;
            while (pictureBox1.Height / 2 + i < pictureBox1.Height)
            {
                graphics.DrawLine(Pens.Gray, 0, pictureBox1.Height / 2 + i + 5, pictureBox1.Width, pictureBox1.Height / 2 + i + 5);
                graphics.DrawLine(Pens.Gray, 0, pictureBox1.Height / 2 - i + 5, pictureBox1.Width, pictureBox1.Height / 2 - i + 5);
                i += 100;
            }

            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            StringFormat drawFormat = new StringFormat();
            graphics.DrawString("0", drawFont, drawBrush, pictureBox1.Width / 2 + 5, pictureBox1.Height / 2 + 5, drawFormat);
            graphics.DrawString("1", drawFont, drawBrush, pictureBox1.Width / 2 + 5, pictureBox1.Height / 2 - 100 + 5, drawFormat);
            graphics.DrawString("1", drawFont, drawBrush, pictureBox1.Width / 2 + 100 + 5, pictureBox1.Height / 2 + 5, drawFormat);
            graphics.DrawString("-1", drawFont, drawBrush, pictureBox1.Width / 2 + 5, pictureBox1.Height / 2 + 100 + 5, drawFormat);
            graphics.DrawString("-1", drawFont, drawBrush, pictureBox1.Width / 2 - 100 + 5, pictureBox1.Height / 2 + 5, drawFormat);
            graphics.DrawString("Im", drawFont, drawBrush, pictureBox1.Width / 2 + 5, 0, drawFormat);
            graphics.DrawString("Re", drawFont, drawBrush, pictureBox1.Width - 20, pictureBox1.Height / 2 + 5, drawFormat);

            graphics.FillEllipse(Brushes.Red, point1);
            graphics.FillEllipse(Brushes.Red, point2);
            pictureBox1.Refresh();

            UpdateChart();
        }

        public void UpdateChart()
        {
            DifferentiaEquationl2nd dif;
            double x1 = (point1.X  - pictureBox1.Width / 2) /100;
            double x2 = (point2.X - pictureBox1.Width / 2) /100;
            double y1 = (point1.Y  - pictureBox1.Height / 2) /100;
            double y2 = (point2.Y  - pictureBox1.Height / 2) /100;

            if(y1 != 0)
            {
                dif = new DifferentiaEquationl2nd(1, -2 * x1, x1 * x1 + y1 * y1,set.K);
            }
            else
            {
                dif = new DifferentiaEquationl2nd(1, -(x1+x2), x1 * x2,set.K);
            }

            chart1.Series.Clear();
            chart1.Series.Add("Y");

            chart1.Series["Y"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Y"].BorderWidth = 3;

            double[] y = dif.decision(set.Step, set.Max, set.Y0, set.Dy0);
            int j = 0;
            for (double i = 0; i <= set.Max;i += set.Step)
            {
                this.chart1.Series["Y"].Points.AddXY(i, y[j]);
                j++;
            }
            this.textBox1.Text = dif.toStr();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tick();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(!checkBox1.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (point1.Contains(e.Location))
                    {
                        point1.X = e.Location.X - 5;
                        point1.Y = e.Location.Y - 5;
                        if (point1.Y != pictureBox1.Height / 2 + 5)
                        {
                            point2.Location = point1.Location;
                            point2.Y = (pictureBox1.Height / 2) + ((pictureBox1.Height / 2 - point1.Y));
                        }
                    }
                    if (point2.Contains(e.Location))
                    {
                        point2.X = e.Location.X - 5;
                        point2.Y = e.Location.Y - 5;
                        if (point2.Y != pictureBox1.Height / 2 + 5)
                        {
                            point1.Location = point2.Location;
                            point1.Y = (pictureBox1.Height / 2) + ((pictureBox1.Height / 2 - point2.Y));
                        }
                    }
                }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (point1.Contains(e.Location))
                    {
                        point1.X = e.Location.X - 5;
                    }
                    else
                    if (point2.Contains(e.Location))
                    {
                        point2.X = e.Location.X - 5;
                    }
                }
            }
            tick();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox1.Checked)
            {
                this.point1.Y = pictureBox1.Height / 2;
                this.point2.Y = pictureBox1.Height / 2;
            }
        }

        private void опцииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(set);

            f.ShowDialog();

            set = f.Set;
        }
    }
}
