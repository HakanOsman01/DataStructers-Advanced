using System;
using System.Threading.Channels;
using AA_Tree;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new AATree<int>();
            tree.Insert(6);
            tree.Insert(2);
            tree.Insert(8);
            tree.Insert(16);
            tree.Insert(10);
            tree.InOrder(a => Console.WriteLine(a));
            Console.WriteLine("======================");
            tree.Delete(10);
            tree.InOrder(a => Console.WriteLine(a));




        }
    }
}
