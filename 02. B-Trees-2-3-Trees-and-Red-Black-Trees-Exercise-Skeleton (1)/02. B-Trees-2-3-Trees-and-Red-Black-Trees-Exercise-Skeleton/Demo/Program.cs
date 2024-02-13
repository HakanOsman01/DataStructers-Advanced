using System;
using _01.RedBlackTree;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var rbt = new RedBlackTree<int>();
            rbt.Insert(10);
            rbt.Insert(20);
            rbt.Insert(15);
            var contains=rbt.Contains(0);
            if (contains)
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
