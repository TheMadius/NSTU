//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from READ.g4 by ANTLR 4.9.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="READParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public interface IREADListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="READParser.prog"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProg([NotNull] READParser.ProgContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="READParser.prog"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProg([NotNull] READParser.ProgContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="READParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpr([NotNull] READParser.ExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="READParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpr([NotNull] READParser.ExprContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="READParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerm([NotNull] READParser.TermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="READParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerm([NotNull] READParser.TermContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="READParser.oper"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOper([NotNull] READParser.OperContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="READParser.oper"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOper([NotNull] READParser.OperContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="READParser.fmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFmt([NotNull] READParser.FmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="READParser.fmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFmt([NotNull] READParser.FmtContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="READParser.intg"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIntg([NotNull] READParser.IntgContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="READParser.intg"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIntg([NotNull] READParser.IntgContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="READParser.id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterId([NotNull] READParser.IdContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="READParser.id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitId([NotNull] READParser.IdContext context);
}