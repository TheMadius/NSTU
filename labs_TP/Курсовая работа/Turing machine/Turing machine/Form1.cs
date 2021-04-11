using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace Turing_machine
{
    public partial class Form1 : Form
    {
        InfinityTape line;
        TuringMachine TurMachine;
        int dx = 0;
        int dy = 35;
        int controlLen;
        string copyLine;
        string Path;

        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            this.button1.Image = Properties.Resources.add;
            this.button2.Image = Properties.Resources.del;

            this.button5.Image = Properties.Resources.play;
            this.button6.Image = Properties.Resources.stop;
            this.button7.Image = Properties.Resources.pause;
            this.button8.Image = Properties.Resources.step;

            this.pictureBox1.Image = Properties.Resources.Haed1;

            init();
        }
        private void init()
        {
            TurMachine = null;
            Path = null;
            copyLine = null;
            this.timer1.Interval = 1000;

            this.восстановитьЛентуToolStripMenuItem.Enabled = false;
            this.оченьБыстроToolStripMenuItem.Checked = false;
            this.быстроToolStripMenuItem.Checked = false;
            this.среднеToolStripMenuItem.Checked = true;
            this.медленноToolStripMenuItem.Checked = false;
            this.оченьМедленноToolStripMenuItem.Checked = false;

            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Columns.Add(" ", " ");
            this.dataGridView1.Columns[" "].Width = 50;
            this.dataGridView1.Columns[" "].ReadOnly = true;
            this.dataGridView1.Columns[" "].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView1.Columns.Add("Q1", "Q1");
            this.dataGridView1.Columns["Q1"].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Rows.Add("_");

            controlLen = 0;

            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = true;

            this.line = new InfinityTape(new Point(478, 53), new Point(482, 73), splitContainer1.Panel1.Controls);

            VoidFilling();

            MoveHead(-dx, -dy);
        }
        private int CountWords(string s, char s0)
        {
            int count = (s.Length - s.Replace("" + s0, "").Length);
            return count;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string str = this.textBox1.Text;
            int k = textBox1.SelectionStart;
            bool flag;

            if (k == 0)
            {
                removeRows(str);
                line.setAlphabet(str);
                return;
            }

            flag = str[k - 1] == '_' || str[k - 1] == ' ';

            if (CountWords(str, str[k - 1]) >= 2 || flag)
            {
                str = str.Remove(k - 1, 1);

                this.textBox1.Text = str;

                this.textBox1.SelectionStart = k - 1;

                if (flag)
                {
                    MessageBox.Show("Использование символа \" \" (пробел) и \"_\" запрещено.", "Информация");
                }
                else
                {
                    MessageBox.Show("Повторение символов запрещено.", "Информация");
                }

                return;
            }

            this.textBox1.Text = str;

            if (controlLen <= str.Length)
            {
                addRows(k - 1, "" + str[k - 1]);
            }
            else
            {
                removeRows(str);
            }

            controlLen = str.Length;

            line.setAlphabet(str);
        }
        private void addRows(int pos, string name)
        {
            this.dataGridView1.Rows.Insert(pos, name);
        }
        private void removeRows(string name)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (!name.Contains((string)this.dataGridView1[0, i].Value))
                {
                    dataGridView1.Rows.RemoveAt(i);
                }
            }

            var act = getAction();

            for (int i = 0; i < this.dataGridView1.Rows.Count - 1 ; ++i)
            {
                for (int j = 1; j < this.dataGridView1.Columns.Count; ++j)
                {
                    if (act.Arract[i][j - 1] != null && !name.Contains(act.Arract[i][j - 1].Symbol))
                    {
                        dataGridView1[j, i].Value = null;
                    }
                }
            }

        }
        private void VoidFilling()
        {
            int width = this.splitContainer1.Panel1.Width;
            int firstPos = line.getPosFirst().X;

            while (firstPos < width)
            {
                line.addRight();
                firstPos = line.getPosFirst().X;
            }

            firstPos = line.getPosLat().X;

            while (firstPos > 0 )
            {
                line.addLeft();
                firstPos = line.getPosLat().X;
            }

            if(((line.getPosLat().X + line.Step) < 0) && (line[line.Min].Text.Length == 0))
            {
                line.removeLeft();
            }

            if ((line.getPosFirst().X - line.Step) > width && (line[line.Max].Text.Length == 0))
            {
                line.removeRight();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int index = this.dataGridView1.Columns.Count;
            this.dataGridView1.Columns.Add("Q"+ index, "Q"+ index);
            this.dataGridView1.Columns["Q" + index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int index = this.dataGridView1.Columns.Count;

            if(index == 2)
            {
                return;
            }

            var act = getAction();

            for(int i = 0; i < this.dataGridView1.Rows.Count;++i)
            {
                for(int j = 1; j < this.dataGridView1.Columns.Count;++j)
                {
                    if(act.Arract[i][j-1] != null && act.Arract[i][j-1].Status == index - 1)
                    {
                        dataGridView1[j,i].Value = null;
                    }
                }
            }

            this.dataGridView1.Columns.Remove("Q" + (index - 1));
        }
        private void сохранитьЛентуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeveLine copLine = new SeveLine(line.getLine(), line.Alphabet, line.ListTextBox[0].Location, line.ListLabel[0].Location, line.Max, line.Min, line.PosHead);
            copyLine = JsonConvert.SerializeObject(copLine);
            this.восстановитьЛентуToolStripMenuItem.Enabled = true;

        }
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
              "Сохранить текущую машину?",
               "Сохранение",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Information);

            if(result == DialogResult.Yes)
            {
                сохратитьToolStripMenuItem_Click(sender,e);
            }

            this.line.delControl();
            this.line.Clear();

            this.textBox1.Text = "";

            init();

        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.tur)|*.tur";
                              
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Path = openFileDialog.FileName;
                StreamReader file = new StreamReader(openFileDialog.FileName);
                string info = file.ReadToEnd();
                var data = JsonConvert.DeserializeObject<SaveData>(info);

                setLine(data.Line.getLineT());
                setAct(data.Act);
                file.Close();
            }
        }
        private void сохратитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Path == null)
            {
                сохранитьКакToolStripMenuItem_Click(sender,e);
            }
            else
            {
                SeveLine copLine = new SeveLine(line.getLine(), line.Alphabet, line.ListTextBox[0].Location, line.ListLabel[0].Location, line.Max, line.Min, line.PosHead);
                var saveData = new SaveData(copLine, getAction());
                string jsonObject = JsonConvert.SerializeObject(saveData);
                FileStream file = File.Create(Path);
                byte[] info = new UTF8Encoding(true).GetBytes(jsonObject);
                file.Write(info, 0, info.Length);
                file.Close();
            }
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Текстовые файлы (*.tur)|*.tur";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SeveLine copLine = new SeveLine(line.getLine(), line.Alphabet, line.ListTextBox[0].Location, line.ListLabel[0].Location, line.Max, line.Min, line.PosHead);
                var saveData = new SaveData(copLine, getAction());
                string jsonObject = JsonConvert.SerializeObject(saveData);
                Path = saveFileDialog.FileName;
                FileStream  file = File.Create(saveFileDialog.FileName);
                byte[] info = new UTF8Encoding(true).GetBytes(jsonObject);
                file.Write(info, 0, info.Length);
                file.Close();
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void setAct(TuringAction act)
        {
            int num = act.Arract[0].Length + 1;
            for (int i = dataGridView1.ColumnCount; i < num; ++i) 
            {
                int index = this.dataGridView1.Columns.Count;
                this.dataGridView1.Columns.Add("Q" + index, "Q" + index);
                this.dataGridView1.Columns["Q" + index].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < act.Arract.Length;++i)
            {
                for(int j = 0; j< act.Arract[i].Length;++j)
                {
                    this.dataGridView1[j + 1, i].Value = act.Arract[i][j];
                }
            }
        }
        private void setLine(InfinityTape newLine)
        {
            line.delControl();
            line.Clear();
            this.controlLen = newLine.Alphabet.Length;
            newLine.Control = splitContainer1.Panel1.Controls;
            this.line = newLine;
            MoveHead(-dx, -dy);
            line.setControl();
            this.dataGridView1.Rows.Clear();

            for (int i = 0; i < newLine.Alphabet.Length; ++i)
            {
                this.dataGridView1.Rows.Add("" + newLine.Alphabet[i]);
            }
            this.dataGridView1.Rows.Add("_");

            this.textBox1.Text = newLine.Alphabet;
        }
        private void восстановитьЛентуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            line.delControl();
            line.Clear();
            InfinityTape newLine = new InfinityTape(JsonConvert.DeserializeObject< SeveLine>(copyLine));
            newLine.Control = splitContainer1.Panel1.Controls;
            this.line = newLine;
            MoveHead(-dx, -dy);
            line.setControl();
            line.setAlphabet(this.textBox1.Text);
            
        }
        private void запускToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button5_Click(sender,e);
        }
        private void выполнитьШагToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button8_Click(sender, e);
        }
        private void паузаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button7_Click(sender, e);
        }
        private void оченьБыстроToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = 250;
            this.оченьБыстроToolStripMenuItem.Checked = true;
            this.быстроToolStripMenuItem.Checked = false;
            this.среднеToolStripMenuItem.Checked = false;
            this.медленноToolStripMenuItem.Checked = false;
            this.оченьМедленноToolStripMenuItem.Checked = false;
        }
        private void быстроToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = 500;
            this.оченьБыстроToolStripMenuItem.Checked = false;
            this.быстроToolStripMenuItem.Checked = true;
            this.среднеToolStripMenuItem.Checked = false;
            this.медленноToolStripMenuItem.Checked = false;
            this.оченьМедленноToolStripMenuItem.Checked = false;
        }
        private void среднеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = 1000;
            this.оченьБыстроToolStripMenuItem.Checked = false;
            this.быстроToolStripMenuItem.Checked = false;
            this.среднеToolStripMenuItem.Checked = true;
            this.медленноToolStripMenuItem.Checked = false;
            this.оченьМедленноToolStripMenuItem.Checked = false;
        }
        private void медленноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = 1250;
            this.оченьБыстроToolStripMenuItem.Checked = false;
            this.быстроToolStripMenuItem.Checked = false;
            this.среднеToolStripMenuItem.Checked = false;
            this.медленноToolStripMenuItem.Checked = true;
            this.оченьМедленноToolStripMenuItem.Checked = false;
        }
        private void оченьМедленноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.timer1.Interval = 1500;
            this.оченьБыстроToolStripMenuItem.Checked = false;
            this.быстроToolStripMenuItem.Checked = false;
            this.среднеToolStripMenuItem.Checked = false;
            this.медленноToolStripMenuItem.Checked = false;
            this.оченьМедленноToolStripMenuItem.Checked = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MoveHead(dx, dy);
            line.MoveLeft();
            VoidFilling();
            MoveHead(-dx, -dy);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MoveHead(dx, dy);
            line.MoveRight();
            VoidFilling();
            MoveHead(-dx, -dy);
        }
        private void MoveHead(int dx,int dy)
        {
            var Pos = line.ListLabel[line.PosHead - line.Min].Location;
            Pos.Y += dy;
            Pos.X += dx;
            line.ListLabel[line.PosHead - line.Min].Location = Pos;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
       
            if (e.ColumnIndex == 0)
            {
                return;
            }

            DataGridViewCell cell = this.dataGridView1.SelectedCells[0];

            addAct adt = new addAct(line.Alphabet, this.dataGridView1.Columns.Count - 1);

            adt.StartPosition = FormStartPosition.CenterScreen;

            adt.ShowDialog();

            if (adt.Act == null)
            {
                return;
            }

            cell.Value = adt.Act;
        }
        private TuringAction getAction()
        {
            TuringAction tuAct = new TuringAction(line.Alphabet);

            ActionT[][] act = new ActionT[this.dataGridView1.Rows.Count][];
            
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                act[i] = new ActionT[this.dataGridView1.Columns.Count - 1];
            }

            for(int i = 0; i < this.dataGridView1.Rows.Count;++i)
            {
                for(int j = 1; j < this.dataGridView1.Columns.Count ; ++j)
                {
                    act[i][j -1] = (ActionT)this.dataGridView1.Rows[i].Cells[j].Value;
                }
            }

            tuAct.setAction(act);

            return tuAct;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.timer1.Start();
            this.паузаToolStripMenuItem.Enabled = true;
            this.запускToolStripMenuItem1.Enabled = false;
            this.выполнитьШагToolStripMenuItem.Enabled = false;
            this.button5.Enabled = false;
            this.button6.Enabled = true;
            this.button7.Enabled = true;
            this.button8.Enabled = false;
            this.button3.Enabled = false;
            this.button4.Enabled = false;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            TurMachine = null;
            this.timer1.Stop();
            this.dataGridView1.Enabled = true;
            this.паузаToolStripMenuItem.Enabled = false;
            this.запускToolStripMenuItem1.Enabled = true;
            this.выполнитьШагToolStripMenuItem.Enabled = true;
            this.textBox1.Enabled = true;
            this.button3.Enabled = true;
            this.button4.Enabled = true;
            this.button5.Enabled = true;
            this.button6.Enabled = false;
            this.button7.Enabled = false;
            this.button8.Enabled = true;

            setWhite();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this.button3.Enabled = true;
            this.button4.Enabled = true;
            this.button8.Enabled = true;
            this.выполнитьШагToolStripMenuItem.Enabled = true;
            this.паузаToolStripMenuItem.Enabled = false;
            this.запускToolStripMenuItem1.Enabled = true;
            this.button5.Enabled = true;
            this.button7.Enabled = false;
        }
        private void setWhite()
        {
            for(int i = 0; i < this.dataGridView1.Rows.Count;++i)
            {
                for(int j = 0; j < this.dataGridView1.Columns.Count;++j)
                {
                    this.dataGridView1[j, i].Style.BackColor = System.Drawing.Color.White;
                }
            }
        }
        private void colorNextAction(int Q)
        {
            setWhite();

            string Sym = this.line[line.PosHead].Text;

            int index = (Sym != "") ? line.Alphabet.IndexOf(Sym[0]) : dataGridView1.Rows.Count - 1;

            this.dataGridView1[Q, index].Style.BackColor = System.Drawing.Color.Green;
        }
        private void colorError(int Q)
        {
            setWhite();

            string Sym = this.line[line.PosHead].Text;

            int index = (Sym != "") ? line.Alphabet.IndexOf(Sym[0]) : dataGridView1.Rows.Count - 1;

            this.dataGridView1[Q, index].Style.BackColor = System.Drawing.Color.Red;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (TurMachine == null)
            {
                this.button6.Enabled = true;
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Enabled = false;
                this.textBox1.Enabled = false;
                TurMachine = new TuringMachine();
                TurMachine.setIlne(this.line);
                TurMachine.Action = getAction();
                colorNextAction(TurMachine.NextQ);
                return;
            }

            MoveHead(dx, dy);

            try
            {
                TurMachine.makeStep();
            }
            catch (Exception er)
            {
                MoveHead(-dx, -dy);
                this.timer1.Stop();
                colorError(TurMachine.NextQ);
                MessageBox.Show(er.Message, "Ошибка!!!");
                button6_Click(sender,e);
                return;
            }

            if (TurMachine.NextQ == 0)
            {
                MoveHead(-dx, -dy);
                this.timer1.Stop();
                MessageBox.Show("Выполнение программы завершено.", "Информация");
                button6_Click(sender, e);
                return;
            }

            MoveHead(-dx, -dy);

            colorNextAction(TurMachine.NextQ);
            VoidFilling();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            button8_Click(sender,e);
        }
    }
}
