using DmiCalc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcUnitTests.Words {
    [TestClass]
    public class ExpressionWordName {

        [TestMethod]
        public void TestSimpleCase() {
            string expression = "35.4 * Variable1 * 1Variable * Function(85)*SomeAnswer";
            var negativeExpected1 = NameExpressionWord.TryCreateToken(expression, expression.IndexOf("1Variable"));
            var negativeExpected2 = NameExpressionWord.TryCreateToken(expression, expression.IndexOf("35.4"));
            var positiveExpected1 = NameExpressionWord.TryCreateToken(expression, expression.IndexOf("Variable1"));
            var positiveExpected2 = NameExpressionWord.TryCreateToken(expression, expression.IndexOf("Function"));
            var positiveExpected3 = NameExpressionWord.TryCreateToken(expression, expression.IndexOf("SomeAnswer"));
            var reponseResult = !negativeExpected1.Corresponds && !negativeExpected2.Corresponds 
                && positiveExpected1.Corresponds && positiveExpected2.Corresponds && positiveExpected3.Corresponds;
            bool valueResult = String.Equals("Variable1", positiveExpected1.Word.ExpressionWordString) &&
                String.Equals("Function", positiveExpected2.Word.ExpressionWordString) &&
                String.Equals("SomeAnswer", positiveExpected3.Word.ExpressionWordString);
            Assert.IsTrue(reponseResult && valueResult);
        }

    }
}
