using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCodeDataEventDriven
{
    internal class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; }
        //父节点
        public TreeNode<T> Parent { get; set; }
        public TreeNode(T value)
        {
            Value = value;
            Children = new List<TreeNode<T>>();
        }
        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }
    }
}
