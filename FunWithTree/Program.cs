using System;
using System.Collections.Generic;
            
namespace FunWithTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Parsing a simple Context Free Gramma with Tree!");

            Tree<NodeTree<NodeData>, SimpleCFC> tree = new Tree<NodeTree<NodeData>, SimpleCFC>(new SimpleCFC());

            /*
            Console.WriteLine(tree.Root);

            List<NodeTree<NodeData>> siblings = tree.GenerateSiblings(tree.Root);
            Console.WriteLine("sublinks of {1} (#{0}):", siblings.Count, tree.Root);
            foreach(NodeTree<NodeData> s in siblings)
            {
                Console.WriteLine(s);
            }

            int i = 2;
            List<NodeTree<NodeData>> sub_siblings = tree.GenerateSiblings(siblings[i]);
            Console.WriteLine("sub-siblinks of {1} ({0}):", siblings.Count, siblings[i]);
            foreach (NodeTree<NodeData> s in sub_siblings)
            {
                Console.WriteLine(s);
            }
            */

            Console.WriteLine("Start Parsing.");
            tree.ParseUpToDepth(3);

            Console.WriteLine("** That's all folks! **");
        }
    }
}
