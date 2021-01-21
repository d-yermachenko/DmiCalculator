using System.Collections.Generic;

namespace DmiCalc.ExpressionTokens.Transformers {
    public class WhiteSpaceRemoverTransformer : ISemanticsTokenTransformer {
        public IList<ExpressionToken> TransformTokens(IList<ExpressionToken> expressionTokens) {
            List<ExpressionToken> resultSet = new List<ExpressionToken>();
            foreach(ExpressionToken token in expressionTokens) {
                if (token != null)
                    resultSet.Add(token);
            }
            return resultSet;
        }
    }
}
