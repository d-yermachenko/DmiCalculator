using DmiCalc.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DmiCalc.ExpressionTokens.Transformers {
    public class OperatorsTreeExpressionTokenTransformer {
        public ExpressionTokenKind[] OperatorKinds { get; set; }



        public static IList<ExpressionToken> GetUnaryPrefixOperation(IList<ExpressionToken> tokens, ExpressionTokenKind[] operatorKinds) {
            List<ExpressionToken> resultTokens = new List<ExpressionToken>();
            for(int i = 0; i < tokens.Count; i++) {
                ExpressionToken curentToken = tokens[i];
                if (operatorKinds.Contains(curentToken.ExpressionTokenKind)) {
                    var nextToken = ExpressionToken.GetNeighbordExpressionToken(tokens, i, 1);
                    if (nextToken != null) {
                        i++;
                        curentToken.ChildExpressionTokens.Add(nextToken);
                    }
                    else
                        throw new DmiCalcException(DmiCalcExceptionCodes.UnaryPrefixOperationOnTheEndOfExpression, new object[] { curentToken });

                    if (nextToken?.ChildExpressionTokens.Count > 0) {
                        foreach(var operationArgumentSubnodes in nextToken.ChildExpressionTokens)
                            operationArgumentSubnodes.ChildExpressionTokens = (List<ExpressionToken>)GetUnaryPrefixOperation(operationArgumentSubnodes.ChildExpressionTokens, operatorKinds);
                    }
                }
                resultTokens.Add(curentToken);
            }
            return resultTokens;
        }

        public static IList<ExpressionToken> GetUnaryPostfixOperation(IList<ExpressionToken> tokens, ExpressionTokenKind[] operatorKinds) {
            List<ExpressionToken> resultTokens = new List<ExpressionToken>();
            for (int i = 0; i < tokens.Count; i++) {
                ExpressionToken curentToken = tokens[i];
                // Verifying need we to check current node for factorial
                if(curentToken.ChildExpressionTokens.Count > 0) {
                    curentToken.ChildExpressionTokens = (List<ExpressionToken>)GetUnaryPostfixOperation(curentToken.ChildExpressionTokens, operatorKinds);
                }

                ExpressionToken nextToken = ExpressionToken.GetNeighbordExpressionToken(tokens, i, 1);
                //Next node is factorial
                if (nextToken != null && operatorKinds.Contains(nextToken.ExpressionTokenKind)) {
                    //so moving position in source list to 1 step ahead
                    i++;
                    // Hiding current node under the sign of factorial
                    nextToken.ChildExpressionTokens.Add(curentToken);
                    //and adding result factorial node to results list
                    resultTokens.Add(nextToken);
                }
                else
                    resultTokens.Add(curentToken);

            }
            return resultTokens;
        }

        public static IList<ExpressionToken> GetBinaryOperation(IList<ExpressionToken> tokens, ExpressionTokenKind[] operatorKinds) {
            List<ExpressionToken> resultsList = new List<ExpressionToken>();
            for(int i = 0; i < tokens.Count; i++) {
                ExpressionToken currentToken = tokens[i];
                if (currentToken.ChildExpressionTokens.Count > 0)
                    currentToken.ChildExpressionTokens = GetBinaryOperation(currentToken.ChildExpressionTokens, operatorKinds).ToList();
                ExpressionToken nextToken = ExpressionToken.GetNeighbordExpressionToken(tokens, i, 1);
                if (nextToken != null && operatorKinds.Contains(nextToken.ExpressionTokenKind)) {
                    IList<ExpressionToken> rightPart = GetBinaryOperation(tokens.ToList().GetRange(i + 2, tokens.Count - i - 2), operatorKinds);
                    nextToken.ChildExpressionTokens.Add(currentToken);
                    foreach (var rightToken in rightPart)
                        nextToken.ChildExpressionTokens.Add(rightToken);
                    resultsList.Add(nextToken);
                    break;
                }
                else
                    resultsList.Add(currentToken);
                

            }
            return resultsList;
        }

        /// <summary>
        /// We have th list of elements, can contain finctions, 
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="operatorKinds"></param>
        /// <returns></returns>
        /// <remarks>
        /// Yhe goal of thim
        /// 
        /// </remarks>
        public static IList<ExpressionToken> GetBinaryOperationMod(IList<ExpressionToken> tokens, ExpressionTokenKind[] operatorKinds) {
            List<ExpressionToken> resultsList = new List<ExpressionToken>();
            ExpressionToken currentToken = null;
            bool currentTokenChanged = true;
            for (int i = 0; i < tokens.Count; ) {
                if(currentToken == null)
                    currentToken = tokens[i];
                if (currentTokenChanged && currentToken != null && currentToken.ChildExpressionTokens.Count > 0)
                    currentToken.ChildExpressionTokens = GetBinaryOperationMod(currentToken.ChildExpressionTokens, operatorKinds).ToList();
                ExpressionToken nextToken = ExpressionToken.GetNeighbordExpressionToken(tokens, i, 1);
                
                //Next token is one of the operations that we looking for 
                if (nextToken != null && operatorKinds.Contains(nextToken.ExpressionTokenKind)) {
                    ExpressionToken rightToken = ExpressionToken.GetNeighbordExpressionToken(tokens, i, 2);
                    if(rightToken != null && rightToken.ChildExpressionTokens.Count > 0)
                        rightToken.ChildExpressionTokens = GetBinaryOperationMod(rightToken.ChildExpressionTokens, operatorKinds).ToList();
                    nextToken.ChildExpressionTokens.Add(currentToken);
                    nextToken.ChildExpressionTokens.Add(rightToken);
                    currentToken = nextToken;
                    i = tokens.IndexOf(rightToken);
                }
                else { 
                    resultsList.Add(currentToken);
                    currentToken = null;
                    i++;
                }
            }
            return resultsList;
        }

        public static IList<ExpressionToken> GetBinaryOperationInverted(IList<ExpressionToken> tokens, ExpressionTokenKind[] operatorKinds) {
            List<ExpressionToken> resultsList = new List<ExpressionToken>();
            for (int i = tokens.Count -1; i >= 0; i--) {
                ExpressionToken currentToken = tokens[i];
                if (currentToken.ChildExpressionTokens.Count > 0)
                    currentToken.ChildExpressionTokens = GetBinaryOperationInverted(currentToken.ChildExpressionTokens, operatorKinds).ToList();
                ExpressionToken potentialOperationToken = ExpressionToken.GetNeighbordExpressionToken(tokens, i, -1);
                if (potentialOperationToken != null && operatorKinds.Contains(potentialOperationToken.ExpressionTokenKind)) {
                    IList<ExpressionToken> leftPart = GetBinaryOperationInverted(tokens.ToList().GetRange(0, i -1), operatorKinds);
                    foreach (var rightToken in leftPart)
                        potentialOperationToken.ChildExpressionTokens.Add(rightToken);
                    potentialOperationToken.ChildExpressionTokens.Add(currentToken);
                    resultsList.Add(potentialOperationToken);
                    break;
                }
                else
                    resultsList.Add(currentToken);


            }
            return resultsList;
        }

    }
}
