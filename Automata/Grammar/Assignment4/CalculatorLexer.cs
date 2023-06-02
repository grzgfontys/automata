//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ./Grammar/Assignment4/Calculator.g4 by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Grammar.Assignment4 {
using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.CLSCompliant(false)]
public partial class CalculatorLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		KW_PRINT=18, COMP_OPERATOR=19, NUMBER=20, IDENT=21, NEWLINE=22, WHITESPACE=23;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"KW_PRINT", "COMP_OPERATOR", "NUMBER", "IDENT", "NEWLINE", "WHITESPACE", 
		"NONZERO_DIGIT", "ZERO", "DIGIT", "LETTER"
	};


	public CalculatorLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public CalculatorLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'{'", "'}'", "'if'", "'else'", "'while'", "'('", "','", "')'", 
		"'='", "'!'", "'and'", "'or'", "'^'", "'*'", "'/'", "'+'", "'-'", "'print'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, "KW_PRINT", "COMP_OPERATOR", "NUMBER", 
		"IDENT", "NEWLINE", "WHITESPACE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Calculator.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static CalculatorLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,23,158,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,1,0,1,0,1,1,1,1,
		1,2,1,2,1,2,1,3,1,3,1,3,1,3,1,3,1,4,1,4,1,4,1,4,1,4,1,4,1,5,1,5,1,6,1,
		6,1,7,1,7,1,8,1,8,1,9,1,9,1,10,1,10,1,10,1,10,1,11,1,11,1,11,1,12,1,12,
		1,13,1,13,1,14,1,14,1,15,1,15,1,16,1,16,1,17,1,17,1,17,1,17,1,17,1,17,
		1,18,1,18,1,18,1,18,1,18,1,18,1,18,1,18,1,18,1,18,3,18,117,8,18,1,19,1,
		19,5,19,121,8,19,10,19,12,19,124,9,19,1,19,3,19,127,8,19,1,20,1,20,1,20,
		5,20,132,8,20,10,20,12,20,135,9,20,1,21,3,21,138,8,21,1,21,1,21,1,22,4,
		22,143,8,22,11,22,12,22,144,1,22,1,22,1,23,1,23,1,24,1,24,1,25,1,25,3,
		25,155,8,25,1,26,1,26,0,0,27,1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,
		10,21,11,23,12,25,13,27,14,29,15,31,16,33,17,35,18,37,19,39,20,41,21,43,
		22,45,23,47,0,49,0,51,0,53,0,1,0,3,2,0,9,9,32,32,1,0,49,57,2,0,65,90,97,
		122,165,0,1,1,0,0,0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,0,
		11,1,0,0,0,0,13,1,0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,1,
		0,0,0,0,23,1,0,0,0,0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,0,
		0,33,1,0,0,0,0,35,1,0,0,0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,0,43,
		1,0,0,0,0,45,1,0,0,0,1,55,1,0,0,0,3,57,1,0,0,0,5,59,1,0,0,0,7,62,1,0,0,
		0,9,67,1,0,0,0,11,73,1,0,0,0,13,75,1,0,0,0,15,77,1,0,0,0,17,79,1,0,0,0,
		19,81,1,0,0,0,21,83,1,0,0,0,23,87,1,0,0,0,25,90,1,0,0,0,27,92,1,0,0,0,
		29,94,1,0,0,0,31,96,1,0,0,0,33,98,1,0,0,0,35,100,1,0,0,0,37,116,1,0,0,
		0,39,126,1,0,0,0,41,128,1,0,0,0,43,137,1,0,0,0,45,142,1,0,0,0,47,148,1,
		0,0,0,49,150,1,0,0,0,51,154,1,0,0,0,53,156,1,0,0,0,55,56,5,123,0,0,56,
		2,1,0,0,0,57,58,5,125,0,0,58,4,1,0,0,0,59,60,5,105,0,0,60,61,5,102,0,0,
		61,6,1,0,0,0,62,63,5,101,0,0,63,64,5,108,0,0,64,65,5,115,0,0,65,66,5,101,
		0,0,66,8,1,0,0,0,67,68,5,119,0,0,68,69,5,104,0,0,69,70,5,105,0,0,70,71,
		5,108,0,0,71,72,5,101,0,0,72,10,1,0,0,0,73,74,5,40,0,0,74,12,1,0,0,0,75,
		76,5,44,0,0,76,14,1,0,0,0,77,78,5,41,0,0,78,16,1,0,0,0,79,80,5,61,0,0,
		80,18,1,0,0,0,81,82,5,33,0,0,82,20,1,0,0,0,83,84,5,97,0,0,84,85,5,110,
		0,0,85,86,5,100,0,0,86,22,1,0,0,0,87,88,5,111,0,0,88,89,5,114,0,0,89,24,
		1,0,0,0,90,91,5,94,0,0,91,26,1,0,0,0,92,93,5,42,0,0,93,28,1,0,0,0,94,95,
		5,47,0,0,95,30,1,0,0,0,96,97,5,43,0,0,97,32,1,0,0,0,98,99,5,45,0,0,99,
		34,1,0,0,0,100,101,5,112,0,0,101,102,5,114,0,0,102,103,5,105,0,0,103,104,
		5,110,0,0,104,105,5,116,0,0,105,36,1,0,0,0,106,117,5,62,0,0,107,108,5,
		62,0,0,108,117,5,61,0,0,109,117,5,60,0,0,110,111,5,60,0,0,111,117,5,61,
		0,0,112,113,5,61,0,0,113,117,5,61,0,0,114,115,5,33,0,0,115,117,5,61,0,
		0,116,106,1,0,0,0,116,107,1,0,0,0,116,109,1,0,0,0,116,110,1,0,0,0,116,
		112,1,0,0,0,116,114,1,0,0,0,117,38,1,0,0,0,118,122,3,47,23,0,119,121,3,
		51,25,0,120,119,1,0,0,0,121,124,1,0,0,0,122,120,1,0,0,0,122,123,1,0,0,
		0,123,127,1,0,0,0,124,122,1,0,0,0,125,127,3,49,24,0,126,118,1,0,0,0,126,
		125,1,0,0,0,127,40,1,0,0,0,128,133,3,53,26,0,129,132,3,53,26,0,130,132,
		3,51,25,0,131,129,1,0,0,0,131,130,1,0,0,0,132,135,1,0,0,0,133,131,1,0,
		0,0,133,134,1,0,0,0,134,42,1,0,0,0,135,133,1,0,0,0,136,138,5,13,0,0,137,
		136,1,0,0,0,137,138,1,0,0,0,138,139,1,0,0,0,139,140,5,10,0,0,140,44,1,
		0,0,0,141,143,7,0,0,0,142,141,1,0,0,0,143,144,1,0,0,0,144,142,1,0,0,0,
		144,145,1,0,0,0,145,146,1,0,0,0,146,147,6,22,0,0,147,46,1,0,0,0,148,149,
		7,1,0,0,149,48,1,0,0,0,150,151,5,48,0,0,151,50,1,0,0,0,152,155,3,49,24,
		0,153,155,3,47,23,0,154,152,1,0,0,0,154,153,1,0,0,0,155,52,1,0,0,0,156,
		157,7,2,0,0,157,54,1,0,0,0,9,0,116,122,126,131,133,137,144,154,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Grammar.Assignment4
