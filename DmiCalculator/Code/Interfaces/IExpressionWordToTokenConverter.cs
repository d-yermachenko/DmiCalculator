using DmiCalc.ExpressionTokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.Interfaces {
    public interface IExpressionWordToTokenConverter {
        ExpressionToken ToExpressionToken(ExpressionWord expressionWord);

    }
}
