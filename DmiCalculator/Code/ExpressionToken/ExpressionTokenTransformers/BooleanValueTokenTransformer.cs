using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.ExpressionTokens.Transformers {
    public class BooleanValueTokenTransformer : ISemanticsTokenTransformer {

        public IList<ExpressionToken> TransformTokens(IList<ExpressionToken> expressionTokens) {
            for(int i = 0; i < expressionTokens.Count; i++) {
                var currentToken = expressionTokens[i];
                if (currentToken.ExpressionTokenKind == ExpressionTokenKind.IntermediateName) {
                    if (Boolean.TryParse(currentToken.ExpressionTokenValue?.ToString(), out bool boolValue)) {
                        currentToken.ExpressionTokenKind = ExpressionTokenKind.BooleanValue;
                        currentToken.ExpressionTokenValue = boolValue;
                    }
                }
            }
            return expressionTokens;
        }
    }
}
