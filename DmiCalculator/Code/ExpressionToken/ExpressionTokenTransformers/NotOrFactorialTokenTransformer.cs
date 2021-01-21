using DmiCalc.ExpressionTokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.ExpressionTokens.Transformers {
    public class NotOrFactorialTokenTransformer : ISemanticsTokenTransformer {
        public IList<ExpressionToken> TransformTokens(IList<ExpressionToken> expressionTokens) {
            for(int i = 0; i < expressionTokens.Count; i++) {
                ExpressionToken currentToken = expressionTokens[i];
                if (currentToken.ExpressionTokenKind == ExpressionTokenKind.IntermediateExclamation) {
                    var nextToken = ExpressionToken.GetNeighbordExpressionToken(expressionTokens, i, 1);
                    if (nextToken != null && (nextToken.ExpressionTokenKind == ExpressionTokenKind.BooleanValue
                        || nextToken.ExpressionTokenKind == ExpressionTokenKind.IntermediateBracketOpen
                        || nextToken.ExpressionTokenKind == ExpressionTokenKind.IntermediateName
                        || nextToken.ExpressionTokenKind == ExpressionTokenKind.Function
                        || nextToken.ExpressionTokenKind == ExpressionTokenKind.Variable)) {
                        currentToken.ExpressionTokenKind = ExpressionTokenKind.UnaryPrefixOperatorNot;
                    }
                    else {
                        var previousToken = ExpressionToken.GetNeighbordExpressionToken(expressionTokens, i, -1);
                        if (previousToken != null && (
                            previousToken.ExpressionTokenKind == ExpressionTokenKind.IntermediateBracketClosed
                            || previousToken.ExpressionTokenKind == ExpressionTokenKind.Variable
                            || previousToken.ExpressionTokenKind == ExpressionTokenKind.NumberValue
                            || previousToken.ExpressionTokenKind == ExpressionTokenKind.IntermediateName
                            ))
                            currentToken.ExpressionTokenKind = ExpressionTokenKind.UnaryPostfixOperatorFactorial;
                        else
                            throw new Exceptions.DmiCalcException(Exceptions.DmiCalcExceptionCodes.CantIdentiFyExplanationOperation, new object[] { currentToken });
                    }
                }
            }
            return expressionTokens;
        }
    }
}
