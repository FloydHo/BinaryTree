namespace BinaryTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BTree<int> theTree = new BTree<int>(50, 100, 25, 1, 10, 75, 65, 85, 61, 45, 35, 15, 10);

            Console.Write("Existiert 50? ");
            Console.WriteLine(theTree.Contains(50));
            Console.Write("Existiert 62? ");
            Console.WriteLine(theTree.Contains(62));

            Console.Write("\nAlle Elemente: \n");
            theTree.PrintInorder();

            theTree.Delete(25);
            Console.Write("\n\n25 gelöscht: \n");
            theTree.PrintInorder();
  
            theTree.Delete(15);
            Console.Write("\n\n15 gelöscht:");
            Console.WriteLine();

            theTree.PrintInorder();
        }
    }
}
