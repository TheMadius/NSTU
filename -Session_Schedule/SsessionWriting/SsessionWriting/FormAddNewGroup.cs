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
using System.IO;
using Newtonsoft.Json;

namespace SsessionWriting
{
    public partial class FormAddNewGroup : Form
    {
        private List<Direction> directions = new List<Direction>();
        private Group gr;
        bool selectDir = false;
        private string jsonPath = @"C:\Users\Дима\-ourse\SsessionWriting\SsessionWriting\directions.json";

        public Group getGroup { get => gr;}

        public FormAddNewGroup()
        {
            InitializeComponent();
        }

        private void FormAddNewGroup_Load(object sender, EventArgs e)
        {
            directions = JsonConvert.DeserializeObject<List<Direction>>(File.ReadAllText(jsonPath));

            foreach (var item in directions)
            {
                this.comboBox1.Items.Add(item.Name);
            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            UInt32 num ;
            short count;
            short sem;
            try
            {
                 num = Convert.ToUInt32(this.textBox2.Text);
                 count = Convert.ToInt16(this.textBox1.Text);
                 sem = Convert.ToInt16(this.textBox3.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный ввод данных!!");
                return;
            }

            if (!selectDir)
            {
                MessageBox.Show("Выберите направление");
                return;
            }

            Direction dis = directions[this.comboBox1.SelectedIndex];

            gr = new Group(num, dis, count, sem);

            Close();

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectDir = true;
        }
    }
}
