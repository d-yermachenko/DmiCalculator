using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc {
    public interface ISemanticsTokenTransformer {
        IList<ExpressionTokens.ExpressionToken> TransformTokens(IList<ExpressionTokens.ExpressionToken> expressionTokens);
    }
}
