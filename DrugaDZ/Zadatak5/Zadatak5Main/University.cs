using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak4Main;

namespace Zadatak5Main
{
    public class University
    {
        public string Name { get; set; }
        public Student[] Students { get; set; }
        public string Country { get; set;  }
        public bool MaleOnly { get; set; }

        public University(string name , Student[] students, string country, bool maleOnly)
        {
            Name = name;
            Students = students;
            Country = country;
            MaleOnly = maleOnly;
        }
        public Student[] getStudents()
        {
            return Students;
        }

    }
}
