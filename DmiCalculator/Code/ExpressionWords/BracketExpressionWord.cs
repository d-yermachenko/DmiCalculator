using System;

namespace DmiCalc {
    public class BracketExpressionWord : ExpressionWord {

        readonly static string[] compareOperators = new string[] { "(", ")", "[", "]", "{", "}" };

        private int FinalizeExpressionToken(string expression, int indent) {
            ExpressionWordString = expression?.Substring(indent)?.GetLongestPossibleBeginning(compareOperators);
            NextCursorPosition = (ExpressionWordString?.Length ?? 0) + indent;
            return NextCursorPosition;
        }


        public static ExpressionWordResult TryCreateToken(string expression, int indent) {
            bool canCreateTocken = !String.IsNullOrEmpty(expression) &&
                expression.Length > indent && expression.Substring(indent).StartsWith(compareOperators);
            BracketExpressionWord token = null;
            if (canCreateTocken) {
                token = new BracketExpressionWord();
                token.FinalizeExpressionToken(expression, indent);
            }
            return new ExpressionWordResult(canCreateTocken, token);
        }

        public override ExpressionWordResult TryCreateExpressionToken(string expression, int indent) =>  TryCreateToken(expression, indent);

    }
}