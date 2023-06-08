using System.Diagnostics;
using Grammar.Assignment5;


namespace Automata.Parsing.Assignment5;

public class BooleanExpressionVisitor : Assignment5BaseVisitor<bool>
{
	private readonly IAssignment5Visitor<int> _intVisitor;

	public BooleanExpressionVisitor(IAssignment5Visitor<int> intVisitor)
	{
		_intVisitor = intVisitor;
	}

	public override bool VisitComparison(Assignment5Parser.ComparisonContext context)
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

	public override bool VisitNegation(Assignment5Parser.NegationContext context) => !Visit(context.booleanExpression());

	public override bool VisitLogicalAnd(Assignment5Parser.LogicalAndContext context) =>
		Visit(context.booleanExpression(0)) && Visit(context.booleanExpression(1));


	public override bool VisitLogicalOr(Assignment5Parser.LogicalOrContext context) =>
		Visit(context.booleanExpression(0)) || Visit(context.booleanExpression(1));

	public override bool VisitParenthesizedBooleanExpression(Assignment5Parser.ParenthesizedBooleanExpressionContext context) =>
		Visit(context.booleanExpression());
}