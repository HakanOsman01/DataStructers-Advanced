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
        private RedBlackTree(Node node)
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
           var node=this.FindElement(this.root, element);
            if (node == null)
            {
                return new RedBlackTree<T>();
            }
            return new RedBlackTree<T>(node);
        }

        private Node FindElement(Node node, T element)
        {
            var currentNode = node;
            while (currentNode != null)
            {
                if (this.IsEqual(element, currentNode.Value))
                {
                    return currentNode;
                }
                if (this.IsLesser(element, currentNode.Value))
                {
                    currentNode=currentNode.Left;
                }
                else if (this.IsLesser(currentNode.Value, element))
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    break;
                }

            }
            return null;
            
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
            if(this.IsRed(node.Right))
            {
                node = this.RotateLeft(node);
            
            }
            if(this.IsRed(node.Left) && this.IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
            }
            if(this.IsRed(node.Left) && this.IsRed(node.Right))
            {
                this.ColorsFlip(node);
            }

         

            return node;



        }

        public void Delete(T element)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }
            this.root = this.Delete(this.root, element);
            if (this.root != null)
            {
                this.root.Color = Black;
            }
        }

        private Node Delete(Node node, T element)
        {
            if (this.IsLesser(element, node.Value))
            {
                if(!this.IsRed(node.Left) && !this.IsRed(node.Left.Left))
                {
                    node=MoveRedLeft(node);
                }
                node.Left=this.Delete(node.Left,element);
            }
            else
            {
                if (this.IsRed(node.Left))
                {
                    node=this.RotateRight(node);
                }
                if (this.IsEqual(element,node.Value) && node.Right==null)
                {

                    return null;
                }
                if (!this.IsRed(node.Right) && !this.IsRed(node.Right.Left))
                {
                    node = this.MoveRedRight(node);
                }
                if (this.IsEqual(element,node.Value))
                {
                    node.Value = this.FindMinimumValueInSubtree(node.Right);
                    node.Right=this.DeleteMin(node.Right);

                }
                else
                {
                    node.Right=this.Delete(node.Right,element);
                }


            }
            return this.FixUp(node);
        }

        private T FindMinimumValueInSubtree(Node node)
        {
            if (node.Left == null)
            {
                return node.Value;
            }
            return FindMinimumValueInSubtree(node.Left);
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }
            this.root = this.DeleteMin(this.root);
            if (this.root != null)
            {
                this.root.Color = Black;
            }
           
        }

        private Node DeleteMin(Node node)
        {
         
            if (node.Left == null)
            {
                return null;
            }
            if(!this.IsRed(node.Left) && !this.IsRed(node.Left.Left))
            {
                node = this.MoveRedLeft(node);
            }
            node.Left=this.DeleteMin(node.Left);
            return this.FixUp(node);
        }
        public void DeleteMax()
        {

            if (this.root == null)
            {
                throw new InvalidOperationException();
            }
            this.root = this.DeleteMax(this.root);
            if (this.root != null)
            {
                this.root.Color = Black;
            }
        }

        private Node DeleteMax(Node node)
        {
            if (this.IsRed(node.Left))
            {
                node = this.RotateRight(node);
            }

            if (node.Right == null)
            {
                return null; 
            }
            if(!this.IsRed(node.Right) && !this.IsRed(node.Right.Left))
            {
                node = this.MoveRedRight(node);
            }

            node.Right=this.DeleteMax(node.Right);
            return node;
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


        //HelperMethod
        private Node FixUp(Node node)
        {
            if (this.IsRed(node.Right))
            {
                node = this.RotateLeft(node);

            }
            if (this.IsRed(node.Left) && this.IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
            }
            if (this.IsRed(node.Left) && this.IsRed(node.Right))
            {
                this.ColorsFlip(node);
            }
            return node;
        }
        private Node MoveRedLeft(Node node)
        {
            this.ColorsFlip(node);
            if (this.IsRed(node.Right.Left))
            {
                node.Right = this.RotateRight(node.Right);
                node = this.RotateLeft(node);
                this.ColorsFlip(node);
            }
            return node;
        }
        private Node MoveRedRight(Node node)
        {
            this.ColorsFlip(node);
            if (this.IsRed(node.Left.Left))
            {
                node= this.RotateRight(node.Left);
                this.ColorsFlip(node);
            }
            return node;
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
            node.Right = temp.Left;
            temp.Left = node;
            temp.Color = temp.Left.Color;
            temp.Left.Color = Red;
            return temp;
        }
        private Node RotateRight(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Color=temp.Right.Color;
            temp.Right.Color=Red;
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