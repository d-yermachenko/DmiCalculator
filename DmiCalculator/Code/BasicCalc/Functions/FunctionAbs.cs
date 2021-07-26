using DmiCalc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmiCalculator.Code.BasicCalc.Functions {
    class FunctionAbs : IOperation {
        public string Name =>"abs";

        public string LocalizedName => "abs";

        public string HelpKey => nameof(FunctionAbs);

        public Func<IDataSource, dynamic[], dynamic> GetCalcMethod() => CalculateAbs;

        public dynamic CalculateAbs(IDataSource ds, dynamic[] args) {
            if (args?.Length == 1)
                return Math.Abs(args[0] as dynamic);
            else
                throw new InvalidOperationException("Wrong number of arguments: expected 1, got " + args.Length);
        }

        public bool IsNamed(string name) {
            return name?.ToLower()?.Equals("abs") ?? false;
        }
    }
}
