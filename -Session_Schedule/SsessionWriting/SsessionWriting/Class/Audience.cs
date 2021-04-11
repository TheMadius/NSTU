using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public class Audience
    {
        private int CountSeats;
        private string number;

        public Audience(int countSeats, string number)
        {
            CountSeats = countSeats;
            this.number = number;
        }

        public int CountSeats1 { get => CountSeats; set => CountSeats = value; }
        public string Number { get => number; set => number = value; }
    }
}
