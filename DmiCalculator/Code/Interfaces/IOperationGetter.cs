using DmiCalc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalc {
    interface IOperationGetter {
        IEnumerable<IOperation> GetOperations();
    }
}
