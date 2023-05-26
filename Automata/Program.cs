#define VISITOR

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Automata.Parsing.Math;
using Grammar.Assignment2;

namespace Automata;

public static class Program
{
	private const string Expression = "3 * (1 + 2)^2";

	public static void Main(string[] args)
	{
		ICharStream stream = CharStreams.fromString(Expression);
		ITokenSource lexer = new MathLexer(stream);
		ITokenStream tokens = new CommonTokenStream(lexer);
		var parser = new MathParser(tokens);
		IParseTree tree = parser.expression();

#if VISITOR
		MathVisitor visitor = new();
		var result = visitor.Visit(tree);
#else
		MathListener listener = new();
		ParseTreeWalker.Default.Walk(listener, tree);

		var result = listener.GetResult((MathParser.ExpressionContext) tree);
#endif

		Console.WriteLine($"{Expression} = {result}");
	}
}