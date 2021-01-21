using DmiCalc;
using DmiCalc.ExpressionTokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.BasicCalc {
    public class CalculationData {
        public IList<ExpressionWord> ExpressionWords { get; set; }

        public IList<ExpressionToken> ExpressionTokens { get; set; }

        public dynamic ExpressionResult;

        public Exception InnerException;
    }
}
