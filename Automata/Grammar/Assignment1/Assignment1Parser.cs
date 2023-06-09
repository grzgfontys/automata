//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ./Grammar/Assignment1/Assignment1.g4 by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Grammar.Assignment1 {
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.CLSCompliant(false)]
public partial class Assignment1Parser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		NAME=1, AGE=2, TEL=3, SEMICOLON=4, WHITESPACE=5, NEWLINE=6;
	public const int
		RULE_file = 0, RULE_human = 1, RULE_fullName = 2, RULE_nameAge = 3, RULE_nameTel = 4;
	public static readonly string[] ruleNames = {
		"file", "human", "fullName", "nameAge", "nameTel"
	};

	private static readonly string[] _LiteralNames = {
		null, null, null, null, "';'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "NAME", "AGE", "TEL", "SEMICOLON", "WHITESPACE", "NEWLINE"
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

	public override string GrammarFileName { get { return "Assignment1.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static Assignment1Parser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public Assignment1Parser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public Assignment1Parser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class FileContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Eof() { return GetToken(Assignment1Parser.Eof, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public HumanContext[] human() {
			return GetRuleContexts<HumanContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public HumanContext human(int i) {
			return GetRuleContext<HumanContext>(i);
		}
		public FileContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_file; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IAssignment1Listener typedListener = listener as IAssignment1Listener;
			if (typedListener != null) typedListener.EnterFile(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IAssignment1Listener typedListener = listener as IAssignment1Listener;
			if (typedListener != null) typedListener.ExitFile(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IAssignment1Visitor<TResult> typedVisitor = visitor as IAssignment1Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFile(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public FileContext file() {
		FileContext _localctx = new FileContext(Context, State);
		EnterRule(_localctx, 0, RULE_file);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 13;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==NAME) {
				{
				{
				State = 10;
				human();
				}
				}
				State = 15;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 16;
			Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class HumanContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public FullNameContext fullName() {
			return GetRuleContext<FullNameContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public NameAgeContext nameAge() {
			return GetRuleContext<NameAgeContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public NameTelContext nameTel() {
			return GetRuleContext<NameTelContext>(0);
		}
		public HumanContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_human; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IAssignment1Listener typedListener = listener as IAssignment1Listener;
			if (typedListener != null) typedListener.EnterHuman(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IAssignment1Listener typedListener = listener as IAssignment1Listener;
			if (typedListener != null) typedListener.ExitHuman(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IAssignment1Visitor<TResult> typedVisitor = visitor as IAssignment1Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitHuman(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public HumanContext human() {
		HumanContext _localctx = new HumanContext(Context, State);
		EnterRule(_localctx, 2, RULE_human);
		try {
			State = 21;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,1,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 18;
				fullName();
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 19;
				nameAge();
				}
				break;
			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 20;
				nameTel();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class FullNameContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode[] NAME() { return GetTokens(Assignment1Parser.NAME); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode NAME(int i) {
			return GetToken(Assignment1Parser.NAME, i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode WHITESPACE() { return GetToken(Assignment1Parser.WHITESPACE, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(Assignment1Parser.SEMICOLON, 0); }
		public FullNameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_fullName; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IAssignment1Listener typedListener = listener as IAssignment1Listener;
			if (typedListener != null) typedListener.EnterFullName(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IAssignment1Listener typedListener = listener as IAssignment1Listener;
			if (typedListener != null) typedListener.ExitFullName(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IAssignment1Visitor<TResult> typedVisitor = visitor as IAssignment1Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFullName(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public FullNameContext fullName() {
		FullNameContext _localctx = new FullNameContext(Context, State);
		EnterRule(_localctx, 4, RULE_fullName);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 23;
			Match(NAME);
			State = 24;
			Match(WHITESPACE);
			State = 25;
			Match(NAME);
			State = 26;
			Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class NameAgeContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode NAME() { return GetToken(Assignment1Parser.NAME, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode WHITESPACE() { return GetToken(Assignment1Parser.WHITESPACE, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode AGE() { return GetToken(Assignment1Parser.AGE, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(Assignment1Parser.SEMICOLON, 0); }
		public NameAgeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_nameAge; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IAssignment1Listener typedListener = listener as IAssignment1Listener;
			if (typedListener != null) typedListener.EnterNameAge(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IAssignment1Listener typedListener = listener as IAssignment1Listener;
			if (typedListener != null) typedListener.ExitNameAge(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IAssignment1Visitor<TResult> typedVisitor = visitor as IAssignment1Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitNameAge(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public NameAgeContext nameAge() {
		NameAgeContext _localctx = new NameAgeContext(Context, State);
		EnterRule(_localctx, 6, RULE_nameAge);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 28;
			Match(NAME);
			State = 29;
			Match(WHITESPACE);
			State = 30;
			Match(AGE);
			State = 31;
			Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class NameTelContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode NAME() { return GetToken(Assignment1Parser.NAME, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode WHITESPACE() { return GetToken(Assignment1Parser.WHITESPACE, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode TEL() { return GetToken(Assignment1Parser.TEL, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SEMICOLON() { return GetToken(Assignment1Parser.SEMICOLON, 0); }
		public NameTelContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_nameTel; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IAssignment1Listener typedListener = listener as IAssignment1Listener;
			if (typedListener != null) typedListener.EnterNameTel(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IAssignment1Listener typedListener = listener as IAssignment1Listener;
			if (typedListener != null) typedListener.ExitNameTel(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IAssignment1Visitor<TResult> typedVisitor = visitor as IAssignment1Visitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitNameTel(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public NameTelContext nameTel() {
		NameTelContext _localctx = new NameTelContext(Context, State);
		EnterRule(_localctx, 8, RULE_nameTel);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 33;
			Match(NAME);
			State = 34;
			Match(WHITESPACE);
			State = 35;
			Match(TEL);
			State = 36;
			Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static int[] _serializedATN = {
		4,1,6,39,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,1,0,5,0,12,8,0,10,0,12,
		0,15,9,0,1,0,1,0,1,1,1,1,1,1,3,1,22,8,1,1,2,1,2,1,2,1,2,1,2,1,3,1,3,1,
		3,1,3,1,3,1,4,1,4,1,4,1,4,1,4,1,4,0,0,5,0,2,4,6,8,0,0,36,0,13,1,0,0,0,
		2,21,1,0,0,0,4,23,1,0,0,0,6,28,1,0,0,0,8,33,1,0,0,0,10,12,3,2,1,0,11,10,
		1,0,0,0,12,15,1,0,0,0,13,11,1,0,0,0,13,14,1,0,0,0,14,16,1,0,0,0,15,13,
		1,0,0,0,16,17,5,0,0,1,17,1,1,0,0,0,18,22,3,4,2,0,19,22,3,6,3,0,20,22,3,
		8,4,0,21,18,1,0,0,0,21,19,1,0,0,0,21,20,1,0,0,0,22,3,1,0,0,0,23,24,5,1,
		0,0,24,25,5,5,0,0,25,26,5,1,0,0,26,27,5,4,0,0,27,5,1,0,0,0,28,29,5,1,0,
		0,29,30,5,5,0,0,30,31,5,2,0,0,31,32,5,4,0,0,32,7,1,0,0,0,33,34,5,1,0,0,
		34,35,5,5,0,0,35,36,5,3,0,0,36,37,5,4,0,0,37,9,1,0,0,0,2,13,21
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace Grammar.Assignment1
