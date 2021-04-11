using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public interface ILesson
    {
        Date Date { get; set; }
        Discipline NameLesson { get; set; }
        Teacher Teacher { get; set; }
        Time Time { get; set; }
        Audience Audience { get; set; }
        string getName();
    }
}
