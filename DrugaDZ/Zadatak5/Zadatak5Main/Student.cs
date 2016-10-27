using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak4Main;

namespace Zadatak5Main
{
    public class Student : IComparable<Student>
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }
        public string Country { get; set; }

        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public override int GetHashCode()
        {
            int result = 1, c;
            c = this.Name.GetHashCode();
            result = 37 * result + c;
            c = this.Jmbag.GetHashCode();
            result = 37 * +c;
            c = this.Gender.GetHashCode();
            result = 37 * +c;

            return result;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (this.GetType() != obj.GetType()))
            {
                return false;
            }

            Student student2 = (Student)obj;
            if (this.Name == null || this.Jmbag == null)
            {
                return false;
            }
            if (this.Jmbag == student2.Jmbag)
            {
                return true;
            }

            return false;

        }

        public override string ToString()
        {
            return this.Name;
        }

        public int CompareTo(Student other)
        {
            //string name1 = this.Name.ToLower();
            //string name2 = other.Name.ToLower();
            //Regex.Replace(name1, @"\s+", "");
            //Regex.Replace(name2, @"\s+", "");
            if (this.Equals(other)) return 0;
            return -1;
        }
        public static bool operator ==(Student student1, Student student2)
        {
            return student1.Equals(student2);

        }
        public static bool operator !=(Student student1, Student student2)
        {
            return !student1.Equals(student2);

        }
    }
}
