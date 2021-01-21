using DmiCalc;
using DmiCalc.ExpressionTokens;
using System;
using System.Collections.Generic;
using System.Text;


namespace DmiCalcReflection.ExpressionToken.Transformers {
    public class VariableOrFunctionOrPropertyTransformer : ISemanticsTokenTransformer {
        public IOperationsProvider FunctionGetter { get; set; }

        public IVariablesProvider VariableGetter { get; set; }

        public IList<DmiCalc.ExpressionTokens.ExpressionToken> TransformTokens(IList<DmiCalc.ExpressionTokens.ExpressionToken> expressionTokens) {
            for (int i = 0; i < expressionTokens.Count; i++) {
                var currentItem = expressionTokens[i];

                if (currentItem?.ExpressionTokenKind == ExpressionTokenKind.IntermediateName) {
                    var nextItem = DmiCalc.ExpressionTokens.ExpressionToken.GetNeighbordExpressionToken(expressionTokens, i, 1);
                    if (nextItem?.ExpressionTokenKind == ExpressionTokenKind.IntermediateBracketOpen
                        && (FunctionGetter?.HasOperation(currentItem?.ExpressionTokenValue?.ToString()) ?? false))
                        currentItem.ExpressionTokenKind = ExpressionTokenKind.Function;

                    if (VariableGetter?.HasVariable(currentItem?.ExpressionTokenValue?.ToString()) ?? false)
                        currentItem.ExpressionTokenKind = ExpressionTokenKind.Variable;
                    if(currentItem?.ExpressionTokenKind == ExpressionTokenKind.IntermediateName){
                        var previousItem = DmiCalc.ExpressionTokens.ExpressionToken.GetNeighbordExpressionToken(expressionTokens, i, -1);
                        if (previousItem?.ExpressionTokenKind == ReflectionOperationTokenKind.ReflectionOperation)
                            currentItem.ExpressionTokenKind = ReflectionOperationTokenKind.ReflectionProperty;

                    }
                }

            }
            return expressionTokens;
        }
    }
}
