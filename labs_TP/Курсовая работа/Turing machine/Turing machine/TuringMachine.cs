using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Turing_machine
{
    class TuringMachine
    {
        TuringAction action;
        InfinityTape ilne;
        int nextQ ;
        public TuringMachine()
        {
            nextQ = 1;
        }

        public void setIlne(InfinityTape Ilne)
        {
            ilne = Ilne;
        }

        public void makeStep()
        {
            if(nextQ == 0)
            {
                return;
            }

            string symbol = ilne[ilne.PosHead].Text;

            ActionT act = action[symbol][nextQ - 1];

            if (act == null)
            {
                throw new Exception("Нет команды в ячейке (" + ((symbol == "")?"_": symbol) + ",Q"+ nextQ+")");
            }

            nextQ = act.Status;

            if(act.Symbol == "_")
            {
                ilne[ilne.PosHead].Text = "";
            }
            else
            {
                ilne[ilne.PosHead].Text = act.Symbol;
            }

            switch (act.Act)
            {
                case Move.Right:
                    ilne.MoveRight();
                    break;
                case Move.Left:
                    ilne.MoveLeft();
                    break;
            }

        }

        internal InfinityTape Ilne { get => ilne; set => ilne = value; }
        internal TuringAction Action { get => action; set => action = value; }
        public int NextQ { get => nextQ; set => nextQ = value; }
    }
}
