using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak6Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Example1();
            //Example2();
            //Example3();
            //Example4();
            //Example5();
            //Example6();
            Example7();
            Console.ReadKey();
        }

        private static void Example1()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            LongOperation("A");
            LongOperation("B");
            LongOperation("C");
            LongOperation("D");
            LongOperation("E");

            stopwatch.Stop();
            Console.WriteLine("Synchronous long operation calls finished {0} sec.", stopwatch.Elapsed.TotalSeconds);
        }

        private static void Example2()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.Invoke(() => LongOperation("A"),
                            () => LongOperation("B"),
                            () => LongOperation("C"),
                            () => LongOperation("D"),
                            () => LongOperation("E"));

            stopwatch.Stop();
            Console.WriteLine("Synchronous long operation calls finished {0} sec.", stopwatch.Elapsed.TotalSeconds);
        }

        private static void Example3()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Parallel.For(0, 1000, (i) =>
            {
                var x = 2;
                var y = 2;
                var sum = x + y;

            });

            stopwatch.Stop();
            Console.WriteLine(" Parallel calls finished {0} ms.", stopwatch.Elapsed.TotalMilliseconds);
            stopwatch.Restart();

            for (int i = 0; i < 1000; i++)
            {
                int x = 2;
                int y = 2;
                int sum = x + y;
            }
            stopwatch.Stop();
            Console.WriteLine(" Sync operation calls finished {0} ms.", stopwatch.Elapsed.TotalMilliseconds);
        }

        private static void Example4()
        {
            int counter = 0;
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                counter += 1;
            });

            Console.WriteLine("Counter should be 100. Counter is {0}", counter);
        }

        private static void Example5()
        {
            int counter = 0;
            object objectUsedForLock = new object();

            Parallel.For(0, 100, (i) =>
            {
                lock (objectUsedForLock)
                {
                    counter += 1;
                }
            });

            Console.WriteLine("Counter should be 100. Counter is {0}", counter);
        }

        private static void Example6()
        {
            List<int> results = new List<int>();
            object objectUsedForLock = new object();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                lock (objectUsedForLock)
                {
                    results.Add(i * i);
                }
            });

            Console.WriteLine("Bag length should be 100. Length is {0}", results.Count);
        }

        private static void Example7()
        {
            ConcurrentBag<int> iterations = new ConcurrentBag<int>();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                iterations.Add(i);
            });
            Console.WriteLine("Bag length should be 100. Length is {0}", iterations.Count);
        }

        public static void LongOperation (string taskName)
        {
            Thread.Sleep(1000);
            Console.WriteLine("{0} Finished. Executing Thread: {1}", taskName, Thread.CurrentThread.ManagedThreadId);

        }
    }
}
