namespace Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Net.Http.Headers;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private List<Hierarchy<T>> childs;
        private Hierarchy<T> parent;
        private T value;
        Dictionary<T,Hierarchy<T>> uniqueHierachy=new Dictionary<T,Hierarchy<T>>();
        

        public Hierarchy(T value)
        {
            this.value = value;
            childs = new List<Hierarchy<T>>();
            uniqueHierachy.Add(value, new Hierarchy<T>(value));

        }


        public int Count => uniqueHierachy.Count;

        public void Add(T element, T child)
        {
            if (!uniqueHierachy.ContainsKey(element))
            {
                throw new InvalidOperationException();
            }
            if (uniqueHierachy[element].childs.Contains(new Hierarchy<T>(child)))
            {
                throw new InvalidOperationException();
            }
            uniqueHierachy[element].childs.Add(new Hierarchy<T>(child));
            
            
            
        }
   

        public bool Contains(T element)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetChildren(T element)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T GetParent(T element)
        {
            throw new NotImplementedException();
        }

        public void Remove(T element)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}