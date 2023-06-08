using Grammar.Assignment5;
using static System.Math;

namespace Automata.Parsing.Assignment5;

public class IntegralExpressionVisitor : Assignment5BaseVisitor<int>
{
	private readonly Func<IDictionary<string, int>> variableValuesProvider;

	private readonly Func<int> GetReturnValue;
	private IDictionary<string, int> VariableValues => variableValuesProvider();
	private readonly Assignment5CustomVisitor _statementVisitor;

	public IntegralExpressionVisitor(Func<IDictionary<string, int>> variableValuesProvider,
	                                 Func<int> getReturnValue,
	                                 Assignment5CustomVisitor statementVisitor)
	{
		this.variableValuesProvider = variableValuesProvider;
		GetReturnValue = getReturnValue;
		this._statementVisitor = statementVisitor;
	}

	public override int VisitLiteral(Assignment5Parser.LiteralContext context) => int.Parse(context.GetText());

	public override int VisitFunctionCall(Assignment5Parser.FunctionCallContext context)
	{
		_statementVisitor.VisitFunctionCall(context);
		return GetReturnValue();
	}

	public override int VisitNestedVar(Assignment5Parser.NestedVarContext context) => VariableValues[context.IDENT().GetText()];

	public override int VisitFactorial(Assignment5Parser.FactorialContext context) => Factorial(Visit(context.expression()));

	public override int VisitBinaryOperation(Assignment5Parser.BinaryOperationContext context)
	{
		int lhs = Visit(context.expression(0));
		int rhs = Visit(context.expression(1));
		string op = context.op.Text;
		return op switch
		       {
			       "+" => lhs + rhs,
			       "-" => lhs - rhs,
			       "*" => lhs * rhs,
			       "/" => lhs / rhs,
			       "^" => (int) Pow(lhs, rhs),
			       _   => throw new ArgumentException($"Unknown binary operator {op}")
		       };
	}

	public override int VisitParenthesizedExpression(Assignment5Parser.ParenthesizedExpressionContext context) =>
		Visit(context.expression());

	private static int Factorial(int n) => Enumerable.Range(1, n).Aggregate(1, (acc, i) => acc * i);
}