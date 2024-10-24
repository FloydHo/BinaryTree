using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BinaryTree
{
    public class BTree<T> : IBinaryTree<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T>? _root;

        public BTree(params T[] arr)
        {
            foreach(var  t in arr)
            {
                Insert(t);
            }
        }

        public void Insert(T value)
        {
            if (_root == null)
            {
                _root = new BinaryTreeNode<T>(value);
            }
            else 
            {
                Insert(value, _root);
            }
        }

        private void Insert(T value,BinaryTreeNode<T> current)
        {
            int compare = value.CompareTo(current.Data);
            switch (compare) {
                case -1:
                    if (current.Left == null) current.Left = new BinaryTreeNode<T>(value);
                    else Insert(value, current.Left);
                    break;
                case 0: 
                case 1:
                    if (current.Right == null) current.Right = new BinaryTreeNode<T>(value);
                    else Insert(value, current.Right);
                    break;
            }
        } 

        public void Clear()
        {
            _root = null!;
        }

        public bool Contains(T value)
        {
            if (_root!.Data.Equals(value)) return true;
            else return Contains(value, _root);
        }

        private bool Contains(T value, BinaryTreeNode<T> current)
        {
            int compare = value.CompareTo(current.Data);
            switch (compare)
            {
                case -1:
                    if (current.Left == null) return false;
                    else return Contains(value, current.Left);
                case 0:
                    return true;
                case 1:
                    if (current.Right == null) return false;
                    else return Contains(value, current.Right);
            }
            return false;
        }

        public void Delete(T value)
        {
            BinaryTreeNode<T> deleteThis = default!;

            if (_root != null && _root.Data.Equals(value)) deleteThis = _root;
            else deleteThis = PrepareDelete(value, _root!);

            if (deleteThis == null) return;
            
            if (deleteThis.Left != null && deleteThis.Right == null)       //Wenn links belegt und rechts frei ist kann der Folgeknoten einfach ersetzt werde, das selbe mit rechts.
            {
                Replace(deleteThis, deleteThis.Left);
            }
            else if (deleteThis.Left == null && deleteThis.Right != null)
            {
                Replace(deleteThis, deleteThis.Right);
            }
            else                        
            {
                SearchReplacementNode(deleteThis, deleteThis.Left!);            
            }
        }

        private BinaryTreeNode<T> PrepareDelete(T value, BinaryTreeNode<T> node)
        {
            int compare = value.CompareTo(node.Data);
            switch (compare)
            {
                case -1:
                    if (node.Left == null) return null!;
                    else if (node.Left.Data.Equals(value))
                    {
                        if (TryUnlink(node.Left))
                        {
                            node.Left = null!;
                            return null!;
                        }
                        return node.Left;
                    }
                    else return PrepareDelete(value, node.Left);
                case 1:
                    if (node.Right == null) return null!;
                    else if (node.Right.Data.Equals(value))
                    {
                        if (TryUnlink(node.Right))
                        {
                            node.Right = null!;
                            return null!;
                        }
                        return node.Right;
                    }
                    else return PrepareDelete(value, node.Right);
                default: return null!;
            }
        }

        //private BinaryTreeNode<T> CheckNode(T value, BinaryTreeNode<T> node)
        //{
        //    if (node.Right == null) return null!;
        //    else if (node.Right.Data.Equals(value))
        //    {
        //        if (TryUnlink(node.Right))
        //        {
        //            node.Left = null!;
        //            return null!;
        //        }
        //        return node.Right;
        //    }
        //    else return PrepareDelete(value, node.Right);
        //}

        private bool TryUnlink(BinaryTreeNode<T> node)
        {
            if (node.Left == null && node.Right == null)
            {
                return true;
            }
            return false;
        }

        private void Replace(BinaryTreeNode<T> oldNode, BinaryTreeNode<T> replacementNode)
        {
            oldNode.Data = replacementNode.Data;
            oldNode.Left = replacementNode.Left;
            oldNode.Right = replacementNode.Right;
        }

        private BinaryTreeNode<T> SearchReplacementNode(BinaryTreeNode<T> deleteNode, BinaryTreeNode<T> checkNode)
        {
            BinaryTreeNode<T> replacementNode;
            if (checkNode.Right == null && checkNode.Left == null)
            {  
               return checkNode;
            }
            else if (checkNode.Right == null && checkNode.Left != null)
            {
                return checkNode;
            }
            else 
            {
                replacementNode = SearchReplacementNode(deleteNode, checkNode.Right!);
            }

            if (replacementNode != null)
            {
                deleteNode.Data = replacementNode.Data;
                checkNode.Right = replacementNode.Left;
                
            }
            return null!;
        }

        public void PrintInorder()
        {
            PrintInorder(_root!);
        }

        private void PrintInorder(BinaryTreeNode<T> node)
        {
            if (node == null) return;
            else
            {
                PrintInorder(node.Left!);
                Console.Write($"{node.Data} ");
                PrintInorder(node.Right!);
            }
        }

        public BinaryTreeNode<T> Search(T value)   //Gibt die Referenz zum aktuellen Objekt anstatt eine Kopie zurück, nicht das beste ich weiß :D
        {
            if (_root!.Data.Equals(value))
            {
                return _root;
            }
            return Search(value, _root);
        }

        private BinaryTreeNode<T> Search(T value, BinaryTreeNode<T> node)
        {
            int compare = value.CompareTo(node.Data);
            switch (compare)
            {
                case -1:
                    if (node.Left == null) return null!;
                    else return Search(value, node.Left);
                case 0:
                    return node;
                case 1:
                    if (node.Right == null) return null!;
                    else return Search(value, node.Right);
                default: return null!;
            }
        }
    }
}
