using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public class Consultation : ILesson
    {
        private Date date;
        private Discipline nameLesson;
        private Teacher teacher;
        private Time time;
        private Audience audience;

        public Consultation(Date date, Discipline nameLesson, Teacher teacher, Time time, Audience audience)
        {
            this.date = date;
            this.nameLesson = nameLesson;
            this.teacher = teacher;
            this.time = time;
            this.audience = audience;
        }
        public Date Date { get => date; set => date = value; }
        public Discipline NameLesson { get => nameLesson; set => nameLesson = value; }
        public Teacher Teacher { get => teacher; set => teacher = value; }
        public Time Time { get => time; set => time = value; }
        public Audience Audience { get => audience; set => audience = value; }

        public string getName()
        {
            return nameLesson.Names + ". Консультация";
        }
    }
}
