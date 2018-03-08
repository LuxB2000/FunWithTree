using System;
using System.Collections.Generic;
using System.Text;

namespace FunWithTree
{
    public interface IIdentified{
        Guid ID { get; }
    }

    public interface INodeData {
        int Depth { get; set; }
        string Text { get; set; }
    }
    /*
     * A simple node's data
     */
    public class NodeData : INodeData{
        private string m_text = "";
        private int m_depth = -1;

        /// <summary>
        /// Gets or sets the depth of the node in the tree.
        /// </summary>
        /// <value>The depth.</value>
        public int Depth
        {
            get { return m_depth; }
            set { m_depth = value; }
        }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return this.m_text; }
            set { m_text = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FunWithTree.NodeData"/> class.
        /// </summary>
        public NodeData(){}
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FunWithTree.NodeData"/> class.
        /// </summary>
        /// <param name="text">Text.</param>
        public NodeData( string text ) : this()
        {
            this.Text = text;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FunWithTree.NodeData"/> class.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="depth">Depth.</param>
        public NodeData(string text, int depth) : this(text)
        {
            this.m_depth = depth;
        }
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:FunWithTree.NodeData"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:FunWithTree.NodeData"/>.</returns>
        public override string ToString()
        {
            return string.Format("d:" + this.Depth +  "-t:" + this.Text);
        }
    }
    /// <summary>
    /// Node interface.
    /// </summary>
    public interface INode<NodeDataType> : IIdentified{
        NodeDataType Data { get; set; }
        //List<INode<NodeDataType>> Siblings { get; set; }
    }
    /*
     * A general class for Node
     */
    public class NodeTree<NodeDataType> : INode<NodeDataType> where NodeDataType : INodeData, new()
    {
        /// <summary>
        /// The private data.
        /// </summary>
        private NodeDataType m_data = new NodeDataType();
        /// <summary>
        /// The private siblings.
        /// </summary>
        private List<NodeTree<NodeDataType>> m_siblings = new List<NodeTree<NodeDataType>> { };


        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid ID { get; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public NodeDataType Data
        {
            get { return this.m_data; }
            set { this.m_data = value; }
        }
        /// <summary>
        /// Gets or sets the siblings of the current Node as a List.
        /// </summary>
        /// <value>The siblings.</value>
        public List<NodeTree<NodeDataType>> Siblings
        {
            get { return this.m_siblings; }
            set { this.m_siblings = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FunWithTree.NodeTree`1"/> class.
        /// </summary>
        public NodeTree()
        {
            this.ID = Guid.NewGuid();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FunWithTree.NodeTree`1"/> class.
        /// </summary>
        /// <param name="data">NodeDataType</param>
        public NodeTree( NodeDataType data ) : this(){
            this.Data = data;
        }
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:FunWithTree.NodeTree`1"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:FunWithTree.NodeTree`1"/>.</returns>
        public override string ToString()
        {
            return string.Format("{0}, {1} siblings", this.Data, this.Siblings.Count);
        }
    }

    /*
     * General class for a tree
     */
    /// <summary>
    /// Tree Interface
    /// </summary>
    public interface ITree<NodeType> : IIdentified {
        
    }
    public class Tree<NodeType, GrammarType> : ITree<NodeType> 
        where NodeType : NodeTree<NodeData>, new()
        where GrammarType: GeneralCFG
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid ID { get; }
        /// <summary>
        /// The private root.
        /// </summary>
        private NodeType m_root = new NodeType();
        /// <summary>
        /// The current node in the tree, use it for parsing.
        /// </summary>
        private NodeType m_current;
        private List<NodeType> m_to_visit = new List<NodeType> { };
        private GrammarType m_grammar;

        /// <summary>
        /// Gets or sets the root of the Tree.
        /// </summary>
        /// <value>The root.</value>
        public NodeType Root { 
            get { return m_root; }
            set
            {
                this.m_root = value;
            }
        }
        /// <summary>
        /// Gets or sets the current node in the tree, use it for parsing.
        /// </summary>
        /// <value>The current.</value>
        public NodeType Current
        {
            get { return m_current; }
            set { m_current = value; }
        }
        /// <summary>
        /// Gets the grammar.
        /// </summary>
        /// <value>The grammar.</value>
        public GrammarType Grammar
        {
            get { return m_grammar; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:FunWithTree.Tree`1"/> class.
        /// </summary>
        public Tree(GrammarType g)
        {
            this.ID = Guid.NewGuid();
            this.m_grammar = g;
            this.Root.Data.Depth = 0;
            this.Root.Data.Text = this.Grammar.StartSymbols[0]; // TODO: be more generic
            this.m_to_visit.Add(this.Root);
        }
        /// <summary>
        /// Parses and builds the tree up to a maximal depth.
        /// TODO: put in dedicated parser.
        /// </summary>
        /// <param name="max_depth">Max depth.</param>
        public void ParseUpToDepth(int max_depth){
            while (this.m_to_visit.Count > 0)
            {
                // pop the first element of the list
                this.Current = this.m_to_visit[0];
                this.m_to_visit.RemoveAt(0);
                if (this.Current.Data.Depth <= max_depth)
                {
                    System.Console.WriteLine(this.Current);
                    Console.WriteLine("\t{0} nodes to visit", this.m_to_visit.Count);

                    // build the children of the current node
                    List<NodeType> sublings = this.GenerateSiblings(this.Current);
                    this.m_to_visit.AddRange(sublings);
                }
            }
        }

        /// <summary>
        /// Generates the sibling.
        /// TODO: put in dedicated parser
        /// </summary>
        /// <returns>The sibling.</returns>
        public List<NodeType> GenerateSiblings(NodeType n)
        {
            // based on the current NodeData, generate a list of NodeData 
            // following the Grammar rules:
            // S->SS
            // S->(S)
            // S->()
            int i = 0, p = 0, L = n.Data.Text.Length;
            string[] start_symbol = this.Grammar.StartSymbols;
            string[] rules = this.Grammar.Rules;
            List<NodeType> l = new List<NodeType> { };

            // no need to parse the current node if there is no start symbol
            if (n.Data.Text.IndexOf(start_symbol[i], StringComparison.CurrentCulture) != -1)
            {
                foreach (string rule in rules)
                {
                    p = n.Data.Text.IndexOf(start_symbol[i], 0, StringComparison.CurrentCulture);
                    while (p != -1)
                    {
                        StringBuilder sibling_text = new StringBuilder(n.Data.Text);
                        sibling_text.Remove(p, 1); // remove the start symbol
                        sibling_text.Insert(p, rule); // insert the rule

                        NodeType sibling = new NodeType();
                        sibling.Data.Text = sibling_text.ToString();
                        sibling.Data.Depth = n.Data.Depth + 1;
                        l.Insert(0, sibling);

                        p = n.Data.Text.IndexOf(start_symbol[i], p+1, StringComparison.CurrentCulture);
                    }

                }
            }


            return l;
        }



    }
}
