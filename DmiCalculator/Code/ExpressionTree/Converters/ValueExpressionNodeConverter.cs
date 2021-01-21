using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DmiCalc.ExpressionTokens;

namespace DmiCalc.ExpressionTree {
    public class ValueExpressionNodeConverter : IExpressionNodeConverter {
        public Tuple<bool, ExpressionNode> TryConvertToNode(ExpressionToken expressionToken) {
            ExpressionNode result = null;
            ExpressionTokenKind[] valuesTypes = new ExpressionTokenKind[] { ExpressionTokenKind.BooleanValue , ExpressionTokenKind.DateValue ,
                ExpressionTokenKind.NumberValue , ExpressionTokenKind.StringLiteral };
            if (valuesTypes.Any(x => x == expressionToken.ExpressionTokenKind)) {
                result = new ValueExpressionNode() {
                    NodeValue = expressionToken.ExpressionTokenValue
                };
            }

            return Tuple.Create(result != null, result);
        }
    }
}
