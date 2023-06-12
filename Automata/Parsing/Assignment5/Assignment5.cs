using Antlr4.Runtime.Tree;
using Grammar.Assignment5;

namespace Automata.Parsing.Assignment5;

public class Assignment5CustomVisitor : Assignment5BaseVisitor<object?> // nullable object because we do not return any value
{
	private readonly Stack<IDictionary<string, int>> _variableContexts = new();
	private IDictionary<string, int> LocalVariableContext => _variableContexts.Peek();

	private bool _isReturning = false;

	private readonly IAssignment5Visitor<int> _intVisitor;
	private readonly IAssignment5Visitor<bool> _boolVisitor;
	private readonly FunctionsManager _functionsManager;
	private int? _returnValue;
	private int ReturnValue => _returnValue
	                           ?? throw new
		                           InvalidOperationException("It is not allowed to access the return value when it was not set");

	public Assignment5CustomVisitor()
	{
		_intVisitor = new IntegralExpressionVisitor(() => LocalVariableContext, () => ReturnValue, this);
		_boolVisitor = new BooleanExpressionVisitor(_intVisitor);
		_functionsManager = new();

		_variableContexts.Push(new Dictionary<string, int>());
	}

	public override object? VisitIfStatement(Assignment5Parser.IfStatementContext context)
	{
		bool shouldVisit = _boolVisitor.Visit(context.booleanExpression());
		var ifBlock = context.statementBlock();
		var elseBlock = context.elseBlock();
		if ( shouldVisit )
		{
			Visit(ifBlock);
		}
		else if ( elseBlock is not null )
		{
			Visit(elseBlock);
		}
		return null;
	}

	public override object? VisitWhileStatement(Assignment5Parser.WhileStatementContext context)
	{
		var condition = context.booleanExpression();
		var block = context.statementBlock();
		while ( _boolVisitor.Visit(condition) )
		{
			Visit(block);
		}
		return null;
	}

	private void HandlePrintFunction(IEnumerable<Assignment5Parser.ExpressionContext> expressions)
	{
		foreach ( var expression in expressions )
		{
			Console.WriteLine(_intVisitor.Visit(expression));
		}
	}

	protected override bool ShouldVisitNextChild(IRuleNode node, object? currentResult) => !_isReturning;

	private void HandleUserFunction(string funName,
	                                IEnumerable<Assignment5Parser.ExpressionContext> expressions)
	{
		if ( !_functionsManager.functionDeclarations.TryGetValue(funName, out var functionDeclaration) )
		{
			throw new KeyNotFoundException($"Function {funName} not defined when called");
		}
		var arguments = expressions.ToList();
		if ( functionDeclaration.ArgumentCount != arguments.Count )
		{
			throw new
				Exception($"Function {funName} expects {functionDeclaration.ArgumentCount} arguments, but {arguments.Count} were given");
		}
		Dictionary<string, int> newVariableContext = new();
		for ( var i = 0; i < arguments.Count; i++ )
		{
			string varName = functionDeclaration.Parameters[i];
			int value = _intVisitor.Visit(arguments[i]);
			newVariableContext[varName] = value;
		}

		_variableContexts.Push(newVariableContext);
		Visit(functionDeclaration.Body);
		_variableContexts.Pop();
	}

	public override object? VisitVariableDeclaration(Assignment5Parser.VariableDeclarationContext context)
	{
		string varName = context.IDENT().GetText();
		int value = _intVisitor.Visit(context.expression());

		LocalVariableContext[varName] = value;
		return null;
	}

	public override object? VisitFunctionDeclaration(Assignment5Parser.FunctionDeclarationContext context)
	{
		string functionName = context.IDENT().GetText();
		var parameters = context.functionParameters()._params.Select(token => token.Text).ToList();
		var body = context.statementBlock();

		_functionsManager.functionDeclarations[functionName] = new FunctionsManager.FunctionDeclaration(functionName, parameters, body);
		return null;
	}

	public override object? VisitFunctionCall(Assignment5Parser.FunctionCallContext context)
	{
		var functionName = context.IDENT().GetText();
		var arguments = context.functionArguments().expression();
		switch ( functionName )
		{
			case "print":
				HandlePrintFunction(arguments);
				break;
			default:
				HandleUserFunction(functionName, arguments);
				break;
		}
		_isReturning = false;
		return null;
	}

	public override object? VisitReturnStatement(Assignment5Parser.ReturnStatementContext context)
	{
		_returnValue = null;
		if ( context.expression() is not null )
		{
			_returnValue = _intVisitor.Visit(context.expression());
		}
		_isReturning = true;
		return null;
	}
}