using Grammar.Assignment1;

namespace Automata.Parsing;

public record Human(string Name, int? Age, string? Telephone);

public class HumanVisitor : Assignment1BaseVisitor<Human?>
{
	public override Human VisitFullName(Assignment1Parser.FullNameContext context)
	{
		string firstName = context.NAME()[0].GetText();
		string lastName = context.NAME()[1].GetText();
		return new Human($"{firstName} {lastName}", null, null);
	}

	public override Human VisitNameAge(Assignment1Parser.NameAgeContext context)
	{
		string name = context.NAME().GetText();
		int age = int.Parse(context.AGE().GetText());
		return new Human(name, age, null);
	}

	public override Human VisitNameTel(Assignment1Parser.NameTelContext context)
	{
		string name = context.NAME().GetText();
		string phone = context.TEL().GetText();
		return new Human(name, null, phone);
	}
}