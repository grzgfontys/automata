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
	private int? _returnValue;
	private readonly FunctionManager _functionManager;
	private int ReturnValue => _returnValue
	                           ?? throw new
		                           InvalidOperationException("It is not allowed to access the return value when it was not set");


	public Assignment5CustomVisitor()
	{
		_intVisitor = new IntegralExpressionVisitor(() => LocalVariableContext, () => ReturnValue, this);
		_boolVisitor = new BooleanExpressionVisitor(_intVisitor);

		_variableContexts.Push(new Dictionary<string, int>());
		_functionManager = new FunctionManager(_variableContexts) {BlockExecutor = block => this.VisitStatementBlock(block)};
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

	private void HandleUserDefinedFunction(string functionName,
	                                       IEnumerable<Assignment5Parser.ExpressionContext> expressions)
	{
		var arguments = expressions.Select(_intVisitor.Visit).ToList();
		_functionManager.InvokeFunction(functionName, arguments);
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
		var parameters = context.functionParameters()._params.Select(token => token.Text);
		var body = context.statementBlock();

		_functionManager.AddFunctionDeclaration(functionName, parameters, body);
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
				HandleUserDefinedFunction(functionName, arguments);
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