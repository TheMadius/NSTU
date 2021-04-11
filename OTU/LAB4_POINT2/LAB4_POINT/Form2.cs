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
    public partial class Form2 : Form
    {
        Settings set;
        public Form2(Settings set)
        {
            InitializeComponent();
            this.set = set;
            this.textBox1.Text = Convert.ToString(set.Y0);
            this.textBox2.Text = Convert.ToString(set.Dy0);
            this.textBox3.Text = Convert.ToString(set.K);
            this.textBox4.Text = Convert.ToString(set.Step);
            this.textBox5.Text = Convert.ToString(set.Max);
        }

        public Settings Set { get => set; set => set = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            set = new Settings(Convert.ToDouble(this.textBox4.Text), Convert.ToDouble(this.textBox1.Text), Convert.ToDouble(this.textBox2.Text), Convert.ToDouble(this.textBox5.Text), Convert.ToDouble(this.textBox3.Text));
            this.Close();
        }
    }
}
