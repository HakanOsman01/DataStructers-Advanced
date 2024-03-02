namespace AA_Tree
{
    using System;

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
        public void Delete(T key)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }
            this.root = Delete(this.root, key);
        }

        private Node Delete(Node node, T key)
        {
            if (node == null)
            {
                return null;
            }
            if (key.CompareTo(node.Value) < 0)
            {
                node.Left=this.Delete(node.Left,key);
            }
            else if (key.CompareTo(node.Value) > 0)
            {
                node.Right = this.Delete(node.Right, key);
            }
            else
            {
                if (this.IsLeaf(node))
                {
                    return null;
                }
                if (node.Right == null)
                {
                    return node.Left;
                }
                if (node.Left == null)
                {
                    return node.Right;
                }
                var temp = node;
                node = this.ReplaceWithSuccessors(temp.Right);
                node.Right = this.DeleteMinNode(temp.Right);
                node.Left=temp.Left;

            }
            return this.FixUp(node);

            
        
        }

        private Node FixUp(Node node)
        {
            if (this.IsOneLevelDown(node))
            {
                node.Level--;
                if (node.Right != null && node.Right.Level>node.Level-1)
                {
                   node.Right.Level--;
                }
            }
            node=this.Skew(node);
            node.Right = this.Skew(node.Right);
            node.Right.Right=this.Skew(node.Right.Right);

            node = this.Split(node);
            node.Right = this.Split(node.Right);

            return node;
        }

        private bool IsOneLevelDown(Node node)
        {
            if (node.Left!=null && node.Left.Level < node.Level-1)
            {
                return true;
            }
            if(node.Right != null && node.Right.Level  < node.Level-1)
            {
                return true;
            }
            return false;

        }
        private Node ReplaceWithSuccessors(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }
            return this.ReplaceWithSuccessors(node.Left);
        }

        private bool IsLeaf(Node node)
        {
            if(node.Left== null && node.Right == null)
            {
                return true;
            }
            return false;
        }
        private Node DeleteMinNode(Node node)
        {
            if (node.Left == null)
            {
                return null;
            }
            node.Left = this.DeleteMinNode(node.Left);
            return node;
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
    }
} 