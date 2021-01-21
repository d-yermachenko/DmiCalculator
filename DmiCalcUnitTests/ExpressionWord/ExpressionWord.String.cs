using Microsoft.VisualStudio.TestTools.UnitTesting;
using DmiCalc;
using System;

namespace DmiCalcUnitTests.Words {
    [TestClass]
    public class ExpressionWordString {
        [TestMethod,
            TestCategory("StringToken.ComplexCase")]
        public void TestIsQuote() {
            string testString = "\"\"\"Test some inQuote message is it \\\"\"\" real \"\" good for nothing \"& \"Another\"\" message \"";
            int startIndex = 0;
            var result = StringLiteralExpressionWord.TryCreateToken(testString, startIndex);
            var isEquals = String.Equals(result.Word.ExpressionWordString, "\"Test some inQuote message is it \"\" real \" good for nothing ");
            var followingChar = testString[startIndex + result.Word.NextCursorPosition];
            Assert.IsTrue(isEquals && followingChar=='&');
        }

        [TestMethod,
         TestCategory("StringToken.ComplexCase")]
        public void TestStringInTheBeginningOfExpressionDoubleQuote() {
            string expressionToTest = "\"\\\"Some string with escape's chars\" + Variable2";
            var testResult = StringLiteralExpressionWord.TryCreateToken(expressionToTest, 0);
            Assert.IsTrue(testResult.Corresponds && String.Equals("\"Some string with escape's chars", testResult.Word.ExpressionWordString));

        }
        [TestMethod,
         TestCategory("StringToken.ComplexCase")]
        public void TestStringInTheEndOfExpressionDoubleQuote() {
            string expressionToTest = "Variable + 12 + \"\\\"Some string with escape's chars\"";
            var testResult = StringLiteralExpressionWord.TryCreateToken(expressionToTest, expressionToTest.IndexOf("\""));
            Assert.IsTrue(testResult.Corresponds && String.Equals("\"Some string with escape's chars", testResult.Word.ExpressionWordString));
        }

        [TestMethod,
         TestCategory("StringToken.ComplexCase")]
        public void TestStringInTheBeginningOfExpressionSimpleQuote() {
            string expressionToTest = "'''SomeString with escape''s character on output \\' and quote' + Variable1";
            var testResult = StringLiteralExpressionWord.TryCreateToken(expressionToTest, 0);
            Assert.IsTrue(testResult.Corresponds && String.Equals("'SomeString with escape's character on output ' and quote", testResult.Word.ExpressionWordString));
        }

        [TestMethod,
         TestCategory("StringToken.ComplexCase")]
        public void TestStringInTheEndOfExpressionSimpleQuote() {
            string expressionToTest = "Variable <>'''SomeString with escape''s character on output \\' and quote'";
            var testResult = StringLiteralExpressionWord.TryCreateToken(expressionToTest, expressionToTest.IndexOf("'"));
            Assert.IsTrue(testResult.Corresponds && String.Equals("'SomeString with escape's character on output ' and quote", testResult.Word.ExpressionWordString));
        }

    }
}
