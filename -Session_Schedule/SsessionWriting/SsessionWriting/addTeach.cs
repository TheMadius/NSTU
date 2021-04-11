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
    public partial class addTeach : Form
    {
        private List<Department> department = new List<Department>();
        private string jsonPath = @"C:\Users\Дима\-ourse\SsessionWriting\SsessionWriting\Department.json";

        private Teacher teach;
        private bool Bteach = false;
        private bool bdepartment = false;

        public Teacher Teach { get => teach; set => teach = value; }

        public addTeach()
        {
           
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddTeach_Load(object sender, EventArgs e)
        {
            department = JsonConvert.DeserializeObject<List<Department>>(File.ReadAllText(jsonPath));

            foreach (var item in department)
            {
                this.comboBox1.Items.Add(item.Name);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;
            string middleName = textBox3.Text;
            if (!bdepartment)
            {
                MessageBox.Show("Выберите кафедру");
                return;
            }
            

            Department dis = department[this.comboBox1.SelectedIndex];

            teach = new Teacher(lastName, firstName, middleName, dis);

            Bteach = true;

            Close();
        }
    }
}
