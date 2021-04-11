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
    public partial class Form1 : Form
    {
        private List<Session> sessions;
        private List<Teacher> teachers;
        private List<Audience> audience;
        private List<DSyllabus> syllabus;

        private string jsonPathT = @"C:\Users\Дима\-ourse\SsessionWriting\SsessionWriting\teachers.json";
        private string jsonPathA = @"C:\Users\Дима\-ourse\SsessionWriting\SsessionWriting\audience.json";
        private string jsonPathS = @"C:\Users\Дима\-ourse\SsessionWriting\SsessionWriting\Sessions.json";
        private string jsonPathSy = @"C:\Users\Дима\-ourse\SsessionWriting\SsessionWriting\syllabus.json";
       
        public List<Session> Sessions { get => sessions; }
        public List<Teacher> Teachers { get => teachers; }
        public List<Audience> Audience { get => audience; }


        public Form1()
        {
            InitializeComponent();
        }
        public int getseletItem()
        {
            return this.comboBox1.SelectedIndex;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sessions = JsonConvert.DeserializeObject<List<Session>>(File.ReadAllText(jsonPathS));
            teachers = JsonConvert.DeserializeObject<List<Teacher>>(File.ReadAllText(jsonPathT));
            audience = JsonConvert.DeserializeObject<List<Audience>>(File.ReadAllText(jsonPathA));
            syllabus = JsonConvert.DeserializeObject<List<DSyllabus>>(File.ReadAllText(jsonPathSy));

            if (sessions == null)
                sessions = new List<Session>();

            foreach (var item in sessions)
            {
                this.comboBox1.Items.Add(item.Group.GetName);
            }
                                   
            this.button3.Enabled = false;
            this.button4.Enabled = false;

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = sessions[this.comboBox1.SelectedIndex].Group.Semester.ToString();

            this.button3.Enabled = true;
            this.button4.Enabled = true;

            update();

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            FormAddNewGroup newform = new FormAddNewGroup();
            newform.ShowDialog();

            if (newform.getGroup == null)
                return;

            Session newSes = new Session(newform.getGroup);

            sessions.Add(newSes);

            this.comboBox1.Items.Add(newSes.Group.GetName);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            AddExem addEm = new AddExem();

            addEm.Teachers = teachers;

            foreach (var item in syllabus)
            {
                if (sessions[this.comboBox1.SelectedIndex].Group.Direction.Id == item.Direction.Id)
                {
                    addEm.Discipline = item.LDiscipline1;
                    break;
                }

            }
            List<Audience> newAud = new List<Audience>();

            foreach (var item in audience)
            {
                if (sessions[this.comboBox1.SelectedIndex].Group.CountStud <= item.CountSeats1)
                {
                    newAud.Add(item);
                }

            }
            addEm.Audience = newAud;
            addEm.Baseform = this;

            addEm.ShowDialog();

            if (!addEm.Cheсk)
                return;

            ILesson ex = addEm.Elem;
            
            if (ex.GetType() == typeof(Exam))
                sessions[this.comboBox1.SelectedIndex].addExem((Exam)ex);
            else
                sessions[this.comboBox1.SelectedIndex].addConsultation((Consultation)ex);

            this.dataGridView1.Rows.Add(
                new object[]
                {
                ex.Date.ToString(),
                ex.getName(),
                ex.Teacher.getShortName(),
                ex.Time.ToString(),
                ex.Audience.Number
                });
                   

        }

        private new void updateGroup()
        {
            this.comboBox1.Items.Clear();

           foreach (var item in sessions)
           {
               this.comboBox1.Items.Add(item.Group.GetName);
           }

            this.dataGridView1.Rows.Clear();
        }

        private new void update()
        {
           
            this.dataGridView1.Rows.Clear();
            foreach (var item in sessions[this.comboBox1.SelectedIndex].Exams1)
            {
                this.dataGridView1.Rows.Add(new object[]
                {
                    item.Date.ToString(),
                    item.getName(),
                    item.Teacher.getShortName(),
                    item.Time.ToString(),
                    item.Audience.Number
                }
                );
            }
            foreach (var item2 in sessions[this.comboBox1.SelectedIndex].Consultation)
            {
                this.dataGridView1.Rows.Add(new object[]
                {
                    item2.Date.ToString(),
                    item2.getName(),
                    item2.Teacher.getShortName(),
                    item2.Time.ToString(),
                    item2.Audience.Number
                }
                );
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.WriteAllText(jsonPathS, JsonConvert.SerializeObject(sessions));
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            DelEl Del = new DelEl();
            Del.Teachers = Teachers;
            Del.ShowDialog();
            teachers = Del.Teachers;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            DelEl Del = new DelEl();
            Del.Audience = audience;
            Del.ShowDialog();
            audience = Del.Audience;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DelEl Del = new DelEl();
            Del.SessionsGr = sessions;
            Del.ShowDialog();
            sessions = Del.SessionsGr;
            updateGroup();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DelEl Del = new DelEl();

            foreach (var item in sessions[this.comboBox1.SelectedIndex].Exams1)
            {
                Del.AddExem(item);
            }

            foreach (var item in sessions[this.comboBox1.SelectedIndex].Consultation)
            {
                Del.AddExem(item);
            }

            Del.ShowDialog();

            List<Consultation> nCo = new List<Consultation>();
            List<Exam> nEx = new List<Exam>();

            foreach (var item in Del.Ex1)
            {
                if (item.GetType() == typeof(Exam))
                    nEx.Add((Exam)item);
                else
                    nCo.Add((Consultation)item);
            }
            sessions[this.comboBox1.SelectedIndex].Consultation = nCo;
            sessions[this.comboBox1.SelectedIndex].Exams1 = nEx;

            update();

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            addTeach newF = new addTeach();
            newF.ShowDialog();

            if (newF.Teach == null)
                return;

            teachers.Add(newF.Teach);
                    
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Form2 newF = new Form2();
            newF.ShowDialog();

            if (newF.Au == null) 
                return;
            
            audience.Add(newF.Au);


        }
    }
}
