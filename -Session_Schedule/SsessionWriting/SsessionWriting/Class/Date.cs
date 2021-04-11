using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public class Date
    {
        private ushort dey ;
        private ushort month ;
        private static ushort[] calendar = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 } ;

        public ushort Dey { get => dey; set => dey = value; }
        public ushort Month { get => month; set => month = value; }

        public override string ToString()
        {
            if (month > 9)
                return (dey.ToString() + "." + month.ToString());
            else
                return (dey.ToString() + ".0" + month.ToString());
        }

        public Date(ushort dey, ushort month)
        {
            if (check(dey, month))
            {
                this.dey = dey;
                this.month = month;
            }
            else
                throw new IndexOutOfRangeException();
        }

        public void setDate(ushort dey, ushort month)
        {
            if (check(dey, month))
            {
                this.dey = dey;
                this.month = month;
            }
            else
                throw new IndexOutOfRangeException();
        }

        private bool check(ushort dey, ushort month)
        {
            if (month > 12)
                return false;
            else
                if (dey > calendar[month - 1])
                return false;

            return true;
        }
      
        public static bool operator<(Date tis , Date other)
        {
            if (tis.month < other.month)
                return true;
            else if(tis.month == other.month)
            {
                if (tis.dey <= other.dey)
                    return true;
                else
                    return false;
            }

            return false;
        }
        public static bool operator >(Date tis, Date other)
        {
            if (tis.month > other.month)
                return true;
            else if (tis.month == other.month)
            {
                if (tis.dey > other.dey)
                    return true;
                else
                    return false;
            }

            return false;
        }
    }
}
