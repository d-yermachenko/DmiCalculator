using DmiCalc;
using System;

namespace DmiCalcReflection.Word {
    public class PointExpressionWord : DmiCalc.ExpressionWord {

        public override ExpressionWordResult TryCreateExpressionToken(string expression, int indent) {
            if (expression[indent] == '.') {
                int position = indent + 1;
                return  new ExpressionWordResult(true, new PointExpressionWord() { ExpressionWordString = ".", NextCursorPosition = position } );
            }
            return new ExpressionWordResult(false, null);
        }
    }
}
