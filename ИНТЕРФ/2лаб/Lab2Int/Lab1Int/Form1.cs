using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab1Int
{
    public partial class Form1 : Form
    {
        public Random Generator = new Random();

        public Form1()
        {
            InitializeComponent();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double res;
            if(double.TryParse(this.textBox2.Text,out res))
                this.textBox3.Text = (1 - res).ToString();
            else
                this.textBox3.Text = "";
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            SubRoute subRoute1 = getData(this.dataGridView2);
            SubRoute subRoute2 = getData(this.dataGridView1);

            int indexSub = Convert.ToInt32(this.textBox4.Text);
            int N = Convert.ToInt32(this.textBox5.Text);
            List<double> time = new List<double>();

            if (indexSub == 1)
                time = experement(subRoute1, subRoute2, N, 1);
            else
                time = experement(subRoute1, subRoute2, N, 0);

            this.textBox6.Text = Convert.ToString(getAvg(time));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SubRoute subRoute1 =  getData(this.dataGridView2);
            SubRoute subRoute2 = getData(this.dataGridView1);
            List<double> time = new List<double>();
            int N = Convert.ToInt32(this.textBox8.Text);
            double vRout = Convert.ToDouble(this.textBox2.Text);

            time = experement(subRoute1, subRoute2,N, vRout);

            this.textBox7.Text = Convert.ToString(getAvg(time));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.chart2.Series[0].Points.Clear();
            SubRoute subRoute1 = getData(this.dataGridView2);
            SubRoute subRoute2 = getData(this.dataGridView1);

            double vRout = Convert.ToDouble(this.textBox2.Text);
            int N = Convert.ToInt32(this.textBox9.Text);
            int K = (int)(1 + 3.22 * Math.Log(N));
            List<double> time = new List<double>();

            time = experement(subRoute1, subRoute2, N, vRout);

            var l = new List<double>();
            var h = getHist(time, K, ref l);

            for (int i = 0; i < h.Count; i++)
                this.chart2.Series[0].Points.AddXY(i + 1, ((double)h[i]) / N);
        }

        double getTime()
        {
            return 1 + 2 * Generator.NextDouble();
        }

        double getAvg(List<double> time)
        {
            double avg = 0;
            foreach (var item in time)
                avg += item;

            return avg / time.Count;
        }
        List<int> getHist(List<double> list, int k, ref List<double> listX)
        {
            List<int> listv = new List<int>();
            List<double> x = new List<double>();
            double min = list.Min();
            double max = list.Max();

            double step = (max - min) / k;

            for(double i = min; i < max; i += step)
            {
                int v = 0;
                foreach (var item in list)
                {
                    if((item >= i) && (item <= i + step))
                        v++;
                }
                listX.Add(min + (step/2));
                listv.Add(v);
            }
            return listv;
        }

        List<double> experement(SubRoute r1, SubRoute r2, int N, double vRout)
        {
            List<double> time = new List<double>();

            for (int k = 0; k < N; ++k)
            {
                SubRoute subRoute;
                Route route = null;

                if (vRout <= Generator.NextDouble())
                    subRoute = r2;
                else
                    subRoute = r1;

                double r = Generator.NextDouble();
                double ver = (double)1 / subRoute.list.Count;
                double t = 0;

                for (int i = 0; i < subRoute.list.Count; i++)
                {
                    if (r > ver)
                        r = r - ver;
                    else
                    {
                        route = subRoute.list[i];
                        break;
                    }
                }
                for (int i = 0; i < route.CountStep; ++i)
                {
                    double err = Generator.NextDouble();
                    if (err < route.P)
                    {
                        i = -1;
                        t += getTime();
                    }
                    else
                    {
                        t += getTime();
                    }
                }
                time.Add(t);
            }
            return time;
        }

        public class Route
        {
            public double P;
            public int CountStep;
            public Route(double P = 0, int CountStep = 0)
            {
                this.P = P;
                this.CountStep = CountStep;
            }

        }
        public class SubRoute
        {
            public List<Route> list;
            public SubRoute(List<Route> list = null)
            {
                this.list = list;
            }
        }

        SubRoute getData(DataGridView data)
        {
            List<Route> list = new List<Route>();

            for (int i = 0; i < data.Rows.Count - 1; i++)
            {
                Route route = new Route();
                route.P = Convert.ToDouble(data.Rows[i].Cells[0].Value.ToString().Replace(".", ","));
                route.CountStep = Convert.ToInt32(data.Rows[i].Cells[1].Value);
                list.Add(route);
            }

            return new SubRoute(list);
        }
        public double Exp(double lambda)
        {
            double r = Generator.NextDouble();
            return func(lambda, r);
        }
        double func(double lambda, double x)
        {
            return -(1 / lambda) * Math.Log(x);
        }

        double downTime(SubRoute r1, SubRoute r2, double vRout, double time, double lambda)
        {
            double tDown = 0, tComing = 0, tPassage = 0;

            while (true)
            {
                tComing += Exp(lambda);

                if(tComing >= time)
                    break;

                double twork = experement(r1, r2, 1, vRout)[0];

                if (tDown < tComing)
                    tDown = tComing;

                tDown += twork;

                if (tDown > time)
                    tDown = time;

                tPassage += tDown - tComing;
            }

            return tPassage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SubRoute subRoute1 = getData(this.dataGridView2);
            SubRoute subRoute2 = getData(this.dataGridView1);
            double vRout = Convert.ToDouble(this.textBox2.Text);
            double allTime = 0;
            double timeP1 = 0;
            double timeP2 = 0;

            double lambda1 = Convert.ToDouble(this.textBox1.Text);
            double lambda2 = Convert.ToDouble(this.textBox10.Text);

            int N = Convert.ToInt32(this.textBox11.Text);

            double P1 = Convert.ToDouble(this.textBox12.Text);
            double P2 = Convert.ToDouble(this.textBox13.Text);

            for(int i = 0; i < N;i++)
            {
                double timeOut = Exp(lambda2);
                double tUp = experement(subRoute1, subRoute2, 1, vRout)[0];

                timeP1 += downTime(subRoute1, subRoute2, vRout, timeOut, lambda1);
                timeP2 += tUp;

                allTime += timeOut + tUp;
            }
            this.textBox14.Text = Convert.ToString( ((timeP1 * P1) + (timeP2 * P2))/ allTime);
        }
    }
}
