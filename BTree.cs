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
            if (_root == null) _root = new BinaryTreeNode<T>(value);
            else Insert(value, _root);
        }

        private void Insert(T value,BinaryTreeNode<T> current)
        {
            int compare = value.CompareTo(current.Data);                                    //Überprüft ob größer(1) kleiner(-1) oder gleich(0)
            switch (compare) 
            {
                case -1:                                                                    // Wenn kleiner, überprüfe ob Links frei ist 
                    if (current.Left == null) current.Left = new BinaryTreeNode<T>(value);  // ja: node wird initalisiert und eine Referenz auf Links gelegt
                    else Insert(value, current.Left);                                       // nein: überprüfe das referenzierte Linke Objekt indem die Methode sich selbst aufruft.
                    break;
                case 0:         //Wenn der Wert gleich groß ist, default rechts
                case 1:
                    if (current.Right == null) current.Right = new BinaryTreeNode<T>(value);
                    else Insert(value, current.Right);
                    break;
            }
        } 

        public void Clear() => _root = null!;
      
        public bool Contains(T value) //Überprüft on Search den Wert finden kann und gibt entsprechend einen bool zurück.
        {
            if (_root!.Data.Equals(value)) return true;
            else if (Search(value) != null) return true;
            return false;
        }

        public void Delete(T value)
        {
            BinaryTreeNode<T> deleteThis = default!;

            if (_root != null && value.Equals(_root.Data)) deleteThis = _root;  //Checkt ob _root null ist oder der zu löschende Wert in _root ist.
            else deleteThis = PrepareDelete(value, _root!);                     //PrepareDelete sucht sich die Node, die gelöscht werden soll (und löscht diese wenn Left & Right null sind) ansonsten gibt sie die node zurück. 

            if (deleteThis == default) return;

            if (deleteThis.Left == null && deleteThis.Right == null)            //Kann nur bei Root treffen da jede andere Möglichkeit in der Methode PrepareDelete abgefangen wird.
            {
                _root = null!;
            }
            else if (deleteThis.Left != null && deleteThis.Right == null)       //Wenn links belegt und rechts frei ist kann der Folgeknoten einfach ersetzt werde, das selbe mit rechts.
            {
                Replace(deleteThis, deleteThis.Left);
            }
            else if (deleteThis.Left == null && deleteThis.Right != null)
            {
                Replace(deleteThis, deleteThis.Right);
            }
            else                        
            {
                SearchReplacementNode(deleteThis);
            }
        }

        private BinaryTreeNode<T> PrepareDelete(T value, BinaryTreeNode<T> node)
        {
            int compare = value.CompareTo(node.Data);
            switch (compare)
            {
                case -1:
                    return CheckNode(value, node, false);
                case 1:
                    return CheckNode(value, node, true);
                default: return null!;
            }
        }

        private BinaryTreeNode<T> CheckNode(T value, BinaryTreeNode<T> node, bool isRight)
        {
            if ((isRight ? node.Right : node.Left) == null) return null!;
            else if ((isRight ? node.Right : node.Left)!.Data.Equals(value))
            {
                if (TryUnlink((isRight ? node.Right : node.Left)!))
                {
                    if (isRight) node.Right = null!;
                    else node.Left = null!;
                    return null!;
                }
                return (isRight ? node.Right : node.Left)!;
            }
            else return PrepareDelete(value, (isRight ? node.Right : node.Left)!);
        }

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

        private BinaryTreeNode<T> SearchReplacementNode(BinaryTreeNode<T> deleteNode) //Falls bei der ersten Iteration trifft muss die Linke Noder des zu löschenden Wertes getauscht werden
        {
            BinaryTreeNode<T> replacementNode = SearchReplacementNode(deleteNode, deleteNode.Left);
            if (replacementNode != null)
            {
                deleteNode.Data = replacementNode.Data;
                deleteNode.Left = replacementNode.Left;
            }
            return null;
        }

        private BinaryTreeNode<T> SearchReplacementNode(BinaryTreeNode<T> deleteNode, BinaryTreeNode<T> checkNode)
        {
            BinaryTreeNode<T> replacementNode;
            if (checkNode.Right == null && checkNode.Left == null || checkNode.Right == null && checkNode.Left != null)
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
            if (_root == null) return null;
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

        public void Invert()
        {
            if(_root != null) Invert(_root!);
        }

        private void Invert(BinaryTreeNode<T> node)
        {
            if (node == null) return;
            {
                Invert(node.Left);
                Invert(node.Right);
                BinaryTreeNode<T> tmp = node.Left;
                node.Left = node.Right;
                node.Right = tmp;
            }
        }
    }
}
