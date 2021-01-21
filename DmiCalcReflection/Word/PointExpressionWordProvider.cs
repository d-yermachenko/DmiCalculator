using DmiCalc;
using DmiCalcReflection.Word;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DmiCalcReflection {
    public class PointExpressionWordProvider: DmiCalc.Interfaces.IExpressionWordsProvider {

        public CultureInfo Culture { get; private set; }
        public PointExpressionWordProvider(CultureInfo cultureInfo) {
            Culture = cultureInfo;
        }

        public IEnumerable<Func<string, int, ExpressionWordResult>> GetExpressionWordsDetectors() {
            List<Func<string, int, ExpressionWordResult>> detectors = new List<Func<string, int, ExpressionWordResult>> {
                new ReflectionNumberExpressionWord(){Culture = this.Culture }.TryCreateExpressionToken,
                new PointExpressionWord().TryCreateExpressionToken,
                BracketExpressionWord.TryCreateToken,
                OperatorExpressionWord.TryCreateToken,
                NameExpressionWord.TryCreateToken,
                WhitespaceExpressionWord.TryCreateToken,
                StringLiteralExpressionWord.TryCreateToken,
                new  ArgumentSeparatorExpressionWord(){Culture = this.Culture }.TryCreateExpressionToken
            };
            return detectors;
        }
    }
}
