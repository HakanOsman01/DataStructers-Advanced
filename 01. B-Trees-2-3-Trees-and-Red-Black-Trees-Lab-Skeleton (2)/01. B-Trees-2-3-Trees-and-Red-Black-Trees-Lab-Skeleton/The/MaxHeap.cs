namespace The
{
    public class MaxHeap<T> : IMaxHeap<T> where T : IComparable<T>
    {
        private List<T> elements;
        public MaxHeap()
        {
            this.elements = new List<T>();
        }
        public int Count =>elements.Count;

        public T Peek()
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return elements[0];
        }


     

        public T ExtractMax()
        {
           if(elements.Count == 0)
           {
                throw new InvalidOperationException();

           }
            T maxElement = this.elements[0];
            this.Swap(0, elements.Count - 1);
            this.elements.RemoveAt(elements.Count - 1);
            this.HeapifyDown(0);
            return maxElement;

        }

        private void HeapifyDown(int index)
        {
            var biggerChildIndex = this.GetBiggerChildIndex(index);
            while(this.IsIndexValid(biggerChildIndex) 
                && this.IsBiggerChild(index, biggerChildIndex))
            {
                this.Swap(index, biggerChildIndex);
                index = biggerChildIndex;
                biggerChildIndex=this.GetBiggerChildIndex(index);

            }

        }
        private bool IsBiggerChild(int index,int childIndex)
        {
            return elements[index].CompareTo(this.elements[childIndex]) < 0;
        }
        private bool IsIndexValid(int index)
        {
            return index>= 0 && index < elements.Count; 
        }

        private int GetBiggerChildIndex(int index)
        {
            var leftChildIndex = (2 * index) + 1;
            var rightChildIndex = (2 * index) + 2;
            if (rightChildIndex < elements.Count)
            {
                if (elements[leftChildIndex].CompareTo(elements[rightChildIndex]) > 0)
                {
                    return leftChildIndex;
                }
                else
                {
                    return rightChildIndex;
                }
            }
            else if(leftChildIndex < elements.Count)
            {
                return leftChildIndex;

            }
            else
            {
                return -1;
            }
        }

        public void Insert(T item)
        {
           this.elements.Add(item);
           this.HeapifyUp(elements.Count-1);


        }

        private void HeapifyUp(int index)
        {
            var parentIndex = GetParentIndex(index);
           while(index>0 && this.IsBiggerParent(index, parentIndex))
           {
                this.Swap(index, parentIndex);
                index=parentIndex;
                parentIndex=this.GetParentIndex(index);
           }

        }

        private void Swap(int index, int parentIndex)
        {
            var temp = this.elements[index];
            this.elements[index]= this.elements[parentIndex];
            this.elements[parentIndex]= temp;
        }

        private bool IsBiggerParent(int index,int parentIndex)
        {
            return this.elements[index].CompareTo(this.elements[parentIndex]) > 0;
        }


        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }
    }
}
