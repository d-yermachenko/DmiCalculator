using DmiCalc.BasicCalc;
using DmiCalc.ExpressionTokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DmiCalc.AppCode.BasicCalc {
    public class DebugCalc :BasicCalculator {
        private readonly IVariablesProvider VariableGetter;

        public DebugCalc(IVariablesProvider variableGetter = null) {
            VariableGetter = variableGetter;
        }

        public virtual IList<ExpressionWord> GetWords(string expression) {
            ExpressionPartComposer composer = new ExpressionPartComposer(ExpressionWordDetectors) {
                Culture = CalculatorCulture
            };
            return composer.ParseExpression(expression);
        }

        public virtual IList<ExpressionToken> GetTokens(string expression) {
            ExpressionPartComposer composer = new ExpressionPartComposer(ExpressionWordDetectors) {
                Culture = CalculatorCulture
            };
            var expressionWords = composer.ParseExpression(expression);
            var semanticTransformers = ComposeSemanticTokenTransformers(VariableGetter);
            return TokenBuilder.ConvertWordsToTokens(expressionWords, semanticTransformers);
        }

        public virtual IList<ExpressionToken> GetNodes(string expression) {
            ExpressionPartComposer composer = new ExpressionPartComposer(ExpressionWordDetectors) {
                Culture = CalculatorCulture
            };
            var expressionWords = composer.ParseExpression(expression);
            return TokenBuilder.ConvertWordsToTokens(expressionWords, base.ComposeSemanticTokenTransformers());
        }

        public virtual IList<ExpressionTree.ExpressionNode> GetExpressionTree(string expression) {
            ExpressionPartComposer composer = new ExpressionPartComposer(ExpressionWordDetectors) {
                Culture = CalculatorCulture
            };
            var expressionWords = composer.ParseExpression(expression);
                var tokens = TokenBuilder.ConvertWordsToTokens(expressionWords, base.ComposeSemanticTokenTransformers());
                var expressionNodeConverter = new ExpressionTree.DefaultExpressionNodeConverter() {
                    Functions = OperationsProvider,
                    Variables = VariableGetter
                };
                return expressionNodeConverter.BuildExpressionTree(tokens).ToList();
            }

        public virtual Task<IList<ExpressionWord>> GetWordsAsync(string expression, CancellationToken cancelationToken) {
            return Task.Factory.StartNew<IList<ExpressionWord>>(()=>GetWords(expression), cancelationToken);
        }

        public virtual Task<IList<ExpressionWord>> GetWordsAsync(string expression) => GetWordsAsync(expression, CancellationToken.None);


        public virtual Task<IList<ExpressionToken>> GetTokensAsync(string expression, CancellationToken cancelationToken) {
            return Task.Factory.StartNew(() => GetTokens(expression), cancelationToken);
        }

        public virtual Task<IList<ExpressionToken>> GetTokensAsync(string expression) => GetTokensAsync(expression, CancellationToken.None);

        public virtual Task<IList<ExpressionToken>> GetNodesAsync(string expression, CancellationToken cancelationToken) {
            return Task.Factory.StartNew(() => GetNodes(expression), cancelationToken);
        }

        public virtual Task<IList<ExpressionToken>> GetNodesAsync(string expression) => GetNodesAsync(expression, CancellationToken.None);

        public virtual Task<IList<ExpressionTree.ExpressionNode>> GetExpressionTreeAsync(string expression, CancellationToken cancellationToken) {
            return Task.Factory.StartNew(() => GetExpressionTree(expression), cancellationToken);
        }

        public virtual Task<IList<ExpressionTree.ExpressionNode>> GetExpressionTreeAsync(string expression) => GetExpressionTreeAsync(expression, CancellationToken.None);


    }
}
