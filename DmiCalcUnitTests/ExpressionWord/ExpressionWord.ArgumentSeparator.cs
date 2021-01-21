using DmiCalc;
using DmiCalc.ExpressionTokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DmiCalcUnitTests.Words {
    [TestClass]
    public class ExpressionWordArgumentSeparator {
        [TestMethod]
        public void GetArgumentSeparatorExemple() {
            string expression = "8*Foo(25; 12; 14)";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionSentence = composer.ParseExpression(expression);
            Assert.IsTrue(expressionSentence[5] is ArgumentSeparatorExpressionWord);
        }

    }
}
