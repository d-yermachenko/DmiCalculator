using DmiCalc.BasicCalc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcUnitTests.BasicCalc {
    
    [TestClass]
    public class BasicCalcAsyncTests {

        [TestMethod]
        public void TwoPlusTwoTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result = basicCalculator.CalculateExpressionAsync("2+2");
            result.Wait();
            Assert.AreEqual(4, result.Result);
        }

        [TestMethod]
        public void AdditionMultiplicationTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result = basicCalculator.CalculateExpressionAsync("3*2*2-9/3-1");
            result.Wait();
            Assert.AreEqual(8, result.Result);
        }

        [TestMethod]
        public void AdditionMultiplicationBraketTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result = basicCalculator.CalculateExpressionAsync("3*2*(8-6)-9/3-1");
            result.Wait();
            Assert.AreEqual(8, result.Result);
        }

        [TestMethod]
        public void FishkiCalcTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpressionAsync("10/2*(4-2)");
            result1.Wait();
            Assert.AreEqual(result1.Result, 10D);
        }

        [TestMethod]
        public void DoubleExpressionTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpressionAsync("10/2*(4-2)");
            result1.Wait();
            bool equals = System.Double.Equals(result1.Result, 10D);
            var result2 = basicCalculator.CalculateExpressionAsync("3*2*(8-6)-9/3-1");
            result2.Wait();
            equals &= System.Double.Equals(result2.Result, 8D);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void FactorialExpressionTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpressionAsync("10/2*(4!-2)");
            result1.Wait();
            Assert.AreEqual(110L, result1.Result);
        }

        [TestMethod]
        public void FacebookCalculationTest1() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpressionAsync("10-10*10+10");
            result1.Wait();
            Assert.AreEqual(-80L, result1.Result);
        }

        [TestMethod]
        public void BasicWrongExpression() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpressionAsync("10-10*10+10");
            result1.Wait();
            Assert.AreEqual(-80L, result1.Result);
        }


        [TestMethod]
        public void AbsExpressionBasic() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpressionAsync("abs(-5)");
            result1.Wait();
            Assert.AreEqual(5, result1.Result);
        }

        [TestMethod]
        public void AbsExpressionOfResult() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpressionAsync("abs(8-4)");
            result1.Wait();
            Assert.AreEqual(4, result1.Result);
        }



    }
}
