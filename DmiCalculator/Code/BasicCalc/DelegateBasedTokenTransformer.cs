using System;
using System.Collections.Generic;
using DmiCalc.ExpressionTokens;

namespace DmiCalc.BasicCalc {
    public class DelegateBasedTokenTransformer : ISemanticsTokenTransformer {

        public Func<IList<DmiCalc.ExpressionTokens.ExpressionToken>, ExpressionTokenKind[], IList<ExpressionToken>> OperatorTreeTransformer { get; set; }

        public ExpressionTokenKind[] TokenKinds { get; set; }

        public IList<ExpressionToken> TransformTokens(IList<ExpressionToken> expressionTokens) {
            return OperatorTreeTransformer?.Invoke(expressionTokens, TokenKinds);
        }


    }
}
