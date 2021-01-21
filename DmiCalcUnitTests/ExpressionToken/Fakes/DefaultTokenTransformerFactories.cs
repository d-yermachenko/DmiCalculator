using DmiCalc.ExpressionTokens;
using DmiCalc;
using System;
using System.Collections.Generic;
using System.Text;
using DmiCalc.ExpressionTokens.Transformers;

namespace DmiCalcUnitTests.ExpressionToken.Fakes {
    public class DefaultTokenTransformerFactories {
        public static Tuple<IList<ISemanticsTokenTransformer>, IOperationsProvider> GetDefaultSemanticTokenTransformers() {
            FooBarFunctionGetter fooBarFunctionGetter = new FooBarFunctionGetter();
            List<ISemanticsTokenTransformer> semanticsTokenTransformers = new List<ISemanticsTokenTransformer>();
            semanticsTokenTransformers.AddRange(ExpressionTokenBuilder.GetDefaultTransformers());
            semanticsTokenTransformers.Add(new VariableAndFunctionDistinctor() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new BracketTreeTokenTransformer() { FunctionGetter = fooBarFunctionGetter });
            semanticsTokenTransformers.Add(new ArgumentsTreeTokenTransformer());
            return Tuple.Create<IList<ISemanticsTokenTransformer>, IOperationsProvider>(semanticsTokenTransformers, fooBarFunctionGetter);
        }
    }
}
