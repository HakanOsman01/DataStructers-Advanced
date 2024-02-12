namespace The
{
    internal class Program
    {
        static void Main(string[] args)
        {
          MaxHeap<int>heap = new MaxHeap<int>();
           heap.Insert(10);
           heap.Insert(20); 
           heap.Insert(30);
           heap.Insert(35);

            int maxElement = heap.ExtractMax();
            Console.WriteLine(maxElement);

        }
    }
}