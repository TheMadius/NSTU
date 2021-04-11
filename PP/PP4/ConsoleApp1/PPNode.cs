using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class PPNode
    {
        private StreamWriter fileW;
        private Semaphor sem = new Semaphor(0);
        private List<Pair> pairs = new List<Pair>();
        private Mutex mutexList = new Mutex();
        private bool flag = true;
        int i;
        static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
        int NOD(int a, int b)
        {
            if (a < b)
                Swap(ref a, ref b);
            while (a != 0 && b != 0)
            {
                a = a % b;
                Swap(ref a, ref b);
            }
            Thread.Sleep(5);
            return a;
        }
        public PPNode(int i)
        {
            fileW = new StreamWriter("output" + Convert.ToString(i) + ".txt", false);
            this.i = i;
        }
        public void read()
        {
            Pair p;
            while (true)
            {
                Program.mutexFile.WaitOne();
                if (Program.fileR.Peek() == -1)
                {
                    Program.mutexFile.ReleaseMutex();
                    break;
                }
                string[] split = Program.fileR.ReadLine().Split(' ');

                p.a = Convert.ToInt32(split[0]);
                p.b = Convert.ToInt32(split[1]);

                Program.mutexFile.ReleaseMutex();
                mutexList.WaitOne();
                pairs.Add(p);
                sem.Release();
                mutexList.ReleaseMutex();
            }
            flag = false;
            sem.Release();
        }
        public void write()
        {
            int res;
            Pair p;
            while (true)
            {
                mutexList.WaitOne();
                if (!flag && pairs.Count == 0)
                {
                    mutexList.ReleaseMutex();
                    break;
                }
                if (pairs.Count == 0)
                {
                    mutexList.ReleaseMutex();
                    sem.WaitOne();
                    continue;
                }
                p = pairs[0];
                pairs.Remove(p);
                mutexList.ReleaseMutex();

                res = NOD(p.a, p.b);
                fileW.WriteLine("NOD(" + p.a + "," + p.b + ") = " + res);
            }
            fileW.Close();
        }
    }
}
