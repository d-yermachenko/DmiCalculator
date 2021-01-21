using DmiCalc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcReflection.Word {
    public class ReflectionNumberExpressionWord : NumberExpressionWord {
        public override ExpressionWordResult TryCreateExpressionToken(string expression, int indent) {
            var result =  base.TryCreateExpressionToken(expression, indent);
            if (result.Corresponds && result?.Word?.ExpressionWordString == ".")
                return new ExpressionWordResult(false, null);
            return result;

        }
    }
}
