using PersoHashCode.Classes;

namespace PersoHashCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person()
            {
                Age = 15,
                FirstName = "Foo",
                SecondName = "Bar",
            };
            Person person1 = new Person()
            {
                Age = 38,
                FirstName = "Hakan",
                SecondName = "Osman",
            };
            Console.WriteLine($"The hash code of the first person:{person.GetHashCode()}");
            Console.WriteLine($"The hash code of the second person:{person1.GetHashCode()}");


        }
    }
}