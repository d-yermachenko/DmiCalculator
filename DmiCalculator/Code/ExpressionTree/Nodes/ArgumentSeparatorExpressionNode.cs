using System.Linq;

namespace DmiCalc.ExpressionTree {
    public class ArgumentSeparatorExpressionNode : ExpressionNode {
        public override dynamic Calculate(IDataSource dataSource) {
            if (ChildNodes.Count == 0)
                return null;
            else if (ChildNodes.Count == 1) {
                return ChildNodes.First().Calculate(dataSource);
            }
            else
                throw new DmiCalc.Exceptions.DmiCalcException(Exceptions.DmiCalcExceptionCodes.ArgumentSepartatorHasManyNodes);
        }

        public override async System.Threading.Tasks.Task<dynamic> CalculateAsync(IDataSource dataSource) {
            if (ChildNodes.Count == 0)
                return null;
            else if (ChildNodes.Count == 1) {
                return await ChildNodes.First().CalculateAsync(dataSource);
            }
            else
                throw new DmiCalc.Exceptions.DmiCalcException(Exceptions.DmiCalcExceptionCodes.ArgumentSepartatorHasManyNodes);
        }
    }
}
