using System;
using System.Collections.Generic;
using System.Text;
using DmiCalc;

namespace DmiCalc.Interfaces {
    public interface IExpressionWordsProvider {
        IEnumerable<Func<string, int, ExpressionWordResult>> GetExpressionWordsDetectors();

    }
}
