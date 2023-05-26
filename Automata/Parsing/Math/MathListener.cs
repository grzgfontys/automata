using Antlr4.Runtime.Tree;
using Grammar.Assignment2;
using static System.Math;

namespace Automata.Parsing.Math;

public class MathListener : MathBaseListener
{
	private readonly Dictionary<IRuleNode, int> values = new();
	public int GetResult(MathParser.ExpressionContext expr) => values[expr];

	public override void ExitLiteral(MathParser.LiteralContext context)
	{
		int value = int.Parse(context.NUMBER().GetText());
		values[context] = value;
	}

	public override void ExitParenthesizedExpression(MathParser.ParenthesizedExpressionContext context)
	{
		values[context] = values[context.expression()];
	}

	public override void ExitFactorial(MathParser.FactorialContext context)
	{
		int value = values[context.expression()];
		int result = Factorial(value);
		values[context] = result;
	}

	public override void ExitAdditionSubtraction(MathParser.AdditionSubtractionContext context)
	{
		int lhs = values[context.expression(0)];
		int rhs = values[context.expression(1)];
		int result = BinaryOpImpl(lhs, rhs, context.op.Text);
		values[context] = result;
	}

	public override void ExitMultiplicationDivision(MathParser.MultiplicationDivisionContext context)
	{
		int lhs = values[context.expression(0)];
		int rhs = values[context.expression(1)];
		int result = BinaryOpImpl(lhs, rhs, context.op.Text);
		values[context] = result;
	}

	public override void ExitPower(MathParser.PowerContext context)
	{
		int lhs = values[context.expression(0)];
		int rhs = values[context.expression(1)];
		int result = BinaryOpImpl(lhs, rhs, PowerOperator);
		values[context] = result;
	}

	private static int Factorial(int n) => Enumerable.Range(1, n).Aggregate(1, (acc, i) => acc * i);

	private const string PowerOperator = "^";

	private static int BinaryOpImpl(int lhs, int rhs, string op)
	{
		return op switch
		       {
			       "+"           => lhs + rhs,
			       "-"           => lhs - rhs,
			       "*"           => lhs * rhs,
			       "/"           => lhs / rhs,
			       PowerOperator => (int) Pow(lhs, rhs),
			       _             => throw new ArgumentException($"Unknown binary operator {op}")
		       };
	}
}