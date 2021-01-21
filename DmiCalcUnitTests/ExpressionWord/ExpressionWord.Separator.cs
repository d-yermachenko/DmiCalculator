using DmiCalc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DmiCalcUnitTests.Words {
    [TestClass]
    public class ExpressionWordsComposer {

        [TestMethod]
        public void TestExpressionTokenComposer() {
            string expression = "8+4 <> ' Some string'!=SomeName";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            var result = composer.ParseExpression(expression);
            Assert.AreEqual(result.Count, 9);
        }


        [TestMethod]
        public void TestExpressionTokenComposerNegativeNumber() {
            string expression = "8+4*-4(5+9)"; 
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            var result = composer.ParseExpression(expression);
            Assert.AreEqual(result[4].ExpressionWordString, "-");
        
           
        }
    }
}
