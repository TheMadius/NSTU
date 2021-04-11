using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turing_machine
{
    enum Move { Right = 'R', Left = 'L', Stand = 'N' }
    class ActionT
    {
       string symbol;
       Move act;
       int status;
       public ActionT()
       {
       }
        public override string ToString() 
        {
            string activ = "";
            switch(act)
            {
                case Move.Left:
                    activ += 'L';
                    break;
                case Move.Right:
                    activ += 'R';
                    break;
                case Move.Stand:
                    activ += 'N';
                    break;
            }

            return symbol + ", " + activ + ", Q" + status;
        }
        public string Symbol { get => symbol; set => symbol = value; }
        public int Status { get => status; set => status = value; }
        public  Move Act { get => act; set => act = value; }
    }
}
