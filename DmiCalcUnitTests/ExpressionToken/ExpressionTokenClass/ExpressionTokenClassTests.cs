using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DmiCalc;
using System.Linq;
using System.Threading.Tasks;
using DmiCalc.ExpressionTokens;

namespace DmiCalcUnitTests.ExpressionToken {
    [TestClass]
    public class ExpressionTokenClassTests {
        [TestMethod]
        public void GetAllValues() {
            var expressionTokenTypes = ExpressionTokenKind.GetAllValues();
            foreach (var element in expressionTokenTypes)
                System.Diagnostics.Trace.WriteLine(element);
            Assert.IsTrue(expressionTokenTypes.Count() > 0);

        }
        



        [TestMethod]
        public void TestCompareTwoValues() {
            ExpressionTokenKind expressionTokenKind = "Matrix";
            var expressionTokenTypes = ExpressionTokenKind.GetAllValues();
            foreach (var element in expressionTokenTypes)
                System.Diagnostics.Trace.WriteLine(element);
            Assert.IsTrue(expressionTokenTypes.Count() > 1 && expressionTokenTypes.Any(x=> (x == "Matrix")));
        }

    }
}
