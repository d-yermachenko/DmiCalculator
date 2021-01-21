using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DmiCalc {

    public class ExpressionPartComposer {

        public IList<Func<string, int, ExpressionWordResult>> WordDetectors { get; private set; }

        public ExpressionPartComposer(IEnumerable<Func<string, int, ExpressionWordResult>> knownWordsProviders) {
            WordDetectors = knownWordsProviders.ToList();
        }

        public System.Globalization.CultureInfo Culture { get; set; }


        private ExpressionWord GetNextWord(string expression, int position) {
            int currentIndex = 0;
            bool isArrived = false;
            ExpressionWord result = null;
            while (currentIndex < WordDetectors.Count && !isArrived) {
                var currentAnswer = WordDetectors[currentIndex].Invoke(expression, position);
                isArrived = currentAnswer.Corresponds;
                result = currentAnswer.Word;
                currentIndex++;
             }
            if (!isArrived)
                throw new DmiCalc.Exceptions.DmiCalcException(Exceptions.DmiCalcExceptionCodes.UnableToDetermineWordType) {
                    Phrase = "(" + expression.Substring(position) + ")",
                    LocalPositions = new int[] { position }
                };
            return result;
        }


        public IList<ExpressionWord> ParseExpression(string expression) {
            List<ExpressionWord> expressionParts = new List<ExpressionWord>();
            int position = 0;
            while(position < expression.Length) {
                var token = GetNextWord(expression, position);
                expressionParts.Add(token);
                position = token.NextCursorPosition;
            }
            return expressionParts;
        }


        




    }
}
