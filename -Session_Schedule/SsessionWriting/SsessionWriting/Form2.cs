using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SsessionWriting.Class;
using Newtonsoft.Json;
using System.IO;

namespace SsessionWriting
{
    public partial class Form2 : Form
    {
        Audience au;
        public Form2()
        {
            InitializeComponent();
        }

        public Audience Au { get => au; set => au = value; }

        private void Button1_Click(object sender, EventArgs e)
        {
            string numder = textBox1.Text;
            ushort cout;

            try
            {
                cout = Convert.ToUInt16(textBox2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный ввод данных!!");
                return;
            }

            au = new Audience(cout, numder);

            Close();

        }
    }
}
