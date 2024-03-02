using System;
using AVLTree;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var tree = new AVL<int>();
            tree.Insert(10);
            tree.Insert(20);
            tree.Insert(30);
            tree.Insert(50);
            tree.Insert(100);
            tree.DeleteMax();
            tree.EachInOrder(value => Console.WriteLine(value));

        }
    }
}
