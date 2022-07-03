using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Лабораторная работа 1 Оценка метрических характеристик программ по Холстеду
namespace lab1_ML
{
    public partial class Form1 : Form
    {
        //Генератор случайных чисел 
        public Random gen = new Random();
        public Form1()
        {
            InitializeComponent();
        }
        //Проверка заполнености массива (нет нулевых элементов)
        private bool check(int[] N)
        {
            foreach (var item in N)
                if (item == 0)
                    return false;
            return true;
        }
        //Одни шаг моделирования написания кода
        private int oneStepModel(int n)
        {
            //Предыдущий номер сгенерированного оператора и операнда
            int past = -1;
            int[] N = new int[n];
            
            //Инициализация массива операндов и операторов
            for (int i = 0; i < n; i++)
                N[i] = 0;
                
            //Моделирование процесса написания кода 
            while(true)
            {
                if (check(N))
                    break;

                int r = gen.Next(1, n + 1);

                if (past == r)
                    continue;
                N[r - 1]++;
                past = r;
            }
            
            //Общее количество операндов и операторов в коде
            return N.Sum();
        }
        private void Button1_Click(object sender, EventArgs E)
        {
            //Получение входных значений
            int n = Convert.ToInt32(this.textBox1.Text);
            int Q = Convert.ToInt32(this.textBox2.Text);
            double k = Convert.ToDouble(this.textBox3.Text);
            //Среднеквадратическое отклонение
            double sigma;
            //Математическое ожидание
            double Mq = 0;
            //Дисперсия
            double Dq = 0;
            //Доверительный интервал
            double e = 0.1;
            
            for (int i = 0; i < Q; ++i)
            {
                int Qi = oneStepModel(n);
                Mq += Qi;
                Dq += Qi * Qi;
            }
            Dq = (Dq - ((Mq*Mq)/Q)) / (Q - 1);
            Mq = Mq / Q;
            sigma = Math.Sqrt(Dq)/ Mq;

            this.textBox4.Text = Convert.ToString(Math.Round(Mq,2));
            this.textBox5.Text = Convert.ToString(Math.Round(Dq,2));
            this.textBox6.Text = Convert.ToString(Math.Round(sigma,2));
            this.textBox7.Text = Convert.ToString(Math.Round(0.9 *(n)*Math.Log(n,2),2));
            this.textBox8.Text = Convert.ToString(Math.Round((Math.PI* Math.PI * n * n)/6,2));
            this.textBox9.Text = Convert.ToString(Math.Round(1 /(2*Math.Log(n,2)),2));

            this.textBox10.Text = Convert.ToString(Math.Round((k*k*Dq/(e*e))+1,2));

        }
    }
}
