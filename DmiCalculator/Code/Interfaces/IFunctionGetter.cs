using System.Collections.Generic;

namespace DmiCalc {
    public interface IOperationsProvider {
        IList<IOperation> Operations { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        IOperation GetOperation(string operationName);

        bool HasOperation(string operationName);
    }
}