using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Automata.Parsing.Assignment3;
using Automata.Parsing.Calculator;
using Automata.Parsing.Math;
using Grammar.Assignment2;
using Grammar.Assignment3;
using Grammar.Assignment4;

namespace Automata;

public static class Program
{
	private static readonly string[] Expressions =
	{
		"3 + 4", "3 - 5", "10 * 30", "45 / 3", "5!", "2^5", "2 + 2 + 5 * 3 + 4", "3 * (1 + 2)^2", "(3*(1+1)/2)+1",
		"((3*(1+1)/2)+1)!", "((3*(1+1)/2)+1)^2", "((3*(1+1)/2)+1)^(3!)"
	};
	
	private static void Assignment2()
	{
		bool useVisitor = false;

		foreach ( string expression in Expressions )
		{
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
	
	private static void Assignment4(string input)
	{
		AntlrInputStream stream = new( input );
		ITokenSource lexer = new CalculatorLexer( stream );
		CommonTokenStream tokens = new( lexer );
		CalculatorParser parser = new( tokens );
		IParseTree tree = parser.file();
		
		var calculator = new  Calculator();
		calculator.Visit(tree);
	}

	public static void Main(string[] args)
	{
		string input = File.ReadAllText(@"input/assignment4.txt");
		Console.WriteLine("---------input---------");
		Console.WriteLine(input);
		Console.WriteLine("-----------------------");

		Console.WriteLine("##########_ANTLR output_##########");
		Assignment4(input);
		Console.WriteLine("##################################");
	}
}