//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ./Grammar/Assignment3/Assignment3.g4 by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Grammar.Assignment3 {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="Assignment3Parser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.CLSCompliant(false)]
public interface IAssignment3Visitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="Assignment3Parser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFile([NotNull] Assignment3Parser.FileContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Assignment3Parser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLine([NotNull] Assignment3Parser.LineContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Assignment3Parser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] Assignment3Parser.FunctionCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Assignment3Parser.variableAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableAssignment([NotNull] Assignment3Parser.VariableAssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ParenthesizedExpression</c>
	/// labeled alternative in <see cref="Assignment3Parser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesizedExpression([NotNull] Assignment3Parser.ParenthesizedExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Factorial</c>
	/// labeled alternative in <see cref="Assignment3Parser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFactorial([NotNull] Assignment3Parser.FactorialContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BinaryOperation</c>
	/// labeled alternative in <see cref="Assignment3Parser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinaryOperation([NotNull] Assignment3Parser.BinaryOperationContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Literal</c>
	/// labeled alternative in <see cref="Assignment3Parser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral([NotNull] Assignment3Parser.LiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>NestedVar</c>
	/// labeled alternative in <see cref="Assignment3Parser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNestedVar([NotNull] Assignment3Parser.NestedVarContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Assignment3Parser.keyword"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitKeyword([NotNull] Assignment3Parser.KeywordContext context);
}
} // namespace Grammar.Assignment3