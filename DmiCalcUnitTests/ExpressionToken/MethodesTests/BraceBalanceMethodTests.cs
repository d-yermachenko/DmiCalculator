using DmiCalc;
using DmiCalc.ExpressionTokens;
using DmiCalc.ExpressionTokens.Transformers;
using DmiCalcUnitTests.ExpressionToken.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;


namespace DmiCalcUnitTests.ExpressionTokens
{


    [TestClass]
    public class BraceFunctionsVariablesTests{
        public static IList<DmiCalc.ExpressionTokens.ExpressionToken> GetDefaultExpressionTokens(string expression) {
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(new CultureInfo("fr-FR")).GetExpressionWordsDetectors());
            var expressionSentence = composer.ParseExpression(expression);
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());
            var tokensList = tokenBuilder.ConvertWordsToTokens(expressionSentence, ExpressionTokenBuilder.GetDefaultTransformers());
            return tokensList;
        }

        [TestMethod]
        public void CheckSimpleBracketBalance() {
            string expression = "8*4-((15-10)*3)";
            var tokens = GetDefaultExpressionTokens(expression);
            var inBraceTokens = BracketTreeTokenTransformer.GetListBraceSuborderedTokens(tokens, 4);
            Assert.IsTrue(inBraceTokens.Count == 7);
        }

        [TestMethod]
        public void CheckSimpleBracketBalanceIncludeSideTokens() {
            string expression = "8*4-((15-10)*3)";
            var tokens = GetDefaultExpressionTokens(expression);
            var inBraceTokens = BracketTreeTokenTransformer.GetListBraceSuborderedTokens(tokens, 4, includeTokensOpenClose: true);
            Assert.IsTrue(inBraceTokens.Count == 9);
        }

        [TestMethod]
        public void CheckCreatingTree() {
            string expression = "8+6 + Foo((2+1)*3+4*(12-1))";
            var nodes = GetDefaultExpressionTokens(expression);
            BracketTreeTokenTransformer bracketTreeTokenTransformer = new BracketTreeTokenTransformer();
            FooBarFunctionGetter fooBarFunctionGetter = new FooBarFunctionGetter();
            bracketTreeTokenTransformer.FunctionGetter = fooBarFunctionGetter;
            var braceSeparatedNodes = bracketTreeTokenTransformer.TransformTokens(nodes);
            Assert.IsTrue(braceSeparatedNodes.Count == 6);
        }

        [TestMethod]
        public void CheckArgumantSeparatorTokens() {
            string expression = "8+6 + Foo((2+1)*3+4*(12-1); 16*7 + 2)";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(new CultureInfo("fr-FR")).GetExpressionWordsDetectors()) {
                Culture = new CultureInfo("FR-fr")
            };
            var expressionWords = composer.ParseExpression(expression);
            List<ISemanticsTokenTransformer> semanticsTokenTransformers = new List<ISemanticsTokenTransformer>();
            semanticsTokenTransformers.AddRange(ExpressionTokenBuilder.GetDefaultTransformers());
            FooBarFunctionGetter fooBarFunctionGetter = new FooBarFunctionGetter();
            semanticsTokenTransformers.Add(new VariableAndFunctionDistinctor() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new BracketTreeTokenTransformer() { FunctionGetter = fooBarFunctionGetter });
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());
            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticsTokenTransformers);
            Assert.IsTrue(nodes.Count == 6);
        }

        [TestMethod]
        public void CheckArgumentSeparatorTokensTransformerSimple() {
            string expression = "8+6 + Foo((2+1)*3+4*(12-1); 16*7 + 2)";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(new CultureInfo("fr-FR")).GetExpressionWordsDetectors()) {
                Culture = new CultureInfo("FR-fr")
            };
            var expressionWords = composer.ParseExpression(expression);
            List<ISemanticsTokenTransformer> semanticsTokenTransformers = new List<ISemanticsTokenTransformer>();
            semanticsTokenTransformers.AddRange(ExpressionTokenBuilder.GetDefaultTransformers());
            FooBarFunctionGetter fooBarFunctionGetter = new FooBarFunctionGetter();
            semanticsTokenTransformers.Add(new VariableAndFunctionDistinctor() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new BracketTreeTokenTransformer() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new ArgumentsTreeTokenTransformer());
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());

            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticsTokenTransformers);
            Assert.IsTrue(nodes.Count == 5);
        }


        [TestMethod]
        public void CheckArgumentSeparatorTokensTransformerComplex() {
            string expression = "8+6 + Foo((2+1)*3+4*(12-1); 16*7 + Bar(14; 25+74 * Foo(20; 30; 40; 566)))";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(new CultureInfo("fr-FR")).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            List<ISemanticsTokenTransformer> semanticsTokenTransformers = new List<ISemanticsTokenTransformer>();
            semanticsTokenTransformers.AddRange(ExpressionTokenBuilder.GetDefaultTransformers());
            FooBarFunctionGetter fooBarFunctionGetter = new FooBarFunctionGetter();
            semanticsTokenTransformers.Add(new VariableAndFunctionDistinctor() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new BracketTreeTokenTransformer() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new ArgumentsTreeTokenTransformer());
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());
            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticsTokenTransformers);
            Assert.IsTrue(nodes.Count == 5);
        }

        
    }
}
 