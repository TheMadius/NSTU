using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3_VM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.textBox1.Visible = false;
            this.label1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.chart1.Series["f"].Points.Clear();
            this.chart1.Series["S(x)"].Points.Clear();

            int Connt = 5;

            double[,] tabl = new double[2, Connt];
            double H = 0.05;
            Polynomial pol;
            
            tabl[0,0] = 0;
            tabl[0,1] = 1;
            tabl[0,2] = 2;
            tabl[0,3] = 3;
            tabl[0,4] = 4;

            tabl[1,0] = 3;
            tabl[1,1] = 5;
            tabl[1,2] = 7;
            tabl[1,3] = -9;
            tabl[1,4] = 4;

            Interpolation inter = new Interpolation(tabl, Connt);


            switch(this.comboBox1.SelectedIndex)
            {
                case 0:
                    pol = inter.Lagrange();
                    break;
                case 1:
                    pol = inter.Newton();
                    break;
                case 2:
                    pol = inter.Smoothing(Convert.ToInt32(textBox1.Text));
                    break;
                default:
                    pol = inter.Lagrange();
                    break;
            }
                      
            this.Text = pol.ToString();

            for (int i = 0; i < Connt; i++)
            {
            this.chart1.Series["f"].Points.AddXY(tabl[0,i], tabl[1, i]);
            }
        
            for (double i = tabl[0, 0]; i < tabl[0, Connt-1]; i += H)
            {
                this.chart1.Series["S(x)"].Points.AddXY(i, pol.F(i));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBox1.SelectedIndex == 2)
            {
                this.textBox1.Visible = true;
                this.label1.Visible = true;
            }
            else
            {
                this.textBox1.Visible = false;
                this.label1.Visible = false;
            }
        }
    }
}
