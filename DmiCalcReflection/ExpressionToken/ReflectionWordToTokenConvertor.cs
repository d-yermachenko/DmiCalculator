using DmiCalc;
using DmiCalc.ExpressionTokens;
using DmiCalc.Interfaces;
using DmiCalcReflection.Word;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcReflection.ExpressionToken {
    public class ReflectionWordToTokenConvertor : ExpressionWordToTokenConvertor, IExpressionWordToTokenConverter {

        public override DmiCalc.ExpressionTokens.ExpressionToken ToExpressionToken(ExpressionWord word) {
            return ToExpressionToken(word as dynamic);
        }
        public DmiCalc.ExpressionTokens.ExpressionToken ToExpressionToken(PointExpressionWord expressionWord) {
            return new DmiCalc.ExpressionTokens.ExpressionToken() {
                ExpressionTokenKind = ReflectionOperationTokenKind.ReflectionOperation,
                ExpressionTokenValue = expressionWord.ExpressionWordString
            };
        }


    }
}
