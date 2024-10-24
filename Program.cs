namespace BinaryTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BTree<int> theTree = new BTree<int>();

            int[] testArr = { 50, 100, 25, 1, 10, 75, 65, 85, 61, 45, 35, 15, 10 };

            foreach (int i in testArr)
            {
                theTree.Insert(i);
            }

            Console.WriteLine(theTree.Contains(50));
            Console.WriteLine(theTree.Contains(15));
            Console.WriteLine(theTree.Contains(62));

            theTree.PrintInorder();

            theTree.Delete(15);
            Console.WriteLine();

            theTree.PrintInorder();

        }
    }
}
