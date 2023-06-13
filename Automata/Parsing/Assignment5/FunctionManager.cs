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
	                                   IReadOnlyList<ParameterDefinition> Parameters,
	                                   BlockContext Body);

	private readonly record struct ParameterDefinition
	{
		public required string Name { get; init; }
		public int? DefaultValue { get; init; }
		public bool IsRequired => !DefaultValue.HasValue;
	}


	private class ParametersVisitor : Assignment5BaseVisitor<IEnumerable<ParameterDefinition>>
	{
		protected override IEnumerable<ParameterDefinition> DefaultResult => Enumerable.Empty<ParameterDefinition>();

		protected override IEnumerable<ParameterDefinition> AggregateResult(IEnumerable<ParameterDefinition> aggregate,
		                                                                    IEnumerable<ParameterDefinition> nextResult) =>
			aggregate.Concat(nextResult);

		public override IEnumerable<ParameterDefinition> VisitRequiredParameterList(
			Assignment5Parser.RequiredParameterListContext context)
		{
			return context._parameters.Select(parameter => new ParameterDefinition {Name = parameter.Text});
		}

		public override IEnumerable<ParameterDefinition> VisitOptionalParameterList(
			Assignment5Parser.OptionalParameterListContext context)
		{
			return context._parameters.Select(parameter => new ParameterDefinition
			{
				Name = parameter.IDENT().GetText(),
				DefaultValue = int.Parse(parameter.NUMBER().GetText())
			});
		}

	}

	private readonly IDictionary<string, FunctionDeclaration> _functionDeclarations =
		new Dictionary<string, FunctionDeclaration>();

	private readonly IVariableManager _variableManager;

	private readonly ParametersVisitor _parametersVisitor = new();

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
	public void AddFunctionDeclaration(Assignment5Parser.FunctionDeclarationContext context)
	{
		string name = context.IDENT().GetText();
		var parameters = _parametersVisitor.Visit(context.functionParameters()).ToList();
		var body = context.statementBlock();
		_functionDeclarations.Add(name, new FunctionDeclaration(name, parameters, body));
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

		int maxArgumentCount = functionDeclaration.Parameters.Count;
		int minArgumentCount = functionDeclaration.Parameters.Count(x => x.IsRequired);
		if ( arguments.Count > maxArgumentCount || arguments.Count < minArgumentCount )
		{
			throw new
				Exception($"Function {name} expects between {minArgumentCount} and {maxArgumentCount} arguments, but {arguments.Count} were given");
		}
		Dictionary<string, int> newVariableContext = new();
		for ( var i = 0; i < functionDeclaration.Parameters.Count; i++ )
		{
			var parameter = functionDeclaration.Parameters[i];
			if ( arguments.Count > i ) // argument is provided by the caller
			{
				newVariableContext[parameter.Name] = arguments[i];
			}
			else
			{
				// cannot be null, since we checked that the number of required parameters
				// is not greater than the number of provided arguments
				newVariableContext[parameter.Name] = parameter.DefaultValue!.Value;
			}
		}

		_variableManager.PushContext(newVariableContext);
		blockExecutor(functionDeclaration.Body);
		_variableManager.PopContext();
	}
}