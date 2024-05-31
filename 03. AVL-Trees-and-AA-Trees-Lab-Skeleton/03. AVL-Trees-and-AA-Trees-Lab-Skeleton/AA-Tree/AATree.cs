namespace AA_Tree
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
       
       
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Level = 1;
                
            }
            public Node Right { get; set; }
            public Node Left { get; set; }
            public T Value { get; set; }
            public int Level { get; set; }
        }
        private Node root;

        public bool Contains(T element)
        {
           var findNode=this.FindNode(this.root, element);  
            if(findNode == null)
            {
                return false;
            }
            return true;
        }

        private  Node FindNode(Node node, T element)
        {
            var currentNode = node;
            while (currentNode != null)
            {
                if (this.IsLesser(element, currentNode.Value))
                {
                    currentNode=currentNode.Left;
                }
                else if (this.IsLesser(currentNode.Value, element))
                {
                    currentNode=currentNode.Right;

                }
                else
                {
                   return currentNode;
                }
            }
            return null;
        }

        public int Count()
        {
           return this.Count(this.root);
        }

        private int Count(Node root)
        {
           if(root == null)
           {
                return 0;
           }
           return 1+Count(root.Left)+Count(root.Right);
        }

        public void InOrder(Action<T> action)
        {
            this.InOrder(this.root, action);
        }

        private void InOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            this.InOrder(node.Left, action);
            action.Invoke(node.Value);
            this.InOrder(node.Right, action);
        }

        public void Insert(T element)
        {
            this.root = this.Insert(this.root, element);
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }
            if (this.IsLesser(element, node.Value))
            {
                node.Left = Insert(node.Left,element);
            }
            else
            {
                node.Right= Insert(node.Right,element);
            }
            node = this.Skew(node);
            node = this.Split(node);
            return node;

        }

        private Node Split(Node node)
        {
            if (node.Right == null || node.Right.Right == null)
            {
                return node;

            }
            if (node.Right.Right.Level == node.Level)
            {
                node = RotateRight(node);
                node.Level++;
            }
            return node;


        }

        private Node RotateRight(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            return temp;
        }

        private Node Skew(Node node)
        {
            
            if (node.Left != null && node.Left.Level==node.Level)
            {
                node = RotateLeft(node);
            }
            return node;
        }

        private Node RotateLeft(Node node)
        {
            var temp=node.Left; 
            node.Left= temp.Right;
            temp.Right= node;
            return temp;
          
        }

        private bool IsLesser(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }

        public void PostOrder(Action<T> action)
        {
            this.PostOrder(this.root, action);
        }

        private void PostOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            this.PostOrder(node.Left, action);
            this.PostOrder(node.Right, action);
            action.Invoke(node.Value);
            
        }

        public void PreOrder(Action<T> action)
        {
            this.PreOrder(this.root, action);
        }
      
        private void PreOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            action.Invoke(node.Value);
            this.PreOrder(node.Left, action);
            this.PreOrder(node.Right,action);   
        }
        public void Delete(T key)
        {
            this.root=this.Delete(key,this.root);
        }
     

        private Node Delete(T element, Node node)
        {
            if (node == null)
            {
                return null;
            }
            if (this.IsLesser(element, node.Value))
            {
                node.Left=this.Delete(element, node.Left);
            }
            else if(this.IsLesser(node.Value, element))
            {
                node.Right=this.Delete(element, node.Right);
            }
            else
            {
                if(node.Right==null && node.Left == null)
                {
                    return null;
                }
                else  if (node.Right == null)
                {
                    return node.Left;
                }
                else if(node.Left == null)
                {
                    return node.Right;
                }
                else
                {
                    var temp = node;
                    node = this.FindMinNode(temp.Right);
                    node.Right = this.DeleteMinNode(temp.Right);
                    node.Left = temp.Left;
                }
               
            }
            node = FixUp(node);
          
            return node;

        }
        private Node FixUp(Node node)
        {
           if(node.Left!=null && node.Right != null)
           {
                if (node.Left.Level < node.Level - 1 || node.Right.Level < node.Level - 1)
                {
                   
                    if (node.Right.Level >  --node.Level)
                    {
                        node.Right.Level = node.Level;
                    }
                }
                node = Skew(node);
                node.Right = Skew(node.Right);
                node.Right.Right = Skew(node.Right.Right);
                node = Split(node);
                node.Right = Split(node.Right);
           }
          
           
            return node;
        }
       
        private Node DeleteMinNode(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            node.Left=DeleteMinNode(node.Left);
            return node;
          
        }

        private Node FindMinNode(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return FindMinNode(node.Left);
        }
        public string PrintRecursively(int indent)
        {
            StringBuilder sb=new StringBuilder();
            this.PrintRecursively(this.root, sb,indent);
            return sb.ToString().Trim();
        }

        private void PrintRecursively(Node node, StringBuilder sb, int indent)
        {
            sb.Append(new string(' ',indent))
                .AppendLine(node.Value.ToString());
            if(node.Left != null)
            {
                this.PrintRecursively(node.Left, sb, indent+2);
            }

            if(node.Right != null)
            {
                this.PrintRecursively(node.Right, sb, indent + 2);
            }
        }
    }
} 