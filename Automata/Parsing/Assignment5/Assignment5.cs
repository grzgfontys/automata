using System.Diagnostics;
using System.Net.Mail;
using Antlr4.Runtime.Tree;
// using Grammar.Assignment5;

namespace Automata.Parsing.Assignment5;

public class Assignment5CustomVisitor : Assignment5BaseVisitor<object?> // nullable object because we do not return any value
{
	private readonly IDictionary<string, int> _variableValues = new Dictionary<string, int>();
	private readonly HashSet<string> _variableInit = new HashSet<string>();
	// private readonly IDictionary<IRuleNode, IDictionary<string, int>> _functionsVariables = new Dictionary<IRuleNode, IDictionary<string, int>>();
	private readonly IDictionary<string, List<Tuple<string,int?>>> _declaredFunctions = new Dictionary<string, List<Tuple<string,int?>>>();
	private readonly IAssignment5Visitor<int> _intVisitor;
	private readonly IAssignment5Visitor<bool> _boolVisitor;

	class FunParam
	{
		public string name;
		public int? value = null;
		public FunParam(string name, int value)
		{
			this.name = name;
			this.value = value;
		}
		public FunParam(string name)
		{
			this.name = name;
		}
	}

	public Assignment5CustomVisitor()
	{
		_intVisitor = new IntegralExpressionVisitor(_variableValues);
		_boolVisitor = new BooleanExpressionVisitor(_intVisitor);
	}

	public override object? VisitIfStatement(Assignment5Parser.IfStatementContext context)
	{
		bool shouldVisit = _boolVisitor.Visit(context.booleanExpression());
		var ifBlock = context.statementBlock();
		var elseBlock = context.elseBlock();
		if ( shouldVisit )
		{
			Visit(ifBlock);
		}
		else if ( elseBlock is not null )
		{
			Visit(elseBlock);
		}
		return null;
	}

	public override object? VisitWhileStatement(Assignment5Parser.WhileStatementContext context)
	{
		var condition = context.booleanExpression();
		var block = context.statementBlock();
		while ( _boolVisitor.Visit(condition) )
		{
			Visit(block);
		}
		return null;
	}

	private void HandlePrintFunction(IEnumerable<Assignment5Parser.ExpressionContext> expressions)
	{
		foreach ( var expression in expressions )
		{
			Console.WriteLine(_intVisitor.Visit(expression));
		}
	}
	private void HandleUserFunction(string funName, IEnumerable<Assignment5Parser.ExpressionContext> expressions)
	{
		List<Assignment5Parser.ExpressionContext> expressionsList = expressions.ToList();
		if(!_declaredFunctions.ContainsKey(funName))
		{
			throw new UnreachableException($"Function {funName} does not exist");
		}
		else if(_declaredFunctions[funName].Count < expressionsList.Count)
		{
			
			throw new UnreachableException($"Function {funName} takes less parameters");
		}
		else
		{
			List<int> funValues = new ();
			int j = 0;
			for (int i = 0; i < expressionsList.Count; i++)
			{
				funValues.Add(_intVisitor.Visit(expressionsList[i]));
				j = i;
			}

			j++;

			if (j < _declaredFunctions[funName].Count)
			{
				for (int i = j; i < _declaredFunctions[funName].Count; i++)
				{
					int? defaultParam = _declaredFunctions[funName][i].Item2;
					if (defaultParam.HasValue)
					{
						funValues.Add(defaultParam.Value);
					}
					else
					{
						throw new UnreachableException($"Function {funName} takes more parameters");
					}
					j = i;
				}
			}
		}
	}

	public override object? VisitInitialisation(Assignment5Parser.InitialisationContext context)
	{
		string varName = context.IDENT().GetText();

		_variableInit.Add(varName);
		return null;
	}

	public override object? VisitVariableAssignment(Assignment5Parser.VariableAssignmentContext context)
	{
		string varName = context.IDENT().GetText();
		int value = _intVisitor.Visit(context.expression());

		_variableValues[varName] = value;
		return null;
	}

	public override object? VisitDefaultParam(Assignment5Parser.DefaultParamContext context)
	{
		string paramName = context.IDENT().GetText();
		int paramValue = _intVisitor.Visit(context.expression());
		
		return new FunParam(paramName, paramValue);
	}

	public override object? VisitParam(Assignment5Parser.ParamContext context)
	{
		string paramName = context.IDENT().GetText();
		
		return new FunParam(paramName);
	}

	public override object? VisitFunctionDeclaration(Assignment5Parser.FunctionDeclarationContext context)
	{
		string functionName = context.functionName().GetText();
		_declaredFunctions[functionName] = new List<Tuple<string,int?>>();

		var paramsCount = context.functionParams().Length;

		for (int i = 0; i < paramsCount; i++)
		{
			FunParam funParam = (FunParam)this.Visit(context.functionParams(i));
			_declaredFunctions[functionName].Add(new Tuple<string, int?>(funParam.name, funParam.value));
		}

		Console.WriteLine(String.Join(",", _declaredFunctions[functionName]));
		return null;
	}

	public override object? VisitFunctionCall(Assignment5Parser.FunctionCallContext context)
	{
		
		// string functionName = context.functionName().GetText();
		// _declaredFunctions[functionName] = new HashSet<string>();
		// int childCount = context.expression().Length;
		//
		// for (int i = 0; i < childCount; i++)
		// {
		// 	_declaredFunctions[functionName].Add(context.expression(i).GetText());
		// 	Console.WriteLine(context.expression(i).GetType());
		// 	if (context.expression(i).GetType() == typeof(Assignment5Parser.NestedVarContext))
		// 	{
		// 		Console.WriteLine(context.expression(i).GetText());
		// 	}
		// }
		var keyword = context.functionName().Start;
		switch ( keyword.Type )
		{
			case Assignment5Parser.KW_PRINT:
				HandlePrintFunction(context.expression());
				break;
			case Assignment5Parser.IDENT:
				HandleUserFunction(context.functionName().IDENT().GetText(), context.expression());
				break;
			default:
				throw new UnreachableException($"Unknown keyword: {keyword.Text}");
		}
		return null;
	}

	public override object? VisitReturnStatement(Assignment5Parser.ReturnStatementContext context)
	{
		int? value = null;
		if (context.expression() != null)
		{
			value = _intVisitor.Visit(context.expression());
		}
		return value;
	}
}