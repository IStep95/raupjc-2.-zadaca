using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak8Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other .NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }

        private static async void LetsSayUserClickedAButtonOnGuiMethod()
        {
            Task<int> task = GetTheMagicNumber();
            await task;
            var result = task.Result;
            Console.WriteLine(result);
        }

        private static async Task<int> GetTheMagicNumber()
        {
            Task<int> task = IKnowIGuyWhoKnowsAGuy();
            await task;
            return task.Result;
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            Task<int> task1 = IKnowWhoKnowsThis(10);
            Task<int> task2 = IKnowWhoKnowsThis(5);
            await task1;
            await task2;
            return task1.Result + task2.Result;
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            Task<int> task = Zadatak7Main.Program.FactorialDigitSumAsync(n);
            await task;
            return unchecked((int)task.Result);
        }

    }
}
