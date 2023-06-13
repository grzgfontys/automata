namespace Automata.Parsing.Assignment5;

public interface IVariableManager
{
	int this[string variableName] { get; set; }
	void PushContext(IDictionary<string, int> newContext);
	void PopContext();
}

public class VariableManager : IVariableManager
{
	private readonly Stack<IDictionary<string, int>> _variableContexts = new();

	private IDictionary<string, int> CurrentContext => _variableContexts.Peek();

	public VariableManager()
	{
		_variableContexts.Push(new Dictionary<string, int>());
	}

	public int this[string variableName]
	{
		get => CurrentContext[variableName];
		set => CurrentContext[variableName] = value;
	}

	public void PushContext(IDictionary<string, int> newContext) => _variableContexts.Push(newContext);
	public void PopContext() => _variableContexts.Pop();
}