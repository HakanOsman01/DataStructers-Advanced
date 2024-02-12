namespace The
{
    public interface IMaxHeap<T>
    {
        int Count { get; }
        public T Peek();
        public void Insert(T item);
        public T ExtractMax();

       
    }
}
