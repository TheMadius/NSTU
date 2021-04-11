using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public class Direction
    {
       public String Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
       public UInt32 Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
       public string ShortNameGrup { get => shortNameGrup; set => shortNameGrup = value; }
        
        public Direction(string shortNameGrup, string name, uint id)
        {
            this.shortNameGrup = shortNameGrup;
            this.name = name;
            this.id = id;
        }

        private String shortNameGrup;
        private String name;
        private UInt32 id;
    }
}
