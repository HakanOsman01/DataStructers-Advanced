using System;
using System.Diagnostics.CodeAnalysis;
using _01.Two_Three;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            //var tree = new TwoThreeTree<IntDemo>();

            //tree.Insert(new IntDemo(10));
            //tree.Insert(new IntDemo(20));
            //tree.Insert(new IntDemo(15));
            //tree.Insert(new IntDemo(35));
            //tree.Insert(new IntDemo(40));
            //tree.Insert(new IntDemo(304));
            //tree.Insert(new IntDemo(4));
            //tree.Insert(new IntDemo(355));
            //tree.Insert(new IntDemo(139));
            //tree.Insert(new IntDemo(39));
            //tree.Insert(new IntDemo(1000));
            //tree.Insert(new IntDemo(99));
            var tree = new TwoThreeTree<Employee>();
            tree.Insert(new Employee("Hakan", 22, 100000));
            tree.Insert(new Employee("Todor Ganchev", 57, 200000));
            tree.Insert(new Employee("Prof Aleksieva", 30, 10));
            var employee = new Employee("Hakai", 22, 100001);
            var output= tree.Contains(employee);
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
    public class Employee : IComparable<Employee>
    {
        public Employee(string name,int age,decimal salary)
        {
            this.Name = name;
            this.Age = age;
            this.Salary = salary;
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public int CompareTo( Employee other)
        {
            return this.Salary.CompareTo(other.Salary);

        }
        public override string ToString()
        {
            return $"{Name}-{Age}{Salary:f2}";
        }

    }
}
