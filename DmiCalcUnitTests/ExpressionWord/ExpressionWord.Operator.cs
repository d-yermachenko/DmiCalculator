using DmiCalc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcUnitTests.Words {
    [TestClass]
    public class ExpressionWordOperator {

        [TestMethod]
        public void OperatorExpressionTokenTest1() {
            string expression1ToTest = "25.694*4178.52>758<>88";
            OperatorExpressionWord factoryToken = new OperatorExpressionWord();
            var answer1 = factoryToken.TryCreateExpressionToken(expression1ToTest, expression1ToTest.IndexOf("*"));
            var answer2 = factoryToken.TryCreateExpressionToken(expression1ToTest, expression1ToTest.IndexOf("758"));
            var answer3 = factoryToken.TryCreateExpressionToken(expression1ToTest, expression1ToTest.IndexOf("<>"));
            bool positionAnswers = answer1.Corresponds && !answer2.Corresponds && answer3.Corresponds && answer3.Word.ExpressionWordString.Equals("<>");
            bool cursorAnswers = (expression1ToTest[answer1.Word.NextCursorPosition] == '4') &&
                (expression1ToTest[answer3.Word.NextCursorPosition] == '8');
            Assert.IsTrue(positionAnswers && cursorAnswers);

        }


    }
}
