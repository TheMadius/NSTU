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
    public partial class AddExem : Form
    {
        private List<Teacher> teachers;
        private List<Audience> audience;
        private List<Discipline> discipline;
        private bool ChekcomboBox2 = false;
        private bool ChekcomboBox3 = false;
        private bool cheсk = false;
        private ILesson elem;
        private Form1 baseform;


        public AddExem()
        {
            InitializeComponent();
        }

        public List<Teacher> Teachers { get => teachers; set => teachers = value; }
        public List<Audience> Audience { get => audience; set => audience = value; }
        public List<Discipline> Discipline { get => discipline; set => discipline = value; }
        public ILesson Elem { get => elem; set => elem = value; }
        public bool Cheсk { get => cheсk; set => cheсk = value; }
        public Form1 Baseform { get => baseform; set => baseform = value; }

        private void AddExem_Load(object sender, EventArgs e)
        {
            foreach (var item in discipline)
            {
                this.comboBox1.Items.Add(item.Names);
            }

            foreach (var item in audience)
            {
                this.comboBox3.Items.Add(item.Number);
            }
            
            this.comboBox2.Enabled = false;
            }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.Enabled = true;

            comboBox2.Items.Clear();

            foreach (var item in Teachers)
            {
                if (item.Department.Id == discipline[this.comboBox1.SelectedIndex].Dep.Id)
                this.comboBox2.Items.Add(item.getShortName());
            }
            
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChekcomboBox2 = true;
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChekcomboBox3 = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string Tame = textBox2.Text;
            string date = textBox1.Text;
            ushort dy, Ma, mi, ho;
            string min = "";
            string hous = "";
            string day = "";
            string Man = "";
            int i;
            for (i = 0; Tame[i] != ':' && i < Tame.Length; i++)
                hous += Tame[i];

            for (i = i + 1; i < Tame.Length; i++)
                min += Tame[i];


            for (i = 0; date[i] != '.' && i < date.Length; i++)
                day += date[i];

            for (i = i + 1; i < date.Length; i++)
                Man += date[i];

            try
            {
                dy = Convert.ToUInt16(day);
                Ma = Convert.ToUInt16(Man);
                mi = Convert.ToUInt16(min);
                ho = Convert.ToUInt16(hous);
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный ввод данных!!");
                return;
            }

            Date dat;
            Time time;
            try
            {
                dat = new Date(dy, Ma);
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный ввод Даты!!");
                return;
            }
            try
            {
                time = new Time(mi, ho);
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный ввод Времени!!");
                return;
            }

            for (i = 0; i < Teachers.Count; i++)
            {
                if (Teachers[i].getShortName() == this.comboBox2.Text)
                    break;
            }

            if (!ChekcomboBox2)
            {
                MessageBox.Show("Не выбран преподаватель");
                return;

            }

            if (!ChekcomboBox3)
            {
                MessageBox.Show("Не выбрана аудитория");
                return;
            }

            if (!this.checkBox1.Checked)
            {
                foreach (var item in baseform.Sessions)
                {
                    foreach (var item2 in item.Exams1)
                    {
                        if (Teachers[i].getShortName() == item2.Teacher.getShortName() && item2.Date.ToString() == dat.ToString())
                        {
                            MessageBox.Show("В этот день у преподавателя есть экзамен");
                            return;
                        }
                    }

                    foreach (var item2 in item.Consultation)
                    {
                        if (item2.Date.ToString() == dat.ToString())
                        {
                            if (item2.Time.clock(0, 4) < time)
                            {
                                MessageBox.Show("Вы это время не возможно поставить консультацию");
                                return;
                            }
                        }
                    }
                }
                foreach (var ite in baseform.Sessions[baseform.getseletItem()].Exams1)
                {
                    if (discipline[this.comboBox1.SelectedIndex].Names == ite.NameLesson.Names)
                    {
                        MessageBox.Show("Экзамен уже назначен");
                        return;
                    }

                    if (ite.Date.ToString() == dat.ToString())
                    {
                        MessageBox.Show("У группы в этот день экзамен");
                        return;
                    }
                }


                foreach (var ite in baseform.Sessions[baseform.getseletItem()].Consultation)
                {
                    if (discipline[this.comboBox1.SelectedIndex].Names == ite.NameLesson.Names)
                    {
                        if (ite.Date.ToString() == dat.ToString() || ite.Date > dat)
                        {
                            MessageBox.Show("Экзамен должен быть после консультации");
                            return;

                        }
                    }
                }
            }


            if (this.checkBox1.Checked)
            {
                foreach (var item in baseform.Sessions)
                {
                    foreach (var item2 in item.Consultation)
                    {
                        if (Teachers[i].getShortName() == item2.Teacher.getShortName() && item2.Date.ToString() == dat.ToString())
                        {
                            MessageBox.Show("В этот день у преподавателя есть консультация");
                            return;
                        }
                    }
                    foreach (var item2 in item.Exams1)
                    {
                        if (item2.Date.ToString() == dat.ToString())
                        {
                            if (item2.Time > time.clock(0, 4))
                            {
                                MessageBox.Show("Вы это время не возможно поставить консультацию");
                                return;
                            }
                        }
                    }

                }
                foreach (var item in baseform.Sessions[baseform.getseletItem()].Consultation)
                {

                    if (discipline[this.comboBox1.SelectedIndex].Names == item.NameLesson.Names)
                    {
                        MessageBox.Show("Консультация уже назначена");
                        return;
                    }
                    if (item.Date.ToString() == dat.ToString())
                    {
                        MessageBox.Show("У группы в этот день есть консультация");
                        return;
                    }

                }
                foreach (var ite in baseform.Sessions[baseform.getseletItem()].Exams1)
                {
                    if (discipline[this.comboBox1.SelectedIndex].Names == ite.NameLesson.Names)
                    {
                        if (ite.Date.ToString() == dat.ToString() || ite.Date < dat)
                        {
                            MessageBox.Show("Консультация должен быть раньше экзамена");
                            return;

                        }
                    }
                }
            }

            foreach (var item in baseform.Sessions)
            {
                foreach (var item2 in item.Exams1)
                {
                    if (item2.Date.ToString() == dat.ToString())
                    {
                        if (item2.Audience.Number == audience[this.comboBox3.SelectedIndex].Number)
                        MessageBox.Show("В этот день аудитория занята");
                        return;
                    }
                }
            }


            foreach (var item in baseform.Sessions)
            {
                foreach (var item2 in item.Consultation)
                {
                    if (item2.Date.ToString() == dat.ToString())
                    {
                        if (item2.Audience.Number == audience[this.comboBox3.SelectedIndex].Number)
                            MessageBox.Show("В этот день аудитория занята");
                        return;
                    }
                }
            }


            if (this.checkBox1.Checked)
                {
                    elem = new Consultation(dat, discipline[this.comboBox1.SelectedIndex], teachers[i], time, audience[this.comboBox3.SelectedIndex]);
                }
                else
                {
                    elem = new Exam(dat, discipline[this.comboBox1.SelectedIndex], teachers[i], time, audience[this.comboBox3.SelectedIndex]);
                }

                this.cheсk = true;

                Close();
           
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddExem_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
