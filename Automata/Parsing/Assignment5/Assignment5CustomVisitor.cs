using Antlr4.Runtime.Tree;
using Grammar.Assignment5;

namespace Automata.Parsing.Assignment5;

public class Assignment5CustomVisitor : Assignment5BaseVisitor<object?> // nullable object because we do not return any value
{
	private bool _isReturning;

	private readonly IAssignment5Visitor<int> _intVisitor;
	private readonly IAssignment5Visitor<bool> _boolVisitor;
	private int? _returnValue;
	private readonly FunctionManager _functionManager;
	private readonly IVariableManager _variableManager;
	private readonly StreamWriter _outputStream;
	private int ReturnValue => _returnValue
	                           ?? throw new
		                           InvalidOperationException("It is not allowed to access the return value when it was not set");
	
	public Assignment5CustomVisitor(Stream outputStream)
	{
		_outputStream = new StreamWriter(outputStream);
		_variableManager = new VariableManager();
		_functionManager = new FunctionManager(_variableManager) {BlockExecutor = block => VisitStatementBlock(block)};
		_intVisitor = new IntegralExpressionVisitor(_variableManager, () => ReturnValue, this);
		_boolVisitor = new BooleanExpressionVisitor(_intVisitor);
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
			_outputStream.WriteLine(_intVisitor.Visit(expression));
		}
	}

	protected override bool ShouldVisitNextChild(IRuleNode node, object? currentResult) => !_isReturning;

	public override object? VisitVariableDeclaration(Assignment5Parser.VariableDeclarationContext context)
	{
		string varName = context.IDENT().GetText();
		int value = _intVisitor.Visit(context.expression());

		_variableManager[varName] = value;
		return null;
	}

	public override object? VisitFunctionDeclaration(Assignment5Parser.FunctionDeclarationContext context)
	{
		_functionManager.AddFunctionDeclaration(context);
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
				var arguments1 = arguments.Select(_intVisitor.Visit).ToList();
				_functionManager.InvokeFunction(functionName, arguments1);
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