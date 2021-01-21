using DmiCalc.BasicCalc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalcUnitTests.BasicCalc {
    
    [TestClass]
    public class BasicCalcTests {

        [TestMethod]
        public void TwoPlusTwoTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result = basicCalculator.CalculateExpression("2+2");
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void AdditionMultiplicationTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result = basicCalculator.CalculateExpression("3*2*2-9/3-1");
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void AdditionMultiplicationBraketTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result = basicCalculator.CalculateExpression("3*2*(8-6)-9/3-1");
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void FishkiCalcTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpression("10/2*(4-2)");
            bool equals = System.Double.Equals(result1, 10D);
            Assert.AreEqual(result1, 10D);
        }

        [TestMethod]
        public void DoubleExpressionTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpression("10/2*(4-2)");
            bool equals = System.Double.Equals(result1, 10D);
            var result2 = basicCalculator.CalculateExpression("3*2*(8-6)-9/3-1");
            equals &= System.Double.Equals(result2, 8D);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void FactorialExpressionTest() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpression("10/2*(4!-2)");
            Assert.AreEqual(110L, result1);
        }

        [TestMethod]
        public void FacebookCalculationTest1() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpression("10-10*10+10");
            Assert.AreEqual(-80L, result1);
        }

        [TestMethod]
        public void BasicWrongExpression() {
            BasicCalculator basicCalculator = new BasicCalculator();
            var result1 = basicCalculator.CalculateExpression("10-10*10+10");
            Assert.AreEqual(-80L, result1);
        }


    }
}
