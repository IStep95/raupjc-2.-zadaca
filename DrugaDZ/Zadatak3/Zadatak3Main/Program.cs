using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak3Main
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
   
            string[] strings = (from item in integers
                                          group item by item into itemGroup
                                          select String.Format("Broj {0} ponavlja se {1} puta", itemGroup.Key, itemGroup.Count())).ToArray();
            int index = 0;
            foreach (string s in strings)
            {
                Console.WriteLine("strings["+ index + "] = " + s);
                index ++;
            }
            Console.ReadKey();

        }
    }
}
