using DmiCalc;
using DmiCalc.ExpressionTokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DmiCalcUnitTests.ExpressionToken
{
    [TestClass]
    public class ExpressionTokenTests
    {
        [TestMethod]
        public void TestExpressionTokenConverter() {
            string expression = "8+4*-4(5+9)";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(new CultureInfo("fr-FR")).GetExpressionWordsDetectors());
            var expressionSentence = composer.ParseExpression(expression);
            var tokenConvertor = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());
            var tokensList = tokenConvertor.ConvertWordsToTokens(expressionSentence, ExpressionTokenBuilder.GetDefaultTransformers());
            Assert.AreEqual(tokensList[4].ExpressionTokenValue, -4L);
        }

        [TestMethod]
        public void TestExpressionTokenWorlds() {
            string expression = "8*4-((15-10)*3)";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(new CultureInfo("fr-FR")).GetExpressionWordsDetectors());
            var expressionSentence = composer.ParseExpression(expression);
            var tokenConvertor = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());
            var tokensList = tokenConvertor.ConvertWordsToTokens(expressionSentence, ExpressionTokenBuilder.GetDefaultTransformers());
            for (int i = 0; i < tokensList.Count; i++) {
                System.Diagnostics.Trace.WriteLine(tokensList[i].ExpressionTokenKind + ": " + tokensList[i].ExpressionTokenValue);
            }
        }



    }
}
