using System.Collections.Generic;

namespace DmiCalc {
    public interface IVariablesProvider {
        IList<IVariable> Variables { get; }

        IVariable GetVariable(string variableName);

        bool HasVariable(string variableName);
    }
}