using Grammar.Assignment5;


namespace Automata.Parsing.Assignment5;

public class FunctionsManager
{
    // ReSharper disable once NotAccessedPositionalProperty.Local
    public record FunctionDeclaration(string Name,
        IReadOnlyList<string> Parameters,
        Assignment5Parser.StatementBlockContext Body)
    {
        public int ArgumentCount => Parameters.Count;
    }

    public readonly IDictionary<string, FunctionDeclaration> functionDeclarations =
        new Dictionary<string, FunctionDeclaration>();
}