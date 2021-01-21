using DmiCalc.ExpressionTokens;
using System;

namespace DmiCalc.ExpressionTree {
    public class VariableExpressionNodeConverter : IExpressionNodeConverter {

        public IVariablesProvider VariableGetter;
        public Tuple<bool, ExpressionNode> TryConvertToNode(ExpressionToken expressionToken) {
            ExpressionNode result = null;
            if (expressionToken != null && expressionToken.ExpressionTokenKind == ExpressionTokenKind.Variable &&
                (VariableGetter?.HasVariable(expressionToken.ExpressionTokenValue.ToString()) ?? false)) {
                result = new ValueExpressionNode() {
                    NodeValue = VariableGetter.GetVariable(expressionToken.ExpressionTokenValue.ToString()).VariableValue
                };
            }
            return Tuple.Create(result != null, result);
        }

    }
}
