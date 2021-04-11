using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2_IM
{
    class Machine
    {
        static Generator gen = new Generator();

        private double verBreakage;
        private double TimeBreak;
        private double m;

        public double TimeBreak1 { get => TimeBreak; }

        public Machine(double verB, double m_)
        {
            verBreakage = verB;
            m = m_;
        }

        private double NormRas()
        {
           while (true)
            {
                double r = gen.getCLT(1000, 0.5, 1);
                if (r > 0)
                {
                    return r;
                }
            }
        }
        private double GetTimeTinc()
        {
            return gen.getUniform(0.2,0.3);
            return 0.25;
        }

        private double GetTreatment()
        {
            return NormRas();
            return 0.5;
        }

        private double GetTimeFix()
        {
            double TimeFix = 0;
           
            for (int i = 0; i < 3; ++i)
            {
                TimeFix += gen.Exp(1/m);
            }

            return TimeFix;
        }
        public Detail treatment(Detail dit)
        {
            double timeTincture = GetTimeTinc();
            double timeAdd = 0;
            TimeBreak = timeTincture;

            while (gen.NextDouble() < verBreakage)
            {
                double Time = GetTimeFix();
                timeAdd += GetTreatment() * gen.NextDouble();
                timeAdd += Time;
                TimeBreak += Time;
            }

            dit.Time = timeTincture + timeAdd + GetTreatment();

            return dit;

        }
    }
}
