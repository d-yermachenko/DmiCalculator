using DmiCalculator.Code.BasicCalc.Functions;
using System.Collections.Generic;

namespace DmiCalc.BasicCalc {
    class BasicFunctionGetter : IOperationsProvider {
        private List<IOperation> _BasicOperations;

        public IList<IOperation> Operations {
            get {
                if(_BasicOperations == null) {
                    _BasicOperations = new List<IOperation>() {
                        new FunctionAdd(),
                        new FunctionSubstract(),
                        new FunctionMultiply(),
                        new FunctionDivide(),
                        new FuncExclamation(),
                        new FunctionAbs()
                    };
                }
                return _BasicOperations;
            }
        }

        private IOperation FindOperation(string operationName) {
            IOperation operation = null;
            for (int i = 0; i < Operations.Count && operation == null; i++)
                if (Operations[i].IsNamed(operationName))
                    operation = Operations[i];
            return operation;
        }

        public IOperation GetOperation(string operationName) {
            IOperation operation = FindOperation(operationName);
            return operation;
        }

        public bool HasOperation(string operationName)  => FindOperation(operationName) != null;
    }
}
