using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turing_machine
{
    class SeveLine
    {
        string line;
        string alfa;
        Point posStartT;
        Point posStartL;
        int max;
        int min;
        int PosHead;
        public SeveLine(string line, string alfa, Point posStartT, Point posStartL, int max , int min,int PosHead)
        {
            this.Line=line;
            this.alfa = alfa;
            this.PosStartT = posStartT;
            this.posStartL = posStartL;
            this.Max = max;
            this.Min = min;
            this.PosHead = PosHead;
        }

        public InfinityTape getLineT()
        {
            InfinityTape Line = new InfinityTape(this);

            return Line;
        }

        public string Line { get => line; set => line = value; }
        public string Alfa { get => alfa; set => alfa = value; }
        public int Max { get => max; set => max = value; }
        public int Min { get => min; set => min = value; }
        public int PosHead1 { get => PosHead; set => PosHead = value; }
        public Point PosStartT { get => posStartT; set => posStartT = value; }
        public Point PosStartL { get => posStartL; set => posStartL = value; }
    }
}
