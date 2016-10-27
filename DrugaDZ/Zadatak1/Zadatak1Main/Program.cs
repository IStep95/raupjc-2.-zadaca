using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1Main
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            string s1 = "Ivo";
            string s2 = "Pero";
            string s3 = "Matija";

            list.Add(s1);
            list.Add(s2);
            list.Add(s3);
            foreach (string s in list) Console.WriteLine(s);
            s1 = "Mato";
            foreach (string s in list) Console.WriteLine(s);

            IEnumerable<string> tmp = list.Where(p => p == "Ana");
            List<TodoItem> list2 = new List<TodoItem>();
            
                      
            var tmp2 = list2.FirstOrDefault();

            dynamic item = new TodoItem("stavka");
            TodoItem item2 = item;
            list2.Add(item);

            item.IsCompleted = true;
           

            var tmp3 = list.FirstOrDefault();
            if ((list.Select(p => p == "Ana").Count()) != 0)
            {
                //Console.WriteLine("Sadrzi");
            }
            else
            {
                //Console.WriteLine("Ne sadrzi");
            }
            Console.ReadKey();
        }
    }
}
