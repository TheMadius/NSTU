using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public class Time
    {
        private ushort minutes;
        private ushort hour;
        private ushort seconds;

        public Time(ushort minutes, ushort hour, ushort seconds = 0)
        {
            this.Minutes = minutes;
            this.Hour = hour;
            this.Seconds = seconds;
        }

        public Time()
        {
            Seconds = 0; ;
            hour = 0;
        }

        public ushort Minutes
        {
            get => minutes;
            set
            {
                if (checkMin(value))
                    minutes = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
        public ushort Hour
        {
            get => hour;
            set
            {
                if (checkHour(value))
                    hour = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
        public ushort Seconds
        {
            get => seconds;
            set
            {
                if (checkSec(value))
                    seconds = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public Time clock(ushort min, ushort hous)
        {
            Time tim = new Time();

            if (this.hour >= hous)
                tim.hour = (ushort)(this.hour - hous);
            else
                tim.hour = (ushort)(24 - (hous - this.hour));


            if (this.minutes >= min)
                tim.minutes = (ushort)(this.minutes - min);
            else
            {
                tim.minutes = (ushort)(60 - (min - this.minutes));
                tim = tim.clock(0, 1);
            }

            return tim;

        }

        public static bool operator<(Time th,Time other)
        {
            if (th.hour < other.hour)
                return true;
            else if (th.hour == other.hour)
            {
                if (th.minutes <= other.minutes)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        public static bool operator>(Time th,Time other)
        {
            if (th.hour > other.hour)
                return true;
            else if (th.hour == other.hour)
            {
                if (th.minutes >= other.minutes)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static bool operator==(Time th, Time other)
        {
            if (th.hour == other.hour && th.minutes == other.minutes)
                return true;
              else
                return false;
        }


        public static bool operator !=(Time th, Time other)
        {
            return !(th == other);
        }

        public override string ToString()
        {
            return (hour.ToString() + ":" + ((minutes > 9) ? minutes.ToString() : "0" + minutes.ToString())) ;
        }

        private bool checkMin(ushort minutes)
        {
            return !(minutes < 0 || minutes > 60);
        }
        private bool checkHour(ushort hour)
        {
            return !(hour < 0 || hour > 24);
        }
        private bool checkSec(ushort seconds)
        {
            return !(seconds < 0 || seconds > 60);
        }
    }
}
