#define VISITOR

using System.Runtime.CompilerServices;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Automata.Parsing.Math;
using Grammar.Assignment2;

namespace Automata;

public static class Program
{
	/*
	private const string Expression = "3 * (1 + 2)^2";
	*/
	private static readonly string[] expressions =
	{
		"3 + 4",
		"3 - 5",
		"10 * 30",
		"45 / 3",
		"5!",
		"2^5",
		"2 + 2 + 5 * 3 + 4",
		"3 * (1 + 2)^2",
		"(3*(1+1)/2)+1",
		"((3*(1+1)/2)+1)!",
		"((3*(1+1)/2)+1)^2",
		"((3*(1+1)/2)+1)^(3!)"
	};

	private static void Assignment2(string expression)
	{
		ICharStream stream = CharStreams.fromString(expression);
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

		Console.WriteLine($"{expression} = {result}");
	}

	public static void Main(string[] args)
	{
		foreach (string expression in expressions)
		{
			Program.Assignment2(expression);
		}
	}
}