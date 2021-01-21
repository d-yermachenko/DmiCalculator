using DmiCalc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.ExpressionTokens.Transformers {
    public class VariableAndFunctionDistinctor : ISemanticsTokenTransformer {
        public IOperationsProvider FunctionGetter { get; set; }

        public IVariablesProvider VariableGetter { get; set; }

        public IList<ExpressionToken> TransformTokens(IList<ExpressionToken> expressionTokens) {
            for(int i = 0; i < expressionTokens.Count; i++) {
                var currentItem = expressionTokens[i];

                if (currentItem?.ExpressionTokenKind == ExpressionTokenKind.IntermediateName) {
                    var nextItem = ExpressionToken.GetNeighbordExpressionToken(expressionTokens, i, 1);
                    if (nextItem.ExpressionTokenKind == ExpressionTokenKind.IntermediateBracketOpen
                        && (FunctionGetter?.HasOperation(currentItem?.ExpressionTokenValue?.ToString())??false))
                        currentItem.ExpressionTokenKind = ExpressionTokenKind.Function;

                    if (VariableGetter?.HasVariable(currentItem?.ExpressionTokenValue?.ToString())??false)
                        currentItem.ExpressionTokenKind = ExpressionTokenKind.Variable;
                }

            }
            return expressionTokens;

        }
    }
}
