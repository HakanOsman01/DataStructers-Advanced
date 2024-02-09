namespace _01.RedBlackTree
{
    using System;

    public class RedBlackTree<T> where T : IComparable
    {
        private const bool Red=true;
        private const bool Black = true;

        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Color = Red;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get;set; }
        }

        public Node root;

        public RedBlackTree()
        {

        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        private void EachInOrder(Node node, Action<T> action)
        {
           if(node == null)
           {

                return;
           }
           this.EachInOrder(node.Left, action);
            action.Invoke(node.Value);
           this.EachInOrder(node.Right, action);
        }

        public RedBlackTree<T> Search(T element)
        {
            throw new NotImplementedException();
        }

        public void Insert(T value)
        {
            this.root = this.Insert(this.root, value);
            this.root.Color = Black;
        }

        private Node Insert(Node node, T value)
        {
            if(node == null)
            {
                return new Node(value);
            }
            if (IsLesser(value, node.Value))
            {
                node.Left=this.Insert(node.Left, value);

            }
            else
            {
                node.Right = this.Insert(node.Right, value);

            }
            if (this.IsRed(node.Right))
            {
                node=this.RotateLeft(node);
            }
            if (this.IsRed(node.Left) && this.IsRed(node.Left.Left))
            {
                node=this.RotateRight(node);
            }
            if (this.IsRed(node.Left) && this.IsRed(node.Right))
            {
                this.ColorsFlip(node);
            }

            return node;



        }

        public void Delete(T key)
        {
            throw new NotImplementedException();
        }

        public void DeleteMin()
        {
            throw new NotImplementedException();
        }

        public void DeleteMax()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            return this.Count(this.root);
        }

        private int Count(Node root)
        {
            if (root == null)
            {
                return 0;
            }
            return 1+this.Count(root.Left)+this.Count(root.Right);
        }

        private void ColorsFlip(Node node)
        {
            node.Color = !node.Color;
            node.Left.Color = !node.Left.Color;
            node.Right.Color=!node.Right.Color;


        }
        private Node RotateLeft(Node node)
        {
            var temp = node.Right;
            temp.Left = node;
            node.Right = temp.Right;
            temp.Color = temp.Left.Color;
            temp.Left.Color = Red;
            return temp;

        }
        private Node RotateRight(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Color = node.Right.Color;
            temp.Color = Red;

            return temp;


        }
        private bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }
            return node.Color == Red;

        }
        //HelperMethod
        private bool IsLesser(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }
        private bool IsEqual(T a, T b)
        {
            return a.CompareTo(b) == 0;

        }
    }
}