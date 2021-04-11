using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab3_VM;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.textBox1.TextAlign = HorizontalAlignment.Right;
            this.textBox2.TextAlign = HorizontalAlignment.Right;
            this.textBox3.TextAlign = HorizontalAlignment.Right;
            this.textBox4.TextAlign = HorizontalAlignment.Right;
            this.textBox5.TextAlign = HorizontalAlignment.Right;
        }

        private void showMessage(string msg)
        {
            MessageBox.Show("Возникла проблема: " + msg, "Ошибка!!!");
        }

        private bool checkNum(string num)
        {
            bool isNum;
            double Num;
            isNum = double.TryParse(num, out Num);
            return isNum;
        }

        private bool checkNumRang(string num)
        {
            int MaxLength = 10;
            string MaxNumm = "2147483647";
            string[] NumArr = num.Split(new char[] {','});
            string wholeNum = NumArr[0];

            wholeNum.Trim(new char[] { '-' });
            

            if (wholeNum.Length < MaxLength)
            {
                return true;
            }

            if (wholeNum.Length > MaxLength)
            {
                return false;
            }

            if (wholeNum.Length == MaxLength)
            {
                int temp = String.Compare(wholeNum, MaxNumm);

                if(temp == 0)
                {
                    return NumArr.Length == 1;
                }

                return (temp < 0);
            }

            return false;
        }

        private bool checkNUM(string Num)
        {
            if (Num.Length == 0)
            {
                showMessage("Не были заполнены поля.");
                return false;
            }

            if (!checkNum(Num))
            {
                showMessage("Введённые  значения не являются числами.");
                return false;
            }

            if (!checkNumRang(Num))
            {
                if (Num[0] == '-')
                    showMessage("Введённые значения выходят из допустимого диапазона снизу.");
                else
                    showMessage("Введённые значения выходят из допустимого диапазона сверху.");
                return false;
            }
            return true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string A = textBox1.Text.Replace(".", ",");
            string B = textBox2.Text.Replace(".", ",");
            string C = textBox3.Text.Replace(".", ",");
            string D = textBox4.Text.Replace(".", ",");
            string E = textBox5.Text.Replace(".", ",");
            string F = textBox6.Text.Replace(".", ",");
            string res = "";

            Matrix BigDet = new Matrix(3, 3);
            Matrix SmolDet = new Matrix(2, 2);

            if (!checkNUM(A) || !checkNUM(B)|| !checkNUM(C)|| !checkNUM(D)|| !checkNUM(E)|| !checkNUM(F)) 
                return;

            BigDet[0, 0] = Convert.ToDouble(A);
            BigDet[0, 1] = Convert.ToDouble(B)/2;
            BigDet[0, 2] = Convert.ToDouble(D)/2;
            BigDet[1, 0] = Convert.ToDouble(B)/2;
            BigDet[1, 1] = Convert.ToDouble(C);
            BigDet[1, 2] = Convert.ToDouble(E)/2;
            BigDet[2, 0] = Convert.ToDouble(D)/2;
            BigDet[2, 1] = Convert.ToDouble(E)/2;
            BigDet[2, 2] = Convert.ToDouble(F);

            SmolDet[0, 0] = Convert.ToDouble(A);
            SmolDet[0, 1] = Convert.ToDouble(B)/2;
            SmolDet[1, 0] = Convert.ToDouble(B)/2;
            SmolDet[1, 1] = Convert.ToDouble(C);

            double Detsm = SmolDet.getDeterminant();
            double DetBg = BigDet.getDeterminant();

            if (DetBg == 0)
            {
                if (Detsm == 0)
                {
                    res = "Две параллельные прямые";
                }
                if (Detsm > 0)
                {
                    res = "Две пересекающиеся мнимые прямые";
                }
                if (Detsm < 0)
                {
                    res = "Две пересекающиеся действительные прямые";
                }
            }
            else
            {
                if (Detsm == 0)
                {
                    res = "Парабола";
                }
                if (Detsm > 0)
                {
                    res = "Эллипс";
                }
                if (Detsm < 0)
                {
                    res = "Гипербола";
                }
            }

            this.textBox10.Text = res;
        }
    }
}
