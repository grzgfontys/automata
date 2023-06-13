using Grammar.Assignment5;

namespace Automata.Parsing.Assignment5;

using BlockContext = Assignment5Parser.StatementBlockContext;

/// <summary>
/// Handles the definitions and invocations of functions
/// </summary>
public class FunctionManager
{
	// ReSharper disable once NotAccessedPositionalProperty.Local
	private record FunctionDeclaration(string Name,
	                                   IReadOnlyList<string> Parameters,
	                                   BlockContext Body)
	{
		public int ArgumentCount => Parameters.Count;
	}


	private readonly IDictionary<string, FunctionDeclaration> _functionDeclarations =
		new Dictionary<string, FunctionDeclaration>();

	private readonly IVariableManager _variableManager;

	public FunctionManager(IVariableManager variableManager)
	{
		_variableManager = variableManager;
	}

	/// <summary>
	/// Action that will be called on the function body upon invocation
	/// </summary>
	public Action<BlockContext>? BlockExecutor { get; set; }

	/// <summary>
	/// Registers a given function
	/// </summary>
	public void AddFunctionDeclaration(string name, IEnumerable<string> parameters, BlockContext block)
	{
		_functionDeclarations.Add(name, new FunctionDeclaration(name, parameters.ToList(), block));
	}


	/// <summary>
	/// Invokes a function with a given name and provided arguments
	/// </summary>
	/// <param name="name">Name of the function to execute</param>
	/// <param name="arguments">The values of arguments to the function</param>
	/// <param name="blockExecutor">Optional action to call on the block of the function</param>
	/// <exception cref="InvalidOperationException">Raised when <paramref name="blockExecutor"/> was not provided
	/// and <see cref="BlockExecutor"/> is null</exception>
	/// <exception cref="KeyNotFoundException">Raised if function with given name was not previously declared</exception>
	public void InvokeFunction(string name, IReadOnlyList<int> arguments, Action<BlockContext>? blockExecutor = null)
	{
		blockExecutor ??= BlockExecutor;
		if ( blockExecutor is null )
		{
			throw new InvalidOperationException("Calling Invoke function with no blockExecutor provided");
		}

		if ( !_functionDeclarations.TryGetValue(name, out var functionDeclaration) )
		{
			throw new KeyNotFoundException($"Function {name} not defined when called");
		}
		if ( functionDeclaration.ArgumentCount != arguments.Count )
		{
			throw new
				Exception($"Function {name} expects {functionDeclaration.ArgumentCount} arguments, but {arguments.Count} were given");
		}
		Dictionary<string, int> newVariableContext = new();
		for ( var i = 0; i < arguments.Count; i++ )
		{
			string varName = functionDeclaration.Parameters[i];
			int value = arguments[i];
			newVariableContext[varName] = value;
		}

		_variableManager.PushContext(newVariableContext);
		blockExecutor(functionDeclaration.Body);
		_variableManager.PopContext();
	}
}