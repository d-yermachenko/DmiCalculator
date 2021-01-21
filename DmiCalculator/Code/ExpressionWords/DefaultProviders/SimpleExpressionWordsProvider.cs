using DmiCalc.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DmiCalc {
    public class BasicWordsProvider : IExpressionWordsProvider {

        public BasicWordsProvider(CultureInfo culture) {
            Culture = culture;
        }

        public CultureInfo Culture { get; private set; }

        public IEnumerable<Func<string, int, ExpressionWordResult>> GetExpressionWordsDetectors() {
            List<Func<string, int, ExpressionWordResult>> detectors = new List<Func<string, int, ExpressionWordResult>> {
                new NumberExpressionWord(){Culture = this.Culture }.TryCreateExpressionToken,
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
