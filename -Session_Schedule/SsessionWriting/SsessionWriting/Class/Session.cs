using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public class Session
    {
        private List<Exam> exams = new List<Exam>();
        private List<Consultation> consultation = new List<Consultation>();
        private Group group;

        public Session(Group group)
        {
            this.group = group;
        }

        public Group Group { get => group; set => group = value; }
        public List<Exam> Exams1 { get => exams; set => exams = value; }
        public List<Consultation> Consultation { get => consultation; set => consultation = value; }

        public void addExem(Exam exem)
        {
            exams.Add(exem);
        }
        public void addConsultation(Consultation exem)
        {
            consultation.Add(exem);
        }
    }
}
