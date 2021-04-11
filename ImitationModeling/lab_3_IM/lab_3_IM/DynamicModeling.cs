using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_IM
{
    public class DynamicModeling
    {
        private double X1;
        private double X2;
        private double Dmax;
        private double Dmin;
        private double Davg;
        private double Pa;
        private double Pb;
        private double Udop;
        private double dt;

        static Random r = new Random();

        private List<double> X10 = new List<double>();
        private List<double> X11 = new List<double>();
        private List<double> X12 = new List<double>();
        private List<double> X13 = new List<double>();

        private List<double> D10 = new List<double>();
        private List<double> D11 = new List<double>();
        private List<double> D12 = new List<double>();
        private List<double> D13 = new List<double>();

        private List<double> X20 = new List<double>();
        private List<double> X21 = new List<double>();
        private List<double> X22 = new List<double>();

        private List<double> D20 = new List<double>();
        private List<double> D21 = new List<double>();
        private List<double> D22 = new List<double>();

        private List<double> Y0A = new List<double>();
        private List<double> Y0B = new List<double>();

        private List<double> Y11 = new List<double>();
        private List<double> Y12 = new List<double>();
        private List<double> Y13 = new List<double>();
        private List<double> Y21 = new List<double>();
        private List<double> Y22 = new List<double>();

        private List<double> YCA = new List<double>();
        private List<double> YCB = new List<double>();

        private List<double> DaneDit = new List<double>();

        public List<double> DaneDit1 { get => DaneDit; }
        public List<double> Y0A1 { get => Y0A;}
        public List<double> Y0B1 { get => Y0B; }
        public List<double> Y111 { get => Y11;  }
        public List<double> Y121 { get => Y12;  }
        public List<double> Y131 { get => Y13;  }
        public List<double> Y211 { get => Y21; }
        public List<double> Y221 { get => Y22;  }

        public DynamicModeling(double X1, double X2,double Y = 50,double CA = 25, double CB = 50,double A0 = 500, double B0 = 500, double rPa = 200, double rPb = 400)
        {
            this.X1 = X1;
            this.X2 = X2;

            dt = 1;
            Dmax = 18;
            Dmin = 2;
            Davg = 10;
            Pa = rPa;
            Pb = rPb;
            Udop = 0.02;

            YCA.Add(CA);
            YCB.Add(CB);

            Y11.Add(Y);
            Y12.Add(Y);
            Y13.Add(Y);
            Y21.Add(Y);
            Y22.Add(Y);

            Y0A.Add(A0);
            Y0B.Add(B0);

            D20.Add(50);
            D21.Add(10);
            D22.Add(10);

            D10.Add(50);
            D11.Add(10);
            D12.Add(10);
            D13.Add(10);

            DaneDit.Add(0);

            X10.Add(10);
            X11.Add(10);
            X12.Add(10);
            X13.Add(10);
            X20.Add(10);
            X21.Add(10);
            X22.Add(10);
        }

        void correction(double du,int index)
        {
           // double alfa = 0.5 * Math.Abs(du);
            double alfa = 3;
            if(du > 0)
            {
                //B
                D21.Add(Dmin + Davg * (Y21[index] / D21[index - 1]) + alfa * Dmax - 1);
                D22.Add(Dmin + Davg * (Y22[index] / D22[index - 1]) + alfa * Dmax - 1);
                D20.Add(Dmin + Davg * (Y0B[index] / D20[index - 1]) + alfa * Dmax - 1);
                //A
                D11.Add(Dmin + Davg * (Y11[index] / D11[index - 1]) + alfa * Dmax + 1);
                D12.Add(Dmin + Davg * (Y12[index] / D12[index - 1]) + alfa * Dmax + 1);
                D13.Add(Dmin + Davg * (Y13[index] / D13[index - 1]) + alfa * Dmax + 1);
                D10.Add(Dmin + Davg * (Y0A[index] / D10[index - 1]) + alfa * Dmax + 1);
            }
            else
            {
                //B
                D21.Add(Dmin + Davg * (Y21[index] / D21[index - 1]) + alfa * Dmax + 1);
                D22.Add(Dmin + Davg * (Y22[index] / D22[index - 1]) + alfa * Dmax + 1);
                D20.Add(Dmin + Davg * (Y0B[index] / D20[index - 1]) + alfa * Dmax + 1);
                //A
                D11.Add(Dmin + Davg * (Y11[index] / D11[index - 1]) + alfa * Dmax - 1);
                D12.Add(Dmin + Davg * (Y12[index] / D12[index - 1]) + alfa * Dmax - 1);
                D13.Add(Dmin + Davg * (Y13[index] / D13[index - 1]) + alfa * Dmax - 1);
                D10.Add(Dmin + Davg * (Y0A[index] / D10[index - 1]) + alfa * Dmax - 1);
            }

        }
        void standard(int index)
        {
            D21.Add(D21[index - 1]);
            D22.Add(D22[index - 1]);

            D11.Add(D11[index - 1]);
            D12.Add(D12[index - 1]);
            D13.Add(D13[index - 1]);
        }

        public void work(double Time)
        {
            double start = dt;
            int index = 0;
            double ua, ub;

            for(double t = start;t <= Time; t+=dt)
            {
                double dx0A, dx0B, dx11, dx12, dx13, dx21, dx22, dxCA, dxCB;

                dx0A = (X1 - D10[index] * dt);
                dx0B = (X2 - D20[index] * dt);
                dx11 = (D10[index] - D11[index]) * dt;
                dx12 = (D11[index] - D12[index]) * dt;
                dx13 = (D12[index] - D13[index]) * dt;
                dx21 = (D20[index] - D21[index]) * dt;
                dx22 = (D21[index] - D22[index]) * dt;
                dxCA = D13[index] * dt;
                dxCB = D22[index] * dt;

                Y0A.Add(Y0A[index] + dx0A);
                Y0B.Add(Y0B[index] + dx0B);

                Y11.Add(Y11[index] + dx11);
                Y12.Add(Y12[index] + dx12);
                Y13.Add(Y13[index] + dx13);

                Y21.Add(Y21[index] + dx21);
                Y22.Add(Y22[index] + dx22);

                YCA.Add(YCA[index] + dxCA);
                YCB.Add(YCB[index] + dxCB);

                index++;

                ua = YCA[index] / Pa;
                ub = YCB[index] / Pb;

                if(ua >= 1 && ub >= 1)
                {
                    YCA[index] -= Pa;
                    YCB[index] -= Pb;
                    DaneDit.Add(DaneDit[index - 1] + 1);
                }
                else
                {
                    DaneDit.Add(DaneDit[index - 1]);
                }

                if(Math.Abs(ua - ub) > Udop)
                {
                    correction(ua - ub, index);
                }
                else
                {
                    standard(index);
                }

                if(Y0A[index] < 0.2 * Y0A[0])
                {
                    Y0A[index] += 0.2 * Y0A[0];
                }

                if (Y0B[index] < 0.2 * Y0B[0])
                {
                    Y0B[index] += 0.2 * Y0B[0];
                }

                X11.Add(Y11[index] / D11[index]);
                X12.Add(Y12[index] / D12[index]);
                X13.Add(Y13[index] / D13[index]);
                X21.Add(Y21[index] / D21[index]);
                X22.Add(Y22[index] / D22[index]);
                X10.Add(Y0A[index] / D10[index]);
                X20.Add(Y0B[index] / D20[index]);
            }
        }
    }
}
