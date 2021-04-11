using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_IM
{
    class Conveyor
    {
        private Machine Mech;

        private Queue<Detail> queue = new Queue<Detail>();

        public Conveyor(double Ver, double M, int S = 3, double TimeDetal = 1)
        {
            sizeQue = S;

            timeDetal = TimeDetal;

            Mech = new Machine(Ver, M);
        }

        private int countPos = 0;
        private int countFork = 0;
        private double timeWork = 0;
        private double timeWorkOnDit = 0;
        private double timeWorkAll = 0;
        private int sizeQue = 3;
        private double timeDetal;

        public int CountPos { get => countPos; }
        public int CountFork { get => countFork; }
        public double TimeWork { get => timeWork; }
        public double TimeWorkOnDit { get => timeWorkOnDit; }
        public double TimeWorkAll { get => timeWorkAll; }

        private double getTimeDetal()
        {
            return timeDetal;
        }

        public void AddQueue(double TimeIN)
        {
            Detail newDet = new Detail();
            countPos++;

            if (queue.Count < sizeQue)
            {
                newDet.TimeIn = TimeIN;
                queue.Enqueue(newDet);
            }
        }
        public void work(double Hour)
        {
            countFork = 0;
            countPos = 0;
            timeWork = 0;
            timeWorkAll = 0;
            double TimeNext = 0;
            double TimeDetal = getTimeDetal();
            double step = 0.01;

            for (double i = 0; i < Hour; i += step)
            {
                if (TimeDetal <= i)
                {
                    AddQueue(TimeDetal);
                    TimeDetal += getTimeDetal();
                }

                if (queue.Count() != 0 && i >= TimeNext)
                {
                    Detail Det = queue.Dequeue();

                    Det = Mech.treatment(Det);

                    TimeNext = i + Det.Time;

                    if (TimeNext > Hour)
                    {
                        timeWorkOnDit = timeWork;
                        //timeWork += (Det.Time - Mech.TimeBreak1);
                        timeWorkAll += (Det.Time - (TimeNext - Hour));
                    }
                    else
                    {
                        timeWork += (Det.Time - Mech.TimeBreak1);
                        timeWorkAll += Det.Time;
                        countFork++;    
                    }
                }
            }
        }
    }
}
