using DmiCalc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcUnitTests.Words {
    [TestClass]
    public class ExpressionWordNumber {


        [TestMethod]
        public void NumberExpressionTokenTestCreationTest1() {
            string expression1ToTest = "25.694 * 4178.52";
            NumberExpressionWord numberExpressionToken = new NumberExpressionWord();
            var firstAnswer = numberExpressionToken.TryCreateExpressionToken(expression1ToTest, 0);
            var secondAnswer = numberExpressionToken.TryCreateExpressionToken(expression1ToTest, expression1ToTest.IndexOf(" *"));
            var thirdAnswer = numberExpressionToken.TryCreateExpressionToken(expression1ToTest, expression1ToTest.IndexOf("*"));
            var fourthAnswer = numberExpressionToken.TryCreateExpressionToken(expression1ToTest, expression1ToTest.IndexOf("4178.52"));
            Assert.IsTrue(firstAnswer.Corresponds && !secondAnswer.Corresponds && !thirdAnswer.Corresponds && fourthAnswer.Corresponds);


        }
    }
}
