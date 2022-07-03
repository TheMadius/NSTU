using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_IST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count == 2)
            {
                return;
            }
            this.dataGridView1.Rows.Remove(this.dataGridView1.Rows[1]);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Insert(1, new DataGridViewRow());
        }

        private void seyError(string mes)
        {
            MessageBox.Show(
                mes,
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[0].Cells[0].Value = 0;
            this.dataGridView1.Rows[0].Cells[0].ReadOnly = true;

            this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[1].Cells[0].Value = 1;
            this.dataGridView1.Rows[1].Cells[0].ReadOnly = true;

            this.dataGridView2.Rows.Add();
            this.dataGridView2.Rows[0].Cells[0].Value = 0;
            this.dataGridView2.Rows[0].Cells[0].ReadOnly = true;

            this.dataGridView2.Rows.Add();
            this.dataGridView2.Rows[1].Cells[0].Value = 1;
            this.dataGridView2.Rows[1].Cells[0].ReadOnly = true;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.Rows.Count == 2)
            {
                return;
            }
            this.dataGridView2.Rows.Remove(this.dataGridView2.Rows[1]);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.dataGridView2.Rows.Insert(1, new DataGridViewRow());
        }

        private double[][] getMatrixInGrid(DataGridView data)
        {
            double[][] matrix = new double[data.RowCount][];

            for (int i = 0; i < data.RowCount; i++)
            {
                matrix[i] = new double[data.ColumnCount];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    var val = data.Rows[i].Cells[j].Value;

                    if(val == null)
                        return null;

                    try
                    {
                        matrix[i][j] = Convert.ToDouble(val.ToString().Replace('.', ','));
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    
                }
            }
            return matrix;
        }

        private void setMatrixInGrid(DataGridView data, double[][] matrix)
        {
            data.Rows.Clear();

            for(int i = 0; i < matrix.Length; ++i)
            {
                data.Rows.Add();
                data.Rows[i].Cells[0].Value = matrix[i][0];
                data.Rows[i].Cells[1].Value = matrix[i][1];
                data.Rows[i].Cells[2].Value = matrix[i][2];
                data.Rows[i].Cells[0].ReadOnly = true;
                data.Rows[i].Cells[1].ReadOnly = true;
                data.Rows[i].Cells[2].ReadOnly = true;
            }
        }
        private bool checkArray(double[][] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if(arr[i][1] < arr[i - 1][1])
                    return false;
                if (arr[i][2] > arr[i - 1][2])
                    return false;
            }

            return true;
        }

        private void sordArray(double[][] arr)
        {
            double[] temp;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i][0] > arr[j][0])
                    {
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }

        private double getValue(double x1, double x2, double y1, double y2, double y)
        {
            double x = ((y - y1) * (x2 - x1) / (y2 - y1)) + x1;
            return x;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var rowA = getMatrixInGrid(this.dataGridView1);

            if(rowA == null)
            {
                seyError("Не все данные введены!!!");
                return;
            }

            sordArray(rowA);

            if (!checkArray(rowA))
            {
                seyError("Выпуклая функция нечеткого числа!!!");
                return;
            }

            this.chart1.Series["GrafA"].Points.Clear();

            for (int i = 0; i < rowA.Length; i++)
                this.chart1.Series["GrafA"].Points.AddXY(rowA[i][1], rowA[i][0]);

            for (int i = rowA.Length - 1; i >= 0; i--)
                this.chart1.Series["GrafA"].Points.AddXY(rowA[i][2], rowA[i][0]);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            var rowB = getMatrixInGrid(this.dataGridView2);
            if (rowB == null)
            {
                seyError("Не все данные введены!!!");
                return;
            }
            sordArray(rowB);

            if (!checkArray(rowB))
            {
                seyError("Выпуклая функция нечеткого числа!!!");
                return;
            }

            this.chart1.Series["GrafB"].Points.Clear();

            for (int i = 0; i < rowB.Length; i++)
                this.chart1.Series["GrafB"].Points.AddXY(rowB[i][1], rowB[i][0]);

            for (int i = rowB.Length - 1; i >= 0; i--)
                this.chart1.Series["GrafB"].Points.AddXY(rowB[i][2], rowB[i][0]);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if(dataGridView4.Rows.Count == 0)
            {
                return;
            }

            var rowA = getMatrixInGrid(this.dataGridView4);
            
            if (rowA == null)
            {
                seyError("Не все данные введены!!!");
                return;
            }

            sordArray(rowA);

            if (!checkArray(rowA))
            {
                seyError("Выпуклая функция нечеткого числа!!!");
                return;
            }

            this.chart1.Series["GrafC"].Points.Clear();

            for (int i = 0; i < rowA.Length; i++)
                this.chart1.Series["GrafC"].Points.AddXY(rowA[i][1], rowA[i][0]);

            for (int i = rowA.Length - 1; i >= 0; i--)
                this.chart1.Series["GrafC"].Points.AddXY(rowA[i][2], rowA[i][0]);
        }

        private double GetPower(double[][] matrix)
        {
            double sum = 0;

            for(int i = 0; i <  matrix.Length; ++i)
            {
                sum += matrix[i][1] + matrix[i][2];
            }

            return (sum / matrix.Length);
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            var rowA = getMatrixInGrid(this.dataGridView1);
            var rowB = getMatrixInGrid(this.dataGridView2);

            if (rowA == null || rowB == null)
            {
                seyError("Не все данные введены!!!");
                return;
            }

            sordArray(rowA);
            sordArray(rowB);

            if (!checkArray(rowA) || !checkArray(rowB))
            {
                seyError("Выпуклая функция нечеткого числа!!!");
                return;
            }

            double pow1 = GetPower(getMatrixInGrid(this.dataGridView1));
            double pow2 = GetPower(getMatrixInGrid(this.dataGridView2));

            if (pow1 > pow2)
                this.button12.BackColor = Color.Green;
            else
                this.button12.BackColor = Color.Red;

            if (pow1 >= pow2)
                this.button13.BackColor = Color.Green;
            else
                this.button13.BackColor = Color.Red;

            if (pow1 < pow2)
                this.button14.BackColor = Color.Green;
            else
                this.button14.BackColor = Color.Red;

            if (pow1 <= pow2)
                this.button15.BackColor = Color.Green;
            else
                this.button15.BackColor = Color.Red;

            if (pow1 == pow2)
                this.button16.BackColor = Color.Green;
            else
                this.button16.BackColor = Color.Red;

            if (pow1 != pow2)
                this.button17.BackColor = Color.Green;
            else
                this.button17.BackColor = Color.Red;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.chart1.Series["GrafA"].Points.Clear();
            this.chart1.Series["GrafB"].Points.Clear();
            this.chart1.Series["GrafC"].Points.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<double[]> res = new List<double[]>();

            var rowA = getMatrixInGrid(this.dataGridView1);
            var rowB = getMatrixInGrid(this.dataGridView2);
            if (rowA == null || rowB == null)
            {
                seyError("Не все данные введены!!!");
                return;
            }

            sordArray(rowA);
            sordArray(rowB);

            if (!checkArray(rowA) || !checkArray(rowB))
            {
                seyError("Выпуклая функция нечеткого числа!!!");
                return;
            }

            int i = 0;

            for (int j = 0; j < rowB.Length;)
            {
                if (rowA[i][0] == rowB[j][0])
                {
                    double[] vec = new double[3];
                    vec[0] = rowA[i][0];
                    vec[1] = rowA[i][1] + rowB[j][1];
                    vec[2] = rowA[i][2] + rowB[j][2];
                    i++;j++;
                    res.Add(vec);
                }
                else
                {
                    if (rowA[i][0] > rowB[j][0])
                    {
                        double x1 = getValue(rowA[i - 1][1], rowA[i][1], rowA[i - 1][0], rowA[i][0], rowB[j][0]);
                        double x2 = getValue(rowA[i - 1][2], rowA[i][2], rowA[i - 1][0], rowA[i][0], rowB[j][0]);
                        double[] vec = new double[3];
                        vec[0] = rowB[j][0];
                        vec[1] = x1 + rowB[j][1];
                        vec[2] = x2 + rowB[j][2];
                        j++;
                        res.Add(vec);
                    }
                    else
                    {
                        double x1 = getValue(rowB[j - 1][1], rowB[j][1], rowB[j - 1][0], rowB[j][0], rowA[i][0]);
                        double x2 = getValue(rowB[j - 1][2], rowB[j][2], rowB[j - 1][0], rowB[j][0], rowA[i][0]);
                        double[] vec = new double[3];
                        vec[0] = rowA[i][0];
                        vec[1] = rowA[i][1] + x1;
                        vec[2] = rowA[i][2] + x2;
                        i++;
                        res.Add(vec);
                    }
                }
            }
            setMatrixInGrid(this.dataGridView4, res.ToArray());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<double[]> res = new List<double[]>();
            int i = 0;

            var rowA = getMatrixInGrid(this.dataGridView1);
            var rowB = getMatrixInGrid(this.dataGridView2);
            if (rowA == null || rowB == null)
            {
                seyError("Не все данные введены!!!");
                return;
            }
            sordArray(rowA);
            sordArray(rowB);

            if (!checkArray(rowA) || !checkArray(rowB))
            {
                seyError("Выпуклая функция нечеткого числа!!!");
                return;
            }

            for (int j = 0; j < rowB.Length;)
            {
                if (rowA[i][0] == rowB[j][0])
                {
                    double[] vec = new double[3];
                    vec[0] = rowA[i][0];
                    vec[1] = rowA[i][1] - rowB[j][2];
                    vec[2] = rowA[i][2] - rowB[j][1];
                    i++; j++;
                    res.Add(vec);
                }
                else
                {
                    if (rowA[i][0] > rowB[j][0])
                    {
                        double x1 = getValue(rowA[i - 1][1], rowA[i][1], rowA[i - 1][0], rowA[i][0], rowB[j][0]);
                        double x2 = getValue(rowA[i - 1][2], rowA[i][2], rowA[i - 1][0], rowA[i][0], rowB[j][0]);

                        double[] vec = new double[3];
                        vec[0] = rowB[j][0];
                        vec[1] = x1 - rowB[j][2];
                        vec[2] = x2 - rowB[j][1];
                        j++;
                        res.Add(vec);
                    }
                    else
                    {
                        double x1 = getValue(rowB[j - 1][1], rowB[j][1], rowB[j - 1][0], rowB[j][0], rowA[i][0]);
                        double x2 = getValue(rowB[j - 1][2], rowB[j][2], rowB[j - 1][0], rowB[j][0], rowA[i][0]);

                        double[] vec = new double[3];
                        vec[0] = rowA[i][0];
                        vec[1] = rowA[i][1] - x2;
                        vec[2] = rowA[i][2] - x1;
                        i++;
                        res.Add(vec);
                    }
                }
            }
            setMatrixInGrid(this.dataGridView4, res.ToArray());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<double[]> res = new List<double[]>();

            var rowA = getMatrixInGrid(this.dataGridView1);
            var rowB = getMatrixInGrid(this.dataGridView2);

            if (rowA == null || rowB == null)
            {
                seyError("Не все данные введены!!!");
                return;
            }

            sordArray(rowA);
            sordArray(rowB);

            if (!checkArray(rowA) || !checkArray(rowB))
            {
                seyError("Выпуклая функция нечеткого числа!!!");
                return;
            }

            int i = 0;

            for (int j = 0; j < rowB.Length;)
            {
                if (rowA[i][0] == rowB[j][0])
                {
                    double[] vec = new double[3];
                    vec[0] = rowA[i][0];
                    vec[1] = rowA[i][1] * rowB[j][1];
                    vec[2] = rowA[i][2] * rowB[j][2];
                    i++; j++;
                    res.Add(vec);
                }
                else
                {
                    if (rowA[i][0] > rowB[j][0])
                    {
                        double x1 = getValue(rowA[i - 1][1], rowA[i][1], rowA[i - 1][0], rowA[i][0], rowB[j][0]);
                        double x2 = getValue(rowA[i - 1][2], rowA[i][2], rowA[i - 1][0], rowA[i][0], rowB[j][0]);
                        double[] vec = new double[3];
                        vec[0] = rowB[j][0];
                        vec[1] = x1 * rowB[j][1];
                        vec[2] = x2 * rowB[j][2];
                        j++;
                        res.Add(vec);
                    }
                    else
                    {
                        double x1 = getValue(rowB[j - 1][1], rowB[j][1], rowB[j - 1][0], rowB[j][0], rowA[i][0]);
                        double x2 = getValue(rowB[j - 1][2], rowB[j][2], rowB[j - 1][0], rowB[j][0], rowA[i][0]);
                        double[] vec = new double[3];
                        vec[0] = rowA[i][0];
                        vec[1] = rowA[i][1] * x1;
                        vec[2] = rowA[i][2] * x2;
                        i++;
                        res.Add(vec);
                    }
                }
            }
            setMatrixInGrid(this.dataGridView4, res.ToArray());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<double[]> res = new List<double[]>();

            var rowA = getMatrixInGrid(this.dataGridView1);
            var rowB = getMatrixInGrid(this.dataGridView2);
            
            if (rowA == null || rowB == null)
            {
                seyError("Не все данные введены!!!");
                return;
            }

            sordArray(rowA);
            sordArray(rowB);

            if (!checkArray(rowA) || !checkArray(rowB))
            {
                seyError("Выпуклая функция нечеткого числа!!!");
                return;
            }

            int i = 0;

            for (int j = 0; j < rowB.Length;)
            {
                if (rowA[i][0] == rowB[j][0])
                {
                    double[] vec = new double[3];
                    vec[0] = rowA[i][0];
                    vec[1] = rowA[i][1] / rowB[j][2];
                    vec[2] = rowA[i][2] / rowB[j][1];
                    i++; j++;
                    res.Add(vec);
                }
                else
                {
                    if (rowA[i][0] > rowB[j][0])
                    {
                        double x1 = getValue(rowA[i - 1][1], rowA[i][1], rowA[i - 1][0], rowA[i][0], rowB[j][0]);
                        double x2 = getValue(rowA[i - 1][2], rowA[i][2], rowA[i - 1][0], rowA[i][0], rowB[j][0]);

                        double[] vec = new double[3];
                        vec[0] = rowB[j][0];
                        vec[1] = x1 / rowB[j][2];
                        vec[2] = x2 / rowB[j][1];
                        j++;
                        res.Add(vec);
                    }
                    else
                    {
                        double x1 = getValue(rowB[j - 1][1], rowB[j][1], rowB[j - 1][0], rowB[j][0], rowA[i][0]);
                        double x2 = getValue(rowB[j - 1][2], rowB[j][2], rowB[j - 1][0], rowB[j][0], rowA[i][0]);

                        double[] vec = new double[3];
                        vec[0] = rowA[i][0];
                        vec[1] = rowA[i][1] / x2;
                        vec[2] = rowA[i][2] / x1;
                        i++;
                        res.Add(vec);
                    }
                }
            }
            setMatrixInGrid(this.dataGridView4, res.ToArray());
        }
    }
}
