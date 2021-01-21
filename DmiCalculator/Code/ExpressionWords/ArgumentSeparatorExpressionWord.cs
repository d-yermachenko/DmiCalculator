using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DmiCalc
{
    public class ArgumentSeparatorExpressionWord : ExpressionWord {

        public char[] ConventionalSeparatorChars { get; protected set; } = new char[] { ';' };

        private bool IsSeparatorChar(char separator) {
            return ConventionalSeparatorChars.Contains(separator);
        }

        private int FindExpressionTokenPosition(string expression, int indent) {
            int result = -1;
            StringBuilder tokenValue = new StringBuilder();
            int currentIndex = indent;
            Func<char, bool> condition = IsSeparatorChar;
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


        public override ExpressionWordResult TryCreateExpressionToken(string expression, int indent) {
            if (expression.Length > indent && IsSeparatorChar(expression[indent])) {
                ArgumentSeparatorExpressionWord result = new ArgumentSeparatorExpressionWord();
                result.FindExpressionTokenPosition(expression, indent);
                return new ExpressionWordResult(true, result);
            }
            else
                return new ExpressionWordResult(false, null);
        }
    }
}
