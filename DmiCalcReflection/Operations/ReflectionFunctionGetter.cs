using DmiCalc;
using System;
using System.Collections.Generic;
using System.Text;
using DmiCalc.BasicCalc;

namespace DmiCalcReflection.Operations {
    public class ReflectionFunctionGetter : IOperationsProvider {
        public IList<IOperation> Operations => new List<IOperation>() {
            new FunctionAdd(),
                        new FunctionSubstract(),
                        new FunctionMultiply(),
                        new FunctionDivide(),
                        new FuncExclamation(),
                        new ReflectionOperation() 

        };

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

        public bool HasOperation(string operationName) => FindOperation(operationName) != null;
    }
}
