using Grammar.Assignment2;
using static System.Math;

namespace Automata.Parsing.Math;

public class MathVisitor : MathBaseVisitor<int>
{
	public override int VisitParenthesizedExpression(MathParser.ParenthesizedExpressionContext context) =>
		Visit(context.expression());

	public override int VisitLiteral(MathParser.LiteralContext context) => int.Parse(context.GetText());

	public override int VisitFactorial(MathParser.FactorialContext context) => Factorial(Visit(context.expression()));

	private static int Factorial(int n) => Enumerable.Range(1, n).Aggregate(1, (acc, i) => acc * i);

	public override int VisitBinaryOperation(MathParser.BinaryOperationContext context)
	{
		const string powerOperator = "^";

		int lhs = Visit(context.expression(0));
		int rhs = Visit(context.expression(1));
		string op = context.op.Text;
		return op switch
		       {
			       "+"           => lhs + rhs,
			       "-"           => lhs - rhs,
			       "*"           => lhs * rhs,
			       "/"           => lhs / rhs,
			       powerOperator => (int) Pow(lhs, rhs),
			       _             => throw new ArgumentException($"Unknown binary operator {op}")
		       };
	}
}