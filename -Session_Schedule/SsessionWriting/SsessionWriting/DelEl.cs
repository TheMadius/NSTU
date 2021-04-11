using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SsessionWriting.Class
{
    public partial class DelEl : Form
    {
        private List<Teacher> teachers;
        private List<Audience> audience;
        private List<Session> sessions;
        private List<ILesson> Ex = new List<ILesson>();
        private bool ChAud = false;
        private bool ChTaech = false;
        private bool ChSessionsEx = false;
        private bool ChSessionsGr = false;


        public DelEl()
        {
            InitializeComponent();
        }

        public List<Teacher> Teachers
        {
            get => teachers;
            set
            {
                teachers = value;
                ChTaech = true;
            }
        }
        public List<Audience> Audience
        {
            get => audience; set
            {
                audience = value;
                ChAud = true;
            }
        }
           
        public List<Session> SessionsGr
        {
            get => sessions; set
            {
                sessions = value;
                ChSessionsGr = true;
            }
        }

        public List<ILesson> Ex1 { get => Ex; }

        public void AddExem(ILesson ex)
        {
            Ex.Add(ex);
            ChSessionsEx = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChAud)
                {
                    audience.RemoveAt(this.listBox1.SelectedIndex);
                    Update();
                }

                if (ChTaech)
                {
                    teachers.RemoveAt(this.listBox1.SelectedIndex);
                    Update();
                }

                if (ChSessionsGr)
                {
                    sessions.RemoveAt(this.listBox1.SelectedIndex);
                    Update();
                }

                if (ChSessionsEx)
                {
                    Ex.RemoveAt(this.listBox1.SelectedIndex);
                    Update();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Выберите элемент");
                return;
            }
                   
        }
        private new void Update()
        {
            this.listBox1.Items.Clear();
            if (ChAud)
            {
                foreach (var item in audience)
                {
                    this.listBox1.Items.Add(item.Number);
                }
            }

            if (ChTaech)
            {
                foreach (var item in teachers)
                {
                    this.listBox1.Items.Add(item.getShortName());
                }
            }

            if (ChSessionsGr)
            {
                foreach (var item in sessions)
                {
                    this.listBox1.Items.Add(item.Group.GetName);
                }
            }

            if (ChSessionsEx)
            {
                foreach (var item in Ex)
                {
                    this.listBox1.Items.Add(item.getName());
                }
            }
        }

        private void DelEl_Load(object sender, EventArgs e)
        {
            Update();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
