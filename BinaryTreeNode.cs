using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BinaryTreeNode<T> where T : IComparable<T>
{
    public T Data { get; set; }
    public BinaryTreeNode(T value)
    {
        Data = value;
    }
    public BinaryTreeNode<T> Left { get; set; }
    public BinaryTreeNode<T> Right { get; set; }


}
