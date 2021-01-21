
using System.Linq;
using System.Threading.Tasks;

namespace DmiCalc.ExpressionTree {
    public class OperationExpressionNode : ExpressionNode {

        public override dynamic Calculate(IDataSource dataSource) {
            int argumentsCount = ChildNodes.Count;
            dynamic[] functionArguments = new dynamic[argumentsCount];
            for(int i = 0; i < argumentsCount; i++) 
                functionArguments[i] = ChildNodes[i].Calculate(dataSource);
            return Operation.GetCalcMethod().Invoke(dataSource, functionArguments);

        }

        public async override System.Threading.Tasks.Task<dynamic> CalculateAsync(IDataSource dataSource) {
            int argumentCount = ChildNodes.Count;
            dynamic[] functionArguments = new dynamic[argumentCount];
            for (int i = 0; i < argumentCount; i++)
                functionArguments[i] = await ChildNodes[i].CalculateAsync(dataSource);
            return Operation.GetCalcMethod().Invoke(dataSource, functionArguments);
        }

        public IOperation Operation { get; set; }

    }
}
