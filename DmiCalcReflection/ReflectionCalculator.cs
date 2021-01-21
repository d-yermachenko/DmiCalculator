using DmiCalc;
using DmiCalc.AppCode.BasicCalc;
using DmiCalc.BasicCalc;
using DmiCalc.ExpressionTokens;
using DmiCalc.ExpressionTokens.Transformers;
using DmiCalcReflection.ExpressionToken;
using DmiCalcReflection.ExpressionToken.Transformers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DmiCalcReflection {
    public class ReflectionCalculator :DebugCalc {
        readonly DmiCalc.Interfaces.IExpressionWordsProvider ExpressionWordsProvider;

        readonly DmiCalc.IOperationsProvider FunctionsGetter;


        public ReflectionCalculator(DmiCalc.IOperationsProvider functionsGetter = null, CultureInfo cultureInfo =null) {
            if (cultureInfo == null)
                cultureInfo = CultureInfo.InvariantCulture;
            if (functionsGetter == null)
                functionsGetter = new Operations.ReflectionFunctionGetter();
            FunctionsGetter = functionsGetter;
            ExpressionWordsProvider = new PointExpressionWordProvider(cultureInfo);
            ExpressionWordDetectors = ExpressionWordsProvider.GetExpressionWordsDetectors();
            TokenBuilder = new ExpressionTokenBuilder(new ReflectionWordToTokenConvertor());
        }

        public override IList<ExpressionWord> GetWords(string expression) {
            CultureInfo culture = CultureInfo.CurrentCulture;
            ExpressionPartComposer composer = new ExpressionPartComposer(new PointExpressionWordProvider(culture).GetExpressionWordsDetectors());
            return composer.ParseExpression(expression);
        }

        public override IList<ISemanticsTokenTransformer> ComposeSemanticTokenTransformers(IVariablesProvider variableGetter = null) {
            List<ISemanticsTokenTransformer> semanticsTokenTransformers = new List<ISemanticsTokenTransformer>();
            semanticsTokenTransformers.AddRange(ExpressionTokenBuilder.GetDefaultTransformers());
            semanticsTokenTransformers.Add(new NotOrFactorialTokenTransformer());
            semanticsTokenTransformers.Add(new VariableOrFunctionOrPropertyTransformer() { FunctionGetter = OperationsProvider, VariableGetter = variableGetter });
            semanticsTokenTransformers.Add(new BracketTreeTokenTransformer() { FunctionGetter = OperationsProvider });
            semanticsTokenTransformers.Add(new ArgumentsTreeTokenTransformer());
            semanticsTokenTransformers.Add(new DelegateBasedTokenTransformer() {
                TokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.UnaryPostfixOperatorFactorial },
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetUnaryPostfixOperation
            });
            semanticsTokenTransformers.Add(new DelegateBasedTokenTransformer() {
                TokenKinds = new ExpressionTokenKind[] { ReflectionOperationTokenKind.ReflectionOperation },
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod
            });
            semanticsTokenTransformers.Add(new DelegateBasedTokenTransformer() {
                TokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorMultiplication },
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod
            });
            semanticsTokenTransformers.Add(new DelegateBasedTokenTransformer() {
                TokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.BinaryOperatorAddition },
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetBinaryOperationMod
            });

            return semanticsTokenTransformers;
        }


    }
}
