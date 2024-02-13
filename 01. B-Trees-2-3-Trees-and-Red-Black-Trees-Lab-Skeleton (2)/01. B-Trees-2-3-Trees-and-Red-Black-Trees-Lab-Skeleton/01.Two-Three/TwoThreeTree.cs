namespace _01.Two_Three
{
    using System;
    using System.Text;

    public class TwoThreeTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public void Insert(T element)
        {
            this.root = this.Insert(this.root, element);
        }
        public bool Contains(T element)
        {
           return this.FindNode(this.root, element) != null;
           
        }

        private TreeNode<T> FindNode(TreeNode<T> node, T element)
        {
            var currentNode = node;
            while (currentNode != null)
            {
                if (currentNode.LeftKey != null && this.IsLesser(element, currentNode.LeftKey))
                {
                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.RightKey != null && currentNode.LeftKey != null &&
                    this.IsLesser(element, currentNode.RightKey)
                    && this.IsBigger(element, currentNode.LeftKey))
                {
                    currentNode = currentNode.MiddleChild;
                }
                else if (currentNode.RightKey != null && 
                    this.IsBigger(element,currentNode.RightKey))
                {
                    currentNode= currentNode.RightChild;

                }
                else
                {
                    break;
                }
            }
            return currentNode;
        }
        private bool IsBigger(T element, T key)
        {
            return element.CompareTo(key) > 0;
        }

        private TreeNode<T> Insert(TreeNode<T> node, T element)
        {
            if (node == null)
            {
                return new TreeNode<T>(element);
            }

            if (node.IsLeaf())
            {
                return this.MergeNodes(node, new TreeNode<T>(element));
            }

            if (IsLesser(element, node.LeftKey))
            {
                var newNode = this.Insert(node.LeftChild, element);

                return newNode == node.LeftChild ? node : this.MergeNodes(node, newNode);
            }
            else if (node.IsTwoNode() || IsLesser(element, node.RightKey))
            {
                var newNode = this.Insert(node.MiddleChild, element);

                return newNode == node.MiddleChild ? node 
                    : this.MergeNodes(node, newNode);
            }
            else
            {
                var newNode = this.Insert(node.RightChild, element);

                return newNode == node.RightChild ? node 
                    : this.MergeNodes(node, newNode);
            }
        }

        private bool IsLesser(T element, T key)
        {
            return element.CompareTo(key) < 0;
        }

        private TreeNode<T> MergeNodes(TreeNode<T> current, TreeNode<T> node)
        {
            if (current.IsTwoNode())
            {
                if (IsLesser(current.LeftKey, node.LeftKey))
                {
                    current.RightKey = node.LeftKey;
                    current.MiddleChild = node.LeftChild;
                    current.RightChild=node.MiddleChild;
                }
                else
                {
                    current.RightKey = current.LeftKey;
                    current.RightChild=current.MiddleChild;
                    current.MiddleChild = node.MiddleChild;
                    current.LeftChild = node.LeftChild;
                    current.LeftKey = node.LeftKey;
                }

                return current;
            }
            else if (IsLesser(node.LeftKey, current.LeftKey))
            {
                var newNode = new TreeNode<T>(current.LeftKey)
                {
                    LeftChild = node,
                    MiddleChild = current
                };

                current.LeftChild = current.MiddleChild;
                current.MiddleChild = current.RightChild;
                current.LeftKey = current.RightKey;
                current.RightKey = default;
                current.RightChild = null;

                return newNode;
            }
            else if (IsLesser(node.LeftKey, current.RightKey))
            {
                node.MiddleChild = new TreeNode<T>(current.RightKey)
                {
                    LeftChild = node.MiddleChild,
                    MiddleChild = current.RightChild
                };

                node.LeftChild = current;
                current.RightKey = default;
                current.RightChild = null;

                return node;
            }
            else
            {
                var newNode = new TreeNode<T>(current.RightKey)
                {
                    LeftChild = current,
                    MiddleChild = node
                };

                node.LeftChild = current.RightChild;
                current.RightKey = default;
                current.RightChild = null;

                return newNode;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            RecursivePrint(this.root, sb);
            return sb.ToString();
        }
        public string Dfs()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int indent = 0;
            this.Dfs(this.root, stringBuilder, indent);
            return stringBuilder.ToString();
        }

        private void Dfs(TreeNode<T> node, StringBuilder stringBuilder, int indent)
        {
            if (node == null)
            {
                return;
            }
            if (node.LeftKey != null)
            {
                stringBuilder.AppendLine(node.LeftKey.ToString())
                    .Append(new string(' ',indent+1));
            }
            if(node.RightKey != null)
            {
                stringBuilder.AppendLine(node.RightKey.ToString())
                    .Append(new string(' ', indent + 2));

            }
            
            
            if (node.IsTwoNode())
            {
                this.Dfs(node.LeftChild,stringBuilder,indent);
                this.Dfs(node.MiddleChild,stringBuilder,indent);
            }
            else if (node.IsThreeNode())
            {
                this.Dfs(node.LeftChild, stringBuilder, indent);
                this.Dfs(node.MiddleChild, stringBuilder, indent);
                this.Dfs(node.RightChild, stringBuilder, indent);

            }
         
        }

        private void RecursivePrint(TreeNode<T> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }

            if (node.LeftKey != null)
            {
                sb.Append(node.LeftKey).Append(" ");
            }

            if (node.RightKey != null)
            {
                sb.Append(node.RightKey).Append(Environment.NewLine);
            }
            else
            {
                sb.Append(Environment.NewLine);
            }

            if (node.IsTwoNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
            }
            else if (node.IsThreeNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
                RecursivePrint(node.RightChild, sb);
            }
        }
    }
}
