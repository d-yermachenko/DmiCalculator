using System;
using System.Linq;

namespace DmiCalc {
    public class StringLiteralExpressionWord : ExpressionWord {

        public static Tuple<int, string> GetQuotedStringFromQuotePosition(string s, int startPosition, bool useDoubleQuoteAsEscape = true, char[] quoteChars = null, char[] escapeChars = null) {
            if (quoteChars == null)
                quoteChars = new char[] { '"' };
            if (escapeChars == null)
                escapeChars = new char[] { '\\' };
            int currentPosition = startPosition;
            bool continueExecution = startPosition < s.Length;
            var stringBuilder = new System.Text.StringBuilder();
            while (continueExecution) {
                char currentChar = s[currentPosition];
                char? nextChar = null;
                if (currentPosition < s.Length - 1)
                    nextChar = s[currentPosition + 1];
                bool isCurrentCharEscapeChar = escapeChars.Contains(currentChar) // if this character escape char
                    || (quoteChars.Contains(currentChar) && useDoubleQuoteAsEscape && nextChar != null && quoteChars.Contains((char)nextChar)); // or double quote also escape char
                if (isCurrentCharEscapeChar) {
                    stringBuilder.Append((char)nextChar);
                    currentPosition += 2;
                }
                else if (quoteChars.Contains(currentChar)) {
                    continueExecution = false;
                }
                else {
                    stringBuilder.Append(currentChar);
                    currentPosition += 1;
                }


            }
            string result = stringBuilder.ToString();
            int cursorNextPosition = currentPosition + 1;
            return Tuple.Create(cursorNextPosition, result);
        }

        public static readonly char[] quotesChars = new char[] { '\'', '"' };

        private int FinalizeExpressionToken(string expression, int indent) {
            if (expression.Length > indent + 2 && quotesChars.Contains(expression[indent])) {
                char usedQuoteChar = expression[indent];
                var stringLiteralValue = GetQuotedStringFromQuotePosition(expression, indent + 1, quoteChars: new char[] { usedQuoteChar });
                ExpressionWordString = stringLiteralValue.Item2;
                NextCursorPosition = stringLiteralValue.Item1;
                return stringLiteralValue.Item1;
            }
            else
                return 0;
        }

        public static ExpressionWordResult TryCreateToken(string expression, int indent) {
            if (expression.Length > indent + 2 && quotesChars.Contains(expression[indent])) {
                StringLiteralExpressionWord result = new StringLiteralExpressionWord();
                result.FinalizeExpressionToken(expression, indent);
                return new ExpressionWordResult(true, result);
            }
            else return new ExpressionWordResult(false, null);
        }

        public override ExpressionWordResult TryCreateExpressionToken(string expression, int indent) {
            return TryCreateToken(expression, indent);
        }
    }
}