using System;
using System.Collections.Generic;

namespace DmiCalc {
    public class OperatorExpressionWord : ExpressionWord {

        readonly static string[] compareOperators = new string[] { "<=", "<", ">", ">=", "!=", "<>", "==" };
        readonly static string[] logicalOperators = new string[] { "!", "NOT ", "&&", "&", "AND ", "||", "|", "OR ", "^|", " XOR " };
        readonly static string[] mathOperators = new string[] { "+", "-", "/", "*", "^" };


        static string[] _AllOperatorsArray = null;

        private static string[] GetAllOperatorsArray() {
            if (_AllOperatorsArray?.Length > 0)
                return _AllOperatorsArray;
            List<string> allOperators = new List<string>();
            allOperators.AddRange(compareOperators);
            allOperators.AddRange(logicalOperators);
            allOperators.AddRange(mathOperators);
            _AllOperatorsArray = allOperators.ToArray();
            return _AllOperatorsArray;
        }

        private int FinalizeExpressionToken(string expression, int indent) {
            ExpressionWordString = expression?.Substring(indent)?.GetLongestPossibleBeginning(GetAllOperatorsArray());
            NextCursorPosition = (ExpressionWordString?.Length ?? 0) + indent;
            return NextCursorPosition;
        }



        public static ExpressionWordResult TryCreateToken(string expression, int indent) {
            var beginning = expression?.Substring(indent)?.GetLongestPossibleBeginning(GetAllOperatorsArray());
            if (!String.IsNullOrEmpty(beginning)) {
                OperatorExpressionWord expressionToken = new OperatorExpressionWord();
                expressionToken.FinalizeExpressionToken(expression, indent);
                return new ExpressionWordResult(true, expressionToken);
            }
            return new ExpressionWordResult(false, null);
        }

        public override ExpressionWordResult TryCreateExpressionToken(string expression, int indent) =>  TryCreateToken(expression, indent);
        
    }
}