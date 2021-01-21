using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.ExpressionTree {
    interface IExpressionNodeConverter {
        Tuple<bool, ExpressionTree.ExpressionNode> TryConvertToNode(ExpressionTokens.ExpressionToken expressionToken);
    }
}
