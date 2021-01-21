using DmiCalc;
using DmiCalc.ExpressionTokens;
using DmiCalc.ExpressionTokens.Transformers;
using DmiCalcUnitTests.ExpressionToken.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace DmiCalcUnitTests.ExpressionToken.MethodesTests {
    [TestClass]
    public class TestUnaryOperations {

        [TestMethod]
        public void TestUnaryPrefixOperationNotRecursive() {
            string expression = "!Foo(!False; !True; !Bar(!False; !True; True))";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(new CultureInfo("fr-FR")).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            semanticTransformers.Item1.Add(new NotOrFactorialTokenTransformer());
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetUnaryPrefixOperation,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.UnaryPrefixOperatorNot }
            });
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());

            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);
            Assert.IsTrue(nodes.Count == 1);
        }

        [TestMethod, TestCategory("DebugTests")]
        public void TestUnaryPostfixOperationNotRecursive() {
            string expression = "(4!-Foo(24!; 15)!)!";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(new CultureInfo("fr-FR")).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            //semanticTransformers.Item1.Add(new NotOrFactorialTokenTransformer());
            var unaryPostfixOperator = new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetUnaryPostfixOperation,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.IntermediateExclamation }
            };
            semanticTransformers.Item1.Add(unaryPostfixOperator);
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());

            var tokens = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);
            Assert.IsTrue(tokens.Count == 1);
        }

        [TestMethod]
        public void CheckArgumentSeparatorTokensExclamationPointFactorial() {
            string expression = "8+6 + Foo((2+1)*3+4*(12-1); 16*7 + Bar(14; 25+74 * Foo(20; 30; 40; 566; !True)))!";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(new CultureInfo("fr-FR")).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            List<ISemanticsTokenTransformer> semanticsTokenTransformers = new List<ISemanticsTokenTransformer>();
            semanticsTokenTransformers.AddRange(ExpressionTokenBuilder.GetDefaultTransformers());
            semanticsTokenTransformers.Add(new NotOrFactorialTokenTransformer());
            semanticsTokenTransformers.Add(new NotOrFactorialTokenTransformer());
            FooBarFunctionGetter fooBarFunctionGetter = new FooBarFunctionGetter();
            semanticsTokenTransformers.Add(new VariableAndFunctionDistinctor() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new BracketTreeTokenTransformer() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new ArgumentsTreeTokenTransformer());
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());

            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticsTokenTransformers);
            Assert.IsTrue(nodes.Last().ExpressionTokenKind == ExpressionTokenKind.UnaryPostfixOperatorFactorial);
        }

        [TestMethod]
        public void CheckArgumentSeparatorTokensExclamationPointFactorialAndNot() {
            string expression = "8+6! || Foo(!False)";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(new CultureInfo("fr-FR")).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            List<ISemanticsTokenTransformer> semanticsTokenTransformers = new List<ISemanticsTokenTransformer>();
            semanticsTokenTransformers.AddRange(ExpressionTokenBuilder.GetDefaultTransformers());
            FooBarFunctionGetter fooBarFunctionGetter = new FooBarFunctionGetter();
            semanticsTokenTransformers.Add(new NotOrFactorialTokenTransformer());
            semanticsTokenTransformers.Add(new VariableAndFunctionDistinctor() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new BracketTreeTokenTransformer() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new ArgumentsTreeTokenTransformer());
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());
            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticsTokenTransformers);
            var expectedFactorialNode = nodes[5].ChildExpressionTokens[0].ChildExpressionTokens[0];
            Assert.IsTrue(nodes[3].ExpressionTokenKind == ExpressionTokenKind.UnaryPostfixOperatorFactorial &&
                expectedFactorialNode.ExpressionTokenKind == ExpressionTokenKind.UnaryPrefixOperatorNot);
        }
    }
}
