using Antlr4.Runtime.Tree;
using static System.Math;

namespace Automata.Parsing.Assignment3;

public class Listener : Assignment3BaseListener
{
    public Dictionary<IRuleNode, int> values = new();
    public Dictionary<string, int> variables = new();
    
    public int GetResult(Assignment3Parser.ExpressionContext expr) => values[expr];

    public void PrintVariables()
    {
        foreach (KeyValuePair<string,int> variable in variables)
        {
            Console.WriteLine($"{variable.Key} = {variable.Value}");
        }
    }
    
    public override void ExitInvokeFunction(Assignment3Parser.InvokeFunctionContext context)
    {
        switch (context.keywords().GetText())
        {
            case "print":
                Console.WriteLine(values[context.expression(0)]);
                break;
        }
    }
    
    public override void ExitVarAssignmentNumber(Assignment3Parser.VarAssignmentNumberContext context)
    {
        string varName = context.VARIABLE().GetText();
        int value = int.Parse(context.NUMBER().GetText());
        variables[varName] = value;
    }
    
    public override void ExitVarAssignmentExpression(Assignment3Parser.VarAssignmentExpressionContext context)
    {
        string varName = context.VARIABLE().GetText();
        int value = values[context.expression()];
        variables[varName] = value;
    }
    
    public override void ExitNestedVar(Assignment3Parser.NestedVarContext context)
    {
        int value = variables[context.VARIABLE().GetText()];
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