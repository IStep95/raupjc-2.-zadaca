using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak7Main
{
   public class Program
    {
        public static void Main(string[] args)
        {

            Task.Run(async () =>
            {
                while (true)
                {
                    int n = 0;
                    Console.WriteLine("Unesite broj n da bi izračunali sumu znamenaka broja n!");
                    string line = "";
                    bool escapePressed = false;
                    while (true)
                    {
                        ConsoleKeyInfo info = Console.ReadKey();
                        if (info.KeyChar == 27)
                        {
                            escapePressed = true;
                            break;
                        }
                        if (info.KeyChar == 13) break;
                        if (info.KeyChar == 8)
                        {
                            line = line.Remove(line.Length-1, 1);
                            //Console.CursorLeft = line.Length-1;
                            Console.Write(" ");
                            Console.CursorLeft = line.Length;
                            continue;
                        }
                        line += info.KeyChar.ToString();
                    }
                    if (escapePressed == true) break;
                    if (IsAllDigits(line) == false) continue;
                    Console.WriteLine();
                    n = int.Parse(line);
                    Task<int> task = FactorialDigitSumAsync(n);
                    int result = await task;
                    Console.WriteLine("Suma znamenaka od "+ n + "! je: "+ result +"\n");
                     
                }
            }).Wait();
        }

        public static async Task<int> FactorialDigitSumAsync(int n)
        {
  
            Task<int> task = Task<int>.Run(() =>
            {
                long resultIn = 1;
                if (n == 0 || n == 1) return 1;
                for (int i = 1; i <= n; i++)
                {
                    resultIn *= i;
                }
                string resultFactorial = resultIn.ToString();
                char[] digits = resultFactorial.ToCharArray();
                int sum = 0;
                for (int i = 0; i < digits.Length; i++)
                {
                    sum += int.Parse(digits[i].ToString());
                }
                return sum;
            });
            await task;
            return task.Result;

        }

        public static bool IsAllDigits(string s)
        {
            return s.All(char.IsDigit);
        }

    }
}
