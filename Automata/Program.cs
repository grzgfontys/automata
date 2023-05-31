using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Automata.Parsing.Assignment3;
using Automata.Parsing.Math;
using Grammar.Assignment2;

namespace Automata;

public static class Program
{
	private static readonly string[] Expressions =
	{
		"3 + 4", "3 - 5", "10 * 30", "45 / 3", "5!", "2^5", "2 + 2 + 5 * 3 + 4", "3 * (1 + 2)^2", "(3*(1+1)/2)+1",
		"((3*(1+1)/2)+1)!", "((3*(1+1)/2)+1)^2", "((3*(1+1)/2)+1)^(3!)"
	};

	private const string Input = """
		aa = 1
		bb = 6 - 2 * 2

		3 + 2

		cc = aa + bb
		dd = bb + 2
		ddd = dd

		3 + 2
		3 * 3 - 2 ^ (2!)

		print(3 * 3 - 2 ^ (2!))
		print(aa)
		print(bb)
		print(cc)

		2 + 2 * 2

		print(dd)
		print(ddd)
		print(ddd!)
		print(aa + bb)
		print(aa * 10)
		print(aa)

		2 * 2 + 2

		print(bb)
		bb = bb^4
		print(bb)
		""";

	private static void Assignment2(string expression)
	{
		bool useVisitor = false;

		ICharStream stream = CharStreams.fromString(expression);
		ITokenSource lexer = new MathLexer(stream);
		ITokenStream tokens = new CommonTokenStream(lexer);
		var parser = new MathParser(tokens);
		IParseTree tree = parser.expression();

		int result;
		if ( useVisitor )
		{
			MathVisitor visitor = new();
			result = visitor.Visit(tree);
		}
		else
		{
			MathListener listener = new();
			ParseTreeWalker.Default.Walk(listener, tree);
			result = listener.GetResult((MathParser.ExpressionContext) tree);
		}

		Console.WriteLine($"{expression} = {result}");
	}

	private static void Assignment3(string input)
	{
		AntlrInputStream stream = new( input );
		Assignment3Lexer lexer = new( stream );
		CommonTokenStream tokens = new( lexer );
		Assignment3Parser parser = new( tokens );
		IParseTree tree = parser.file();

		ParseTreeWalker walker = new();
		Listener listener = new();
		walker.Walk(listener, tree);
	}

	public static void Main(string[] args)
	{
		Console.WriteLine("---------input---------");
		Console.WriteLine(Input);
		Console.WriteLine("-----------------------");

		Console.WriteLine("##########_ANTLR output_##########");
		Assignment3(Input);
		Console.WriteLine("##################################");
	}
}