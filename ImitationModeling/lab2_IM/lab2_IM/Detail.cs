using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_IM
{
    class Detail
    {
        private double time;
        private double timeIn;
        public Detail(double t_time = 0)
        {
            Time = t_time;
        }
        public double Time { get => time; set => time = value; }
        public double TimeIn { get => timeIn; set => timeIn = value; }
    }
}
