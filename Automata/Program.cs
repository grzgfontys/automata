using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Automata.Parsing.Human;
using Automata.Parsing.Math;
using Grammar.Assignment1;
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
		MathVisitor visitor = new();

		var result = visitor.Visit(tree);
		Console.WriteLine($"{Expression} = {result}");
	}
}