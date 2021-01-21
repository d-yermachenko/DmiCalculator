using System.Collections.Generic;

namespace DmiCalc.ExpressionTree
{
    public abstract class ExpressionNode : ICalculable, ICalculableAsync
    {
        public IDataSource CurrentCalcDatasource { get; set; }
        public List<ExpressionNode> ChildNodes { get; set; }

        public abstract dynamic Calculate(IDataSource dataSource);
        public abstract System.Threading.Tasks.Task<dynamic> CalculateAsync(IDataSource dataSource);
    }
}
