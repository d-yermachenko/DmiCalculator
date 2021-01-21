using DmiCalc;
using DmiCalc.ExpressionTokens.Transformers;
using DmiCalc.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmiCalc.ExpressionTokens {
    public class ExpressionTokenBuilder {

        public ExpressionTokenBuilder(IExpressionWordToTokenConverter converter) {
            Converter = converter;
        }

        private readonly IExpressionWordToTokenConverter Converter;

        public  IList<ExpressionToken> ConvertWordsToTokens(IList<ExpressionWord> wordsList, IEnumerable<ISemanticsTokenTransformer> tokenProcessors) {
            IList<ExpressionToken> tokenList = new List<ExpressionToken>();
            foreach (ExpressionWord word in wordsList)
                tokenList.Add(Converter.ToExpressionToken(word));
            foreach (var tokenTransformer in tokenProcessors)
                tokenList = tokenTransformer.TransformTokens(tokenList);
            return tokenList;
        }

        public async Task<IList<ExpressionToken>> ConvertWordsToTokensAsync(IList<ExpressionWord> wordsList, IEnumerable<ISemanticsTokenTransformer> tokenProcessors) {
            IList<ExpressionToken> tokenList = new ExpressionToken[wordsList.Count];
            var orderedWordList = wordsList.AsParallel().AsOrdered();
            Parallel.For(0, wordsList.Count,(i) => {
                tokenList[i] = Converter.ToExpressionToken(wordsList[i]);
            }); ;
            foreach (var tokenTransformer in tokenProcessors)
                tokenList = tokenTransformer.TransformTokens(tokenList);
            return await Task.FromResult(tokenList);
        }



        public static ISemanticsTokenTransformer[] GetDefaultTransformers() {
            return new ISemanticsTokenTransformer[] {
                new WhiteSpaceRemoverTransformer(),
                new NegativeNumberTransformer(),
                new BooleanValueTokenTransformer(),
               
            };
        }


    }
}
