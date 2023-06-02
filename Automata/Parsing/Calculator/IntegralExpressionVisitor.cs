using Grammar.Assignment4;
using static System.Math;

namespace Automata.Parsing.Calculator;

public class IntegralExpressionVisitor : CalculatorBaseVisitor<int>
{
	private readonly IDictionary<string, int> _variableValues;

	public IntegralExpressionVisitor(IDictionary<string, int> variableValues)
	{
		_variableValues = variableValues;
	}

	public override int VisitParenthesizedExpression(CalculatorParser.ParenthesizedExpressionContext context) =>
		Visit(context.expression());

	public override int VisitFactorial(CalculatorParser.FactorialContext context) => Factorial(Visit(context.expression()));

	private static int Factorial(int n) => Enumerable.Range(1, n).Aggregate(1, (acc, i) => acc * i);

	public override int VisitBinaryOperation(CalculatorParser.BinaryOperationContext context)
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

	public override int VisitLiteral(CalculatorParser.LiteralContext context) => int.Parse(context.GetText());

	public override int VisitNestedVar(CalculatorParser.NestedVarContext context) => _variableValues[context.IDENT().GetText()];
}