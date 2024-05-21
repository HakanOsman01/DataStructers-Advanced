using System;
using System.Diagnostics;
using System.Linq;
using _01.RedBlackTree;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var rbt = new RedBlackTree<int>();
            const int number = 10000000;
            for (int i = 1; i <= number; i++)
            {
                rbt.Insert(i);
            }
            int[]array= new int[number];
            int startIndex = 0;
            for (int i = 1; i <= number; i++)
            {
                array[startIndex++] = i;

            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            bool isThere = array.Contains(number);
            stopwatch.Stop();
            Console.WriteLine($"Time in array to searc {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();
            stopwatch.Start();
            bool isThereInRbt = rbt.Contains(number);
            stopwatch.Stop();
            Console.WriteLine($"Time in red-black tree to search {stopwatch.ElapsedMilliseconds}");


        }
    }
}
