
using System.Threading.Tasks;

namespace DmiCalc.ExpressionTree {
    public class ValueExpressionNode : ExpressionNode {

        public override dynamic Calculate(IDataSource dataSource) {
            return NodeValue;
        }

        public async override System.Threading.Tasks.Task<dynamic> CalculateAsync(IDataSource dataSource) {
            return await Task.FromResult(NodeValue);
        }

        public dynamic NodeValue { get; set; }
    }
}
