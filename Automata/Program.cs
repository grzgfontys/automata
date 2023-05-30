#define VISITOR

using System.Reflection;
using System.Runtime.CompilerServices;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Automata.Parsing.Assignment3;
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

	private static void Assignment3(string input)
	{
		AntlrInputStream stream = new AntlrInputStream(input);
		Assignment3Lexer lexer = new Assignment3Lexer(stream);
		CommonTokenStream tokens = new CommonTokenStream(lexer);
		Assignment3Parser parser = new Assignment3Parser(tokens);
		IParseTree tree = parser.file();
		// Console.WriteLine(tree.ToStringTree(parser));

		ParseTreeWalker walker = new ParseTreeWalker();
		Listener listener = new();
		walker.Walk(listener, tree);
		
		// var result = listener.GetResult((Assignment3Parser.ExpressionContext) tree);

		// listener.PrintVariables();
	}

	public static void Main(string[] args)
	{
		// foreach (string expression in expressions)
		// {
		// 	Program.Assignment2(expression);
		// }

		string input =
// @"3 + 2
// 3 * 3 - 2 ^ (2!)
// 2 + 2 * 2
// 2 * 2 + 2
// asd = 3 * 3 - 2 ^ (2!)
// dd = 0
// bb = 20
// cc = 30
// cc = 99
// dd = 3 + dd
// dd!
// 3!
// print(1)
// print(dd)
// print(3 * 3 - 2 ^ (2!))
// dd";
@"
aa = 1
bb = 6 - 2 * 2
cc = aa + bb
dd = bb + 2
ddd = dd

print(3 * 3 - 2 ^ (2!))
print(aa)
print(bb)
print(cc)
print(dd)
print(ddd)
print(aa + bb)
print(aa * 10)
print(aa)
print(bb)";
		
		Console.WriteLine(input);
		Console.WriteLine("--------------");
		
		Assignment3(input);
		
	}
}