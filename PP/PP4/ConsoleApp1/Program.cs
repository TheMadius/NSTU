using ConsoleApp1.Properties;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        public static Mutex mutexFile = new Mutex();
        public static StreamReader fileR = new StreamReader("data.txt");
        static void Main(string[] args)
        {
            Stopwatch time = new Stopwatch();
            Console.WriteLine("Введите количество узлов: ");
            int num = Convert.ToInt32(Console.ReadLine());
            Thread[][] threads = new Thread[2][];
            threads[0] = new Thread[num];
            threads[1] = new Thread[num];
            PPNode[] nodes = new PPNode[num];

            time.Start();
            for (int i = 0; i < num; ++i)
            {
                nodes[i] = new PPNode(i);
                threads[0][i] = new Thread(nodes[i].read);
                threads[1][i] = new Thread(nodes[i].write);
                threads[0][i].Start();
                threads[1][i].Start();
            }

            for (int i = 0; i < num; ++i)
            {
                threads[0][i].Join();
                threads[1][i].Join();
            }
            time.Stop();

            Console.WriteLine("Время: " + time.ElapsedMilliseconds + " ms");
        }
    }
}
