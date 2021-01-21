using System.Globalization;
using System.Linq;
using DmiCalc;
using DmiCalc.ExpressionTokens;
using DmiCalc.ExpressionTokens.Transformers;
using DmiCalcUnitTests.ExpressionToken.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DmiCalcUnitTests.ExpressionToken.MethodesTests {

   

    [TestClass]
    public class OperatorTokenTreeBuilderTests {

        

        [TestMethod]
        public void TestSimpleAddition() {
            //string expression = "(26.2-24.2)*3 + 12/4";
            string expression = "26.2 - 24.2";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                 OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperation,
                 OperatorTokenKinds = new ExpressionTokenKind[] {ExpressionTokenKind.BinaryOperatorAddition} 
            });
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());

            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);
            Assert.IsTrue(nodes.Count == 1);
        }

        [TestMethod]
        public void TestDoubleAddition() {
            string expression = "26.2 - 24.2 + 4";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperation,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorAddition }
            });
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());

            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);

            Assert.IsTrue(nodes?.Count == 1 && nodes?.ElementAt(0).ChildExpressionTokens.ElementAt(1).ExpressionTokenKind == ExpressionTokenKind.BinaryOperatorAddition );
        }

        [TestMethod]
        public void TestDoubleAdditionInverted() {
            string expression = "26.2 - 24.2 + 4";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationInverted,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorAddition }
            });
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());
            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);
            ServiceUnits.ExpressionNodeTreeVisualizer.AddConsoleVisualisation(
                nodes, 0, (indent, token) => {
                    System.Diagnostics.Trace.WriteLine(string.Format("{0}{1}({2})", string.Concat(Enumerable.Repeat("\t", indent)), token.ExpressionTokenKind, token.ExpressionTokenValue));
                });
            Assert.IsTrue(nodes?.Count == 1 && nodes?.ElementAt(0).ChildExpressionTokens.ElementAt(0).ExpressionTokenKind == ExpressionTokenKind.BinaryOperatorAddition);
        }

        [TestMethod]
        public void TestComplexAddition() {
            string expression = "26.2+24.2+4+(8+Foo(24+5;1+2))";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer =OperatorsTreeExpressionTokenTransformer.GetBinaryOperation,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorAddition }
            });
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());

            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);
            ServiceUnits.ExpressionNodeTreeVisualizer.AddConsoleVisualisation(
                nodes, 0, (indent, token) => {
                    System.Diagnostics.Trace.WriteLine(string.Format("{0}{1}({2})", string.Concat(Enumerable.Repeat("\t", indent)), token.ExpressionTokenKind, token.ExpressionTokenValue));
                });
            Assert.IsTrue(nodes.Count == 1);
        }

        [TestMethod]
        public void TestComplexAdditionMultiplication() {
            string expression = "26.2/2+24.2*1.5+4+(8+Foo(24+5*4;1+2))";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperation,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorMultiplication }
            });
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperation,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorAddition }
            });

            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());
            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);
            System.Diagnostics.Trace.WriteLine(expression);
            ServiceUnits.ExpressionNodeTreeVisualizer.AddConsoleVisualisation(
                nodes, 0, (indent, token) => {
                    System.Diagnostics.Trace.WriteLine(string.Format("{0}{1}({2})", string.Concat(Enumerable.Repeat("\t", indent)), token.ExpressionTokenKind, token.ExpressionTokenValue));
                });
            Assert.IsTrue(nodes.Count == 1);
        }

        [TestMethod]
        public void TestAdditionMultiplication() {
            string expression = "3*2-9/3-1";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorMultiplication }
            });
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorAddition }
            });
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());

            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);
            ServiceUnits.ExpressionNodeTreeVisualizer.AddConsoleVisualisation(
            nodes, 0, (indent, token) => {
                System.Diagnostics.Trace.WriteLine(string.Format("{0}{1}({2})", string.Concat(Enumerable.Repeat("\t", indent)), token.ExpressionTokenKind, token.ExpressionTokenValue));
            });
            Assert.IsTrue(nodes?.Count == 1 && nodes?.ElementAt(0).ChildExpressionTokens.ElementAt(0).ExpressionTokenKind == ExpressionTokenKind.BinaryOperatorAddition);
        }

        [TestMethod]
        public void TestAdditionMultiplication2() {
            string expression = "3*2*2-9/3-1";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorMultiplication }
            });
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorAddition }
            });
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());
            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);
            ServiceUnits.ExpressionNodeTreeVisualizer.AddConsoleVisualisation(
            nodes, 0, (indent, token) => {
                System.Diagnostics.Trace.WriteLine(string.Format("{0}{1}({2})", string.Concat(Enumerable.Repeat("\t", indent)), token.ExpressionTokenKind, token.ExpressionTokenValue));
            });
            Assert.IsTrue(nodes?.Count == 1 && nodes?.ElementAt(0).ChildExpressionTokens.ElementAt(0).ExpressionTokenKind == ExpressionTokenKind.BinaryOperatorAddition);
        }

        [TestMethod]
        public void TestAdditionMultiplication3() {
            string expression = "3*2*2-9/(2+1)-1";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorMultiplication }
            });
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorAddition }
            });
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());

            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);
            ServiceUnits.ExpressionNodeTreeVisualizer.AddConsoleVisualisation(
            nodes, 0, (indent, token) => {
                System.Diagnostics.Trace.WriteLine(string.Format("{0}{1}({2})", string.Concat(Enumerable.Repeat("\t", indent)), token.ExpressionTokenKind, token.ExpressionTokenValue));
            });
            Assert.IsTrue(nodes?.Count == 1 && nodes?.ElementAt(0).ChildExpressionTokens.ElementAt(0).ExpressionTokenKind == ExpressionTokenKind.BinaryOperatorAddition);
        }


        [TestMethod]
        public void TestAdditionMultiplication4() {
            string expression = "3*2*Foo(5*3+2; 2+4)-9/(2+1)-1";
            ExpressionPartComposer composer = new ExpressionPartComposer(new BasicWordsProvider(CultureInfo.CurrentCulture).GetExpressionWordsDetectors());
            composer.Culture = new CultureInfo("FR-fr");
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = DefaultTokenTransformerFactories.GetDefaultSemanticTokenTransformers();
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorMultiplication }
            });
            semanticTransformers.Item1.Add(new DelegateBasedTokenTransformer() {
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod,
                OperatorTokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorAddition }
            });
            ExpressionTokenBuilder tokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());

            var nodes = tokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers.Item1);
            ServiceUnits.ExpressionNodeTreeVisualizer.AddConsoleVisualisation(
            nodes, 0, (indent, token) => {
                System.Diagnostics.Trace.WriteLine(string.Format("{0}{1}({2})", string.Concat(Enumerable.Repeat("\t", indent)), token.ExpressionTokenKind, token.ExpressionTokenValue));
            });
            Assert.IsTrue(nodes?.Count == 1 && nodes?.ElementAt(0).ChildExpressionTokens.ElementAt(0).ExpressionTokenKind == ExpressionTokenKind.BinaryOperatorAddition);
        }





    }
}
