using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcUnitTests.ServiceUnits {
    public class ExpressionNodeTreeVisualizer {
        public static void AddConsoleVisualisation(IList<DmiCalc.ExpressionTokens.ExpressionToken> expressionTokens, int indent,
            Action<int, DmiCalc.ExpressionTokens.ExpressionToken> action) {
            for(int i = 0; i < expressionTokens.Count; i++) {
                action(indent, expressionTokens[i]);
                if (expressionTokens[i].ChildExpressionTokens.Count > 0)
                    AddConsoleVisualisation(expressionTokens[i].ChildExpressionTokens, indent + 1, action);
            }
        }

        public static void AddConsoleVisualisation(IList<DmiCalc.ExpressionTree.ExpressionNode> expressionTokens, int indent,
            Action<int, DmiCalc.ExpressionTree.ExpressionNode> action) {
            for (int i = 0; i < expressionTokens.Count; i++) {
                action(indent, expressionTokens[i]);
                if (expressionTokens[i].ChildNodes.Count > 0)
                    AddConsoleVisualisation(expressionTokens[i].ChildNodes, indent + 1, action);
            }
        }

    }
}
