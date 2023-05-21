using Grammar.Assignment1;

namespace Automata.Parsing;

public class FileVisotor : Assignment1BaseVisitor<IEnumerable<Human>>
{
	private readonly HumanVisitor humanVisitor = new();

	public override IEnumerable<Human> VisitFile(Assignment1Parser.FileContext context) =>
		from human in context.human() select humanVisitor.VisitHuman(human);
}