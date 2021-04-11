using System;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void AddTriangle_Click(object sender, EventArgs e)
        {

            TestNumber a1 = new TestNumber(a.Text);
            TestNumber b1 = new TestNumber(b.Text);
            TestNumber c1 = new TestNumber(c.Text);

            if ((a1.TestNum() && b1.TestNum() && c1.TestNum()) == true)
            {
                Triangle tang = new Triangle
                {
                    a = Convert.ToSingle(a1.num),
                    b = Convert.ToSingle(b1.num),
                    c = Convert.ToSingle(c1.num)
                };

                tang.TestTang();
            }

        }

    }
}
