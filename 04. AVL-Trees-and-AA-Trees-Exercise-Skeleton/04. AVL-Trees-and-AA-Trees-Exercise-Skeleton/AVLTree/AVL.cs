﻿namespace AVLTree
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
        public AVL() {}
        public Node Root { get; private set; }
        private AVL(Node node)
        {

            this.PreOrderCopy(node);

        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }
            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        public void DeleteMax()
        {
            if (this.Root == null)
            {
                return;
            }
            this.Root = this.DeleteMax(this.Root);
        }

        
        public bool Contains(T element)
        {
           return this.Contains(this.Root, element) != null;
        }

        private Node Contains(Node node, T element)
        {
            if(node == null)
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
                return this.Contains(node.Right,element);
            }
            else
            {
                return node;
            }
           
        }

        public void Delete(T element)
        {
            this.Root = this.Delete(this.Root, element);
        }

        private Node Delete(Node node, T element)
        {
            if (node == null)
            {
                return null;
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                node.Left=this.Delete(node.Left, element);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right=this.Delete(node.Right, element);
            }
            else
            {
               
                if (node.Right == null && node.Left==null)
                {

                    return null;
                }
                else if(node.Right == null)
                {
                    return node.Left;
                }
                else if (node.Left == null)
                {
                    return node.Right;
                }
                else
                {
                    Node temp = this.FindSmallestChild(node.Right);
                    node.Value=temp.Value;
                    node.Right = this.Delete(node.Right, temp.Value);

                }
              

            }
            node=this.Balance(node);
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            return node;

        }

        private Node FindSmallestChild(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }
            return this.FindSmallestChild(node.Left);
        }

        public void DeleteMin()
        {
            if (this.Root == null)
            {
                return;
            }
            else
            {
                this.Root = this.DeleteMin(this.Root);
            }
           
            
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left==null)
            {

                return node.Right;

            }
            node.Left = this.DeleteMin(node.Left);
            node = this.Balance(node);
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            return node;
            
        }

        public void Insert(T element)
        {
            this.Root = this.Insert(this.Root, element);
        }
        public int Count()
        {
            return this.Count(this.Root);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1+this.Count(node.Left)+this.Count(node.Right);
        }


        public AVL<T> Search(T value)
        {
            var findNode=this.Contains(this.Root,value);
            if (findNode == null)
            {
                return new AVL<T>();
            }
            return new AVL<T>(findNode);
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }
            if(element.CompareTo(node.Value) < 0)
            {
                node.Left=this.Insert(node.Left, element);
            }
            else
            {
                node.Right=this.Insert(node.Right, element);
            }

            node = this.Balance(node);
            node.Height=Math.Max(Height(node.Left), Height(node.Right))+1;

            return node;

        }
       

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }
            node = Balance(node);
            node.Height=Math.Max(Height(node.Left),Height(node.Right))+1;
            node.Right = this.DeleteMax(node.Right);
            return node;

        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
        }
        public void EachPreOrder(Action<T> action)
        {
            this.EachPreOrder(this.Root, action);
        }
        public void EachPostOrder(Action<T> action)
        {
            this.EachPostOrder(this.Root, action);
        }

        private void EachPostOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            this.EachPostOrder(node.Left, action);
            this.EachPostOrder(node.Right, action);
            action.Invoke(node.Value);
        }

        private void EachPreOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            action.Invoke(node.Value);
            this.EachInOrder(node.Left, action);
            this.EachInOrder(node.Right, action);
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            EachInOrder(node.Left, action);
            action.Invoke(node.Value);
            EachInOrder(node.Right, action);
        }
        // Helper Methods
        private int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;
        }
        private Node RightRotation(Node node)
        {
            var temp = node.Left;
            node.Left=temp.Right;
            temp.Right = node;
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            return temp;
        }
        private Node LeftRotation(Node node)
        {
            var temp = node.Right;
            node.Right=temp.Left;
            temp.Left=node;

            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            return temp;
        }
        private Node Balance(Node node)
        {
            var balanceFactor=Height(node.Left)-Height(node.Right);
            if (balanceFactor > 1)
            {
                var childBalanceFactor = Height(node.Left.Left) - Height(node.Left.Right);
                if (childBalanceFactor < 0)
                {
                    node.Left=LeftRotation(node.Left);

                }

                node=RightRotation(node);

            }
            else if (balanceFactor < -1)
            {
                var childBalanceFactor= Height(node.Right.Left) - Height(node.Right.Right);
                if(childBalanceFactor > 0)
                {
                    node.Right=RightRotation(node.Right);
                }

                node=LeftRotation(node);

            }
            return node;
        }

    }
}
