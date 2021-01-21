using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc {
    public interface IVariable : ICalculable {
        string VariableName { get; }

        object VariableValue { get; }
    }
}
