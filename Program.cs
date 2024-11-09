namespace BinaryTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AufgabePruefen();
            //TestString();
        }

        public static void AufgabePruefen()
        {
            BTree<int> theTree = new BTree<int>(50, 100, 25, 1, 10, 75, 65, 85, 61, 45, 35, 15, 10);
            //BTree<int> theTree = new BTree<int>(50);

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
            Console.Write("\n\n15 gelöscht:\n");
            theTree.PrintInorder();

            theTree.Delete(61);
            Console.Write("\n\n61 gelöscht:\n");

            theTree.PrintInorder();
            Console.WriteLine();

            theTree.Delete(50);
            Console.Write("\n\n50 gelöscht:\n");
            theTree.PrintInorder();
            Console.WriteLine();

            theTree.Invert();
            Console.Write("\n\nInvert:\n");
            theTree.PrintInorder();
            Console.WriteLine();
        }

        public static void TestString()
        {
            BTree<string> theTree2 = new BTree<string>("Hallo", "Welt", "Wie", "geht", "es", "dir");

            Console.Write("\nAlle Elemente: \n");
            theTree2.PrintInorder();
            Console.Write("\n\n'Hallo' gelöscht: \n");
            theTree2.Delete("Hallo");
            theTree2.PrintInorder();
        }
    }
}
