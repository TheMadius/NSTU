using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turing_machine
{
    public partial class addAct : Form
    {
        string alphabet;
        ActionT act = new ActionT();
        int count;
        bool Flag1, Flag2, Flag3;
        bool FlagCheck = false;

        internal ActionT Act { get => act;  }

        public addAct(string alfa,int countQ)
        {
            InitializeComponent();
            alphabet = alfa;
            count = countQ;
            this.button1.Enabled = false;
            Flag1 = false;
            Flag2 = false;
            Flag3 = false;

            for (int i = 0; i < countQ;++i)
            {
                this.comboBox3.Items.Add("Q" + (i + 1));
            }
            this.comboBox3.Items.Add("Конец");

            for (int i = 0; i < alphabet.Length; ++i)
            {
                this.comboBox1.Items.Add(alphabet[i]);
            }
            this.comboBox1.Items.Add('_');
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            act.Symbol = this.comboBox1.Text;
            Flag1 = true;
            if (Flag1 && Flag2 && Flag3)
            {
                this.button1.Enabled = true;
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            act.Status = (this.comboBox3.SelectedIndex != count) ? this.comboBox3.SelectedIndex + 1 : 0;
            Flag3 = true;
            if (Flag1 && Flag2 && Flag3)
            {
                this.button1.Enabled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBox2.SelectedIndex)
            {
                case 0:
                    act.Act = Turing_machine.Move.Right;
                    break;
                case 1:
                    act.Act = Turing_machine.Move.Left;
                    break;
                case 2:
                    act.Act = Turing_machine.Move.Stand;
                    break;
            }

            Flag2 = true;

            if (Flag1 && Flag2 && Flag3)
            {
                this.button1.Enabled = true;
            }
        }

        private void addAct_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!FlagCheck)
                act = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FlagCheck = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            act = null;
            this.Close();
        }
    }
}
