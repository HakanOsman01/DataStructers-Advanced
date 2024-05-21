namespace _8._Condense_Array_to_Number
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[]array=Console.ReadLine()
               .Split(' ',StringSplitOptions.RemoveEmptyEntries)
               .Select(x=>int.Parse(x))
               .ToArray();
            
            int originalLenght=array.Length;
            for (int i = 0; i < originalLenght-1; i++)
            {

                int[] condensArray = new int[array.Length-1];

                for (int j = 0; j < array.Length-1; j++)
                {
                    condensArray[j] = array[j]+array[j+1];
                   
                
                }
                array=condensArray;
            }
            Console.WriteLine(array[0]);
        }
    }
}