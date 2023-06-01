using System.Diagnostics;
using Antlr4.Runtime.Tree;
using Grammar.Assignment3;
using static System.Math;

namespace Automata.Parsing.Assignment3;

public class Listener : Assignment3BaseListener
{
	private readonly Dictionary<IRuleNode, int> values = new();
	private readonly Dictionary<string, int> variables = new();

	public int GetResult(Assignment3Parser.ExpressionContext expr) => values[expr];

	public void PrintVariables()
	{
		foreach ( var variable in variables )
		{
			Console.WriteLine($"{variable.Key} = {variable.Value}");
		}
	}

	public override void ExitFunctionCall(Assignment3Parser.FunctionCallContext context)
	{
		var keyword = context.keyword().Start;
		switch ( keyword.Type )
		{
			case Assignment3Parser.KW_PRINT:
				HandlePrintFunction(context.expression());
				break;
			default:
				throw new UnreachableException($"Unknown keyword: {keyword.Text}");
		}
	}

	private void HandlePrintFunction(IEnumerable<Assignment3Parser.ExpressionContext> expressions)
	{
		foreach ( var expression in expressions )
		{
			Console.WriteLine(values[expression]);
		}
	}

	public override void ExitVariableAssignment(Assignment3Parser.VariableAssignmentContext context)
	{
		string varName = context.IDENT().GetText();
		int value = values[context.expression()];
		variables[varName] = value;
	}

	public override void ExitNestedVar(Assignment3Parser.NestedVarContext context)
	{
		int value = variables[context.IDENT().GetText()];
		values[context] = value;
	}

	public override void ExitLiteral(Assignment3Parser.LiteralContext context)
	{
		int value = int.Parse(context.NUMBER().GetText());
		values[context] = value;
	}

	public override void ExitParenthesizedExpression(Assignment3Parser.ParenthesizedExpressionContext context)
	{
		values[context] = values[context.expression()];
	}

	public override void ExitFactorial(Assignment3Parser.FactorialContext context)
	{
		int value = values[context.expression()];
		int result = Factorial(value);
		values[context] = result;
	}

	public override void ExitBinaryOperation(Assignment3Parser.BinaryOperationContext context)
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