using System;
using System.Linq;
using System.Text;

namespace DmiCalc {
    public class NumberExpressionWord : ExpressionWord {

        private static string[] GetAllowedChars(System.Globalization.CultureInfo cultureInfo) => new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", cultureInfo.NumberFormat.CurrencyDecimalSeparator };

        private int FinalizeExpressionToken(string expression, int indent) {
            StringBuilder valueBuilder = new StringBuilder();
            int currentIndex = indent;
            bool continueExecution = true;
            var allowedChars = GetAllowedChars(Culture);
            for (; currentIndex < expression.Length && continueExecution; currentIndex++) {
                continueExecution &= allowedChars.Contains(expression[currentIndex].ToString());
                if (continueExecution)
                    valueBuilder.Append(expression[currentIndex]);
            }
            ExpressionWordString = valueBuilder.ToString();
            NextCursorPosition = (ExpressionWordString?.Length ?? 0) + indent;
            return indent + ExpressionWordString?.Length??0;
        }

        private static ExpressionWordResult TryCreateToken(string expression, int indent, System.Globalization.CultureInfo cultureInfo) {
            if (cultureInfo == null) 
                cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            string[] allowedChars = GetAllowedChars(cultureInfo);
            bool canBeCreated = expression?.Substring(indent)?.StartsWith(allowedChars) ?? false;
            NumberExpressionWord word = null;
            if (canBeCreated) {
                word = new NumberExpressionWord();
                word.FinalizeExpressionToken(expression, indent);
            }
            return new ExpressionWordResult(canBeCreated, word);
        }


        public static ExpressionWordResult TryCreateToken(string expression, int indent) => TryCreateToken(expression, indent, null);
        public override ExpressionWordResult TryCreateExpressionToken(string expression, int indent) {
            return TryCreateToken(expression, indent, Culture);
        }

    }
}