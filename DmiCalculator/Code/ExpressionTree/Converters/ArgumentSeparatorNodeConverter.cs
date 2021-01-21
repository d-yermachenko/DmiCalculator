using System;
using System.Collections.Generic;
using System.Text;
using DmiCalc.ExpressionTokens;

namespace DmiCalc.ExpressionTree {
    public class ArgumentSeparatorExpressionNodeConverter : IExpressionNodeConverter {
        public Tuple<bool, ExpressionNode> TryConvertToNode(ExpressionToken expressionToken) {
            ExpressionNode result = null;
            if (expressionToken != null && (expressionToken.ExpressionTokenKind == ExpressionTokenKind.FunctionArgumentSeparator ||
                expressionToken.ExpressionTokenKind == ExpressionTokenKind.IntermediateBracketOpen) ) {
                result = new ArgumentSeparatorExpressionNode();
            }
            return Tuple.Create(result != null, result);
        }
    }
}
