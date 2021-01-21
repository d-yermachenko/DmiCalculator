using DmiCalc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcUnitTests.Words {
    [TestClass]
    public class ExpressionWordWhitespace {

        [TestMethod]
        public void TestWhiteSpaceFormulaBasic() {
            string expression = "8 :2 (  \t2+2)";
            var positiveExpressionToken1 = WhitespaceExpressionWord.TryCreateToken(expression, 1);
            var positiveExpressionToken2 = WhitespaceExpressionWord.TryCreateToken(expression, 6);
            Assert.IsTrue(positiveExpressionToken1.Corresponds && positiveExpressionToken1.Corresponds 
                && expression[positiveExpressionToken2.Word.NextCursorPosition] == '2');
        }
    }
}
