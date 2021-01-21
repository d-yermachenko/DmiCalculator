using System;
using System.Linq;
using System.Text;

namespace DmiCalc {
    public class NameExpressionWord : ExpressionWord {

        private static readonly char[] AdditionalAllowedInVariableChars = new char[] { '_' };

        private int FinalizeExpressionToken(string expression, int indent) {
            int result = -1; 
            StringBuilder tokenValue = new StringBuilder();
            int currentIndex = indent;
            Func<char, bool> condition = FirstCharCondition;
            while (currentIndex < expression.Length && condition.Invoke(expression[currentIndex])) {
                tokenValue.Append(expression[currentIndex]);
                currentIndex++;
                condition = FollowingsCharsCondition;
            }
            if (!String.IsNullOrEmpty(tokenValue.ToString())) {
                ExpressionWordString = tokenValue.ToString();
                NextCursorPosition = tokenValue.Length + indent;
            }
            return result;

        }

        public static ExpressionWordResult TryCreateToken(string expression, int indent) {
           if(expression.Length > indent && FirstCharCondition.Invoke(expression[indent])){
                NameExpressionWord result = new NameExpressionWord();
                result.FinalizeExpressionToken(expression, indent);
                return new ExpressionWordResult(true, result);
            }
           else
                return new ExpressionWordResult(false, null);
        }

        private static Func<char, bool> FirstCharCondition = (char character) => {
            return Char.IsLetter(character) || AdditionalAllowedInVariableChars.Contains(character);
        };

        private static Func<char, bool> FollowingsCharsCondition = (char character) => {
            return Char.IsLetterOrDigit(character) || AdditionalAllowedInVariableChars.Contains(character);
        };

        public override ExpressionWordResult TryCreateExpressionToken(string expression, int indent) {
            return TryCreateToken(expression, indent);
        }
    }
}