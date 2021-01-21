using System;
using System.Text;

namespace DmiCalc {
    public class WhitespaceExpressionWord : ExpressionWord {

        private int FinalizeExpressionToken(string expression, int indent) {
            int result = -1;
            StringBuilder tokenValue = new StringBuilder();
            int currentIndex = indent;
            Func<char, bool> condition = Char.IsWhiteSpace;
            while (currentIndex < expression.Length && condition.Invoke(expression[currentIndex])) {
                tokenValue.Append(expression[currentIndex]);
                currentIndex++;
            }
            if (!String.IsNullOrEmpty(tokenValue.ToString())) {
                ExpressionWordString = tokenValue.ToString();
                NextCursorPosition = tokenValue.Length + indent;
            }
            return result;
        }

        public static ExpressionWordResult TryCreateToken(string expression, int indent) {
            if (expression.Length > indent && Char.IsWhiteSpace(expression[indent])) {
                WhitespaceExpressionWord result = new WhitespaceExpressionWord();
                result.FinalizeExpressionToken(expression, indent);
                return new ExpressionWordResult(true, result);
            }
            else
                return new ExpressionWordResult(false, null);
        }

        public override ExpressionWordResult TryCreateExpressionToken(string expression, int indent) => TryCreateToken(expression, indent);
    }
}