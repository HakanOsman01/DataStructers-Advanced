using System;
using System.Diagnostics.CodeAnalysis;
using _01.Two_Three;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var tree = new TwoThreeTree<IntDemo>();

            tree.Insert(new IntDemo(10));
            tree.Insert(new IntDemo(20));
            tree.Insert(new IntDemo(15));
            tree.Insert(new IntDemo(35));
            tree.Insert(new IntDemo(40));
            tree.Insert(new IntDemo(304));
            tree.Insert(new IntDemo(4));
            tree.Insert(new IntDemo(355));
            tree.Insert(new IntDemo(139));
            tree.Insert(new IntDemo(39));
            tree.Insert(new IntDemo(1000));
            tree.Insert(new IntDemo(99));
            string output = tree.Dfs();
            Console.WriteLine(output);

        }
        
    }
    public class IntDemo : IComparable<IntDemo>
    {
        public IntDemo(int value)
        {
            this.Value = value;
        }
        public int Value { get; set; }
        public int CompareTo( IntDemo other)
        {
          return this.Value.CompareTo(other.Value);
        }
        public override string ToString()
        {
            return $"{this.Value}";
        }

    }
}
