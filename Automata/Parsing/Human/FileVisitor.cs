using Grammar.Assignment1;

namespace Automata.Parsing.Human;

public class FileVisitor : Assignment1BaseVisitor<IEnumerable<Human>>
{
	private readonly HumanVisitor humanVisitor = new();

	public override IEnumerable<Human> VisitFile(Assignment1Parser.FileContext context) =>
		context.human().Select(humanVisitor.VisitHuman).Where(h => h is not null).Cast<Human>();
}