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
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);
            tree.Insert(6);
            Action<int>action=(a)=>Console.WriteLine(a);
            tree.PreOrder(action);
        }
    }
}
