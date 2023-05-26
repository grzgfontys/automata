using Antlr4.Runtime;
using Grammar.Assignment2;

namespace Automata.Parsing.Math;

public class MathVisitor : MathBaseVisitor<int>
{
	public override int VisitParenthesizedExpression(MathParser.ParenthesizedExpressionContext context) =>
		Visit(context.expression());

	public override int VisitLiteral(MathParser.LiteralContext context) => int.Parse(context.GetText());

	public override int VisitAdditionSubtraction(MathParser.AdditionSubtractionContext context)
	{
		int lhs = Visit(context.expression(0));
		int rhs = Visit(context.expression(1));
		string op = context.op.Text;
		return BinaryOpImpl(lhs, rhs, op);
	}

	public override int VisitMultiplicationDivision(MathParser.MultiplicationDivisionContext context)
	{
		int lhs = Visit(context.expression(0));
		int rhs = Visit(context.expression(1));
		string op = context.op.Text;
		return BinaryOpImpl(lhs, rhs, op);
	}

	private static int BinaryOpImpl(int lhs, int rhs, string op)
	{
		return op switch
		       {
			       "+" => lhs + rhs,
			       "-" => lhs - rhs,
			       "*" => lhs * rhs,
			       "/" => lhs / rhs,
			       _   => throw new ArgumentException($"Unknown binary operator {op}")
		       };
	}
}