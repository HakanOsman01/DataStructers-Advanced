using System;
using _01.Two_Three;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var tree = new TwoThreeTree<string>();
            tree.Insert("A");
            tree.Insert("B");
            tree.Insert("C");
            tree.Insert("D");
            tree.Insert("E");
            tree.Insert("F");
            bool findElement = tree.Contains("F");
            if (findElement == true)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
            

        }
    }
}
