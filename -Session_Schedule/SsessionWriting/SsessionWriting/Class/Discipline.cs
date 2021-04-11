using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public class Discipline
    {
        private string names;
        private Department dep;

        public Discipline(string names, Department dep)
        {
            this.names = names;
            this.dep = dep;
        }

        public string Names { get => names; set => names = value; }
        public Department Dep { get => dep; set => dep = value; }
    }
}
