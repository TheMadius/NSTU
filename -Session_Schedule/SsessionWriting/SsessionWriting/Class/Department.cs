using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
   public class Department
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

        public Department(string name, uint id)
        {
            this.name = name;
            this.id = id;
        }
              
        private String name;
        private UInt32 id;
    }
}
