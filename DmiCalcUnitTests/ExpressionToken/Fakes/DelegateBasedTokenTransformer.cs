using DmiCalc;
using DmiCalc.ExpressionTokens;
using System;
using System.Collections.Generic;

namespace DmiCalcUnitTests.ExpressionToken.Fakes {
    public class DelegateBasedTokenTransformer : ISemanticsTokenTransformer {

        public Func<IList<DmiCalc.ExpressionTokens.ExpressionToken>, ExpressionTokenKind[], IList<DmiCalc.ExpressionTokens.ExpressionToken>> OperatorTreeTransformer { get; set; }

        public ExpressionTokenKind[] OperatorTokenKinds { get; set; }

        public IList<DmiCalc.ExpressionTokens.ExpressionToken> TransformTokens(IList<DmiCalc.ExpressionTokens.ExpressionToken> expressionTokens) {
            return OperatorTreeTransformer?.Invoke(expressionTokens, OperatorTokenKinds);
        }


    }

    
}
