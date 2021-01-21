using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.ExpressionTokens {

    /// <summary>
    /// Classifies expression word and manages subnodes
    /// </summary>
    public class ExpressionToken {

        public ExpressionToken() {
            ExpressionTokenKind = ExpressionTokenKind.Undefined;
            ChildExpressionTokens = new List<ExpressionToken>();
        }

        /// <summary>
        /// Kind of expression token
        /// </summary>
        public ExpressionTokenKind ExpressionTokenKind { get; set; }


        /// <summary>
        /// Value of expression token: number, word, operator
        /// </summary>
        public object ExpressionTokenValue { get; set; }

        /// <summary>
        /// Brace-defined level of calc priority
        /// Greater is more prioritized
        /// </summary>
        public IList<ExpressionToken> ChildExpressionTokens { get; internal set; }


        /// <summary>
        /// Gets neighbord expession token and suppresses IndexOutOfRange exception
        /// </summary>
        /// <param name="expression">List of tockens which represents expression</param>
        /// <param name="position">position to take the neighbords</param>
        /// <param name="indent">indent. Use negative numbers to take previous tokens</param>
        /// <returns></returns>
        public static ExpressionToken GetNeighbordExpressionToken(IList<ExpressionToken> expression, int position, int indent) {
            int index = position + indent;
            if (index >= 0 && expression.Count >= 1 && index < expression.Count)
                return expression[index];
            else
                return null;
        }
    }
}
