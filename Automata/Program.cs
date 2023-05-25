using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Automata.Parsing.Human;
using Grammar.Assignment1;

namespace Automata;

public static class Program
{
	private const string FilePath = "test.txt";

	public static void Main(string[] args)
	{
		using FileStream file = File.OpenRead(FilePath);
		ICharStream stream = CharStreams.fromStream(file);
		ITokenSource lexer = new Assignment1Lexer(stream);
		ITokenStream tokens = new CommonTokenStream(lexer);
		var parser = new Assignment1Parser(tokens);

		IParseTree tree = parser.file();
		FileVisitor visitor = new();

		var result = visitor.Visit(tree) ?? Enumerable.Empty<Human>();
		foreach ( Human human in result )
		{
			Console.WriteLine(human);
		}
	}
}