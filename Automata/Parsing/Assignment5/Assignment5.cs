using System.Diagnostics;
using Antlr4.Runtime.Tree;

// using Grammar.Assignment5;

namespace Automata.Parsing.Assignment5;

public class Assignment5CustomVisitor : Assignment5BaseVisitor<object?> // nullable object because we do not return any value
{
	private readonly IDictionary<string, int> _variableValues = new Dictionary<string, int>();
	private readonly HashSet<string> _variableInit = new();

	private IDictionary<string, int> _functionsVariables = new Dictionary<string, int>();

	// Stores function context for executing code inside the function by name of function
	private readonly IDictionary<string, IRuleNode> _functionsContexts = new Dictionary<string, IRuleNode>();

	// Stores names of function, then in list are parameters for function, one parameter in tuple name and value
	private readonly IDictionary<string, List<Tuple<string, int?>>> _declaredFunctions =
		new Dictionary<string, List<Tuple<string, int?>>>();

	private readonly IAssignment5Visitor<int> _intVisitor;
	private readonly IAssignment5Visitor<bool> _boolVisitor;

	private class FunParam
	{
		public required string Name { get; init; }
		public int? Value { get; init; }
	}

	public Assignment5CustomVisitor()
	{
		_intVisitor = new IntegralExpressionVisitor(_variableValues);
		_boolVisitor = new BooleanExpressionVisitor(_intVisitor);
	}

	private Assignment5CustomVisitor(IDictionary<string, int> variableValues)
	{
		_variableValues = variableValues;
		_intVisitor = new IntegralExpressionVisitor(_variableValues);
		_boolVisitor = new BooleanExpressionVisitor(_intVisitor);
	}

	private static object? HelperVisitor(IParseTree scopedStatementBlockContext, IDictionary<string, int> variableValues)
	{
		var visitor = new Assignment5CustomVisitor(variableValues);
		return visitor.Visit(scopedStatementBlockContext);
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

	private Dictionary<string, int> HandleUserFunction(string funName,
	                                                   IEnumerable<Assignment5Parser.ExpressionContext> expressions)
	{
		var expressionsList = expressions.ToList();
		Dictionary<string, int> functionsVariables = new();
		if ( !_declaredFunctions.ContainsKey(funName) )
		{
			// TODO: This is not unreachable exception
			throw new UnreachableException($"Function {funName} does not exist");
		}
		if ( _declaredFunctions[funName].Count < expressionsList.Count )
		{
			// TODO: This is not unreachable exception
			throw new UnreachableException($"Function {funName} takes less parameters");
		}
		int j = 0;
		for ( int i = 0; i < expressionsList.Count; i++ )
		{
			functionsVariables[_declaredFunctions[funName][i].Item1] = _intVisitor.Visit(expressionsList[i]);
			j = i;
		}

		j++;

		if ( j >= _declaredFunctions[funName].Count )
			return functionsVariables;

		for ( int i = j; i < _declaredFunctions[funName].Count; i++ )
		{
			int? defaultParam = _declaredFunctions[funName][i].Item2;
			if ( defaultParam.HasValue )
			{
				functionsVariables[_declaredFunctions[funName][i].Item1] = defaultParam.Value;
			}
			else
			{
				// TODO: This is not unreachable exception
				throw new UnreachableException($"Function {funName} takes more parameters");
			}
		}

		return functionsVariables;
	}

	public override object? VisitInitialisation(Assignment5Parser.InitialisationContext context)
	{
		string varName = context.IDENT().GetText();
		if ( !_variableInit.Contains(varName) && !_variableValues.ContainsKey(varName) )
		{
			_variableInit.Add(varName);
		}
		else
		{
			// TODO: This is not unreachable exception
			throw new UnreachableException($"Second initialization of variable: {varName}");
		}
		return null;
	}

	public override object? VisitExpressionAssignment(Assignment5Parser.ExpressionAssignmentContext context)
	{
		string varName = context.IDENT().GetText();
		int value = _intVisitor.Visit(context.expression());

		_variableValues[varName] = value;
		return null;
	}

	public override object? VisitFunctionAssignment(Assignment5Parser.FunctionAssignmentContext context)
	{
		string varName = context.IDENT().GetText();
		var value = Visit(context.functionCall());

		if ( value is int intValue )
		{
			_variableValues[varName] = intValue;
		}
		return null;
	}

	public override object VisitDefaultParam(Assignment5Parser.DefaultParamContext context)
	{
		return new FunParam
		{
			Name = context.IDENT().GetText(), 
			Value = _intVisitor.Visit(context.expression())
		};
	}

	public override object VisitParam(Assignment5Parser.ParamContext context)
	{
		return new FunParam
		{
			Name = context.IDENT().GetText()
		};
	}

	public override object? VisitFunctionDeclaration(Assignment5Parser.FunctionDeclarationContext context)
	{
		string functionName = context.functionName().GetText();
		_declaredFunctions[functionName] = new List<Tuple<string, int?>>();
		_functionsContexts[functionName] = context.statementBlock();

		var paramsCount = context.functionParams().Length;

		for ( int i = 0; i < paramsCount; i++ )
		{
			FunParam? funParam = (FunParam?) Visit(context.functionParams(i));
			if ( funParam is not null )
				_declaredFunctions[functionName].Add(new Tuple<string, int?>(funParam.Name, funParam.Value));
		}

		Console.WriteLine(string.Join(",", _declaredFunctions[functionName]));
		return null;
	}

	public override object? VisitFunctionCall(Assignment5Parser.FunctionCallContext context)
	{
		var keyword = context.functionName().Start;
		switch ( keyword.Type )
		{
			case Assignment5Parser.KW_PRINT:
				HandlePrintFunction(context.expression());
				break;
			case Assignment5Parser.IDENT:
				_functionsVariables = HandleUserFunction(context.functionName().IDENT().GetText(), context.expression());
				// this.Visit(_functionsContexts[context.functionName().IDENT().GetText()]);
				var value = HelperVisitor(_functionsContexts[context.functionName().IDENT().GetText()], _functionsVariables);
				_functionsVariables.Clear();
				return value;
			default:
				throw new UnreachableException($"Unknown keyword: {keyword.Text}");
		}
		return null;
	}

	public override object? VisitStatementBlock(Assignment5Parser.StatementBlockContext context)
	{
		int childCount = context.ChildCount;
		for ( int i = 0; i < childCount; ++i )
		{
			if ( context.GetChild(i) is Assignment5Parser.ReturnStatementContext )
			{
				return Visit(context.GetChild(i));
			}
			Visit(context.GetChild(i));
		}
		return null;
	}

	public override object? VisitReturnStatement(Assignment5Parser.ReturnStatementContext context)
	{
		int? value = null;
		if ( context.expression() is not null )
		{
			value = _intVisitor.Visit(context.expression());
		}
		return value;
	}
}