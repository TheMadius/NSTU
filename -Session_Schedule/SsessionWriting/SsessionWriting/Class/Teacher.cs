using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SsessionWriting.Class
{
    public class Teacher
    {
        private string firstName;
        private string lastName;
        private string middleName;
        private Department department;

        public Teacher(string lastName , string firstName, string middleName, Department department)
        {
            this.FirstName = firstName;
            this.lastName = lastName;
            this.middleName = middleName;
            this.department = department;
        }

        public string getShortName()
        {
            return (lastName + " " + FirstName[0] + "." + middleName[0] + "."); 
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public Department Department { get => department; set => department = value; }
    }
}
