using System;
using DmiCalc;
using System.Collections.Generic;

namespace DmiCalcUnitTests.ExpressionToken.Fakes {
    class FooBarFunctionGetter : IOperationsProvider {

        public FooBarFunctionGetter() {
            Operations = new List<IOperation>();
        }

        public IList<IOperation> Operations { get; private set; }

        public IOperation GetOperation(string operationName) {
            if (String.Equals(operationName, "Foo"))
                return new FunctionFoo();
            else if (String.Equals(operationName, "Bar"))
                return new FunctionBar();
            else
                return null;
        }

        public bool HasOperation(string operationName) {
            return GetOperation(operationName) != null;
        }
    }
}
