using DmiCalc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcUnitTests.Words {
    [TestClass]
    public class ExpressionWordBracket {

        [TestMethod, TestCategory("BracketExpressionToken")]
        public void TestBasicExpressionBracket() {
            string expression = "8:2(2+2)";
            var negativeToken = BracketExpressionWord.TryCreateToken(expression, 0);
            var positiveExpressionToken = BracketExpressionWord.TryCreateToken(expression, 3);
            Assert.IsTrue(!negativeToken.Corresponds && positiveExpressionToken.Corresponds);
        }
        
    }
}
