namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Height = 1;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }
        }

        public Node Root { get; private set; }

        public bool Contains(T element)
        {
            return this.Contains(this.Root, element) != null;
        }

        private Node Contains(Node node, T element)
        {
            if(node== null)
            {
                return null;
            }
            var compare = element.CompareTo(node.Value);
            if (compare < 0)
            {
                return this.Contains(node.Left, element);
            }
            else if(compare>0)
            {
                return this.Contains(node.Right, element);
            }
            return node;
        }

        public void Delete(T element)
        {
            throw new InvalidOperationException();
        }

        public void DeleteMin()
        {
            throw new InvalidOperationException();
        }

        public void Insert(T element)
        {
            this.Root = this.Insert(this.Root, element);
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }
            if (element.CompareTo(node.Value)<0)
            {
                node.Left= this.Insert(node.Left,element);
            }
            else
            {
                node.Right=Insert(node.Right, element);
            }
             node = this.Balance(node);
             node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            return node;
        }

        private Node Balance(Node node)
        {
           var balanceFactor=Height(node.Left)-Height(node.Right);
            if (balanceFactor > 1)
            {
                var childBalanceFactor = Height(node.Left.Left) 
                    - Height(node.Left.Right);
               
                if (childBalanceFactor < 0)
                {
                    node.Left=RotateLeft(node.Left);
                }
                node = RotateRight(node);

            }
            else if (balanceFactor < -1)
            {
                var childBalanceFactor = Height(node.Right.Left) 
                    - Height(node.Right.Right);
                if (childBalanceFactor > 0)
                {
                    node.Right=RotateRight(node.Right);
                }
                node=RotateLeft(node);

            }
            return node;
        }
        private Node RotateLeft(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            return temp;
        }
        private Node RotateRight(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            node.Height=Math.Max(Height(node.Left),Height(node.Right))+1;

            return node;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
        }
        //Helper Methods
        public int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;

        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            this.EachInOrder(node.Left, action);
            action.Invoke(node.Value);
            this.EachInOrder(node.Right, action);

        }
    }
}
