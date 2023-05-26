using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Automata.Parsing.Math;
using Grammar.Assignment2;

namespace Automata;

public static class Program
{
	private const string Expression = "3 * (1 + 3)";

	public static void Main(string[] args)
	{
		ICharStream stream = CharStreams.fromString(Expression);
		ITokenSource lexer = new MathLexer(stream);
		ITokenStream tokens = new CommonTokenStream(lexer);
		var parser = new MathParser(tokens);

		IParseTree tree = parser.expression();
		MathListener listener = new();
		ParseTreeWalker.Default.Walk(listener, tree);
		
		var result = listener.GetResult((MathParser.ExpressionContext) tree);
		Console.WriteLine($"{Expression} = {result}");
	}
}