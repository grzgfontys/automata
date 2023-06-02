using System.Diagnostics;
using Grammar.Assignment4;

namespace Automata.Parsing.Calculator;

public class Calculator : CalculatorBaseVisitor<object?> // nullable object because we do not return any value
{
	private readonly IDictionary<string, int> _variables = new Dictionary<string, int>();
	private readonly ICalculatorVisitor<int> _intVisitor;
	private readonly ICalculatorVisitor<bool> _boolVisitor;

	public Calculator()
	{
		_intVisitor = new IntegralExpressionVisitor(_variables);
		_boolVisitor = new BooleanExpressionVisitor(_intVisitor);
	}

	public override object? VisitIfStatement(CalculatorParser.IfStatementContext context)
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

	public override object? VisitWhileStatement(CalculatorParser.WhileStatementContext context)
	{
		var condition = context.booleanExpression();
		var block = context.statementBlock();
		while ( _boolVisitor.Visit(condition) )
		{
			Visit(block);
		}
		return null;
	}

	public override object? VisitFunctionCall(CalculatorParser.FunctionCallContext context)
	{
		var keyword = context.keyword().Start;
		switch ( keyword.Type )
		{
			case CalculatorParser.KW_PRINT:
				HandlePrintFunction(context.expression());
				break;
			default:
				throw new UnreachableException($"Unknown keyword: {keyword.Text}");
		}
		return null;
	}

	private static void HandlePrintFunction(IEnumerable<CalculatorParser.ExpressionContext> expressions)
	{
		foreach ( var expression in expressions )
		{
			Console.WriteLine(expression.GetText());
		}
	}

	public override object? VisitVariableAssignment(CalculatorParser.VariableAssignmentContext context)
	{
		string varName = context.IDENT().GetText();
		int value = _intVisitor.Visit(context.expression());

		_variables[varName] = value;
		return null;
	}
}