using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using DmiCalc.ExpressionTokens;
using DmiCalc;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using DmiCalc.ExpressionTokens.Transformers;

namespace DmiCalc.BasicCalc {

    public class BasicCalculator {
        public virtual IEnumerable<Func<string, int, ExpressionWordResult>> ExpressionWordDetectors { get; protected set; }
        public virtual IOperationsProvider OperationsProvider { get; protected set; }

        public virtual CultureInfo CalculatorCulture { get; protected set; }

        public virtual ExpressionTokenBuilder TokenBuilder { get; protected set; } 

        public BasicCalculator(CultureInfo cultureInfo = null) {
            if (cultureInfo == null)
                cultureInfo = CultureInfo.CurrentCulture;
            CalculatorCulture = cultureInfo;
            ExpressionWordDetectors = new BasicWordsProvider(CalculatorCulture).GetExpressionWordsDetectors();
            OperationsProvider = new BasicFunctionGetter();
            TokenBuilder = new ExpressionTokenBuilder(new ExpressionWordToTokenConvertor());
        }

        public virtual IList<ISemanticsTokenTransformer> ComposeSemanticTokenTransformers(IVariablesProvider variableGetter = null ) {
            List<ISemanticsTokenTransformer> semanticsTokenTransformers = new List<ISemanticsTokenTransformer>();
            semanticsTokenTransformers.AddRange(ExpressionTokenBuilder.GetDefaultTransformers());
            semanticsTokenTransformers.Add(new NotOrFactorialTokenTransformer());
            semanticsTokenTransformers.Add(new VariableAndFunctionDistinctor() { FunctionGetter = OperationsProvider, VariableGetter = variableGetter });
            semanticsTokenTransformers.Add(new BracketTreeTokenTransformer() { FunctionGetter = OperationsProvider });
            semanticsTokenTransformers.Add(new ArgumentsTreeTokenTransformer());
            semanticsTokenTransformers.Add(new DelegateBasedTokenTransformer() {
                TokenKinds = new ExpressionTokenKind[] { ExpressionTokenKind.UnaryPostfixOperatorFactorial },
                OperatorTreeTransformer = OperatorsTreeExpressionTokenTransformer.GetUnaryPostfixOperation
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


        public dynamic CalculateExpression(string expression, IVariablesProvider variableGetter = null) {
            var calculationResult = CalculateExpressionMetaData(expression, variableGetter);
            if(calculationResult.InnerException != null) {
                throw new Exception(calculationResult.InnerException.Message, calculationResult.InnerException);
            }
            return calculationResult.ExpressionResult;
        }

        public async Task<dynamic> CalculateExpressionAsync(string expression, IVariablesProvider variableGetter = null) {
            var calculationResult = await CalculateExpressionMetaDataAsync(expression, variableGetter);
            if (calculationResult.InnerException != null) {
                throw new Exception(calculationResult.InnerException.Message, calculationResult.InnerException);
            }
            return calculationResult.ExpressionResult;
        }

        public async Task<dynamic> CalculateExpressionAsync(string expression, CancellationToken cancellationToken, IVariablesProvider variableGetter = null) {
            return await Task.Factory.StartNew(async () => { return await CalculateExpressionAsync(expression, variableGetter); }, cancellationToken);
        }


        public virtual CalculationData CalculateExpressionMetaData(string expression, IVariablesProvider variablesProvider = null) {
            CalculationData calculationData = new CalculationData();
            try {
                ExpressionPartComposer composer = new ExpressionPartComposer(ExpressionWordDetectors) {
                    Culture = CalculatorCulture
                };
                var expressionWords = composer.ParseExpression(expression);
                calculationData.ExpressionWords = expressionWords;
                var semanticTransformers = ComposeSemanticTokenTransformers(variablesProvider);
                var tokens = TokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers);
                calculationData.ExpressionTokens = tokens;
                if (tokens.Count != 1)
                    throw new NotImplementedException("Cant parse expression \"" + expression + "\"");
                var expressionNodeConverter = new ExpressionTree.DefaultExpressionNodeConverter() {
                    Functions = OperationsProvider,
                    Variables = variablesProvider
                };
                var nodes = expressionNodeConverter.BuildExpressionTree(tokens);
                dynamic result = nodes[0].Calculate(new BasicDatasource() { VariablesProvider = variablesProvider, Culture = CalculatorCulture });
                calculationData.ExpressionResult = result;
            }
            catch(Exception ex) {
                calculationData.InnerException = ex;
            }
            return calculationData;

        }

        public virtual async Task<CalculationData> CalculateExpressionMetaDataAsync(string expression, IVariablesProvider variablesProvider = null) {
            CalculationData calculationData = new CalculationData();
            try {
                ExpressionPartComposer composer = new ExpressionPartComposer(ExpressionWordDetectors) {
                    Culture = CalculatorCulture
                };
                var expressionWords = composer.ParseExpression(expression);
                calculationData.ExpressionWords = expressionWords;
                var semanticTransformers = ComposeSemanticTokenTransformers(variablesProvider);
                var tokens = await TokenBuilder.ConvertWordsToTokensAsync(expressionWords, semanticTransformers);
                calculationData.ExpressionTokens = tokens;
                if (tokens.Count != 1)
                    throw new NotImplementedException("Cant parse expression \"" + expression + "\"");
                var expressionNodeConverter = new ExpressionTree.DefaultExpressionNodeConverter() {
                    Functions = OperationsProvider,
                    Variables = variablesProvider
                };
                var nodes = expressionNodeConverter.BuildExpressionTree(tokens);
                dynamic result = await nodes[0].CalculateAsync(new BasicDatasource() { VariablesProvider = variablesProvider, Culture = CalculatorCulture });
                calculationData.ExpressionResult = result;
            }
            catch (Exception ex) {
                calculationData.InnerException = ex;
            }
            return calculationData;

        }

    }
}
