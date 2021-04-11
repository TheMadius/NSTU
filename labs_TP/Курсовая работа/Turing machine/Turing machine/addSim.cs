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
    public partial class addSim : Form
    {
        string alphabet ;
        string chuseSim = "";
        bool Flag = false;
        public addSim(string alf)
        {
            InitializeComponent();
            alphabet = alf;
            this.button1.Enabled = false;

            for (int i = 0; i < alphabet.Length; ++i) 
            {
                this.comboBox1.Items.Add(alphabet[i]);
            }
            this.comboBox1.Items.Add('_');
        }

        public string ChuseSim { get => chuseSim; }

        private void button1_Click(object sender, EventArgs e)
        {
            if(chuseSim == "_")
            {
                chuseSim = "";
            }
            Flag = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chuseSim = null;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chuseSim = this.comboBox1.Text;
            this.button1.Enabled = true;
        }

        private void addSim_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!Flag)
              chuseSim = null;
        }
    }
}
