using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Channels;
using AA_Tree;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new AATree<int>();
            int[] input = {
            18,
            13,
            1,
            7,
            42,
            73,
            56,
            24,
            6,
            2,
            74,
            69,
            55
            };
            for (int i = 0; i < input.Length; i++)
            {
                tree.Insert(input[i]);
            }
          
          
          




        }
    }
    public class Student : IComparable<Student>
    {
        public Student(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }

        public int CompareTo( Student other)
        {
            return Id.CompareTo(other.Id );
        }
        public override string ToString()
        {
            return $"The id of student is: {this.Id}";
        }
    }
}
