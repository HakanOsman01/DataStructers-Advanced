using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Channels;
using AVLTree;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var tree = new AVL<int>();
            List<int> list = new List<int>();
            for (int i = 1; i <= 100000; i++)
            {
                list.Add(i);
            }
            for (int i = 1; i <= 100000; i++)
            {
                tree.Insert(i);
            }
           

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            bool isListContains = list.Contains(100000);
            stopwatch.Stop();
            TimeSpan timeSpan = stopwatch.Elapsed;
            Console.WriteLine($"{timeSpan.ToString("mm\\:ss\\.ff")}");
            stopwatch.Restart();
            stopwatch.Start();
            bool IsTreeContains=tree.Contains(100000);
            stopwatch.Stop();
            TimeSpan timeSpan1=stopwatch.Elapsed;
            Console.WriteLine($"{timeSpan1.ToString("mm\\:ss\\.ff")}");

        }
    }
}
