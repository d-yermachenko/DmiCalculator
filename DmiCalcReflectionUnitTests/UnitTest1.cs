using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReflectionCalcUnitTests {
    [TestClass]
    public class ReflectionCalcPoCTests {
        [TestMethod]
        public void TestLexemesSeparation()
        {
            DmiCalcReflection.ReflectionCalculator reflectionCalculator = new DmiCalcReflection.ReflectionCalculator(new DmiCalcReflection.Operations.ReflectionFunctionGetter());
            var words = reflectionCalculator.GetWords("12.58+SomeObject.Property.Property");
            Assert.AreEqual(7, words.Count);
        }

        [TestMethod]
        public void TestTokensSeparation() {
            DmiCalcReflection.ReflectionCalculator reflectionCalculator = new DmiCalcReflection.ReflectionCalculator(new DmiCalcReflection.Operations.ReflectionFunctionGetter());
            var tokens = reflectionCalculator.GetTokens("12.58+SomeObject.Property.Property");
            Assert.AreEqual(1, tokens.Count);
        }
    }
}
