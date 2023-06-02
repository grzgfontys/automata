using System.Diagnostics;
using Grammar.Assignment4;

namespace Automata.Parsing.Calculator;

public class BooleanExpressionVisitor : CalculatorBaseVisitor<bool>
{
	private readonly ICalculatorVisitor<int> _intVisitor;

	public BooleanExpressionVisitor(ICalculatorVisitor<int> intVisitor)
	{
		_intVisitor = intVisitor;
	}

	public override bool VisitParenthesizedBooleanExpression(CalculatorParser.ParenthesizedBooleanExpressionContext context) =>
		Visit(context.booleanExpression());

	public override bool VisitComparison(CalculatorParser.ComparisonContext context)
	{
		int lhs = _intVisitor.Visit(context.expression(0));
		int rhs = _intVisitor.Visit(context.expression(1));

		var op = context.COMP_OPERATOR().GetText();
		return op switch
		       {
			       "<"  => lhs < rhs,
			       ">"  => lhs > rhs,
			       "<=" => lhs <= rhs,
			       ">=" => lhs >= rhs,
			       "==" => lhs == rhs,
			       "!=" => lhs != rhs,
			       _    => throw new UnreachableException($"Unknown comparison operator {op}")
		       };
	}

	public override bool VisitNegation(CalculatorParser.NegationContext context) => !Visit(context.booleanExpression());

	public override bool VisitLogicalAnd(CalculatorParser.LogicalAndContext context) =>
		Visit(context.booleanExpression(0)) && Visit(context.booleanExpression(1));


	public override bool VisitLogicalOr(CalculatorParser.LogicalOrContext context) =>
		Visit(context.booleanExpression(0)) || Visit(context.booleanExpression(1));
}