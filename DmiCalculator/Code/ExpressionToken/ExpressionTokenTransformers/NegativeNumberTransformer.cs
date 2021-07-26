using System;
using System.Collections.Generic;

namespace DmiCalc.ExpressionTokens.Transformers {

    /// <summary>
    /// Holds les cases 
    /// </summary>
    public class NegativeNumberTransformer : ISemanticsTokenTransformer
    {
        public IList<ExpressionToken> TransformTokens(IList<ExpressionToken> expressionTokens) {
            List<ExpressionToken> resultSet = new List<ExpressionToken>();
            for (int i = 0; i < expressionTokens.Count; i++) {
                var currentExpressionToken = expressionTokens[i];
                if (currentExpressionToken.ExpressionTokenKind == ExpressionTokenKind.NumberValue) {
                    var prepreviousToken = ExpressionToken.GetNeighbordExpressionToken(expressionTokens, i, -2);
                    var previosToken = ExpressionToken.GetNeighbordExpressionToken(expressionTokens, i, -1);

                    //Checking  2 tokens on the left - was it the math operation or null
                    bool isPrePreviousTokenOperatorOrNull = prepreviousToken == null //Meens that in 2 tokens on the left it was the math operation or null
                        || prepreviousToken?.ExpressionTokenKind == ExpressionTokenKind.BinaryOperatorAddition
                        || prepreviousToken?.ExpressionTokenKind == ExpressionTokenKind.BinaryOperatorMultiplication
                        || prepreviousToken?.ExpressionTokenKind == ExpressionTokenKind.BinaryOperatorRelation
                        || prepreviousToken?.ExpressionTokenKind == ExpressionTokenKind.IntermediateBracketOpen
                        || prepreviousToken?.ExpressionTokenKind == ExpressionTokenKind.IntermediateBraceOpen;
                    // Checking 1 token on the left - was it minus sign
                    bool isPreviousTokenIsMinusOperator = previosToken?.ExpressionTokenKind == ExpressionTokenKind.BinaryOperatorAddition &&
                        String.Equals(previosToken?.ExpressionTokenValue, "-");
                    //Checking current token - is it number
                    bool isCurrentTokenNumber = currentExpressionToken?.ExpressionTokenKind == ExpressionTokenKind.NumberValue;
                    //Checking current token - is it known variable or expression
                    bool isCurrentTokenExpression = currentExpressionToken?.ExpressionTokenKind == ExpressionTokenKind.Variable ||
                        currentExpressionToken?.ExpressionTokenKind == ExpressionTokenKind.IntermediateBraceOpen;

                    bool doNegativation = isPrePreviousTokenOperatorOrNull && isPreviousTokenIsMinusOperator && (isCurrentTokenNumber || isCurrentTokenExpression);
                    //Negativiting current token if condition fullfilled
                    if (doNegativation) {
                        if (isCurrentTokenNumber) {
                            resultSet.Remove(previosToken);
                            currentExpressionToken = NegativateNumberToken(currentExpressionToken);
                        }
                        else if (isCurrentTokenExpression)
                            throw new NotImplementedException("Negativation of expression is not implemented");
                    }
                }
                resultSet.Add(currentExpressionToken);
            }
            return resultSet;
        }

        private ExpressionToken NegativateNumberToken(ExpressionToken currentExpressionToken) {
            if (currentExpressionToken.ExpressionTokenKind == ExpressionTokenKind.NumberValue)
                currentExpressionToken.ExpressionTokenValue = NegativateNumber(currentExpressionToken.ExpressionTokenValue);
            return currentExpressionToken;
        }

        #region Negativate



        object NegativateNumber(object number) {
            return Negativate(number as dynamic);
        }

        short Negativate(short value) { return (short)(value * -1); }
        int Negativate(int value) { return (int)(value * -1); }

        long Negativate(long value) { return (long)(value * -1); }

        float Negativate(float value) { return (float)(value * -1); }

        double Negativate(double value) { return (double)(value * -1); }

        decimal Negativate(decimal value) { return (decimal)(value * -1M); }
        #endregion
    }
}
