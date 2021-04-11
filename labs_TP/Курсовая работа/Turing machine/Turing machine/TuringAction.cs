using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turing_machine
{
    class TuringAction
    {
        ActionT[][] arract;
        string alphabet;
        public ActionT[][] Arract { get => arract; set => arract = value; }
        public string Alphabet { get => alphabet; set => alphabet = value; }

        public TuringAction(string alphabet)
        {
            this.alphabet = alphabet;
        }

        public void setAction(ActionT[][] arract)
        {
            this.arract = arract;
        }

        public ActionT[] this[string Symvol]
        {
            get
            {
                for(int i = 0; i < alphabet.Length; ++i )
                {
                    if ("" + alphabet[i] == Symvol)
                        return arract[i];
                }
                return arract[alphabet.Length];
            }
        }

    }
}

