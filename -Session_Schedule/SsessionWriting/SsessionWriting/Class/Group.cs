using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public class Group
    {

        public Group(UInt32 countStud, Direction direction, short m_number, short semester)
        {
            this.m_countStud = countStud;
            this.m_number = m_number;
            this.m_direction = direction;
            this.semester = semester;
        }

        public UInt32 CountStud
        {
            get
            {
                return m_countStud;
            }
            set
            {
                m_countStud = value;
            }
        }
        public Direction Direction
        {
            get
            {
                return m_direction;
            }
            set
            {
                m_direction = value;
            }
        }
        public short Number {
            get
            {
                return m_number;
            }
            set
            {
                m_number = value;
            }
        }


        public string GetName
        {
            get
            {
                return m_direction.ShortNameGrup + "-" + m_number;
            }
        }

        public short Semester { get => semester; set => semester = value; }

        private UInt32 m_countStud;
        private Direction m_direction;
        private short m_number;
        private short semester;

    }
}
