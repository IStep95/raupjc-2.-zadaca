
using System;
using System.Collections.Generic;
using System.Linq;

namespace Zadatak4Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
            Example2();
            Console.ReadKey();
        }

        private static void Example1()
        {
            List<Student> list = new List<Student>()
            {
                new Student (" Ivan ", jmbag :" 001234567 ")
            };

            var ivan = new Student(" Ivan ", jmbag: " 001234567 ");
            Console.WriteLine(ivan);
            // false :(

            bool anyIvanExists = list.Any(s => s == ivan);
        }

        private static void Example2()
        {
            var list = new List<Student>()
            {
            new Student (" Ivan ", jmbag :" 001234567 "),
            new Student (" Ivan ", jmbag :" 001234567 ")
            };
            // 2 :(
            var distinctStudents = list.Distinct().Count();
        }
    }
}
