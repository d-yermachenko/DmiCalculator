using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DmiCalc.ExpressionTokens;

namespace DmiCalc.ExpressionTokens.Transformers {
    public class ArgumentsTreeTokenTransformer : ISemanticsTokenTransformer {

        public IList<ExpressionToken> TransformTokens(IList<ExpressionToken> expressionTokens) {
            List<List<ExpressionToken>> subExpressions = new List<List<ExpressionToken>>() { new List<ExpressionToken>() };
            string argumentSeparator = String.Empty;
            List<ExpressionToken> argumentsSeparatedList = new List<ExpressionToken>();
            
            for (int i = 0; i < expressionTokens.Count; i++) {
                
                var currentToken = expressionTokens[i];
                if (currentToken.ChildExpressionTokens.Count > 0)
                    currentToken.ChildExpressionTokens = (List<ExpressionToken>)TransformTokens(currentToken.ChildExpressionTokens);

                var previousToken = ExpressionToken.GetNeighbordExpressionToken(expressionTokens, i, -1);
                ///Cheking is it the function - in this case we need to look for subExpressions
                if (previousToken?.ExpressionTokenKind == ExpressionTokenKind.Function &&
                    currentToken.ExpressionTokenKind == ExpressionTokenKind.IntermediateBracketOpen &&
                    currentToken.ChildExpressionTokens.Count > 0) {

                    //Separating current tokens's subTokens for subExpressions
                    //i.e., for function Foo(8+9; 12+15*Bar(15; 20 +1))  it extracts the arguments [{8+9}; {12+15*Bar({15}; {20 +1}))}]
                    for (int ci = 0; ci < currentToken.ChildExpressionTokens.Count; ci++) {
                        var currentSubToken = currentToken.ChildExpressionTokens[ci];
                        if (currentSubToken.ExpressionTokenKind == ExpressionTokenKind.FunctionArgumentSeparator) {
                            subExpressions.Add(new List<ExpressionToken>());
                            if (String.IsNullOrWhiteSpace(argumentSeparator))
                                argumentSeparator = currentSubToken.ExpressionTokenValue?.ToString();
                        }
                        else
                            subExpressions.Last().Add(currentSubToken);
                    }


                    //Adding subexpressions to argument separated lists
                    for (int di = 0; di < subExpressions.Count; di++) {
                        var functionArgumentToken = new ExpressionToken() {
                            ExpressionTokenKind = ExpressionTokenKind.FunctionArgumentSeparator,
                            ExpressionTokenValue = argumentSeparator,
                        };
                        foreach(var element in TransformTokens(subExpressions[di]))
                            functionArgumentToken.ChildExpressionTokens.Add(element);
                        previousToken.ChildExpressionTokens.Add(functionArgumentToken);
                    }


                }
                else
                    argumentsSeparatedList.Add(currentToken);


            }

            return argumentsSeparatedList;
        }
    }
}
