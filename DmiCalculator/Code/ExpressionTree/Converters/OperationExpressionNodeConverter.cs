using DmiCalc.ExpressionTokens;
using System;
using System.Linq;

namespace DmiCalc.ExpressionTree {
    public class OperationExpressionNodeConverter : IExpressionNodeConverter {
        public IOperationsProvider OperationsGetter { get; set; }
        public Tuple<bool, ExpressionTree.ExpressionNode> TryConvertToNode(ExpressionToken expressionToken) {
            ExpressionTree.ExpressionNode result = null;
            ExpressionTokenKind[] operators = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorAddition, ExpressionTokenKind.BinaryOperatorMultiplication,
            ExpressionTokenKind.BinaryOperatorRelation, ExpressionTokenKind.Function, ExpressionTokenKind.UnaryPostfixOperatorFactorial, ExpressionTokenKind.UnaryPrefixOperatorNot,
            ExpressionTokenKind.UnaryPrefixOperatorNot};
            if (operators.Any(x=>x == expressionToken.ExpressionTokenKind)) {
                    result = new OperationExpressionNode() {
                        Operation = OperationsGetter.GetOperation(expressionToken.ExpressionTokenValue.ToString())
                    };
            }
            return Tuple.Create(result != null, result);
        }
    }
}
