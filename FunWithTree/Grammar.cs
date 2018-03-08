using System;
namespace FunWithTree
{
    /// <summary>
    /// A general Context free grammar.
    /// </summary>
    public interface IContextFreeGrammar : IIdentified
    {
        string[] StartSymbols { get; }
        string[] Rules { get; }
    }

    /// <summary>
    /// Abstract General Context Free Grammar.
    /// </summary>
    public abstract class GeneralCFG : IContextFreeGrammar
    {
        protected GeneralCFG() { }
        public abstract string[] StartSymbols { get; }
        public abstract string[] Rules { get; }
        public abstract Guid ID { get; }
    }

    /// <summary>
    /// A simple CFC grammar generating correct parenthesis string.
    /// </summary>
    public class SimpleCFC : GeneralCFG
    {
        private readonly string[] start_symbol = { "S" };
        private readonly string[] rules = { "SS", "(S)", "()" };

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public override Guid ID { get; } = Guid.NewGuid();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FunWithTree.SimpleCFC"/> class.
        /// </summary>
        public SimpleCFC() { }

        /// <summary>
        /// Gets the grammar's start symbols.
        /// </summary>
        /// <value>The start symbols.</value>
        public override string[] StartSymbols
        {
            get { return this.start_symbol; }
        }
        /// <summary>
        /// Gets the grammar's rules.
        /// </summary>
        /// <value>The rules.</value>
        public override string[] Rules
        {
            get { return this.rules; }
        }
    }
}
