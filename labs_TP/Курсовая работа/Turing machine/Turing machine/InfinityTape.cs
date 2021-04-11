using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turing_machine
{
    class InfinityTape 
    {
        List<TextBox> listTextBox = new List<TextBox>();
        List<Label> listLabel = new List<Label>();
        Control.ControlCollection control ;

        string alphabet = "";

        int step = 20;
        int posHead;
        int max;
        int min;
        
        public int Step { get => step;  set => step = value; }
        public int Max { get => max;  set => max = value; }
        public int Min { get => min;  set => min = value; }
        public int PosHead { get => posHead; set => posHead = value; }
        public List<TextBox> ListTextBox { get => listTextBox; set => listTextBox = value; }
        public List<Label> ListLabel { get => listLabel; set => listLabel = value; }
        public string Alphabet { get => alphabet; set => alphabet = value; }
        public Control.ControlCollection Control { get => control; set => control = value; }

        public InfinityTape(Point posMidButton, Point posMidLable, Control.ControlCollection containerControl)
        {
            max = 0;
            min = 0;
            posHead = 0;
            control = containerControl;
            listTextBox.Add(getText(posMidButton, max.ToString()));
            listLabel.Add(getLable(posMidLable, max.ToString()));

            control.Add(listTextBox[max]);
            control.Add(listLabel[max]);

            listLabel[max].BringToFront();
        }

        public InfinityTape(SeveLine seve)
        {
            int j = 0;
            for (int i = seve.Min; i <= seve.Max; ++i)
            {
                Point posT = seve.PosStartT;
                Point posL = seve.PosStartL;

                posT.X += step * j;
                posL.X += step * j;

                listTextBox.Add(getText(posT, i.ToString()));
                listLabel.Add(getLable(posL, i.ToString()));

                listTextBox[listTextBox.Count - 1].Text = (seve.Line[j] == '_') ? "" : "" + seve.Line[j];

                j++;
            }
            this.alphabet = seve.Alfa;
            this.posHead = seve.PosHead1;
            this.max = seve.Max;
            this.min = seve.Min;
        }

        public string getLine()
        {
            string line = "";

            for(int i = 0;i < this.listTextBox.Count;++i)
            {
                if(listTextBox[i].Text == "")
                {
                    line += "_";
                }
                else
                {
                    line += listTextBox[i].Text;
                }
            }
            return line;
        }
          
        public void addRight()
        {
            int count = listTextBox.Count();
            Point newBPos = listTextBox[count - 1].Location;
            Point newLPos = listLabel[count - 1].Location;

            newBPos.X += step;
            newLPos.X += step;

            ++max;

            listTextBox.Add(getText(newBPos, max.ToString()));
            listLabel.Add(getLable(newLPos, max.ToString()));

            control.Add(listTextBox[count]);
            control.Add(listLabel[count]);

            listLabel[count].BringToFront();
        }

        public void removeLeft()
        {
            min++;
            control.Remove(listTextBox[0]);
            listTextBox.RemoveAt(0);
            control.Remove(listLabel[0]);
            listLabel.RemoveAt(0);
        }
        public void removeRight()
        {
            int count = listTextBox.Count() - 1;
            control.Remove(listTextBox[count]);
            listTextBox.RemoveAt(count);
            control.Remove(listLabel[count]);
            listLabel.RemoveAt(count);

            max--;

        }

        public void setControl()
        {
            for (int i = 0; i < listLabel.Count; ++i)
            {
                control.Add(listLabel[i]);
                control.Add(this.listTextBox[i]);
                listLabel[i].BringToFront();
            }
        }

        public void delControl()
        {
            for(int i = 0; i < listLabel.Count;++i)
            {
                control.Remove(listLabel[i]);
                control.Remove(this.listTextBox[i]);
            }
        }
        public void setAlphabet(string alphabet)
        {
            this.alphabet = alphabet;

            for(int i = 0; i < listTextBox.Count;++i)
            {
                if (!alphabet.Contains(listTextBox[i].Text))
                {
                    listTextBox[i].Text = "";
                }
            }

        }
        public void addLeft()
        {
            Point newBPos = listTextBox[0].Location;
            Point newLPos = listLabel[0].Location;

            newBPos.X -= step;
            newLPos.X -= step;
            
            --min;

            listTextBox.Insert(0,getText(newBPos, min.ToString()));
            listLabel.Insert(0, getLable(newLPos, min.ToString()));

            control.Add(listTextBox[0]);
            control.Add(listLabel[0]);

            listLabel[0].BringToFront();

        }

        public void MoveLeft()
        {
            posHead--;
                      
            for (int i = 0; i < listTextBox.Count; ++i)
            {
                Point pos;
                pos = listTextBox[i].Location;
                pos.X += step;
                listTextBox[i].Location = pos;

                pos = listLabel[i].Location;
                pos.X += step;
                listLabel[i].Location = pos;
            }
        }

        public Point getPosLat()
        {
            return listTextBox[0].Location;
        }

        public Point getPosFirst()
        {
            return listTextBox[listTextBox.Count - 1].Location;
        }

        public void MoveRight()
        {
            posHead++;

            for (int i = 0; i < listTextBox.Count; ++i)
            {
                Point pos;
                pos = listTextBox[i].Location;
                pos.X -= step;
                listTextBox[i].Location = pos;

                pos = listLabel[i].Location;
                pos.X -= step;
                listLabel[i].Location = pos;
            }

        }
        private TextBox getText(Point pos, string num)
        {
            TextBox newBut = new TextBox();

            newBut.Location = pos;
            newBut.Name = "textBoxLine" + num;
            newBut.Size = new System.Drawing.Size(20, 20);
            newBut.BackColor = System.Drawing.Color.PeachPuff;
            newBut.BringToFront();
            newBut.DoubleClick += new System.EventHandler(doubleClickInText);
            newBut.ReadOnly = true;
            newBut.TextAlign = HorizontalAlignment.Center;
            return newBut;
        }

        private Label getLable(Point pos, string num)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.BackColor = System.Drawing.Color.Transparent;
            label.Location = pos;
            label.Name = "labelLine" + num;
            label.Size = new System.Drawing.Size(13, 13);
            label.TabIndex = 4;
            label.Text = num;
            
            return label;
        }

        private void doubleClickInText(object sender, EventArgs e)
        {
            TextBox text = (TextBox)sender;

            addSim addSim = new addSim(alphabet);

            addSim.StartPosition = FormStartPosition.CenterScreen;

            addSim.ShowDialog();

            if(addSim.ChuseSim == null)
            {
                return;
            }

            text.Text = addSim.ChuseSim;

        }

        public void Clear()
        {
            listTextBox.Clear();
            listLabel.Clear();
        }
        public TextBox this[int index]
        {
            get
            {
                int indx = index - min;
                return listTextBox[indx];
            }
        }
    }
}
