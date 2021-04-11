using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turing_machine
{
    class SaveData
    {
        SeveLine line;
        TuringAction act;

        public SaveData(SeveLine line, TuringAction act)
        {
            Line = line;
            Act = act;
        }

        public SeveLine Line { get => line; set => line = value; }
        public TuringAction Act { get => act; set => act = value; }
    }
}
