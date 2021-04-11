using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Semaphor
    {
        int count;
        int max;
        int step;
        Semaphore sem;
        Mutex m = new Mutex();
        public Semaphor(int value,int step = 100)
        {
            count = value;
            max = value + step;
            this.step = step;
            sem = new Semaphore(value, max);
        }

        public void WaitOne()
        {
            sem.WaitOne();
            m.WaitOne();
            count--;
            m.ReleaseMutex();
        }

        public void Release()
        {
            sem.Release();
            m.WaitOne();
            count++;
            if(count == max)
            {
                max += step;
                sem = new Semaphore(count, max);
            }
            m.ReleaseMutex();
        }
    }
}
