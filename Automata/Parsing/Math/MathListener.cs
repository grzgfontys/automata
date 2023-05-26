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

	public override void ExitBinaryOperation(MathParser.BinaryOperationContext context)
	{
		const string powerOperator = "^";
		
		int lhs = values[context.expression(0)];
		int rhs = values[context.expression(1)];
		string op = context.op.Text;
		values[context] = op switch
		       {
			       "+"           => lhs + rhs,
			       "-"           => lhs - rhs,
			       "*"           => lhs * rhs,
			       "/"           => lhs / rhs,
			       powerOperator => (int) Pow(lhs, rhs),
			       _             => throw new ArgumentException($"Unknown binary operator {op}")
		       };
	}

	private static int Factorial(int n) => Enumerable.Range(1, n).Aggregate(1, (acc, i) => acc * i);
}