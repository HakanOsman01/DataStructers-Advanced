namespace _01.Two_Three
{
    using System;
    using System.Text;

    public class TwoThreeTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public void Insert(T element)
        {
            this.root=this.Insert(this.root, element);
        }

        private TreeNode<T> Insert(TreeNode<T> node, T element)
        {
            if(node== null)
            {
                return new TreeNode<T>(element);
            }
            else if (node.IsLeaf())
            {
                return this.MergeNodes(node, new TreeNode<T>(element));
            }
            else if (IsLesser(node.LeftKey, element))
            {
                node=this.Insert(node.LeftChild, element);
                return node == node.LeftChild ? node 
                    : this.MergeNodes(node, new TreeNode<T>(element));

            }
            else if(node.IsTwoNode() || IsLesser(node.RightKey, element))
            {

                node = this.Insert(node.MiddleChild, element);
                return node == node.MiddleChild ? node
                    : this.MergeNodes(node, new TreeNode<T>(element));
            }
            else
            {
                node = this.Insert(node.RightChild, element);
                return node == node.RightChild ? node
                    : this.MergeNodes(node, new TreeNode<T>(element));
            }


        }
        private bool IsLesser(T element,T key) 
        {
            return element.CompareTo(key) < 0;

        }

        private TreeNode<T> MergeNodes(TreeNode<T> currentNode, TreeNode<T> node)
        {
            if (currentNode.IsTwoNode())
            {
                if (IsLesser(currentNode.LeftKey, node.LeftKey))
                {
                    currentNode.RightKey= currentNode.LeftKey;
                    currentNode.LeftKey=node.LeftKey;
                    currentNode.MiddleChild = node.LeftChild;

                }
                else
                {
                   
                    

                }
            }
            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            RecursivePrint(this.root, sb);
            return sb.ToString();
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
