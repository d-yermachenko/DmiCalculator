using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc.ExpressionTokens.Transformers {
    public class BracketTreeTokenTransformer : ISemanticsTokenTransformer {
        public IOperationsProvider FunctionGetter;


        public IVariablesProvider VariableGetter;
        public IList<ExpressionToken> TransformTokens(IList<ExpressionToken> expressionTokens) {
            List<ExpressionToken> transformedTokens = new List<ExpressionToken>();
            for (int i = 0; i < expressionTokens.Count; i++) {
                var previousToken = ExpressionToken.GetNeighbordExpressionToken(expressionTokens, i, -1);
                var currentToken = expressionTokens[i];

                if (currentToken?.ChildExpressionTokens.Count > 0)
                    currentToken.ChildExpressionTokens = (List<ExpressionToken>)TransformTokens(currentToken?.ChildExpressionTokens);

                ///Action if current token kind is 
                if (currentToken?.ExpressionTokenKind == ExpressionTokenKind.IntermediateBracketOpen) {

                    ExpressionToken expressioRootToken = currentToken;// defines root token to add to expression. It cen be BracketToken or FunctionToken

                    IList<ExpressionToken> subOrderedTokens = GetListBraceSuborderedTokens(expressionTokens, i);
                    //FunctionCase
                    if (previousToken?.ExpressionTokenKind == ExpressionTokenKind.IntermediateName
                    && (FunctionGetter?.HasOperation(previousToken?.ExpressionTokenValue?.ToString()) ?? false)) {
                        previousToken.ExpressionTokenKind = ExpressionTokenKind.Function;
                        expressioRootToken = previousToken;
                    }
                    // Action suborder all tokens in braces to previous token, which is function
                    i += subOrderedTokens.Count + 1;
                    subOrderedTokens = TransformTokens(subOrderedTokens);
                    foreach(var element in subOrderedTokens)
                        expressioRootToken.ChildExpressionTokens.Add(element);
                }
                transformedTokens.Add(currentToken);
            }
            return transformedTokens;
        }

        public static IList<ExpressionToken> GetListBraceSuborderedTokens(IList<ExpressionToken> expressionTokens, int indent,
            ExpressionTokenKind openingTokenKind = null,
            ExpressionTokenKind closingTokenKind = null,
            bool includeTokensOpenClose = false) {
            ExpressionTokenKind openingTokenKindVal;
            _ = ExpressionTokenKind.Undefined;
            if (openingTokenKind == null)
                openingTokenKindVal = ExpressionTokenKind.IntermediateBracketOpen;
            else
                openingTokenKindVal = openingTokenKind;

            ExpressionTokenKind closingTokenKindVal;
            if (closingTokenKind == null)
                closingTokenKindVal = ExpressionTokenKind.IntermediateBracketClosed;
            else
                closingTokenKindVal = closingTokenKind;

            if (expressionTokens.Count < indent + 1)
                throw new IndexOutOfRangeException();
            int braceLevel;
            if (expressionTokens[indent].ExpressionTokenKind == openingTokenKindVal) {
                braceLevel = 1;
            }
            else
                throw new Exceptions.DmiCalcException(Exceptions.DmiCalcExceptionCodes.WrongBraceKind, new object[] { expressionTokens[indent] }) { LocalPositions = new int[] { indent } };

            List<ExpressionToken> expressionTokensInBraces = new List<ExpressionToken>();
            if (includeTokensOpenClose)
                expressionTokensInBraces.Add(expressionTokens[indent]);
            for (int i = indent + 1; i < expressionTokens.Count && braceLevel > 0; i++) {
                ExpressionToken currentExpressionToken = expressionTokens[i];
                if (currentExpressionToken.ExpressionTokenKind == openingTokenKindVal)
                    braceLevel++;
                if (currentExpressionToken.ExpressionTokenKind == closingTokenKindVal)
                    braceLevel--;
                if (braceLevel != 0 || includeTokensOpenClose)
                    expressionTokensInBraces.Add(currentExpressionToken);
            }
            if (braceLevel != 0)
                throw new Exceptions.DmiCalcException(Exceptions.DmiCalcExceptionCodes.WrongBraceBalance);

            return expressionTokensInBraces;
        }
    }
}
