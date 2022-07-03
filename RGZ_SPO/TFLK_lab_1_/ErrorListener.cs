using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;


namespace TFLK_lab_1_
{

    public class SyntaxError
    {
        public TextWriter output;
        public IRecognizer recognizer;
        public IToken offendingSymbol;
        public int line;
        public int charPositionInLine;
        public string msg;
        public RecognitionException e;

        public SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            this.output = output;
            this.recognizer = recognizer;
            this.offendingSymbol = offendingSymbol;
            this.line = line;
            this.charPositionInLine = charPositionInLine;
            this.e = e;
            this.msg = msg;
        }

        public override string ToString() 
        {
            string str = "Msg: ";
            str += msg;
            str += ", in line " + line;
            str += ", char position in line " + charPositionInLine;

            return str;
        }
    }

    public class SyntaxErrorListener : BaseErrorListener
    {
        List<SyntaxError> syntaxErrors;

        public List<SyntaxError> SyntaxErrors { get => syntaxErrors;}

        public SyntaxErrorListener()
        {
            syntaxErrors = new List<SyntaxError>();
        }

        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            syntaxErrors.Add(new SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e));
        }
    }

    public class SyntaxErrorListenerLexer : IAntlrErrorListener<int>
    {
        List<SyntaxError> syntaxErrors;

        public List<SyntaxError> SyntaxErrors { get => syntaxErrors; }

        public SyntaxErrorListenerLexer()
        {
            syntaxErrors = new List<SyntaxError>();
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            syntaxErrors.Add(new SyntaxError(output, recognizer, null, line, charPositionInLine, msg, e));
        }
    }
}
