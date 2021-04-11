using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public class DSyllabus
    {
        private Direction direction;
        private List<Discipline> LDiscipline = new List<Discipline>();

        public DSyllabus(Direction direction, List<Discipline> lDiscipline)
        {
            this.direction = direction;
            LDiscipline = lDiscipline;
        }

        public Direction Direction { get => direction; set => direction = value; }
        public List<Discipline> LDiscipline1 { get => LDiscipline; set => LDiscipline = value; }
    }
}
